using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DlibDotNet;

/* The code is partly adopted from github and online resources */

namespace FaceMorphing
{
    class Detector
    {
        public Detector()
        {
            landmark_detector = Dlib.GetFrontalFaceDetector();
        }

        public Detector(string weight_path)
        {
            landmark_detector = Dlib.GetFrontalFaceDetector();
            shape_predictor = ShapePredictor.Deserialize(weight_path);
        }

        private FrontalFaceDetector landmark_detector;
        private ShapePredictor shape_predictor;

        public void set_predictor_weight(string weight_path)
        {
            shape_predictor = ShapePredictor.Deserialize(weight_path);
        }

        public void detect_landmark(string image_path, ref Matrix2d keypoints)
        {
            // assert there are 68 keypoints on a face
            keypoints.set_shape(68, 2);
            Array2D<RgbPixel> image = Dlib.LoadImage<RgbPixel>(image_path);
            Dlib.PyramidUp(image);
            DlibDotNet.Rectangle[] bbox = landmark_detector.Operator(image);
            // assert only one bbox because there could only be one face per image
            // if there are several faces, we just use the first one
            FullObjectDetection keypoint_result = shape_predictor.Detect(image, bbox[0]);
            // assert len(keypoint_result) == 68
            for (int i = 0; i < 68; i++)
            {
                keypoints.m[i][1] = keypoint_result.GetPart(Convert.ToUInt32(i)).X / 2.0;
                keypoints.m[i][0] = keypoint_result.GetPart(Convert.ToUInt32(i)).Y / 2.0;
            }
        }
    }
}
