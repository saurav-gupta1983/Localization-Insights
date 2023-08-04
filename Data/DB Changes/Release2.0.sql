/*
Navicat MySQL Data Transfer

Source Server         : MySQL
Source Server Version : 50157
Source Host           : localhost:3306
Source Database       : localization

Target Server Type    : MYSQL
Target Server Version : 50157
File Encoding         : 65001

Date: 2012-07-30 12:38:17
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `productsprints`
-- ----------------------------
DROP TABLE IF EXISTS `productsprints`;
CREATE TABLE `productsprints` (
  `ProductSprintID` bigint(20) NOT NULL AUTO_INCREMENT,
  `ProductVersionID` bigint(20) NOT NULL,
  `Sprint` varchar(50) NOT NULL,
  `SprintDetails` varchar(200) DEFAULT NULL,
  `StartDate` date NOT NULL,
  `EndDate` date NOT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`ProductSprintID`),
  UNIQUE KEY `UI_ProductSprints` (`ProductVersionID`,`Sprint`) USING BTREE,
  KEY `UI_ProductSprints_ProductVersionID` (`ProductVersionID`),
  CONSTRAINT `FK_ProductSprint_ProductVersions` FOREIGN KEY (`ProductVersionID`) REFERENCES `productversions` (`ProductVersionID`)
) ENGINE=InnoDB AUTO_INCREMENT=69 DEFAULT CHARSET=utf8;

ALTER TABLE ProjectPhases ADD ProductSprintID bigint DEFAULT NULL;
ALTER TABLE ProjectPhases ADD CONSTRAINT FK_ProjectPhases_ProductSprints FOREIGN KEY (ProductSprintID) REFERENCES ProductSprints(ProductSprintID);

Update Screens Set ScreenIdentifier = 'Define Product Master Data', PageName = 'DefineProductMasterDataUserControl.ascx' Where ScreenID = 14;
Update ScreenLabels Set ScreenValue= 'Define Product Master Data' Where ScreenLabelID IN (57,120,175);

INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('14', 'Tab Define Product Sprints', 'Define Product Sprints');
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('14', 'Tab Locales and Platforms', 'Locales and Platforms');
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('14', 'Grid Sprint', 'Sprint');
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('14', 'Grid Sprint Details', 'Sprint Details');
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('14', 'Grid Start Date', 'Start Date');
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('14', 'Grid End Date', 'End Date');
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('14', 'Product Sprint Mandatory', 'Please specify the Sprint.');
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('14', 'StartDate', 'Please specify the Sprint Start Date.');
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('14', 'EndDate', 'Please specify the Sprint End Date.');
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('14', 'Date Check', 'The Sprint End Date should be greater than Sprint Start Date.');
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('13', 'Label Phase Type', 'Phase Type:');
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('31', 'All Phase Types', 'All Phase Types');
INSERT INTO `screenlabels`(ScreenID, ScreenLabel, ScreenValue) VALUES ('31', 'All Product Sprints', 'All Sprints');
