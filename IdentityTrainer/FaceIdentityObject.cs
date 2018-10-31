
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Slugify;

namespace IdentityTrainer
{    
    /// <summary>
    /// Deprecating
    /// </summary>
    class FaceIdentityObject
    {
        private String personName;
        private String personGroup;
        private String personDir;

        private bool isValid;

        // All settings are saved in App.config
        private AppSettingsReader settingsReader;

        public string PersonGroup { get => personGroup; set => personGroup = value; }
        public string PersonName { get => personName; set => personName = value; }
        public string PersonDir { get => personDir; set => personDir = value; }
        public bool IsValid { get => isValid; set => isValid = value; }

        public FaceIdentityObject()
        {
            settingsReader = new AppSettingsReader();
            this.IsValid = false;
        }

        public void CreateDirectories()
        {
            SlugHelper slug = new SlugHelper();
            String personNameSlug = slug.GenerateSlug(this.personName);
            String personGroupSlug = slug.GenerateSlug(this.personGroup);
            this.PersonDir = String.Format("{0}/captures/{1}/{2}", settingsReader.GetValue("app_data", typeof(string)).ToString(), personGroupSlug, personNameSlug);
            
            System.IO.Directory.CreateDirectory(this.PersonDir);

            this.IsValid = true;
        }

        
    }
}
