using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityTrainer
{
    /// <summary>
    /// Data Model for persons local store
    /// </summary>
    /// <table>
    ///     personmodel
    /// </table>
    /// <columns>
    ///     id INT(11) UNSIGNED NOT NULL AUTO INCREMENT
    ///     name VARCHAR(255) NOT NULL
    ///     personId VARCHAR(255) NOT NULL
    ///     groupId INT(11) UNSIGNED NOT NULL
    /// </columns>
    /// <indices>
    ///     PRIMARY KEY(id)
    ///     UNIQUE KEY personId
    ///     KEY group (groupId)
    /// </indices>
    /// <constraints>
    ///     CONSTRAINT group FOREIGN KEY groupId REFERENCES groupmodel.id ON DELETE CASCADE ON UPDATE NO ACTION
    /// </constraints>
    /// <raw_query>
    /// CREATE TABLE IF NOT EXISTS `personmodel` (
    ///     `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
    ///     `name` varchar(255) NOT NULL DEFAULT '0',
    ///     `personId` varchar(255) NOT NULL DEFAULT '0',
    ///     `groupId` int (11) unsigned NOT NULL DEFAULT '0',
    ///     PRIMARY KEY(`id`),
    ///     UNIQUE KEY `personId` (`personId`),
    ///     KEY `group` (`groupId`),
    ///     CONSTRAINT `group` FOREIGN KEY(`groupId`) REFERENCES `groupmodel` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
    /// )
    /// </raw_query>
    class PersonModel
    {
        public int id { get; set; }
        public String name { get; set; }
        public String personId { get; set; }
        public int groupId { get; set; }
    }
}
