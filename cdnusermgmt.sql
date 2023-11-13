/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50505
Source Host           : localhost:3306
Source Database       : cdnusermgmt

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2023-11-13 14:09:27
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for t_freelance
-- ----------------------------
DROP TABLE IF EXISTS `t_freelance`;
CREATE TABLE `t_freelance` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(10) NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `Mail` varchar(100) DEFAULT NULL,
  `PhoneNumber` varchar(16) DEFAULT NULL,
  `Skillsets` varchar(255) DEFAULT NULL,
  `Hobby` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of t_freelance
-- ----------------------------
INSERT INTO `t_freelance` VALUES ('1', 'Azidi', 'Mohd Azidi', 'mohd_azidi@face.com', '015-109876', '[\"C#\",\"C++\",\"BPM\"]', '[\"Cycing\",\"Gardening\"]');
INSERT INTO `t_freelance` VALUES ('2', 'john_doe', null, 'john.doe@example.com', '1234567890', '[\"C#\",\"ASP.NET\",\"Web Development\"]', '[\"Reading\",\"Gaming\"]');
