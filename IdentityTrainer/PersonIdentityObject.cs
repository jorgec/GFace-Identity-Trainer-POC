using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityTrainer
{
    class PersonIdentityObject
    {
        private AppSettingsReader settingsReader;
        private String dir;
        private GroupIdentityObject group;
        private PersonModel person;

        public string Dir { get => dir; set => dir = value; }
        internal GroupIdentityObject Group { get => group; set => group = value; }
        internal PersonModel Person { get => person; set => person = value; }

        public PersonIdentityObject()
        {
            settingsReader = new AppSettingsReader();
        }

        
    }
}
