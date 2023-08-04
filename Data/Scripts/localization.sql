/*
Navicat MySQL Data Transfer

Source Server         : MySQL
Source Server Version : 50157
Source Host           : localhost:3306
Source Database       : localization

Target Server Type    : MYSQL
Target Server Version : 50157
File Encoding         : 65001

Date: 2011-08-28 14:30:54
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `bugs`
-- ----------------------------
DROP TABLE IF EXISTS `bugs`;
CREATE TABLE `bugs` (
  `BugsID` bigint(20) NOT NULL AUTO_INCREMENT,
  `WSRDataID` bigint(20) NOT NULL,
  `TotalBugs` int(20) DEFAULT NULL,
  `BugsRegressed` int(20) DEFAULT NULL,
  `BugsRegressedPending` int(20) DEFAULT NULL,
  `Hours` int(20) DEFAULT NULL,
  `Remarks` varchar(1000) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`BugsID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bugs
-- ----------------------------
INSERT INTO `bugs` VALUES ('1', '3', '234', '2', '45', '113', 'T5', 'ADMIN', null, 'ADMIN', null);
INSERT INTO `bugs` VALUES ('2', '15', '11', '12', '13', '16', 'TEST5', 'ADMIN', null, 'ADMIN', null);

-- ----------------------------
-- Table structure for `category`
-- ----------------------------
DROP TABLE IF EXISTS `category`;
CREATE TABLE `category` (
  `CategoryID` bigint(20) NOT NULL AUTO_INCREMENT,
  `Category` varchar(50) NOT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of category
-- ----------------------------
INSERT INTO `category` VALUES ('1', 'Weekly Status Report', null, null, null, null);

-- ----------------------------
-- Table structure for `efforts`
-- ----------------------------
DROP TABLE IF EXISTS `efforts`;
CREATE TABLE `efforts` (
  `EffortsID` bigint(20) NOT NULL AUTO_INCREMENT,
  `WSRDataID` bigint(20) NOT NULL,
  `MachineSetup` int(20) DEFAULT NULL,
  `Meetings` int(20) DEFAULT NULL,
  `WSR` int(20) DEFAULT NULL,
  `Remarks` varchar(1000) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`EffortsID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of efforts
-- ----------------------------
INSERT INTO `efforts` VALUES ('1', '3', '0', '0', '0', '', 'ADMIN', null, 'ADMIN', null);
INSERT INTO `efforts` VALUES ('2', '15', '14', '17', '18', 'TEST6', 'ADMIN', null, 'ADMIN', null);

-- ----------------------------
-- Table structure for `outstandingdeliverables`
-- ----------------------------
DROP TABLE IF EXISTS `outstandingdeliverables`;
CREATE TABLE `outstandingdeliverables` (
  `OutstandingDeliverablesID` bigint(20) NOT NULL DEFAULT '0',
  `WSRDataID` bigint(20) DEFAULT NULL,
  `PrevWeekDeliverables` varchar(200) DEFAULT NULL,
  `OriginalScheduleDate` date DEFAULT NULL,
  `Reason` varchar(500) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`OutstandingDeliverablesID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of outstandingdeliverables
-- ----------------------------

-- ----------------------------
-- Table structure for `products`
-- ----------------------------
DROP TABLE IF EXISTS `products`;
CREATE TABLE `products` (
  `ProductID` bigint(20) NOT NULL AUTO_INCREMENT,
  `ProductCode` varchar(50) NOT NULL,
  `Product` varchar(50) DEFAULT NULL,
  `ProductVersion` varchar(50) NOT NULL,
  `ProductYear` year(4) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`ProductID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of products
-- ----------------------------
INSERT INTO `products` VALUES ('1', 'AI', 'Illustrator', '16.0_CS6', '2012', null, null, null, null);
INSERT INTO `products` VALUES ('2', 'ID', 'InDesign', '16.0', '2012', null, null, null, null);
INSERT INTO `products` VALUES ('3', 'IC', 'InCopy', '16.0', '2012', null, null, null, null);

-- ----------------------------
-- Table structure for `roles`
-- ----------------------------
DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles` (
  `RoleID` bigint(20) NOT NULL AUTO_INCREMENT,
  `RoleCode` varchar(50) NOT NULL,
  `Role` varchar(50) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of roles
-- ----------------------------
INSERT INTO `roles` VALUES ('1', 'GEN', 'General', null, null, null, null);
INSERT INTO `roles` VALUES ('2', 'VW', 'View', null, null, null, null);
INSERT INTO `roles` VALUES ('3', 'WR', 'Write', null, null, null, null);
INSERT INTO `roles` VALUES ('4', 'REP', 'Reporting', null, null, null, null);
INSERT INTO `roles` VALUES ('5', 'ADMIN', 'ADMINISTRATOR', null, null, null, null);
INSERT INTO `roles` VALUES ('6', 'ADMINREP', 'ADMINISTRATOR-REPORTING', null, null, null, null);

-- ----------------------------
-- Table structure for `sections`
-- ----------------------------
DROP TABLE IF EXISTS `sections`;
CREATE TABLE `sections` (
  `SectionID` bigint(20) NOT NULL AUTO_INCREMENT,
  `Section` varchar(50) NOT NULL,
  `CategoryID` bigint(20) NOT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`SectionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sections
-- ----------------------------

-- ----------------------------
-- Table structure for `testcasesstats`
-- ----------------------------
DROP TABLE IF EXISTS `testcasesstats`;
CREATE TABLE `testcasesstats` (
  `TestCasesID` bigint(20) NOT NULL AUTO_INCREMENT,
  `WSRDataID` bigint(20) NOT NULL,
  `TestCasesExecuted` int(20) DEFAULT NULL,
  `Hours` int(20) DEFAULT NULL,
  `Remarks` varchar(1000) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`TestCasesID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of testcasesstats
-- ----------------------------
INSERT INTO `testcasesstats` VALUES ('1', '3', '34', '2', 'T4', 'ADMIN', null, 'ADMIN', null);
INSERT INTO `testcasesstats` VALUES ('2', '15', '10', '15', 'TEST4', 'ADMIN', null, 'ADMIN', null);

-- ----------------------------
-- Table structure for `userproducts`
-- ----------------------------
DROP TABLE IF EXISTS `userproducts`;
CREATE TABLE `userproducts` (
  `UserProductID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserID` bigint(20) NOT NULL,
  `ProductID` bigint(20) NOT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`UserProductID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of userproducts
-- ----------------------------
INSERT INTO `userproducts` VALUES ('1', '2', '1', null, null, null, null);
INSERT INTO `userproducts` VALUES ('2', '2', '2', null, null, null, null);
INSERT INTO `userproducts` VALUES ('3', '2', '3', null, null, null, null);

-- ----------------------------
-- Table structure for `userrights`
-- ----------------------------
DROP TABLE IF EXISTS `userrights`;
CREATE TABLE `userrights` (
  `UserRightID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserProductID` bigint(20) NOT NULL,
  `SectionID` bigint(20) DEFAULT NULL,
  `RoleID` bigint(20) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`UserRightID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of userrights
-- ----------------------------

-- ----------------------------
-- Table structure for `users`
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `LocalizationUserID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserID` varchar(50) NOT NULL,
  `UserName` varchar(100) DEFAULT NULL,
  `EmailID` varchar(200) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`LocalizationUserID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES ('2', 'sauragup', 'sauragup', 'sauragup@adobe.com', 'ADMIN', null, null, null);

-- ----------------------------
-- Table structure for `uservendors`
-- ----------------------------
DROP TABLE IF EXISTS `uservendors`;
CREATE TABLE `uservendors` (
  `UserVendorID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserID` bigint(20) NOT NULL,
  `VendorID` bigint(20) NOT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`UserVendorID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of uservendors
-- ----------------------------
INSERT INTO `uservendors` VALUES ('1', '2', '1', null, null, null, null);

-- ----------------------------
-- Table structure for `vendors`
-- ----------------------------
DROP TABLE IF EXISTS `vendors`;
CREATE TABLE `vendors` (
  `VendorID` bigint(20) NOT NULL AUTO_INCREMENT,
  `VendorCode` varchar(50) NOT NULL,
  `Vendor` varchar(100) DEFAULT NULL,
  `VendorLocation` varchar(100) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`VendorID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of vendors
-- ----------------------------
INSERT INTO `vendors` VALUES ('1', 'IND', 'Adobe India', 'Noida', null, null, null, null);
INSERT INTO `vendors` VALUES ('2', 'ASC', 'Adobe China', 'China', null, null, null, null);
INSERT INTO `vendors` VALUES ('3', 'ASJ', 'Adobe Japan', 'Japan', null, null, null, null);
INSERT INTO `vendors` VALUES ('4', 'SJ', 'Adobe US', 'US', null, null, null, null);
INSERT INTO `vendors` VALUES ('5', 'QAI', 'QA Infotech', 'India', null, null, null, null);
INSERT INTO `vendors` VALUES ('6', 'WIP', 'WIPRO', 'India', null, null, null, null);
INSERT INTO `vendors` VALUES ('7', 'LB', 'Lion Bridge', null, null, null, null, null);

-- ----------------------------
-- Table structure for `weeks`
-- ----------------------------
DROP TABLE IF EXISTS `weeks`;
CREATE TABLE `weeks` (
  `WeekID` bigint(20) NOT NULL AUTO_INCREMENT,
  `WeekStartDate` date NOT NULL,
  `WeekEndDate` date DEFAULT NULL,
  `Year` year(4) DEFAULT NULL,
  `IsActive` bit(1) DEFAULT b'0',
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`WeekID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of weeks
-- ----------------------------
INSERT INTO `weeks` VALUES ('1', '2011-08-01', '2011-08-05', null, '', null, null, null, null);
INSERT INTO `weeks` VALUES ('2', '2011-08-08', '2011-08-12', null, '', null, null, null, null);
INSERT INTO `weeks` VALUES ('3', '2011-08-15', '2011-08-19', null, '', null, null, null, null);

-- ----------------------------
-- Table structure for `wsrdata`
-- ----------------------------
DROP TABLE IF EXISTS `wsrdata`;
CREATE TABLE `wsrdata` (
  `WSRDataID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserProductID` bigint(20) NOT NULL,
  `UserVendorID` bigint(20) DEFAULT NULL,
  `WeekID` bigint(20) DEFAULT NULL,
  `RedIssues` varchar(1000) DEFAULT NULL,
  `YellowIssues` varchar(1000) DEFAULT NULL,
  `GreenAccom` varchar(1000) DEFAULT NULL,
  `NextWeekDeliverables` varchar(1000) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`WSRDataID`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of wsrdata
-- ----------------------------
INSERT INTO `wsrdata` VALUES ('3', '1', '1', '3', 'TEST', 'TEST1', 'TEST2', 'TEST6', 'ADMIN', null, 'ADMIN', null);
INSERT INTO `wsrdata` VALUES ('15', '1', '1', '2', 'TEST1', 'TEST2', 'TEST3', 'TEST7', 'ADMIN', null, 'ADMIN', null);
