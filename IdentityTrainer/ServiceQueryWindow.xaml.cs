using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using EasyHttp.Http;
using System.Web;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using Microsoft.ProjectOxford.Face.Contract;

namespace IdentityTrainer
{
    /// <summary>
    /// Interaction logic for ServiceQueryWindow.xaml
    /// </summary>
    public partial class ServiceQueryWindow : Window
    {

        private static HttpClient http;

        // Face API
        private readonly IFaceServiceClient faceServiceClient;
        private String api_key;
        private String api_url;

        // All settings are saved in App.config
        private AppSettingsReader settingsReader;

        // api results
        private DataTable groupsDT;
        private DataTable personsDT;
        private DataTable personFacesDT;
        private Dictionary<String, String[]> persistedFaces;

        // UI
        private String currentGroupId;
        private String currentPersonId;

        public ServiceQueryWindow()
        {
            InitializeComponent();

            this.settingsReader = new AppSettingsReader();
            this.api_key = this.settingsReader.GetValue("api_key", typeof(string)).ToString();
            this.api_url = this.settingsReader.GetValue("api_url", typeof(string)).ToString();
            this.faceServiceClient = new FaceServiceClient(api_key, api_url);

            groupsDT = new DataTable();
            personsDT = new DataTable();
            personFacesDT = new DataTable();

            LoadGroups();
            
        }

        private void _(String l)
        {
            TxtLog.Text = l + "\n" + TxtLog.Text;
        }

        private async void LoadGroups()
        {
            await FetchGroupsAsync();
            try
            {
                GridGroups.ItemsSource = groupsDT.DefaultView;
            }catch(Exception e)
            {
                _(e.Message);
            }
        }

        private async void LoadPersons()
        {
            await FetchPersonsInGroupAsync();
            try
            {
                GridPersons.ItemsSource = personsDT.DefaultView;
            }catch(Exception e)
            {
                _(e.Message);
            }
        }

        private async Task FetchGroupsAsync()
        {
            _("Querying service for groups...");
            groupsDT = new DataTable();
            try
            {
                PersonGroup[] groups = await faceServiceClient.ListPersonGroupsAsync();
                groupsDT.Columns.Add("Person Group Id", typeof(String));
                groupsDT.Columns.Add("Person Group Name", typeof(String));

                foreach(PersonGroup group in groups)
                {
                    DataRow row = groupsDT.NewRow();
                    row[0] = group.PersonGroupId;
                    row[1] = group.Name;
                    groupsDT.Rows.Add(row);
                }
            }catch(FaceAPIException e)
            {
                _(e.Message);
            }
            _("Done.");
        }

        private async Task FetchPersonsInGroupAsync()
        {
            _("Grabbing list of persons...");
            personsDT = new DataTable();
            try
            {
                Person[] _persons = await faceServiceClient.ListPersonsInPersonGroupAsync(currentGroupId);

                persistedFaces = new Dictionary<String, String[]>();
                personsDT.Columns.Add("Name", typeof(String));
                personsDT.Columns.Add("Person Id", typeof(String));                
                
                foreach(Person p in _persons)
                {
                    
                    DataRow row = personsDT.NewRow();                    
                    row[0] = p.Name;
                    row[1] = p.PersonId;
                    personsDT.Rows.Add(row);
                    persistedFaces.Add(p.PersonId.ToString(), ParsePersistedFaces(p.PersistedFaceIds));
                }
            }
            catch(FaceAPIException e)
            {
                _(e.Message);
            }
            _("Done.");
        }


        /// <summary>
        /// In-memory dictionary store of persisted faces since this comes free with the ListPersonsInPersonGroupAsync call for free, to avoid additional API calls
        /// </summary>
        /// <param name="faceIds"></param>
        /// <returns></returns>
        private String[] ParsePersistedFaces(Guid[] faceIds)
        {
            List<String> _faceIds = new List<string>();
            if(faceIds.Length > 0)
            {
                foreach(Guid faceId in faceIds)
                {
                    _faceIds.Add(faceId.ToString());
                }
            }

            return _faceIds.ToArray();
        }

        private void GridGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView selectedRow = (DataRowView)GridGroups.SelectedItems[0];
                currentGroupId = selectedRow["Person Group Id"].ToString();
                LoadPersons();
            }catch(Exception ex)
            {
                _(ex.Message);
            }
            
        }

        private void GridPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView selectedRow = (DataRowView)GridPersons.SelectedItem;
                if(selectedRow != null)
                {
                    currentPersonId = selectedRow["Person Id"].ToString();
                    try
                    {
                        ShowPersistedFaceIds(persistedFaces[currentPersonId]);
                    }
                    catch (KeyNotFoundException ex)
                    {
                        _(ex.Message + "key not found");
                    }
                }                
            }
            catch (Exception ex)
            {
                _(ex.Message + "chuchu");
            }
        }

        private void ShowPersistedFaceIds(String[] faceIds)
        {
            String persistedFacesList = "";
            if(faceIds.Length > 0)
            {
                foreach(String faceId in faceIds)
                {
                    persistedFacesList = String.Format("{0}{1}\n", persistedFacesList, faceId);
                }
            }
            TxtPersistedFaceIds.Text = persistedFacesList;
        }
    }
}
