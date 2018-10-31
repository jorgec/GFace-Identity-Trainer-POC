using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityTrainer
{
    class GFaceOpenCV
    {
        // All settings are saved in App.config
        private AppSettingsReader settingsReader;

        private String cascadePath;

        public GFaceOpenCV()
        {
            this.settingsReader = new AppSettingsReader();

            this.cascadePath = this.settingsReader.GetValue("cascade_path", typeof(string)).ToString();
        }

        /// <summary>
        /// GetFaces iterates over the haar cascades stored in the cascade_path folder and runs mat image past each one
        /// If a classifier detects a face, it breaks out of the loop; otherwise, it tries the next classifier
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public GFaceOpenCVFaceObject GetFaces(Mat mat)
        {
            Mat srcImage = mat;
            Mat grayImage = new Mat();

            Cv2.CvtColor(srcImage, grayImage, ColorConversionCodes.BGRA2GRAY);
            Cv2.EqualizeHist(grayImage, grayImage);

            List<CascadeClassifier> classifiers = new List<CascadeClassifier>();
            this.LoadCascades(out classifiers);

            List<Rect> faceRects = new List<Rect>();
            foreach(CascadeClassifier classifier in classifiers)
            {
                Rect[] f = classifier.DetectMultiScale(
                    image: grayImage,
                    scaleFactor: 1.1,
                    minNeighbors: 3,
                    flags: HaarDetectionType.DoCannyPruning | HaarDetectionType.DoRoughSearch | HaarDetectionType.ScaleImage,
                    minSize: new Size(30, 30)
                );

                // check if this classifier has detected a face
                if (f.Length > 0)
                {
                    foreach (Rect _f in f)
                    {
                        faceRects.Add(_f);
                    }
                    // break out since additional classifiers are no longer necessary
                    break;
                }
            }

            Rect[] faces = faceRects.ToArray();

            GFaceOpenCVFaceObject gFaces = new GFaceOpenCVFaceObject();
            gFaces.Faces = faces;
            gFaces.SrcImage = srcImage;

            return gFaces;
            
        }

        /// <summary>
        /// Reads the cascade_path folder for haar cascades
        /// </summary>
        /// <param name="classifiers"></param>
        /// <returns></returns>
        private bool LoadCascades(out List<CascadeClassifier> classifiers)
        {
            classifiers = new List<CascadeClassifier>();
            try
            {
                string[] files = Directory.GetFiles(this.cascadePath);
                foreach(string file in files)
                {
                    classifiers.Add(new CascadeClassifier(file));
                }
                return true;
            }
            catch (DirectoryNotFoundException e)
            {
                return false;
            }
        }
    }
}
