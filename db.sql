-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.7.20-log - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL Version:             9.5.0.5196
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for gface_trainer
CREATE DATABASE IF NOT EXISTS `gface_trainer` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `gface_trainer`;

-- Dumping structure for table gface_trainer.groupmodel
CREATE TABLE IF NOT EXISTS `groupmodel` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `groupId` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `group_id` (`groupId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table gface_trainer.personmodel
CREATE TABLE IF NOT EXISTS `personmodel` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL DEFAULT '0',
  `personId` varchar(255) NOT NULL DEFAULT '0',
  `groupId` int(11) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `personId` (`personId`),
  KEY `group` (`groupId`),
  CONSTRAINT `group` FOREIGN KEY (`groupId`) REFERENCES `groupmodel` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
