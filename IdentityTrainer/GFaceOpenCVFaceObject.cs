using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityTrainer
{
    class GFaceOpenCVFaceObject
    {
        private Rect[] faces;
        private Mat srcImage;

        public Rect[] Faces { get => faces; set => faces = value; }
        public Mat SrcImage { get => srcImage; set => srcImage = value; }
        
    }
}
