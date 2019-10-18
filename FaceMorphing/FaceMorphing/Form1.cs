using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace FaceMorphing
{
    public partial class Form1 : Form
    {
        Transform solver;
        Mat input_image;
        Mat output_image;
        Mat reference_image;
        Mat linecap_image;
        Matrix2d input_keypoints;
        Matrix2d reference_keypoints;
        Matrix2d output_keypoints;
        Detector face_landmark_detector;

        public Form1()
        {
            InitializeComponent();

            string linecap_path = "./images/linecap.png";  // image for display
            linecap_image = new Mat(linecap_path);
            linecap_image = linecap_image.Resize(new OpenCvSharp.Size(linecapPB.Width, linecapPB.Height));
            linecapPB.BackgroundImage = linecap_image.ToBitmap();

            interpolation_methodCB.Items.Add("Nearest");
            interpolation_methodCB.Items.Add("Bilinear");
            interpolation_methodCB.Items.Add("Bicubic");
            interpolation_methodCB.SelectedIndex = 0;

            input_imageTB.Text = "./images/face-images/8.jpg";
            select_referenceTB.Text = "./images/face-images/6.jpg";

            solver = new Transform();
            reference_keypoints = new Matrix2d();
            input_keypoints = new Matrix2d();
            output_keypoints = new Matrix2d();

            face_landmark_detector = new Detector("./weight/shape_predictor_68_face_landmarks.dat");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void input_imageBtn_Click(object sender, EventArgs e)
        {
            // read in input image
            string image_path = input_imageTB.Text;
            try
            {
                input_image = new Mat(image_path);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("No such image!", "Error");
                return;
            }
            if (input_image.Empty())
            {
                MessageBox.Show("Can't load the image!", "Error");
                return;
            }

            // read in input keypoints
            string keypoint_path;
            int last_dot_idx = image_path.LastIndexOf('.');
            if (last_dot_idx == -1)  // input is the index of image
            {
                keypoint_path = "./images/face-images/" + image_path + ".txt";
                image_path = "./images/face-images/" + image_path + ".jpg";
            }
            else  // input is the full path
            {
                StringBuilder keypoint_path_ = new StringBuilder();
                for (int i = 0; i < last_dot_idx; i++)
                {
                    keypoint_path_.Append(image_path[i]);
                }
                keypoint_path_.Append(".txt");
                keypoint_path = keypoint_path_.ToString();
            }
            // look for keypoint file first
            // if not exist, detect keypoints using Detector
            try
            {
                read_keypoints(keypoint_path, ref input_keypoints);
            }
            catch (System.IO.FileNotFoundException)
            {
                try
                {
                    face_landmark_detector.detect_landmark(image_path, ref input_keypoints);
                }
                catch (System.IndexOutOfRangeException)
                {
                    MessageBox.Show("Can not detect keypoints, please generate a keypoint file.", "Error");
                    return;
                }
            }

            // display
            Mat img = input_image.Resize(new OpenCvSharp.Size(input_imagePB.Width, input_imagePB.Height));
            input_imagePB.BackgroundImage = img.ToBitmap();
            input_keypointsBtn.Text = "Show Keypoints";
            output_keypoints.clear_matrix();
        }

        private void save_resultBtn_Click(object sender, EventArgs e)
        {
            if (output_image.Empty())
            {
                MessageBox.Show("No image to save!", "Error");
                return;
            }
            string save_path = save_imageTB.Text;
            output_image.SaveImage(save_path);
            MessageBox.Show("Successfully save the image!", "Info");
        }

        private void transformBtn_Click(object sender, EventArgs e)
        {
            if (input_keypoints.rows == 0)
            {
                MessageBox.Show("Please select an input image first!", "Error");
                return;
            }
            if (reference_keypoints.rows == 0)
            {
                MessageBox.Show("Please select a reference image first!", "Error");
                return;
            }

            int interpolation_method = interpolation_methodCB.SelectedIndex;

            solver.src = input_image.Clone();
            solver.refer = reference_image.Clone();
            solver.refer_keypoints = new Matrix2d(reference_keypoints);
            solver.src_keypoints = new Matrix2d(input_keypoints);
            solver.dst_keypoints = new Matrix2d(input_keypoints.rows, input_keypoints.cols);
            transformBtn.Enabled = false;
            solver.transform(interpolation_method);
            output_image = solver.dst.Clone();
            output_keypoints = new Matrix2d(solver.dst_keypoints);

            Mat img = output_image.Resize(new OpenCvSharp.Size(output_imagePB.Width, output_imagePB.Height));
            output_imagePB.BackgroundImage = img.ToBitmap();

            string save_path = "./images/results/face_" +
                (interpolation_method == 0 ? "nearest" : interpolation_method == 1 ? "bilinear" : "bicubic") + ".jpg";
            output_image.SaveImage(save_path);
            transformBtn.Enabled = true;
            MessageBox.Show("Transform Success!", "Info");
            result_keypointsBtn.Text = "Show Keypoints";
        }

        private void select_referenceBtn_Click(object sender, EventArgs e)
        {
            // read in reference image
            string image_path = select_referenceTB.Text;
            try
            {
                reference_image = new Mat(image_path);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("No such image!", "Error");
                return;
            }
            if (reference_image.Empty())
            {
                MessageBox.Show("Can't load the image!", "Error");
                return;
            }

            // read in input keypoints
            string keypoint_path;
            int last_dot_idx = image_path.LastIndexOf('.');
            if (last_dot_idx == -1)  // input is the index of image
            {
                keypoint_path = "./images/face-images/" + image_path + ".txt";
                image_path = "./images/face-images/" + image_path + ".jpg";
            }
            else  // input is the full path
            {
                StringBuilder keypoint_path_ = new StringBuilder();
                for (int i = 0; i < last_dot_idx; i++)
                {
                    keypoint_path_.Append(image_path[i]);
                }
                keypoint_path_.Append(".txt");
                keypoint_path = keypoint_path_.ToString();
            }
            // look for keypoint file first
            // if not exist, detect keypoints using Detector
            try
            {
                read_keypoints(keypoint_path, ref reference_keypoints);
            }
            catch (System.IO.FileNotFoundException)
            {
                try
                {
                    face_landmark_detector.detect_landmark(image_path, ref reference_keypoints);
                }
                catch (System.IndexOutOfRangeException)
                {
                    MessageBox.Show("Can not detect keypoints, please generate a keypoint file.", "Error");
                    return;
                }
            }

            // display
            Mat img = reference_image.Resize(new OpenCvSharp.Size(reference_imagePB.Width, reference_imagePB.Height));
            reference_imagePB.BackgroundImage = img.ToBitmap();
            reference_keypointsBtn.Text = "Show Keypoints";
            output_keypoints.clear_matrix();
        }

        private void input_keypointsBtn_Click(object sender, EventArgs e)
        {
            if (input_keypoints.rows == 0)
            {
                MessageBox.Show("Please select an input image first!", "Error");
                return;
            }
            Mat img = input_image.Clone();
            if (input_keypointsBtn.Text == "Show Keypoints")
            {
                draw_keypoints(ref img, input_keypoints);
                input_keypointsBtn.Text = "Hide Keypoints";
            }
            else
            {
                input_keypointsBtn.Text = "Show Keypoints";
            }
            img = img.Resize(new OpenCvSharp.Size(input_imagePB.Width, input_imagePB.Height));
            input_imagePB.BackgroundImage = img.ToBitmap();
        }

        private void reference_keypointsBtn_Click(object sender, EventArgs e)
        {
            if (reference_keypoints.rows == 0)
            {
                MessageBox.Show("Please select an reference image first!", "Error");
                return;
            }
            Mat img = reference_image.Clone();
            if (reference_keypointsBtn.Text == "Show Keypoints")
            {
                draw_keypoints(ref img, reference_keypoints);
                reference_keypointsBtn.Text = "Hide Keypoints";
            }
            else
            {
                reference_keypointsBtn.Text = "Show Keypoints";
            }
            img = img.Resize(new OpenCvSharp.Size(reference_imagePB.Width, reference_imagePB.Height));
            reference_imagePB.BackgroundImage = img.ToBitmap();
        }

        private void result_keypointsBtn_Click(object sender, EventArgs e)
        {
            if (output_keypoints.rows == 0)
            {
                MessageBox.Show("No output image yet!", "Error");
                return;
            }
            Mat img = output_image.Clone();
            if (result_keypointsBtn.Text == "Show Keypoints")
            {
                draw_keypoints(ref img, output_keypoints);
                result_keypointsBtn.Text = "Hide Keypoints";
            }
            else
            {
                result_keypointsBtn.Text = "Show Keypoints";
            }
            img = img.Resize(new OpenCvSharp.Size(output_imagePB.Width, output_imagePB.Height));
            output_imagePB.BackgroundImage = img.ToBitmap();
        }
        /*********************** utility functions ***********************/
        // parse input string to number
        private double parse_number(string number)
        {
            // number has form "123.456e+78.9"
            int idx = number.IndexOf('+');
            string index = number.Substring(idx + 1, number.Length - idx - 1);
            double index_value = double.Parse(index);
            string num = number.Substring(0, idx - 1);
            double num_value = double.Parse(num);
            double final_result = num_value * Math.Pow(10, index_value);
            return final_result;
        }
        // read keypoint coordinates from txt file
        private void read_keypoints(string file_name, ref Matrix2d keypoints)
        {
            // read in keypoints data
            List<string> line_list = new List<string>();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(file_name))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line_list.Add(line);
                }
                sr.Close();
            }
            int num_keypoint = line_list.Count();
            keypoints.set_shape(num_keypoint, 2);  // [num, 2]
            for (int i = 0; i < num_keypoint; i++)
            {
                string line = line_list[i];  // "123e+02 456e+02", split by ' '
                int idx = line.IndexOf(' ');
                string x = line.Substring(0, idx);
                string y = line.Substring(idx + 1, line.Length - idx - 1);
                keypoints.m[i][1] = (double)Convert.ToInt32(parse_number(x));
                keypoints.m[i][0] = (double)Convert.ToInt32(parse_number(y));
            }
        }
        // draw keypoints on face
        private void draw_keypoints(ref Mat face, Matrix2d keypoints)
        {
            int thickness = Convert.ToInt32((face.Height + face.Width) / 150.0);
            for (int i = 0; i < keypoints.rows; i++)
            {
                Cv2.Circle(face, Convert.ToInt32(keypoints.m[i][1]), Convert.ToInt32(keypoints.m[i][0]),
                    1, Scalar.Blue, thickness);
            }
        }
    }
}
