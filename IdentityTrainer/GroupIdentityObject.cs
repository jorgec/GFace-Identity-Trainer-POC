using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slugify;
namespace IdentityTrainer
{
    class GroupIdentityObject
    {

        private AppSettingsReader settingsReader;

        private int id;
        private String groupName;
        private String groupId;

        private String dir;

        public int Id { get => id; set => id = value; }
        public string GroupName { get => groupName; }

        public string GroupId { get => groupId; }
        public string Dir { get => dir; set => dir = value; }

        public GroupIdentityObject()
        {
            settingsReader = new AppSettingsReader();
        }

        public bool SetGroupName(String g)
        {
            if (String.IsNullOrEmpty(g))
            {
                return false;
            }
            groupName = g;
            SetGroupId(g);
            CreateDirectory();
            return true;
        }

        private void SetGroupId(String g)
        {
            SlugHelper slug = new SlugHelper();
            String _g = slug.GenerateSlug(g);
            groupId = _g;
        }

        public void CreateDirectory()
        {
            Dir = String.Format("{0}/captures/{1}", settingsReader.GetValue("app_data", typeof(string)).ToString(), GroupId);            
            System.IO.Directory.CreateDirectory(Dir);
            
        }
    }
}
