/*
Navicat MySQL Data Transfer

Source Server         : MySQL
Source Server Version : 50157
Source Host           : localhost:3306
Source Database       : localization

Target Server Type    : MYSQL
Target Server Version : 50157
File Encoding         : 65001

Date: 2012-07-24 11:44:31
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `releasetypes`
-- ----------------------------
DROP TABLE IF EXISTS `releasetypes`;
CREATE TABLE `releasetypes` (
  `ReleaseTypeID` bigint(20) NOT NULL AUTO_INCREMENT,
  `ReleaseTypeCode` varchar(50) DEFAULT NULL,
  `ReleaseType` varchar(200) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`ReleaseTypeID`),
  UNIQUE KEY `UI_ReleaseTypeCode` (`ReleaseTypeCode`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of releasetypes
-- ----------------------------
INSERT INTO `releasetypes` VALUES ('4', 'PP', 'Perpetual Patch', null, null, null, null);
INSERT INTO `releasetypes` VALUES ('5', 'SP', 'Subscription Patch', null, null, null, null);
INSERT INTO `releasetypes` VALUES ('6', 'RL', 'Release (Installer)', null, null, null, null);
INSERT INTO `releasetypes` VALUES ('7', 'RS', 'Release + Subscription Patch', null, null, null, null);


ALTER TABLE ProductVersions ADD ReleaseTypeID bigint DEFAULT NULL;
ALTER TABLE ProductVersions ADD CONSTRAINT FK_ProductVersions_ReleaseTypes FOREIGN KEY (ReleaseTypeID) REFERENCES ReleaseTypes(ReleaseTypeID);
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('12', 'Grid Release Type', 'Release Type');




