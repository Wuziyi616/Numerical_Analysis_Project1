using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
using OpenCvSharp.Extensions;

using MyPoint = OpenCvSharp.Point2d;

namespace FaceMorphing
{
    class Transform
    {
        /*
        This class contains algorithms for TPS transform.
        Member Variables:
            src: input image.
            dst: image after transform.
            refer: image used for transform reference.
        */
        public Transform()
        {
            rows = 0;
            cols = 0;
            interpolation_method = 0;
            min_x = min_y = 0;
            max_x = max_y = 10000;
            A = new Matrix2d();
            B = new Matrix2d();
            C = new Matrix2d();
            K = new Matrix2d();
            P = new Matrix2d();
            L = new Matrix2d();
            Y = new Matrix2d();
            X = new Matrix2d();
        }

        public Mat src;
        public Mat dst;
        public Mat refer;
        private int rows, cols;
        private int interpolation_method;
        private int min_x, max_x, min_y, max_y;  // lower and upper bound for face area
                                                 // only transform points within this area

        private Matrix2d A, B, C;  // for interpolation calculation
        private Matrix2d K, P, L, Y, X;  // for TPS transform calculation
        public Matrix2d refer_keypoints;  // keypoints on refer face
        public Matrix2d src_keypoints;  // keypoints on src face
        public Matrix2d dst_keypoints;  // keypoints on dst face

        // interface
        public void transform(int method)
        {
            rows = src.Rows;
            cols = src.Cols;
            interpolation_method = method;
            clear_all_matrix();

            // get dst keypoints coordinates because we want to display them
            get_TPS_transform_matrix(src_keypoints, refer_keypoints);
            MyPoint p = new MyPoint();
            for (int i = 0; i < src_keypoints.rows; i++)
            {
                p.X = src_keypoints.m[i][0]; p.Y = src_keypoints.m[i][1];
                TPS_coordinate_transform(ref p, src_keypoints);
                dst_keypoints.m[i][0] = p.X; dst_keypoints.m[i][1] = p.Y;
            }

            // do TPS transform to get final dst face!
            TPS_transform();
        }
        // calculate TPS solutions from src points to dst points
        // can be used for coordinate transform later
        private void get_TPS_transform_matrix(Matrix2d src_points, Matrix2d dst_points)
        {
            // assert src_points.rows == dst_points.rows
            clear_all_matrix();

            // generate matrix K, [n, n]
            int row = src_points.rows;
            K.set_shape(row, row);
            double r_2 = 0.0, U = 0.0;
            MyPoint p1 = new MyPoint(), p2 = new MyPoint();
            for (int i = 0; i < row; i++)
            {
                p1.X = src_points.m[i][0]; p1.Y = src_points.m[i][1];
                for (int j = 0; j < row; j++)
                {
                    if (i == j)
                    {
                        K.m[i][j] = 0.0;
                        continue;
                    }
                    if (i > j) continue;
                    p2.X = src_points.m[j][0]; p2.Y = src_points.m[j][1];
                    r_2 = get_r_2(p1, p2);
                    U = TPS_U(r_2);
                    K.m[i][j] = U; K.m[j][i] = U;
                }
            }

            // generate matrix P, [n, 3]
            P.set_shape(row, 3);
            for (int i = 0; i < row; i++)
            {
                P.m[i][0] = 1.0; P.m[i][1] = src_points.m[i][0]; P.m[i][2] = src_points.m[i][1];
            }

            // generate matrix Y, [n + 3, 2]
            Y.set_shape(row + 3, 2);
            Y.m[row][0] = Y.m[row][1] = Y.m[row + 1][0] = Y.m[row + 1][1] = Y.m[row + 2][0] = Y.m[row + 2][1] = 0.0;
            for (int i = 0; i < row; i++)
            {
                Y.m[i][0] = dst_points.m[i][0]; Y.m[i][1] = dst_points.m[i][1];
            }

            // generate matrix L, [n + 3, n + 3]
            L.set_shape(row + 3, row + 3);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    L.m[i][j] = K.m[i][j];
                }
            }
            L.m[row][row] = L.m[row][row + 1] = L.m[row][row + 2] = L.m[row + 1][row] = 
                L.m[row + 1][row + 1] = L.m[row + 1][row + 2] = L.m[row + 2][row] = L.m[row + 2][row + 1] = 
                L.m[row + 2][row + 2] = 0.0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    L.m[i][row + j] = P.m[i][j];
                    L.m[row + j][i] = P.m[i][j];
                }
            }

            // solve equation and get X, [n + 3, 2]
            X = Matrix2d.dot_product(Matrix2d.inverse(L), Y);
        }
        // do TPS transform to get the output image
        private void TPS_transform()
        {
            get_TPS_transform_matrix(refer_keypoints, src_keypoints);
            MyPoint p = new MyPoint();
            // first generate a whole image that contains all the pixels after transform
            // should be large enough to allow for big coordinate transform
            int height = refer.Height;
            int width = refer.Width;
            Mat whole = new Mat(height, width, src.Type());
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    p.X = (double)i; p.Y = (double)j;
                    TPS_coordinate_transform(ref p, refer_keypoints);
                    if (interpolation_method == 0)
                    {
                        whole.Set<Vec3b>(i, j, nearest_value(p));
                    }
                    else if (interpolation_method == 1)
                    {
                        whole.Set<Vec3b>(i, j, bilinear_value(p));
                    }
                    else if (interpolation_method == 2)
                    {
                        whole.Set<Vec3b>(i, j, bicubic_value(p));
                    }
                }
            }
            constrain_image(whole);
        }

        /*********************** coordinate transform ***********************/
        // TPS coordinate transform
        private void TPS_coordinate_transform(ref MyPoint p, Matrix2d src_points)
        {
            int row = X.rows;  // n + 3
            Matrix2d P = new Matrix2d(1, row);  // [1, n + 3]
            P.m[0][row - 3] = 1.0; P.m[0][row - 2] = p.X; P.m[0][row - 1] = p.Y;
            double r_2 = 0.0;
            MyPoint temp = new MyPoint();
            for (int i = 0; i < row - 3; i++)
            {
                temp.X = src_points.m[i][0]; temp.Y = src_points.m[i][1];
                r_2 = get_r_2(p, temp);
                P.m[0][i] = TPS_U(r_2);
            }
            Matrix2d result = Matrix2d.dot_product(P, X);  // [1, 2]
            p.X = result.m[0][0]; p.Y = result.m[0][1];
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
        // get bound for after_transform_image
        // so the output image will have shape [max_x - min_x, max_y - min_y]
        // slice whole_image to dst_image
        private void constrain_image(Mat whole)
        {
            bool flag = false;
            for (min_x = 0; min_x < whole.Height; min_x++)
            {
                for (int i = 0; i < whole.Width; i++)
                {
                    if (whole.At<Vec3b>(min_x, i) != black_vec())
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag) break;
            }
            flag = false;
            for (min_y = 0; min_y < whole.Width; min_y++)
            {
                for (int i = 0; i < whole.Height; i++)
                {
                    if (whole.At<Vec3b>(i, min_y) != black_vec())
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag) break;
            }
            flag = false;
            for (max_x = whole.Height - 1; max_x >= 0; max_x--)
            {
                for (int i = 0; i < whole.Width; i++)
                {
                    if (whole.At<Vec3b>(max_x, i) != black_vec())
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag) break;
            }
            flag = false;
            for (max_y = whole.Width - 1; max_y >= 0; max_y--)
            {
                for (int i = 0; i < whole.Height; i++)
                {
                    if (whole.At<Vec3b>(i, max_y) != black_vec())
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag) break;
            }
            // get sub_image from whole image
            dst = new Mat(max_x - min_x, max_y - min_y, src.Type());
            for (int i = min_x; i < max_x; i++)
            {
                for (int j = min_y; j < max_y; j++)
                {
                    dst.Set<Vec3b>(i - min_x, j - min_y, whole.At<Vec3b>(i, j));
                }
            }
            // refine dst keypoints according to min_x, min_y
            for (int i = 0; i < dst_keypoints.rows; i++)
            {
                dst_keypoints.m[i][0] -= min_x;
                dst_keypoints.m[i][1] -= min_y;
            }
        }
        // function U for TPS transform
        private double TPS_U(double r_2)
        {
            if (r_2 < 1e-12)
            {
                return 0;
            }
            return r_2 * Math.Log(r_2);
        }
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
        // clear all member variable matrix
        private void clear_all_matrix()
        {
            A.clear_matrix();
            B.clear_matrix();
            C.clear_matrix();
            K.clear_matrix();
            P.clear_matrix();
            L.clear_matrix();
            Y.clear_matrix();
            X.clear_matrix();
        }
        // get r_2 between two points
        private double get_r_2(MyPoint p1, MyPoint p2)
        {
            return Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2);
        }
    }
}
