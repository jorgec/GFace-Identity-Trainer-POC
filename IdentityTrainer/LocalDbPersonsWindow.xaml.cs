using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IdentityTrainer
{
    /// <summary>
    /// CRUD operations for local persons storage
    /// </summary>
    public partial class LocalDbPersonsWindow : Window
    {

        // local db
        private OrmLiteConnectionFactory dbFactory;
        private MySqlConnectionStringBuilder connstr;

        private List<PersonModel> persons;

        private GroupModel group;
        private PersonIdentityObject personIdObject;

        // All settings are saved in App.config
        private AppSettingsReader settingsReader;

        // Face API
        private readonly IFaceServiceClient faceServiceClient;
        private String api_key;
        private String api_url;

        // UI
        public ObservableCollection<string> groupsList;

        private void _(String l)
        {
            TxtLog.Text = l + "\n" + TxtLog.Text;
        }
        public LocalDbPersonsWindow()
        {
            InitializeComponent();

            // settings
            this.settingsReader = new AppSettingsReader();

            // API
            this.api_key = this.settingsReader.GetValue("api_key", typeof(string)).ToString();
            this.api_url = this.settingsReader.GetValue("api_url", typeof(string)).ToString();
            this.faceServiceClient = new FaceServiceClient(api_key, api_url);

            // database
            connstr = new MySqlConnectionStringBuilder();
            connstr.Server = settingsReader.GetValue("db_server", typeof(string)).ToString();
            connstr.UserID = settingsReader.GetValue("db_user", typeof(string)).ToString();
            connstr.Password = settingsReader.GetValue("db_password", typeof(string)).ToString();
            connstr.Database = settingsReader.GetValue("db_db", typeof(string)).ToString();

            dbFactory = new OrmLiteConnectionFactory(connstr.ToString(), MySqlDialect.Provider);

            DataTable result = LoadPersonsData();
            GridPersonModel.ItemsSource = result.DefaultView;

            personIdObject = new PersonIdentityObject();

            // UI
            group = new GroupModel();
            LoadGroups();
        }

        private void LoadGroups()
        {
            groupsList = new ObservableCollection<string>();
            using (var db = dbFactory.Open())
            {
                List<GroupModel> groups = db.Select<GroupModel>();
                foreach (GroupModel g in groups)
                {
                    groupsList.Add(g.groupId);
                }
            }
            CboGroups.ItemsSource = groupsList;
        }

        private DataTable LoadPersonsData()
        {
            var db = dbFactory.Open();
            persons = db.Select<PersonModel>();

            DataTable personsDT = new DataTable();

            personsDT.Columns.Add("id", typeof(Int32));
            personsDT.Columns.Add("name", typeof(String));
            personsDT.Columns.Add("personId", typeof(String));
            personsDT.Columns.Add("groupId", typeof(Int32));

            foreach(var r in persons)
            {
                DataRow row = personsDT.NewRow();
                row[0] = r.id;
                row[1] = r.name;
                row[2] = r.personId;
                row[3] = r.groupId;

                personsDT.Rows.Add(row);
            }

            db.Close();

            return personsDT;
        }

        private void BtnCreatePerson_Click(object sender, RoutedEventArgs e)
        {
            if(group == null)
            {
                MessageBox.Show("Group must be set!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            String personName = TxtPersonName.Text.Trim();

            if (String.IsNullOrEmpty(personName))
            {
                MessageBox.Show("Name can't be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _(String.Format("Attempting to create {0}...", personName));
            CreatePerson(personName);
        }

        private async void CreatePerson(String personName)
        {
            var db = dbFactory.Open();
            
            PersonModel person = db.Single<PersonModel>(x => x.name == personName);
            if (person == null)
            {
                person = new PersonModel();
                person.name = personName;
                person.groupId = group.id;

                try
                {
                    CreatePersonResult rPerson = await faceServiceClient.CreatePersonInPersonGroupAsync(group.groupId, personName);
                    _(String.Format("{0} created with id {1}", personName, rPerson.PersonId));
                    person.personId = rPerson.PersonId.ToString();
                    db.Insert(person);

                    DataTable result = LoadPersonsData();
                    GridPersonModel.ItemsSource = result.DefaultView;
                }
                catch(FaceAPIException e)
                {
                    _(e.Message);
                }
                db.Close();
            }
            else
            {
                MessageBox.Show("That person already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CboGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!String.IsNullOrEmpty(CboGroups.SelectedValue.ToString()))
            {
                group = SelectGroup(CboGroups.SelectedValue.ToString());
            }
        }

        private GroupModel SelectGroup(String groupId)
        {
            var db = dbFactory.Open();
            return db.Single<GroupModel>(x => x.groupId == groupId);
            
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int pk = 0;
            try
            {
                DataRowView selectedRow = (DataRowView)GridPersonModel.SelectedItems[0];
                pk = (int)selectedRow["id"];
                int groupId = (int)selectedRow["groupId"];

                using (var db = dbFactory.Open())
                {
                    PersonModel p = db.Single<PersonModel>(x => x.id == pk);
                    GroupModel g = db.Single<GroupModel>(x => x.id == groupId);
                    DeletePersonAsync(p.personId, g.groupId);
                    _("Deleting from local DB...");
                    db.Delete(p);
                    _("Done.");
                }
                DataTable result = LoadPersonsData();
                GridPersonModel.ItemsSource = result.DefaultView;

            }
            catch (Exception ex)
            {
                _(ex.Message);
            }
        }

        private async void DeletePersonAsync(String p, String groupId)
        {
            try
            {
                Guid guid = new Guid();
                Guid.TryParse(p, out guid);
                _("Deleting from Cloud service...");
                await faceServiceClient.DeletePersonFromPersonGroupAsync(groupId, guid);
            }catch(FaceAPIException e)
            {
                _(e.Message);
            }
            _("Done.");
        }
    }
}
