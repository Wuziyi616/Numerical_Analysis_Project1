using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMorphing
{
    class Task
    {
        /*
        This class is the combination of all the config of the morphing task.
        Member variables:
            transform_type: 0 means rotation, 1 means distortion.
            max_angle: used in transform_type == 0, max rotation angle.
            max_radius: used in transform_type == 0, max rotation radius.
            interpolation_method: 0 means nearest, 1 means bilinear, 2 means bicubic.
        */
        public Task()
        {
            transform_type = 0;
            max_angle = 0.0;
            max_radius = 0.0;
            interpolation_method = 0;
        }

        public int transform_type;
        public double max_angle;
        public double max_radius;
        public int interpolation_method;
    }
}
