/*
Navicat MySQL Data Transfer

Source Server         : MySQL
Source Server Version : 50157
Source Host           : localhost:3306
Source Database       : localization

Target Server Type    : MYSQL
Target Server Version : 50157
File Encoding         : 65001

Date: 2011-08-17 14:28:42
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `bugs`
-- ----------------------------
DROP TABLE IF EXISTS `Bugs`;
CREATE TABLE `Bugs` (
  `BugsID` bigint(20) NOT NULL AUTO_INCREMENT,
  `WeekID` bigint(20) NOT NULL,
  `TotalBugs` bigint(20) DEFAULT NULL,
  `BugsRegressed` bigint(20) DEFAULT NULL,
  `BugsRegressedPending` bigint(20) DEFAULT NULL,
  `Hours` bigint(20) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`BugsID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bugs
-- ----------------------------

-- ----------------------------
-- Table structure for `efforts`
-- ----------------------------
DROP TABLE IF EXISTS `Efforts`;
CREATE TABLE `Efforts` (
  `EffortsID` bigint(20) NOT NULL AUTO_INCREMENT,
  `WeekID` bigint(20) NOT NULL,
  `MachineSetup` bigint(20) DEFAULT NULL,
  `Meetings` bigint(20) DEFAULT NULL,
  `WSR` bigint(20) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`EffortsID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of efforts
-- ----------------------------

-- ----------------------------
-- Table structure for `products`
-- ----------------------------
DROP TABLE IF EXISTS `Products`;
CREATE TABLE `Products` (
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of products
-- ----------------------------

-- ----------------------------
-- Table structure for `roles`
-- ----------------------------
DROP TABLE IF EXISTS `Roles`;
CREATE TABLE `Roles` (
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
INSERT INTO `Roles` VALUES ('1', 'GEN', 'General', null, null, null, null);
INSERT INTO `Roles` VALUES ('2', 'VIEW', 'View', null, null, null, null);
INSERT INTO `Roles` VALUES ('3', 'WRITE', 'Write', null, null, null, null);
INSERT INTO `Roles` VALUES ('4', 'REPORT', 'Reporting', null, null, null, null);
INSERT INTO `Roles` VALUES ('5', 'ADMIN', 'ADMINISTRATOR', null, null, null, null);
INSERT INTO `Roles` VALUES ('6', 'ADMINREP', 'ADMINISTRATOR-REPORTING', null, null, null, null);

-- ----------------------------
-- Table structure for `Category`
-- ----------------------------
DROP TABLE IF EXISTS `Category`;
CREATE TABLE `Category` (
  `CategoryID` bigint(20) NOT NULL AUTO_INCREMENT,
  `Category` varchar(50) NOT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Category
-- ----------------------------
-- ----------------------------
-- Table structure for `sections`
-- ----------------------------
DROP TABLE IF EXISTS `Sections`;
CREATE TABLE `Sections` (
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
-- Table structure for `testcases`
-- ----------------------------
DROP TABLE IF EXISTS `TestCases`;
CREATE TABLE `TestCases` (
  `TestCasesID` bigint(20) NOT NULL AUTO_INCREMENT,
  `WeekID` bigint(20) NOT NULL,
  `TestCasesExecuted` bigint(20) DEFAULT NULL,
  `Hours` bigint(20) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`TestCasesID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of TestCases
-- ----------------------------

-- ----------------------------
-- Table structure for `UserProducts`
-- ----------------------------
DROP TABLE IF EXISTS `UserProducts`;
CREATE TABLE `UserProducts` (
  `UserProductID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserID` bigint(20) NOT NULL,
  `ProductID` bigint(20) NOT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`UserProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of userproducts
-- ----------------------------

-- ----------------------------
-- Table structure for `userrights`
-- ----------------------------
DROP TABLE IF EXISTS `UserRights`;
CREATE TABLE `UserRights` (
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
DROP TABLE IF EXISTS `Users`;
CREATE TABLE `Users` (
  `LocalizationUserID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserID` varchar(50) NOT NULL,
  `UserName` varchar(100) DEFAULT NULL,
  `EmailID` varchar(200) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`LocalizationUserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of users
-- ----------------------------

-- ----------------------------
-- Table structure for `uservendors`
-- ----------------------------
DROP TABLE IF EXISTS `UserVendors`;
CREATE TABLE `UserVendors` (
  `UserVendorID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserID` bigint(20) NOT NULL,
  `VendorID` bigint(20) NOT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`UserVendorID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of uservendors
-- ----------------------------

-- ----------------------------
-- Table structure for `vendors`
-- ----------------------------
DROP TABLE IF EXISTS `Vendors`;
CREATE TABLE `Vendors` (
  `VendorID` bigint(20) NOT NULL AUTO_INCREMENT,
  `VendorCode` varchar(50) NOT NULL,
  `Vendor` varchar(100) DEFAULT NULL,
  `VendorLocation` varchar(100) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`VendorID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of vendors
-- ----------------------------

-- ----------------------------
-- Table structure for `weeks`
-- ----------------------------
DROP TABLE IF EXISTS `Weeks`;
CREATE TABLE `Weeks` (
  `WeekID` bigint(20) NOT NULL AUTO_INCREMENT,
  `WeekStartDate` date NOT NULL,
  `WeekEndData` date DEFAULT NULL,
  `Year` year(4) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`WeekID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of weeks
-- ----------------------------

-- ----------------------------
-- Table structure for `wsrdata`
-- ----------------------------
DROP TABLE IF EXISTS `WSRData`;
CREATE TABLE `WSRData` (
  `WSRDataID` bigint(20) NOT NULL AUTO_INCREMENT,
  `ProductID` bigint(20) NOT NULL,
  `VendorID` bigint(20) DEFAULT NULL,
  `WeekID` bigint(20) DEFAULT NULL,
  `RedIssues` varchar(1000) DEFAULT NULL,
  `YellowIssues` varchar(1000) DEFAULT NULL,
  `GreenAccom` varchar(1000) DEFAULT NULL,
  `TestCaseID` bigint(20) NOT NULL,
  `BugsID` bigint(20) DEFAULT NULL,
  `EffortsID` bigint(20) DEFAULT NULL,
  `AddedBy` varchar(50) DEFAULT NULL,
  `AddedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`WSRDataID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of wsrdata
-- ----------------------------
