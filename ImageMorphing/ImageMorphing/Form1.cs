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

namespace ImageMorphing
{
    public partial class Form1 : Form
    {
        Task task_config;
        Transform solver;
        Mat input_image;
        Mat output_image;
        bool flag;  // false until we input image

        public Form1()
        {
            InitializeComponent();

            transform_typeCB.Items.Add("Rotation");
            transform_typeCB.Items.Add("Distortion");
            transform_typeCB.SelectedIndex = 0;

            interpolation_methodCB.Items.Add("Nearest");
            interpolation_methodCB.Items.Add("Bilinear");
            interpolation_methodCB.Items.Add("Bicubic");
            interpolation_methodCB.SelectedIndex = 0;

            input_imageTB.Text = "../../images/THU.jpg";
            transform_angleTB.Text = Convert.ToString(30.0);

            task_config = new Task();
            solver = new Transform();
            flag = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        // read the input image and display
        private void input_imageBtn_Click(object sender, EventArgs e)
        {
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
            Mat img = input_image.Resize(new OpenCvSharp.Size(input_imagePB.Width, input_imagePB.Height));
            input_imagePB.BackgroundImage = img.ToBitmap();
            transform_radiusTB.Text = Convert.ToString(input_image.Height / 2.0);
            flag = true;
        }
        // save the image after transform
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
        // transform input_image according to given config and save the result in output_image
        private void transformBtn_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                MessageBox.Show("Please select an input image first!", "Error");
                return;
            }

            // update transform config
            task_config.transform_type = transform_typeCB.SelectedIndex;
            task_config.interpolation_method = interpolation_methodCB.SelectedIndex;
            task_config.max_angle = Convert.ToDouble(transform_angleTB.Text);
            task_config.max_radius = Convert.ToDouble(transform_radiusTB.Text);

            solver.src = input_image.Clone();
            transformBtn.Enabled = false;
            solver.transform(task_config);
            output_image = solver.dst.Clone();

            Mat img = output_image.Resize(new OpenCvSharp.Size(output_imagePB.Width, output_imagePB.Height));
            output_imagePB.BackgroundImage = img.ToBitmap();

            string save_path = "../../images/THU_" + (task_config.transform_type == 0 ? "rotation" : "distortion") +
                "_" + (task_config.interpolation_method == 0 ? "nearest" : task_config.interpolation_method == 1 ? "bilinear" : "bicubic") +
                "_" + Convert.ToString(task_config.max_angle) + ".jpg";
            output_image.SaveImage(save_path);
            transformBtn.Enabled = true;
            MessageBox.Show("Transform Success!", "Info");
        }
    }
}
