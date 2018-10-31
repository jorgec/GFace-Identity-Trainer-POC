using Microsoft.ProjectOxford.Face;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
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
    /// Group Management sync for local and Cognitive Faces API
    /// </summary>
    public partial class LocalDbGroupsWindow : Window
    {
        // local db
        private OrmLiteConnectionFactory dbFactory;
        private MySqlConnectionStringBuilder connstr;

        private List<GroupModel> groups;

        // All settings are saved in App.config
        private AppSettingsReader settingsReader;

        // Face API
        private readonly IFaceServiceClient faceServiceClient;
        private String api_key;
        private String api_url;

        private void _(String l)
        {
            TxtLog.Text = l + "\n" + TxtLog.Text;
        }

        /// <summary>
        /// CRUD operations for local groups storage
        /// </summary>
        public LocalDbGroupsWindow()
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

            DataTable result = LoadGroupsData();
            GridGroupModel.ItemsSource = result.DefaultView;
        }

        private DataTable LoadGroupsData()
        {
            var db = dbFactory.Open();
            groups = db.Select<GroupModel>();

            DataTable groupsDT = new DataTable();
            
            groupsDT.Columns.Add("id", typeof(Int32));
            groupsDT.Columns.Add("name", typeof(String));
            groupsDT.Columns.Add("groupId", typeof(String));

            foreach (var r in groups)
            {
                DataRow row = groupsDT.NewRow();
                row[0] = r.id;
                row[1] = r.name;
                row[2] = r.groupId;
                
                groupsDT.Rows.Add(row);
            }

            db.Close();

            return groupsDT;

        }

        private void BtnCreateGroup_Click(object sender, RoutedEventArgs e)
        {
            CreateGroupAsync();
            
        }

        private async void CreateGroupAsync()
        {
            GroupIdentityObject groupId = new GroupIdentityObject();
            if( groupId.SetGroupName(TxtGroupName.Text))
            {
                try
                {
                    _(String.Format("Attempting to create group: {0} with ID {1}", groupId.GroupName, groupId.GroupId));
                    await faceServiceClient.CreatePersonGroupAsync(groupId.GroupId, groupId.GroupName);

                    GroupModel group = new GroupModel();
                    group.groupId = groupId.GroupId;
                    group.name = groupId.GroupName;

                    using(var db = dbFactory.Open())
                    {
                        db.Insert(group);
                    }
                    DataTable result = LoadGroupsData();
                    GridGroupModel.ItemsSource = result.DefaultView;

                    _("Done.");
                }catch (FaceAPIException e)
                {
                    _(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Name can't be blank!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private async void DeleteGroupAsync(String g)
        {
            try
            {
                _("Deleting from Cloud service...");
                await faceServiceClient.DeletePersonGroupAsync(g);
            }catch(FaceAPIException e)
            {
                _(e.Message);
            }
            _("Done.");
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int pk = 0;
            try
            {
                DataRowView selectedRow = (DataRowView)GridGroupModel.SelectedItems[0];
                pk = (int)selectedRow["id"];

                using (var db = dbFactory.Open())
                {
                    GroupModel g = db.Single<GroupModel>(x => x.id == pk);

                    DeleteGroupAsync(g.groupId);
                    _("Deleting from local DB...");
                    db.Delete(g);
                    _("Done.");
                }

                DataTable result = LoadGroupsData();
                GridGroupModel.ItemsSource = result.DefaultView;
            }catch(Exception ex)
            {
                _(ex.Message);
            }
            
        }
    }
}
