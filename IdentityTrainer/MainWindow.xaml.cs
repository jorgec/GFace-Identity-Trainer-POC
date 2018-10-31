using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using MySql.Data.MySqlClient;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace IdentityTrainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private CameraState cs;
        private Timer timer;

        private Bitmap rawPreview;

        private FaceIdentityObject faceIdentity;

        // OpenCV fields
        private int streamCount;
        private List<String> imagePaths;
        private String cascadePath;

        // Face API
        private readonly IFaceServiceClient faceServiceClient;
        private String api_key;
        private String api_url;

        const int apiDelay = 3300;

        // local db
        private OrmLiteConnectionFactory dbFactory;
        private MySqlConnectionStringBuilder connstr;

        // All settings are saved in App.config
        private AppSettingsReader settingsReader;

        // UI
        private ObservableCollection<string> groupsList;
        private ObservableCollection<string> personsList;
        private GroupModel currentGroup;
        private PersonModel currentPerson;

        public Bitmap RawPreview { get => rawPreview; set => rawPreview = value; }

        public MainWindow()
        {
            InitializeComponent();
            faceIdentity = new FaceIdentityObject();
            cs = CameraState.Instance;

            this.settingsReader = new AppSettingsReader();

            this.cascadePath = this.settingsReader.GetValue("main_cascade", typeof(string)).ToString();

            this.api_key = this.settingsReader.GetValue("api_key", typeof(string)).ToString();
            this.api_url = this.settingsReader.GetValue("api_url", typeof(string)).ToString();
            this.faceServiceClient = new FaceServiceClient(api_key, api_url);

            // local stream
            this.streamCount = 0;
            this.imagePaths = new List<String>();

            // database
            connstr = new MySqlConnectionStringBuilder();
            connstr.Server = settingsReader.GetValue("db_server", typeof(string)).ToString();
            connstr.UserID = settingsReader.GetValue("db_user", typeof(string)).ToString();
            connstr.Password = settingsReader.GetValue("db_password", typeof(string)).ToString();
            connstr.Database = settingsReader.GetValue("db_db", typeof(string)).ToString();

            dbFactory = new OrmLiteConnectionFactory(connstr.ToString(), MySqlDialect.Provider);

            // UI
            LoadGroups();
            currentGroup = null;
            currentPerson = null;

        }

        /***************************************************************************************************************************
         * UI Event Handlers
         ***************************************************************************************************************************/

        private void RBtnCamera_Click(object sender, RoutedEventArgs e)
        {
            if (this.cs.IsRunning)
            {
                this.timer.Dispose();
                this.cs.CameraOff();
            }
            else
            {

                this.cs.CameraOn();
                this.timer = new Timer(ShowPreview, 0, 0, cs.Interval);
            }
        }

        private void RBtnGrab_Click(object sender, RoutedEventArgs e)
        {
            if (this.faceIdentity.IsValid)
            {
                if (this.cs.Mat != null)
                {
                    this.GrabFace(this.cs.Mat);
                }
                else
                {
                    MessageBox.Show("No image in stream", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Names can't be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RBtnClearStream_Click(object sender, RoutedEventArgs e)
        {
            this.imagePaths = new List<string>();
            this.streamCount = 0;
            GrpPreviews.Header = String.Format("Current Image Stream: {0}", this.streamCount);
            PnlPreview.Children.Clear();
        }

        private void RBtnDbSetupGroup_Click(object sender, RoutedEventArgs e)
        {
            LocalDbGroupsWindow localDbGroups = new LocalDbGroupsWindow();
            localDbGroups.Show();
        }

        private void RBtnDbSetupPersons_Click(object sender, RoutedEventArgs e)
        {
            LocalDbPersonsWindow localDbPersons = new LocalDbPersonsWindow();
            localDbPersons.Show();
        }

        private void RBtnTrain_Click(object sender, RoutedEventArgs e)
        {
            TrainService();
        }

        private async void RBtnLoadToTrainer_Click(object sender, RoutedEventArgs e)
        {
            await LoadToTrainer(sender);
        }

        private void RBtnQueryService_Click(object sender, RoutedEventArgs e)
        {
            ServiceQueryWindow wServiceQuery = new ServiceQueryWindow();
            wServiceQuery.Show();
        }

        private void CboGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetCurrentGroup(CboGroups.SelectedValue.ToString());
        }

        private void CboPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetCurrentPerson(CboPersons.SelectedValue.ToString());
        }

        private void BtnRefreshIdentities_Click(object sender, RoutedEventArgs e)
        {
            CboGroups.SelectedValue = "";
            CboPersons.SelectedValue = "";
            LoadGroups();
        }

        /// <summary>
        /// Instantiate FaceIdentity object and create directories
        /// TODO:
        /// - deprecate this in favor of individual Group and Person objects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNamesSet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String personName = CboPersons.SelectedValue.ToString();
                String personGroup = CboGroups.SelectedValue.ToString();
                if (!String.IsNullOrEmpty(personName) && !String.IsNullOrEmpty(personGroup))
                {
                    faceIdentity.PersonGroup = personGroup;
                    faceIdentity.PersonName = personName;

                    faceIdentity.CreateDirectories();

                    MessageBox.Show("Names set!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Names can't be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnNamesReset_Click(object sender, RoutedEventArgs e)
        {
            faceIdentity = new FaceIdentityObject();
            CboGroups.SelectedValue = "";
            CboPersons.SelectedValue = "";
            currentPerson = null;
            currentGroup = null;
        }

        /***************************************************************************************************************************
         * Methods
         ***************************************************************************************************************************/
        
        /// <summary>
        /// Basic log outputter
        /// </summary>
        /// <param name="l"></param>
        private void _(String l)
        {
            TxtTrainerLog.Text = l + "\n" + TxtTrainerLog.Text;
        }

        
        /// <summary>
        /// Threaded previewer of the camera catures
        /// </summary>
        /// <param name="state"></param>
        private void ShowPreview(object state)
        {
            this.Dispatcher.Invoke(() =>
            {
                // get mat file from CameraState object
                Mat mat = cs.Mat;
                if (mat != null)
                {
                    try
                    {
                        this.rawPreview = BitmapConverter.ToBitmap(mat);
                        // display converted mat
                        ImgRamPreview.Source = this.rawPreview.ToBitmapSource();
                    }catch(Exception e)
                    {
                        _(e.Message);
                    }
                    
                }
            });
            
        }

        /// <summary>
        /// Grabs a mat and saves the faces to disk
        /// Keeps track of the saved images in RAM
        /// </summary>
        /// <param name="mat"></param>
        private void GrabFace(Mat mat)
        {
            GFaceOpenCV gFace = new GFaceOpenCV();
            GFaceOpenCVFaceObject faces = gFace.GetFaces(mat);

            OpenCvSharp.Rect[] _faces = faces.Faces;
            
            if(_faces.Length > 0)
            {
                foreach(OpenCvSharp.Rect faceRect in _faces)
                {
                    this.streamCount++;
                    Mat detectedFaceImage = new Mat(faces.SrcImage, faceRect);
                    String path = String.Format("{0}/{1}.png", this.faceIdentity.PersonDir, this.streamCount);
                    detectedFaceImage.SaveImage(path);
                    this.imagePaths.Add(path);

                    GeneratePreview(path);
                }
            }
        }

        /// <summary>
        /// Display thumbnails of faces captured by OpenCV
        /// </summary>
        /// <param name="f"></param>
        private void GeneratePreview(String f)
        {
            System.Windows.Controls.Image cap = new System.Windows.Controls.Image();
            cap.Source = new Bitmap(f).ToBitmapSource();
            cap.Width = 60;
            cap.Height = 60;
            cap.Margin = new Thickness(0, 0, 5, 5);
            PnlPreview.Children.Add(cap);

            GrpPreviews.Header = String.Format("Current Image Stream: {0}", this.streamCount);
        }

        /// <summary>
        /// Send saved frames to Cognitive Faces Trainer associated to a previously created group and person
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private async Task LoadToTrainer(object sender)
        {
            // make sure that group and person data are set
            if (currentPerson != null && currentGroup != null)
            {
                // make sure there are photos in stream
                if (this.imagePaths.Count >= 0)
                {
                    Guid personGuid = new Guid();
                    Guid.TryParse(currentPerson.personId, out personGuid);

                    foreach (String path in this.imagePaths)
                    {
                        using (Stream s = File.OpenRead(path))
                        {
                            // Throttle requests so as not to go over the limit
                            _(String.Format("Attempting to add photo {0} of {1} to {2}...", path, currentPerson.name, currentGroup.name));
                            await Task.Delay(apiDelay);                            
                            try
                            {
                                await faceServiceClient.AddPersonFaceInPersonGroupAsync(currentGroup.groupId, personGuid, s);
                                _("Success.");
                            }
                            catch (FaceAPIException eAddPersonFaceInPersonGroupAsync)
                            {
                                _(eAddPersonFaceInPersonGroupAsync.Message);
                            }
                        }
                    }
                    _("Done.");
                }
                else
                {
                    MessageBox.Show("No image in stream", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Names can't be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// API call to execute Training on uploaded faces for group
        /// </summary>
        private async void TrainService()
        {
            _("Training service...");
            await Task.Delay(apiDelay);
            await faceServiceClient.TrainPersonGroupAsync(faceIdentity.PersonGroup);
            TrainingStatus trainingStatus = null;
            while (true)
            {                
                trainingStatus = await faceServiceClient.GetPersonGroupTrainingStatusAsync(faceIdentity.PersonGroup);
                if(!trainingStatus.Status.Equals("running"))
                {
                    break;
                }
                await Task.Delay(1000);
            }
            _("Done.");
        }

        

        private void SetCurrentGroup(String g)
        {
            using (var db = dbFactory.Open())
            {
                currentGroup = db.Single<GroupModel>(x => x.groupId == g);
                LoadPersons(currentGroup.id);
            }
        }

        private void LoadGroups()
        {
            groupsList = new ObservableCollection<string>();
            using (var db = dbFactory.Open())
            {
                var groups = db.Select<GroupModel>().OrderBy(e => e.name);
                foreach (GroupModel g in groups)
                {
                    groupsList.Add(g.groupId);
                }
            }
            CboGroups.ItemsSource = groupsList;
        }

        private void LoadPersons(int groupId)
        {
            personsList = new ObservableCollection<string>();
            using(var db = dbFactory.Open())
            {
                var persons = db.Select<PersonModel>(x => x.groupId == groupId).OrderBy(e => e.name);
                foreach(PersonModel p in persons)
                {
                    personsList.Add(p.name);
                }
            }
            CboPersons.ItemsSource = personsList;
        }

        private void SetCurrentPerson(String p)
        {
            using(var db = dbFactory.Open())
            {
                currentPerson = db.Single<PersonModel>(x => x.name == p);
            }
        }
        
    }
}
