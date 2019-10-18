using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
using OpenCvSharp.Extensions;

using MyPoint = OpenCvSharp.Point2d;

namespace ImageMorphing
{
    class Transform
    {
        /*
        This class contains algorithms for rotation and distortion.
        Member Variables:
            src: input image.
            dst: image after transform.
        */
        public Transform()
        {
            rows = 0;
            cols = 0;
            interpolation_method = 0;
            max_angle = 0;
            max_radius = 0;
            A = new Matrix2d();
            B = new Matrix2d();
            C = new Matrix2d();
        }

        public Mat src;
        public Mat dst;
        private int rows;
        private int cols;
        private double max_angle;
        private double max_radius;
        private int interpolation_method;

        private Matrix2d A, B, C;  // for interpolation calculation

        // interface
        public void transform(Task config)
        {
            dst = src.Clone();
            rows = src.Rows;
            cols = src.Cols;
            max_angle = config.max_angle;
            max_radius = config.max_radius;
            interpolation_method = config.interpolation_method;
            A.clear_matrix();
            B.clear_matrix();
            C.clear_matrix();
            if (config.transform_type == 0)
            {
                rotation();
            }
            else if (config.transform_type == 1)
            {
                distortion();
            }
        }
        // rotation transform
        private void rotation()
        {
            double distance;
            MyPoint midpoint = new MyPoint((double)rows / 2.0, (double)cols / 2.0);
            MyPoint p = new MyPoint(0.0, 0.0);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    p.X = (double)i;
                    p.Y = (double)j;
                    distance = p.DistanceTo(midpoint);
                    rotation_coordinate_transform(ref p, distance);
                    if (interpolation_method == 0)
                    {
                        dst.Set<Vec3b>(i, j, nearest_value(p));
                    }
                    else if (interpolation_method == 1)
                    {
                        dst.Set<Vec3b>(i, j, bilinear_value(p));
                    }
                    else if (interpolation_method == 2)
                    {
                        dst.Set<Vec3b>(i, j, bicubic_value(p));
                    }
                }
            }
        }
        // distortion transform
        private void distortion()
        {
            bool flag;
            double distance;
            MyPoint midpoint = new MyPoint((double)rows / 2.0, (double)cols / 2.0);
            MyPoint p = new MyPoint(0.0, 0.0);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    p.X = (double)i;
                    p.Y = (double)j;
                    distance = p.DistanceTo(midpoint);
                    flag = distortion_coordinate_transform(ref p, distance);
                    if (!flag)  // pixel is out of frame, set it to BlackVec
                    {
                        dst.Set<Vec3b>(i, j, black_vec());
                        continue;
                    }
                    if (interpolation_method == 0)
                    {
                        dst.Set<Vec3b>(i, j, nearest_value(p));
                    }
                    else if (interpolation_method == 1)
                    {
                        dst.Set<Vec3b>(i, j, bilinear_value(p));
                    }
                    else if (interpolation_method == 2)
                    {
                        dst.Set<Vec3b>(i, j, bicubic_value(p));
                    }
                }
            }
        }

        /*********************** coordinate functions ***********************/
        // coordinate transform for rotation
        private void rotation_coordinate_transform(ref MyPoint p, double distance)
        {
            if (distance >= max_radius) return;  // no need to rotate
            if (distance == 0.0) return;  // center point won't be rotated
            double x = p.X - rows / 2.0, y = p.Y - cols / 2.0;
            double angle = max_angle * (max_radius - distance) / max_radius;
            angle = angle / 180.0 * Math.PI;
            double sin_value = Math.Sin(angle), cos_value = Math.Cos(angle);
            p.X = x * cos_value + y * sin_value + rows / 2.0;
            p.Y = y * cos_value - x * sin_value + cols / 2.0;
        }
        // coordinate transform for distortion
        private bool distortion_coordinate_transform(ref MyPoint p, double distance)
        {
            if (distance == 0.0) return true;  // center point won't be distorted
            // if this pixel is not within the frame, we should it to BlackVec
            if (max_angle >= 0 && distance > max_radius)
            {
                return false;
            }
            double x = p.X - rows / 2.0, y = p.Y - cols / 2.0;
            double ori_distance = 0.0;
            if (max_angle >= 0)
                ori_distance = max_radius * Math.Asin(distance / max_radius);
            else
                ori_distance = max_radius * Math.Sin(distance / max_radius);
            p.X = ori_distance * x / distance + rows / 2.0;
            p.Y = ori_distance * y / distance + cols / 2.0;
            return true;
        }
        /*********************** interpolation functions ***********************/
        // nearest
        private Vec3b nearest_value(MyPoint p)
        {
            int i = (int)Math.Floor(p.X), j = (int)Math.Floor(p.Y);
            double u = p.X - i, v = p.Y - j;
            i = u < 0.5 ? i : i + 1;
            j = v < 0.5 ? j : j + 1;

            if (index_out_of_range(i, j))
            {
                return black_vec();
            }
            return src.At<Vec3b>(i, j);
        }
        // bilinear
        private Vec3b bilinear_value(MyPoint p)
        {
            Vec3b result = new Vec3b(0, 0, 0);
            int i = (int)Math.Floor(p.X), j = (int)Math.Floor(p.Y);
            double u = p.X - i, v = p.Y - j;

            // generate matrix A = [[1-u, u]]
            A.set_shape(1, 2);
            A.m[0][0] = 1.0 - u; A.m[0][1] = u;
            // generate matrix C = [[1 - v], [v]]
            C.set_shape(2, 1);
            C.m[0][0] = 1.0 - v; C.m[1][0] = v;
            // generate matrix B = [[f(i, j), f(i, j + 1)], [f(i + 1, j), f(i + 1, j + 1)]]
            // have to do so for 3 times since it's a 3-channel image
            B.set_shape(2, 2);
            Vec3b vec1 = index_out_of_range(i, j) ? black_vec() : src.At<Vec3b>(i, j);
            Vec3b vec2 = index_out_of_range(i, j + 1) ? black_vec() : src.At<Vec3b>(i, j + 1);
            Vec3b vec3 = index_out_of_range(i + 1, j) ? black_vec() : src.At<Vec3b>(i + 1, j);
            Vec3b vec4 = index_out_of_range(i + 1, j + 1) ? black_vec() : src.At<Vec3b>(i + 1, j + 1);
            // iterate over RGB dim
            for (int idx = 0; idx < 3; idx++)
            {
                B.m[0][0] = vec1[idx]; B.m[0][1] = vec2[idx]; B.m[1][0] = vec3[idx]; B.m[1][1] = vec4[idx];
                double value = Matrix2d.dot_product(Matrix2d.dot_product(A, B), C).m[0][0];
                value = value < 0.0 ? 0.0 : value > 255.0 ? 255.0 : value;
                result[idx] = Convert.ToByte(Math.Round(value));
            }
            return result;
        }
        // bicubic
        private Vec3b bicubic_value(MyPoint p)
        {
            Vec3b result = new Vec3b(0, 0, 0);
            int i = (int)Math.Floor(p.X), j = (int)Math.Floor(p.Y);
            double u = p.X - i, v = p.Y - j;

            // generate matrix A = [[S(u + 1), S(u), S(u - 1), S(u - 2)]]
            A.set_shape(1, 4);
            A.m[0][0] = bicubic_s(u + 1.0); A.m[0][1] = bicubic_s(u); A.m[0][2] = bicubic_s(u - 1.0); A.m[0][3] = bicubic_s(u - 2.0);
            // generate matrix C = [[S(v + 1)], [S(v)], [S(v - 1)], [S(v - 2)]]
            C.set_shape(4, 1);
            C.m[0][0] = bicubic_s(v + 1.0); C.m[1][0] = bicubic_s(v); C.m[2][0] = bicubic_s(v - 1.0); C.m[3][0] = bicubic_s(v - 2.0);
            // generate 4x4 matrix B
            B.set_shape(4, 4);
            Vec3b vec11 = index_out_of_range(i - 1, j - 1) ? black_vec() : src.At<Vec3b>(i - 1, j - 1);
            Vec3b vec12 = index_out_of_range(i - 1, j) ? black_vec() : src.At<Vec3b>(i - 1, j);
            Vec3b vec13 = index_out_of_range(i - 1, j + 1) ? black_vec() : src.At<Vec3b>(i - 1, j + 1);
            Vec3b vec14 = index_out_of_range(i - 1, j + 2) ? black_vec() : src.At<Vec3b>(i - 1, j + 2);
            Vec3b vec21 = index_out_of_range(i, j - 1) ? black_vec() : src.At<Vec3b>(i, j - 1);
            Vec3b vec22 = index_out_of_range(i, j) ? black_vec() : src.At<Vec3b>(i, j);
            Vec3b vec23 = index_out_of_range(i, j + 1) ? black_vec() : src.At<Vec3b>(i, j + 1);
            Vec3b vec24 = index_out_of_range(i, j + 2) ? black_vec() : src.At<Vec3b>(i, j + 2);
            Vec3b vec31 = index_out_of_range(i + 1, j - 1) ? black_vec() : src.At<Vec3b>(i + 1, j - 1);
            Vec3b vec32 = index_out_of_range(i + 1, j) ? black_vec() : src.At<Vec3b>(i + 1, j);
            Vec3b vec33 = index_out_of_range(i + 1, j + 1) ? black_vec() : src.At<Vec3b>(i + 1, j + 1);
            Vec3b vec34 = index_out_of_range(i + 1, j + 2) ? black_vec() : src.At<Vec3b>(i + 1, j + 2);
            Vec3b vec41 = index_out_of_range(i + 2, j - 1) ? black_vec() : src.At<Vec3b>(i + 2, j - 1);
            Vec3b vec42 = index_out_of_range(i + 2, j) ? black_vec() : src.At<Vec3b>(i + 2, j);
            Vec3b vec43 = index_out_of_range(i + 2, j + 1) ? black_vec() : src.At<Vec3b>(i + 2, j + 1);
            Vec3b vec44 = index_out_of_range(i + 2, j + 2) ? black_vec() : src.At<Vec3b>(i + 2, j + 2);
            // iterate over RGB dim
            for (int idx = 0; idx < 3; idx++)
            {
                B.m[0][0] = vec11[idx]; B.m[0][1] = vec12[idx]; B.m[0][2] = vec13[idx]; B.m[0][3] = vec14[idx];
                B.m[1][0] = vec21[idx]; B.m[1][1] = vec22[idx]; B.m[1][2] = vec23[idx]; B.m[1][3] = vec24[idx];
                B.m[2][0] = vec31[idx]; B.m[2][1] = vec32[idx]; B.m[2][2] = vec33[idx]; B.m[2][3] = vec34[idx];
                B.m[3][0] = vec41[idx]; B.m[3][1] = vec42[idx]; B.m[3][2] = vec43[idx]; B.m[3][3] = vec44[idx];
                double value = Matrix2d.dot_product(Matrix2d.dot_product(A, B), C).m[0][0];
                value = value < 0.0 ? 0.0 : value > 255.0 ? 255.0 : value;
                result[idx] = Convert.ToByte(Math.Round(value));
            }
            return result;
        }

        /*********************** utility functions ***********************/
        // function S for bicubic interpolation
        private double bicubic_s(double x)
        {
            double abs_x = Math.Abs(x);
            if (abs_x <= 1)
            {
                return 1.0 - 2.0 * Math.Pow(abs_x, 2) + Math.Pow(abs_x, 3);
            }
            if (abs_x < 2)
            {
                return 4.0 - 8.0 * abs_x + 5.0 * Math.Pow(abs_x, 2) - Math.Pow(abs_x, 3);
            }
            return 0.0;
        }
        // black pixel for index out of range
        private Vec3b black_vec()
        {
            return new Vec3b(0, 0, 0);
        }
        // judge whether index is out of range
        private bool index_out_of_range(int i, int j)
        {
            return (i < 0 || i >= rows || j < 0 || j >= cols);
        }
    }
}
