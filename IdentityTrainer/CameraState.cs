using OpenCvSharp;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace IdentityTrainer
{
    /* 
     * CameraState singleton
     * Contains camera methods and parameters
     * 
     * Initialization:
     * - cs = CameraState.Instance
     * 
     * Methods:
     * - InitializeCamera(int interval int exposure, bool isRunnning)
     * - CameraOn()
     * - CameraOff()
     */
    public sealed class CameraState
    {
        private VideoCapture camera;                                            // camera object
        private int interval;                                                   // determines FPS
        private int exposure;                                                   // determines camera brightness
        private int frameCount = 0;                                             // keeps track of how many frames have been captured

        private bool isRunning = false;                                         // flag to note if camera is running
        private bool isRenderingFaces = false;                                  // flag to note if camera is rendering faces        

        public VideoCapture Camera { get => camera; set => camera = value; }    // camera object
        private Timer timer;
        private Mat mat;                                                        // captured object

        /*
         * Lazy singleton implementation
         */
        private static readonly Lazy<CameraState> lazy = new Lazy<CameraState>(() => new CameraState());

        public static CameraState Instance { get { return lazy.Value; } }

        public int Interval { get => interval; set => interval = value; }
        public int Exposure { get => exposure; set => exposure = value; }
        public int FrameCount { get => frameCount; set => frameCount = value; }
        public bool IsRunning { get => isRunning; set => isRunning = value; }
        public bool IsRenderingFaces { get => isRenderingFaces; set => isRenderingFaces = value; }
        public Mat Mat { get => mat; set => mat = value; }
        public Timer Timer { get => timer; set => timer = value; }

        private CameraState()
        {
            
            this.InitializeCamera();
        }

        public void InitializeCamera(int interval = 250, int exposure = -5, bool running = false)
        {
            this.Interval = interval;
            this.Exposure = exposure;
            this.IsRunning = running;
        }

        public bool CameraOn()
        {
            try
            {
                this.Camera = new VideoCapture();
                Camera.Open(1);
                this.Timer = new Timer(GrabImage, 0, 0, this.Interval);
                this.IsRunning = true;
                return true;
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }

        public bool CameraOff()
        {
            if (this.IsRunning)
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    this.Timer.Dispose();
                    this.camera.Dispose();                    
                    this.IsRunning = false;
                });
            }
            return false;
        }

        /// <summary>
        /// GrabImage
        /// Grabs an image from the camera and stores it in mat
        /// </summary>
        private void GrabImage(object state)
        {
            
            if (!this.IsRunning) return;
            
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                this.mat = new Mat();
                this.camera.Read(this.mat);
                this.FrameCount++;                                

            });

        }

        
    }
}
