using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityTrainer
{
    /// <summary>
    /// Data Model for Groups local store
    /// </summary>
    /// <table>
    ///     groupmodel
    /// </table>
    /// <columns>
    ///     id INT(11) UNSIGNED NOT NULL AUTO INCREMENT PK
    ///     name VARCHAR(255) NOT NULL
    ///     groupId VARCHAR(255) NOT NULL
    /// </columns>
    /// <indices>
    ///     PRIMARY KEY(id)
    ///     UNIQUE KEY groupId
    /// </indices>
    /// <raw_query>
    /// CREATE TABLE IF NOT EXISTS `groupmodel` (
    ///     `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
    ///     `name` varchar(255) NOT NULL,
    ///     `groupId` varchar(255) NOT NULL,
    ///     PRIMARY KEY(`id`),
    ///     UNIQUE KEY `group_id` (`groupId`)
    /// )
    /// </raw_query>
    class GroupModel
    {
        public int id { get; set; }
        public String name { get; set; }
        public String groupId { get; set; }
    }
}
