-- --------------------------------------------------------
-- Host:                         192.168.1.2
-- Wersja serwera:               8.0.32-0ubuntu0.22.04.2 - (Ubuntu)
-- Serwer OS:                    Linux
-- HeidiSQL Wersja:              12.4.0.6659
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Zrzut struktury bazy danych SODP
DROP DATABASE IF EXISTS `SODP`;
CREATE DATABASE IF NOT EXISTS `SODP` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_bin */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `SODP`;

-- Zrzut struktury tabela SODP.AspNetRoleClaims
CREATE TABLE IF NOT EXISTS `AspNetRoleClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` int NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.AspNetRoleClaims: ~0 rows (około)

-- Zrzut struktury tabela SODP.AspNetUserClaims
CREATE TABLE IF NOT EXISTS `AspNetUserClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.AspNetUserClaims: ~0 rows (około)

-- Zrzut struktury tabela SODP.AspNetUserLogins
CREATE TABLE IF NOT EXISTS `AspNetUserLogins` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `ProviderKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `UserId` int NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.AspNetUserLogins: ~0 rows (około)

-- Zrzut struktury tabela SODP.AspNetUserRoles
CREATE TABLE IF NOT EXISTS `AspNetUserRoles` (
  `UserId` int NOT NULL,
  `RoleId` int NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_SODP.Roles` FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_SODP.Users` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.AspNetUserRoles: ~8 rows (około)
REPLACE INTO `AspNetUserRoles` (`UserId`, `RoleId`) VALUES
	(1, 1),
	(11, 1),
	(4, 2),
	(11, 2),
	(12, 2),
	(2, 3),
	(3, 3),
	(11, 3);

-- Zrzut struktury tabela SODP.AspNetUserTokens
CREATE TABLE IF NOT EXISTS `AspNetUserTokens` (
  `UserId` int NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.AspNetUserTokens: ~0 rows (około)

-- Zrzut struktury tabela SODP.Branches
CREATE TABLE IF NOT EXISTS `Branches` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Sign` varchar(10) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ActiveStatus` tinyint(1) NOT NULL DEFAULT '1',
  `CreateTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `ModifyTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `Order` int NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`),
  KEY `BranchesIX_Order` (`Order`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.Branches: ~2 rows (około)
REPLACE INTO `Branches` (`Id`, `Sign`, `Name`, `ActiveStatus`, `CreateTimeStamp`, `ModifyTimeStamp`, `Order`) VALUES
	(1, 'A', 'ARCHITEKTUTA', 1, '2022-12-09 08:44:24.405251', '2022-12-09 08:44:52.927261', 1),
	(2, 'K', 'KONSTRUKCJA', 1, '2022-12-09 08:45:11.024844', '2022-12-09 07:45:11.024282', 1);

-- Zrzut struktury tabela SODP.BranchLicenses
CREATE TABLE IF NOT EXISTS `BranchLicenses` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CreateTimeStamp` datetime(6) NOT NULL,
  `ModifyTimeStamp` datetime(6) NOT NULL,
  `BranchId` int NOT NULL,
  `LicenseId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `BranchLicensesIX_Branch` (`BranchId`),
  KEY `BranchLicensesIX_License` (`LicenseId`),
  CONSTRAINT `FK_BranchLicenses_Branches_BranchId` FOREIGN KEY (`BranchId`) REFERENCES `Branches` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_BranchLicenses_Licenses_LicenseId` FOREIGN KEY (`LicenseId`) REFERENCES `Licenses` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- Zrzucanie danych dla tabeli SODP.BranchLicenses: ~1 rows (około)
REPLACE INTO `BranchLicenses` (`Id`, `CreateTimeStamp`, `ModifyTimeStamp`, `BranchId`, `LicenseId`) VALUES
	(1, '2023-03-11 23:22:31.640126', '2023-03-11 22:22:31.532434', 1, 1);

-- Zrzut struktury tabela SODP.BranchRoles
CREATE TABLE IF NOT EXISTS `BranchRoles` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CreateTimeStamp` datetime(6) NOT NULL,
  `ModifyTimeStamp` datetime(6) NOT NULL,
  `PartBranchId` int NOT NULL,
  `Role` int NOT NULL,
  `LicenseId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `BranchRolesIX_License` (`LicenseId`),
  KEY `IX_BranchRoles_PartBranchId` (`PartBranchId`),
  CONSTRAINT `FK_BranchRoles_Licenses_LicenseId` FOREIGN KEY (`LicenseId`) REFERENCES `Licenses` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_BranchRoles_PartBranches_PartBranchId` FOREIGN KEY (`PartBranchId`) REFERENCES `PartBranches` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- Zrzucanie danych dla tabeli SODP.BranchRoles: ~0 rows (około)

-- Zrzut struktury tabela SODP.Certificates
CREATE TABLE IF NOT EXISTS `Certificates` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `DesignerId` int NOT NULL,
  `Number` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `StartDate` datetime(6) NOT NULL,
  `EndDate` datetime(6) NOT NULL,
  `CreateTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `ModifyTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`),
  KEY `CertificatesIX_Designer` (`DesignerId`),
  CONSTRAINT `FK_Certificates_Designers_DesignerId` FOREIGN KEY (`DesignerId`) REFERENCES `Designers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.Certificates: ~0 rows (około)

-- Zrzut struktury tabela SODP.Designers
CREATE TABLE IF NOT EXISTS `Designers` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT '',
  `Firstname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Lastname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `ActiveStatus` tinyint(1) NOT NULL DEFAULT '1',
  `CreateTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `ModifyTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.Designers: ~0 rows (około)
REPLACE INTO `Designers` (`Id`, `Title`, `Firstname`, `Lastname`, `ActiveStatus`, `CreateTimeStamp`, `ModifyTimeStamp`) VALUES
	(1, 'inż', 'Grześ', 'Dziedzic', 1, '2022-12-09 08:45:50.994740', '2022-12-09 09:01:17.754616');

-- Zrzut struktury tabela SODP.Dictionary
CREATE TABLE IF NOT EXISTS `Dictionary` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CreateTimeStamp` datetime(6) NOT NULL,
  `ModifyTimeStamp` datetime(6) NOT NULL,
  `ParentId` int DEFAULT NULL,
  `Sign` varchar(10) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ActiveStatus` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `DictioanryIX_ParentId` (`ParentId`),
  CONSTRAINT `FK_Dictionary_Dictionary_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `Dictionary` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- Zrzucanie danych dla tabeli SODP.Dictionary: ~0 rows (około)

-- Zrzut struktury tabela SODP.Investors
CREATE TABLE IF NOT EXISTS `Investors` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CreateTimeStamp` datetime(6) NOT NULL,
  `ModifyTimeStamp` datetime(6) NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ActiveStatus` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`),
  KEY `InvestorsIX_Name` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- Zrzucanie danych dla tabeli SODP.Investors: ~0 rows (około)

-- Zrzut struktury tabela SODP.Licenses
CREATE TABLE IF NOT EXISTS `Licenses` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CreateTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `ModifyTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `DesignerId` int NOT NULL,
  `Content` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `LicensesIX_Designer` (`DesignerId`),
  CONSTRAINT `FK_Licenses_Designers_DesignerId` FOREIGN KEY (`DesignerId`) REFERENCES `Designers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- Zrzucanie danych dla tabeli SODP.Licenses: ~0 rows (około)
REPLACE INTO `Licenses` (`Id`, `CreateTimeStamp`, `ModifyTimeStamp`, `DesignerId`, `Content`) VALUES
	(1, '2022-12-09 08:46:37.895350', '2023-03-11 23:22:31.640197', 1, '55555555');

-- Zrzut struktury tabela SODP.PartBranches
CREATE TABLE IF NOT EXISTS `PartBranches` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CreateTimeStamp` datetime(6) NOT NULL,
  `ModifyTimeStamp` datetime(6) NOT NULL,
  `ProjectPartId` int NOT NULL,
  `BranchId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_PartBranches_BranchId` (`BranchId`),
  KEY `PartBranchesIX_ProjectPartId` (`ProjectPartId`),
  CONSTRAINT `FK_PartBranches_Branches_BranchId` FOREIGN KEY (`BranchId`) REFERENCES `Branches` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_PartBranches_ProjectParts_ProjectPartId` FOREIGN KEY (`ProjectPartId`) REFERENCES `ProjectParts` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- Zrzucanie danych dla tabeli SODP.PartBranches: ~0 rows (około)

-- Zrzut struktury tabela SODP.Parts
CREATE TABLE IF NOT EXISTS `Parts` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CreateTimeStamp` datetime(6) NOT NULL,
  `ModifyTimeStamp` datetime(6) NOT NULL,
  `Order` int NOT NULL DEFAULT '1',
  `Sign` varchar(10) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ActiveStatus` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`),
  KEY `PartIX_Order` (`Order`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- Zrzucanie danych dla tabeli SODP.Parts: ~0 rows (około)

-- Zrzut struktury tabela SODP.ProjectParts
CREATE TABLE IF NOT EXISTS `ProjectParts` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CreateTimeStamp` datetime(6) NOT NULL,
  `ModifyTimeStamp` datetime(6) NOT NULL,
  `ProjectId` int NOT NULL,
  `Sign` varchar(10) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Order` int NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`),
  KEY `ProjectPartsIX_Project` (`ProjectId`),
  CONSTRAINT `FK_ProjectParts_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- Zrzucanie danych dla tabeli SODP.ProjectParts: ~0 rows (około)

-- Zrzut struktury tabela SODP.Projects
CREATE TABLE IF NOT EXISTS `Projects` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Number` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `StageId` int NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `Status` int NOT NULL,
  `Title` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT '',
  `Address` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT '',
  `BuildingCategory` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT '',
  `CreateTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `Investor` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT '',
  `LocationUnit` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT '',
  `ModifyTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `BuildingPermit` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT '',
  `DevelopmentDate` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `ProjectsIX_NumberStage` (`Number`,`StageId`),
  KEY `ProjectsIX_Stage` (`StageId`),
  CONSTRAINT `FK_Projects_Stages_StageId` FOREIGN KEY (`StageId`) REFERENCES `Stages` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1083 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.Projects: ~584 rows (około)
REPLACE INTO `Projects` (`Id`, `Number`, `StageId`, `Name`, `Description`, `Status`, `Title`, `Address`, `BuildingCategory`, `CreateTimeStamp`, `Investor`, `LocationUnit`, `ModifyTimeStamp`, `BuildingPermit`, `DevelopmentDate`) VALUES
	(431, '2025', 29, 'Legnica_Myvita_Zaklad_produkcji_suplementow_diety', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(432, '1905', 29, 'Winkelmann_I_remonWC', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(433, '2028', 33, 'CCC_audyt_ogrzewania', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(434, '1909', 36, 'Wroclaw_Toyota', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-23 15:20:54.528695', '', NULL),
	(435, '1929', 29, 'WinkelmannIII_pomieszczenie_szlifowania', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(436, '1921', 29, 'Winkelmann_II_wjazd_portiernia', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(437, '2018', 24, 'Dlugoleka_Team', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(438, '1910', 35, 'WinkelmannI_SprawdzeniePosadzki', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(439, '2033', 29, 'WinkelmannIII_wydzielenie_cleanroom', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(440, '2023', 24, 'Wilczyce_chlodnia', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(441, '2006', 24, 'WinkelmannI_scianaogniowaprzejscie', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(442, '1999', 24, 'NewWiadomoJakNazwanyPoznan', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(443, '1924', 24, 'Winkelmann_IV_sluza', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(444, '2015', 29, 'Dlugoleka_Dobrygowski_wiata', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(445, '2029', 24, 'Lear_wyciag_lakiernia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(446, '2017', 37, 'Poznan_PIA_kontenery', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(447, '1610', 26, 'Toyota_Lubin_serwis_blacharsko_lakierniczy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(448, '2022', 24, 'Koscielec_chlodnia_rozbudowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(449, '2016', 32, 'Dauck_rozbiorkaobiektow', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(450, '1732', 24, 'Polkowice_dobudowa_biurowca', 'monitor', 1, 'komin', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-18 11:53:26.121442', '', NULL),
	(451, '2104', 29, 'Paszowice_fundamenty', NULL, 2, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(452, '1732', 29, 'Polkowice_dobudowa_biurowca', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(453, '1924', 29, 'Winkelmann_IV_sluza', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(454, '1929', 24, 'WinkelmannIII_pomieszczenie_szlifowania', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(455, '2024', 29, 'Legnica_Zaklad_produkcji_lodu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(456, '2101', 24, 'Lear_przeniesienie_sprezarkowni', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(457, '2013', 24, 'Winkelmann_I_przerobieniejadalninaszatnie', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(458, '2033', 24, 'WinkelmannIII_wydzielenie_cleanroom', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(459, '2032', 24, 'Turek_Messer_Rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-27 09:07:35.557477', '', NULL),
	(460, '1610', 29, 'Toyota_Lubin_serwis_blacharsko_lakierniczy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(461, '1909', 29, 'Wroclaw_Toyota', NULL, 2, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-23 13:50:38.733276', '', NULL),
	(462, '1927', 24, 'Legnica_Parafia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-17 15:29:14.043877', '', NULL),
	(463, '2004', 29, 'Swiebodzice_Masterform_rozbudowahaleprodukcyjne', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(464, '1824', 31, 'Winkelmann_I_ogrzewanie_wentylacja', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(465, '2004', 24, 'Swiebodzice_Masterform_rozbudowahaleprodukcyjne', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(466, '1907', 24, 'Polkowice_SiT_Rozbudowa_Hali', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(467, '1921', 24, 'Winkelmann_II_wjazd_portiernia', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(468, '2109', 24, 'Prochowice_Dagmar_Hala_Produkcyjna', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(469, '2014', 24, 'Lubin_SwPa_namiot_rozbudowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(470, '1920', 35, 'TurekMesserFundamentPodZbiornik', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(471, '1714', 24, 'Lubin_Dobrygowski_Lexus', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(472, '2015', 24, 'Dlugoleka_Dobrygowski_wiata', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(473, '2020', 29, 'Poznan_TCS_przebudowa_biura', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(474, '2103', 24, 'Ziemnice_Domek', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-24 14:07:39.853777', '', NULL),
	(475, '1917', 29, 'Lubon_lakiernia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(476, '1917', 24, 'Lubon_lakiernia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(477, '1807', 24, 'Poznan_Toyota_Lexus', '', 1, 'komin', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-02-02 18:04:47.939261', '', NULL),
	(478, '1839', 29, 'Winkelmann_II_biuro_informatykow', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(479, '2026', 24, 'Koscielec_Hala_magazynowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(480, '2032', 29, 'Turek_Messer_Rozbudowa', NULL, 2, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-26 14:55:37.784370', '', NULL),
	(481, '2003', 24, 'ZielonaG_SwitonPaczkowskiNamiot', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(482, '1916', 24, 'Prochowice_Derfol_hala', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(483, '2006', 29, 'WinkelmannI_scianaogniowaprzejscie', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(484, '2027', 24, 'Koscielec_kontenery_mieszkalne', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(485, '2026', 29, 'Koscielec_Hala_magazynowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(486, '1713', 29, 'Winkelmann_IV_ZSU', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(487, '1926', 24, 'Winkelmann_II_akumulatorownia_odciag', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(488, '1928', 33, 'ProchowiceEWG', NULL, 2, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(489, '2105', 35, 'Winkelmann_II_III_magazyn_uszkodzenie_slupa', NULL, 2, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(490, '1842', 24, 'LEAR_biura_magazynu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(491, '2007', 24, 'Torun_AudiPlockPopup', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(492, '2102', 24, 'Lubin_SwPa_Gabinet', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(493, '1733', 24, 'Lubin_SwPa_strefa_przyg', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(494, '1807', 38, 'Poznan_Toyota_Lexus', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(495, '1717', 24, 'Winkelmann_III_IV_Przebudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(496, '1732', 25, 'Polkowice_dobudowa_biurowca', 'komin', 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2022-12-16 08:40:39.985251', '', NULL),
	(497, '1834', 24, 'Poznan_DWA_Krancowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(498, '2024', 24, 'Legnica_Zaklad_produkcji_lodu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(499, '2030', 37, 'Prostynia_hala', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(500, '1824', 29, 'Winkelmann_I_ogrzewanie_wentylacja', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(501, '2101', 29, 'Lear_przeniesienie_sprezarkowni', NULL, 2, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(502, '1713', 24, 'Winkelmann_IV_ZSU', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(503, '2019', 24, 'Lear_wentylacja_akumulatorowni', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(504, '2022', 29, 'Koscielec_chlodnia_rozbudowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(505, '2106', 29, 'Winkelmann_I_przebudowa_drzwi_portierni', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(506, '2001', 24, 'BykowAlucromPrzebudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(507, '1807', 29, 'Poznan_Toyota_Lexus', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-27 16:35:07.945519', '', NULL),
	(508, '1727', 24, 'Babin_Silosy', 'zbiornik', 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(509, '1824', 24, 'Winkelmann_I_ogrzewanie_wentylacja', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(510, '2021', 24, 'Koscielec_budynki_mieszkalne', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(511, '1909', 24, 'Wroclaw_Toyota', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-23 14:45:29.987008', '', NULL),
	(512, '1838', 24, 'ZimnaWodaZjad', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(513, '2025', 24, 'Legnica_Myvita_Zaklad_produkcji_suplementow_diety', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(514, '2108', 24, 'Lubin_Dobrygowski_Lexus', NULL, 2, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(515, '1807', 25, 'Poznan_Toyota_Lexus', '', 0, 'monitor', '', '', '0001-01-01 00:00:00.000000', '', '', '2022-12-16 08:41:08.631087', '', NULL),
	(516, '2008', 35, 'Winkelmann_I_analizauszkodzenslupa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(517, '1821', 29, 'Winkelmann_I_hala4_umywalnia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(518, '2005', 29, 'WinkelmannIII_ScianaOddzielajaca', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(519, '1610', 25, 'Toyota_Lubin_serwis_blacharsko_lakierniczy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(520, '2107', 29, 'Winkelmann_I_przeniesienie_stezenia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(521, '2002', 35, 'LearOpiniaTechnicznaPomost', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(522, '2031', 31, 'Winkelmann_kogeneracja', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(523, '1423', 24, 'Zielona_Gora_Seat_VW', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(524, '1912', 24, 'Lear_hala_pras_przebudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(525, '1314', 29, 'Winkelmann_I_Hala_V_wzmocnienie', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(526, '1507', 24, 'Poznan_Porsche_Obornicka_kontenery', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(527, '1535', 25, 'Dlugoleka_Infiniti', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(528, '1519', 35, 'Winkelmann_IV', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(529, '1339', 26, 'Winkelmann_I_przebudowa_wjazdu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(530, '1341', 38, 'Winkelmann_I_przebudowa_ukladu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(531, '1531', 31, 'Swieradow_Kompleks_narciarski', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(532, '1523', 31, 'Swieradow_trasa_narciarska', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(533, '1817', 24, 'Winkelmann_I_fundament_WZ', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(534, '1401', 24, 'Winkelmann_I_biurowiec_przebudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(535, '1815', 24, 'Mirkow_dom_warsztat', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(536, '1532', 24, 'Winkelmann_III_laboratorium_dygestprium', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(537, '1443', 39, 'Poznan_Skoda', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(538, '1830', 29, 'Winkelmann_I_palarnia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(539, '1835', 24, 'LegnicaPrzychodniaAsnyka', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(540, '1809', 24, 'Zlotoryja_myjnia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(541, '1719', 40, 'Winkelmann_Projekt_organizacji_ruchu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(542, '1451', 24, 'Poznan_VGP_Plac_skladowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(543, '1902', 24, 'Winkelmann_Parking', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(544, '1312', 31, 'Janiszowice_Magazyn_zbozowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(545, '1622', 24, 'Winkelmann_III_akumulatorownia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(546, '1911', 29, 'Zlotoryja_Moldrog', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(547, '1811', 24, 'Jawor_Mercedes', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(548, '1333', 31, 'Lubin', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(549, '1724', 29, 'Kielce_Alucrom_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(550, '1615', 24, 'Winkelmann_stolowka_przebudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(551, '1915', 24, 'Lear_namiot', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(552, '1335', 24, 'Winkelmann_I_zamnkniecie_Hali_3', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(553, '1448', 31, 'Polkowice_Hala_przy_CCC', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(554, '1450', 29, 'Turek_Messer_zaklad_Napelniania_Butli', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(555, '1425', 29, 'Warszawa_VW_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(556, '1619', 25, 'Lubin_SwPa_Myjnia_plus_serwis_zerowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(557, '1539', 24, 'Suchy_Las_Reanult_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(558, '1320', 29, 'Winkelmann_IV', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(559, '1803', 29, 'Poznan_logistyka_VW', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(560, '1635', 24, 'Polkowice_antresola', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(561, '1619', 29, 'Lubin_SwPa_Myjnia_plus_serwis_zerowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(562, '1922', 35, 'WinkelmannI_OpiniaTechnicznaTelomat', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(563, '1912', 29, 'Lear_hala_pras_przebudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(564, '1621', 24, 'Leszno_Ciesiolka_myjnia_1538', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(565, '1605', 24, 'Poznan_przebudowa_ukladu_komunikacyjnego_legalizacja', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(566, '1317', 24, 'Dauck_ogrodzenie', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(567, '1509', 24, 'Swidnica_DWA', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(568, '1411', 24, 'Winkelmann_IV_sciana_ogniowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(569, '1616', 24, 'Olesnica_Alucrom', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(570, '1405', 24, 'Winkelmann_II_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(571, '1428', 29, 'Poznan_Skoda_Zabudowa_dzialki', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(572, '1818', 29, 'Winkelmann_I_przebudowa_biura_IH', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(573, '1542', 25, 'Winkelmann_I_zakupy_przestawienie_sciany_ppoz', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(574, '1438', 29, 'Winkelmann_I_fundamenty_pod_regaly_hala', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(575, '1622', 29, 'Winkelmann_III_akumulatorownia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(576, '1913', 24, 'Boleslawiec_baza_logistyki', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(577, '1504', 24, 'Krakow_Skoda_Rebrending', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(578, '1414', 24, 'Turek_Messer_Parking', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(579, '1638', 24, 'Lear_Pomost_techniczny', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(580, '1833', 24, 'Kornik_Lakiernia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(581, '1724', 34, 'Kielce_Alucrom_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(582, '1518', 31, 'Poznan_VW_uzytkowe_aranzacja', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(583, '1441', 24, 'Winkelmann_I_Dzial_zakupow_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(584, '1706', 29, 'Winkelmann_II_III_hala_magazynowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(585, '1324', 29, 'Winkelmann_I_DKJ', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(586, '1535', 34, 'Dlugoleka_Infiniti', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(587, '1337', 24, 'Warszawa_VW_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(588, '1644', 31, 'Winkelmann_I_przebudowa_szatni', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(589, '1310', 29, 'Warszawa_Audi_Polczynska_Pylony', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(590, '1536', 24, 'Zrodla_Hala_Empol', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(591, '1729', 33, 'Koleje_Dolnoslaskie_Hala_nadzor', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(592, '1345', 39, 'Winkelmann_I', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(593, '1449', 33, 'Komorniki_CM', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(594, '1331', 29, 'Warszawa_Skoda_Pylon', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(595, '1614', 25, 'Pszenno_Rehabilitacja', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(596, '1512', 24, 'Dlugoleka_Vatt_hala_zsu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(597, '1914', 29, 'Lubin_SwPa_podnosnik', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(598, '1521', 38, 'Poznan_VW_Rebrending', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(599, '1641', 24, 'Jelenia_Gora_ZORKA_II_kontener_aceton', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(600, '1442', 29, 'Winkelmann_I_biura_utrzymania_ruchu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(601, '1404', 24, 'Lubin_Skoda', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(602, '1705', 24, 'Winkelmann_I_rozbudowa_placu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(603, '1836', 24, 'PoznanSkodaRozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(604, '1904', 29, 'Winkelmann_III_Azot', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(605, '1536', 29, 'Zrodla_Hala_Empol', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(606, '1831', 29, 'Swieradow_taras', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(607, '1430', 39, 'Winkelmann_II', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(608, '1810', 29, 'Winkelmann_zaplecze_kontenerowe', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(609, '1536', 25, 'Zrodla_Hala_Empol', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(610, '1308', 25, 'Winkelmann_IV', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(611, '1706', 24, 'Winkelmann_II_III_hala_magazynowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-19 08:08:27.189762', '', NULL),
	(612, '1702', 29, 'Modlnica_Toyota_Serwis', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(613, '1406', 24, 'Winkelmann_IV_spawalnia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(614, '1618', 24, 'Winkelmann_II_plac', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(615, '1618', 25, 'Winkelmann_II_plac', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(616, '1426', 26, 'Leszno_Reality_Park', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(617, '1620', 24, 'Zielona_Gora_namiot', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(618, '1412', 29, 'Winkelmann_II_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(619, '1633', 25, 'Lubin_Seat_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(620, '1329', 29, 'Winkelmann_IV_strop_silikonowni', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(621, '1435', 29, 'Winkelmann_I_hala_3_sluza', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(622, '1642', 31, 'Jelenia_Gora_ZORKA_III_zagospodarowanie_dzialek', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(623, '1446', 24, 'Winkelmann_I_Laboratorium_prob_wodnych', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(624, '1810', 24, 'Winkelmann_zaplecze_kontenerowe', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(625, '1711', 25, 'Wroclaw_Skoda', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(626, '1521', 24, 'Poznan_VW_Rebrending', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(627, '1413', 39, 'Winkelmann_III_IV', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(628, '1426', 41, 'Leszno_Reality_Park', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(629, '1351', 24, 'Janiszowice_Mroznia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(630, '1708', 29, 'Swieradow_SKI_SUN_sanitariaty', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(631, '1526', 24, 'Leszno_Budynek_handlowo_usl', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(632, '1722', 31, 'Winkelmann_I_Zakupy_wentylacja', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(633, '1422', 24, 'Pietrzykowice_Mercedes_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(634, '1903', 24, 'Olesnica_Szatnie', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(635, '1444', 24, 'Poznan_Skoda_Obornicka_Rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(636, '1321', 25, 'Winkelmann_III_IV_wjazd', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(637, '1643', 24, 'Poznan_Porsche_dostosowanie_do_wymogow', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(638, '1326', 42, 'Winkelmann_I_Wiatrolap', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(639, '1439', 24, 'Lubin_Audi_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(640, '1630', 24, 'Winkelmann_IV_instalacje_do_suszarni', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(641, '1535', 24, 'Dlugoleka_Infiniti', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(642, '1646', 29, 'Winkelmann_III_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(643, '1618', 29, 'Winkelmann_II_plac', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(644, '1635', 29, 'Polkowice_antresola', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(645, '1623', 24, 'Winkelmann_II_antresola', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(646, '1711', 26, 'Wroclaw_Skoda', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(647, '1319', 24, 'Dauck_budynek_biurowo_mieszkalny', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(648, '1418', 29, 'Kochlice_czesc_biurowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(649, '1507', 26, 'Poznan_Porsche_Obornicka_kontenery', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(650, '1610', 24, 'Toyota_Lubin_serwis_blacharsko_lakierniczy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(651, '1408', 24, 'Kochlice_czesc_biurowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(652, '1538', 24, 'Leszno_myjnia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(653, '1630', 29, 'Winkelmann_IV_instalacje_do_suszarni', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(654, '1704', 25, 'Winkelmann_I_plac_i_namiot_logistyki', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(655, '1328', 29, 'Dauck_budynek_biurowo_mieszkalny', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(656, '1629', 24, 'Swieradow_SKI_SUN_wypozyczalnia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(657, '1204', 25, 'Galeria_Kielce', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(658, '1446', 29, 'Winkelmann_I_Laboratorium_prob_wodnych', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(659, '1637', 29, 'Polkowice_CCC_antresola', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(660, '1812', 31, 'Warszawa_Porshe_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(661, '1536', 31, 'Zrodla_Hala_Empol', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(662, '1720', 24, 'Zieloga_Gora_Seat_VW_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(663, '1626', 24, 'Haerter_parking_rozbudowa_Legnica', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(664, '1437', 31, 'Gorzow_Wlkp_Mercedes', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(665, '1534', 31, 'Boleslawiec_Guardian_Socjal', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(666, '1716', 24, 'Wroclaw_Audi', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(667, '1805', 31, 'Polkowice_CCC', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(668, '1836', 29, 'PoznanSkodaRozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(669, '1520', 35, 'Winkelmann_IV_Ocena_wydajnosci_wentylacji', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(670, '1440', 25, 'Grajewski_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(671, '1439', 29, 'Lubin_Audi_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(672, '1524', 24, 'Leszno_Rozbudowa_blacharni_Super_POL', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(673, '1347', 24, 'Turek_Messer', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(674, '1441', 29, 'Winkelmann_I_Dzial_zakupow_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(675, '1334', 24, 'Winkelmann_I_rozbudowa_hal_7_8', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(676, '1450', 25, 'Turek_Messer_zaklad_Napelniania_Butli', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(677, '1427', 24, 'Turek_Zbiorniki', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(678, '1511', 24, 'Winkelmann_IV_stolowka', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(679, '1410', 25, 'Gates_Namiot', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(680, '1322', 24, 'Warszawa_VW_przebudowa_antresoli', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(681, '1316', 24, 'Grajewski_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(682, '1710', 29, 'Wrzesnia_Modernizacja_Hali_BUS', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(683, '1535', 29, 'Dlugoleka_Infiniti', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(684, '1640', 24, 'Jelenia_Gora_ZORKA_II_namiot', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(685, '1352', 24, 'Janiszowice_Magazyn_Zbozowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(686, '1712', 29, 'Legnica_Kancelaria', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(687, '1530', 24, 'Winkelmann_I_fundamenty_pod_urzadzenia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(688, '1402', 24, 'Winkelmann_III_IV_rozbiorka', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(689, '1445', 24, 'Winkelmann_IV_odzysk_ciepla', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(690, '1625', 24, 'Winkelmann_I_parking', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(691, '1422', 29, 'Pietrzykowice_Mercedes_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(692, '1354', 29, 'Dauck_Wzmocnienie_Wiaty_Garazowej', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(693, '1409', 24, 'Winkelmann_I_rozbudowa_namiotu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(694, '1518', 24, 'Poznan_VW_uzytkowe_aranzacja', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(695, '1436', 24, 'Polkowice_Estella_namiot', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(696, '1433', 29, 'Winkelmann_I_hala_8_Attyka', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(697, '1353', 24, 'Opole_Mazda', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(698, '1840', 29, 'Winkelmann_III_fundament_wycinarka_laserowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(699, '1832', 43, 'Winkelmann_InstrukcjaOdsniezania', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(700, '1426', 25, 'Leszno_Reality_Park', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(701, '1827', 39, 'Winkelmann_Inwentaryzacja', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(702, '1428', 24, 'Poznan_Skoda_Zabudowa_dzialki', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(703, '1711', 29, 'Wroclaw_Skoda', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(704, '1804', 29, 'Winkelmann_IVc_fundamenty_prasa_suwnica', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(705, '1724', 25, 'Kielce_Alucrom_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(706, '1421', 38, 'Poznan_Audi', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(707, '1734', 24, 'Winkelmann_II_namiot_1618', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(708, '1808', 29, 'Leszno_Toyota_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(709, '1606', 29, 'Winkelmann_IV_przeniesienie_narzedziowni', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(710, '1514', 24, 'Winkelmann_IV_Rozbudowa_pom_montazu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(711, '1704', 41, 'Winkelmann_I_plac_i_namiot_logistyki', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(712, '1633', 24, 'Lubin_Seat_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(713, '1823', 24, 'Poznan_VW_Pompownia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(714, '1822', 29, 'Jawor_Mercedes_rozklad_blach_VH_RM', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(715, '1646', 34, 'Winkelmann_III_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(716, '1632', 29, 'Winkelmann_I_rozbudowa_budynku_konstruktorow', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(717, '1307', 29, 'Gates_Rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(718, '1819', 24, 'Winkelmann_I_fundament_LS7_1817', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(719, '1718', 24, 'Lubin_DWA', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(720, '1542', 24, 'Winkelmann_I_zakupy_przestawienie_sciany_ppoz', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(721, '1325', 24, 'Dauck_Kontenery', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(722, '1808', 24, 'Leszno_Toyota_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(723, '1336', 29, 'Winkelmann_I_sciana_ogniowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(724, '1435', 24, 'Winkelmann_I_hala_3_sluza', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(725, '1615', 29, 'Winkelmann_stolowka_przebudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(726, '1423', 25, 'Zielona_Gora_Seat_VW', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(727, '1513', 24, 'Lubin_Seat_tymczasowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(728, '1802', 31, 'Vatt_MPZP', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(729, '1803', 24, 'Poznan_logistyka_VW', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(730, '1431', 24, 'Poznan_VW_Krancowa_Myjnia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(731, '1330', 39, 'Strzegom_Przychodnia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(732, '1808', 25, 'Leszno_Toyota_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(733, '1648', 29, 'Lear_Przeniesienie_biura_na_antresole', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(734, '1434', 25, 'Dlugoleka_Team_salon_samochodowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(735, '1814', 39, 'Poznan_inwentaryzacja_biurowca_TCS', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(736, '1906', 29, 'Poznan_VW_przejazd', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(737, '1806', 24, 'Poznan_PorscheRozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(738, '1313', 24, 'Ernestynow_Wiata_przystankowa_przeniesienie', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(739, '1628', 29, 'Lear_posadowienie_antresoli', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(740, '1611', 29, 'Wrzesnia_pale', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(741, '1631', 24, 'Leszno_DWA_Audi_Select', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(742, '1711', 34, 'Wroclaw_Skoda', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(743, '1646', 24, 'Winkelmann_III_rozbudowa_ONI', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(744, '1736', 24, 'Rybnik_Messer', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(745, '1527', 24, 'Winkelmann_IV_altana_dla_pracownikow', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(746, '1702', 24, 'Modlnica_Toyota_Serwis', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(747, '1711', 30, 'Wroclaw_Skoda', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(748, '1703', 43, 'Winkelmann', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(749, '1702', 25, 'Modlnica_Toyota_Serwis', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(750, '1434', 26, 'Dlugoleka_Team_salon_samochodowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(751, '1407', 39, 'Polkowice_VW_laboratorium', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(752, '1338', 29, 'Winkelmann_III_szatnie', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(753, '1726', 24, 'Poznan_VGP_Zlobek', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(754, '1515', 24, 'Warszawa_PIA_Audi_Podnosnik', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(755, '1730', 24, 'Legnica_HML_Zbiornik_Messer', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(756, '1623', 29, 'Winkelmann_II_antresola', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(757, '1528', 31, 'Lubin_Dobrygowski_magazyn_opon', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(758, '1930', 35, 'WinkelmannI_AnalizaEstakadyWciagnika', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(759, '1442', 24, 'Winkelmann_I_biura_utrzymania_ruchu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(760, '1344', 24, 'Poznan_plac_skladowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(761, '1711', 24, 'Wroclaw_Skoda', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(762, '1816', 33, 'Polkowice_CCC_nadzor', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(763, '1343', 24, 'Poznan_Salon_Audi', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(764, '1825', 29, 'Poznan_VWP_posadzka', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(765, '1403', 29, 'Turek_Messer', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(766, '1424', 38, 'Warszawa_VW_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(767, '1507', 44, 'Poznan_Porsche_Obornicka_kontenery', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(768, '1606', 24, 'Winkelmann_IV_przeniesienie_narzedziowni', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(769, '1716', 37, 'Wroclaw_Audi', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(770, '1724', 24, 'Kielce_Alucrom_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(771, '1708', 24, 'Swieradow_SKI_SUN_sanitariaty', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(772, '1602', 24, 'Swieradow_przebudowa_stacji_gornej', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(773, '1619', 24, 'Lubin_SwPa_Myjnia_plus_serwis_zerowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(774, '1806', 29, 'Poznan_PorscheRozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(775, '1309', 24, 'TCS_strop', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(776, '1820', 24, 'Ernestynow_Dauck_hala_magazynowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(777, '1725', 29, 'Bykow_Alucrom', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(778, '1514', 29, 'Winkelmann_IV_Rozbudowa_pom_montazu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(779, '1347', 25, 'Turek_Messer', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(780, '1645', 24, 'Lubin_Sw_Pa_Namioty', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(781, '1415', 29, 'Winkelmann_IV_spawalnia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(782, '1731', 24, 'Janow_Lubelski_Alucrom', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(783, '1429', 26, 'Leszno_Reanult_Przebudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(784, '1904', 24, 'Winkelmann_III_Azot', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(785, '1737', 24, 'Winkelmann_IV_lutowanie', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(786, '1516', 33, 'Pawlowice_farma_fotowoltaiczna', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(787, '1508', 24, 'Leszno_Leszno_Rebrending', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(788, '1612', 24, 'Winkelmann_II_laboratorium', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(789, '1826', 29, 'Winkelmann_I_konstrukcja_nad_trawiatorami', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(790, '1447', 35, 'Polkowice_CCC_analiza_pod_monitoring', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(791, '1813', 24, 'Swieradow_wyciag_orczykowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(792, '1614', 24, 'Pszenno_Rehabilitacja', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(793, '1706', 34, 'Winkelmann_II_III_hala_magazynowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(794, '1711', 37, 'Wroclaw_Skoda', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(795, '1817', 29, 'Winkelmann_I_fundament_WZ', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(796, '1429', 24, 'Leszno_Renault_Przebudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(797, '1529', 24, 'Lubin_Seat', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(798, '1646', 25, 'Winkelmann_III_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(799, '1840', 24, 'Winkelmann_III_fundament_wycinarka_laserowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(800, '2012', 29, 'WinkelmannI_h7_posadzka', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(801, '1707', 24, 'Dlugoleka_wjazd', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(802, '1505', 24, 'Poznan_Obornicka_DWA', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(803, '1911', 24, 'Zlotoryja_Moldrog', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(804, '1638', 29, 'Lear_Pomost_techniczny', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(805, '1426', 24, 'Leszno_Reality_Park', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(806, '1925', 24, 'Leszno_Galeria_1526', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(807, '1706', 25, 'Winkelmann_II_III_hala_magazynowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(808, '1705', 29, 'Winkelmann_I_rozbudowa_placu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(809, '1444', 29, 'Poznan_Skoda_Obornicka_Rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(810, '1723', 24, 'Viessman_Zbiornik', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(811, '1906', 24, 'Poznan_VW_przejazd', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(812, '1434', 24, 'Dlugoleka_Team_salon_samochodowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(813, '1603', 24, 'Dauck_Przebudowa_zjazdu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(814, '1923', 24, 'Jelcz_Acana', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(815, '1830', 24, 'Winkelmann_I_palarnia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(816, '1541', 31, 'Dlugoleka_Mazda', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(817, '1634', 24, 'Swieradow_Hotel', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(818, '1355', 29, 'Winkelmann_I_lakiernia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(819, '1639', 24, 'Krakow_Toyota', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(820, '1301', 24, 'Gates_Rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(821, '1627', 24, 'Polkowice_CTI_ANCOR_Hala_Produkcyjna', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(822, '1704', 31, 'Winkelmann_I_koncepcja_rozbudowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(823, '1315', 26, 'Winkelmann_III', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(824, '1608', 24, 'Warszawa_PIA_dostosowanie_do_wymogow', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(825, '1423', 29, 'Zielona_Gora_Seat_VW', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(826, '1342', 29, 'Wroclaw_Kielczowska', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(827, '1608', 29, 'Warszawa_PIA_dostosowanie_do_wymogow', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(828, '1522', 24, 'Winkelmann_IV_wiata_na_rowery', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(829, '1327', 29, 'Winelmann_laboratorium', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(830, '1725', 24, 'Bykow_Alucrom', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(831, '1647', 29, 'Janikowo_LINTECH_fundament_manipulatora', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(832, '1434', 29, 'Dlugoleka_Team_salon_samochodowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(833, '1420', 24, 'Poznan_Hala_Namiotowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(834, '1419', 24, 'Poznan_Plac_Manewrowy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(835, '1837', 29, 'Hotel_Nowodworski', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(836, '1414', 25, 'Turek_Messer_Parking', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(837, '1346', 24, 'Legnica_Wroclawska_Mieszkanie', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(838, '1302', 31, 'VW_przebudowa_komunikacji', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(839, '1616', 25, 'Olesnica_Alucrom', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(840, '1607', 24, 'Turek_Messer_Parking_Praconiczy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(841, '1311', 31, 'Apinex_zaklad_produkcyjny', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(842, '1304', 25, 'Wienerberger', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(843, '1433', 24, 'Winkelmann_I_hala_8_Attyka', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(844, '1911', 30, 'Zlotoryja_Moldrog', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(845, '1841', 24, 'Winkelmann_I_DKJ_biuro', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(846, '1721', 24, 'Winkelmann_I_I5_biurowiec', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(847, '1332', 29, 'Winkelmann_III_V_zamkniecie_przejazdu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(848, '1648', 24, 'Lear_Przeniesienie_biura_na_antresole', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(849, '1624', 24, 'Bykow_Alucrom', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(850, '1628', 24, 'Lear_posadowienie_antresoli', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(851, '1829', 35, 'WinkelmannIII_OcenaSlupa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(852, '1517', 24, 'Tarnowiec_dworek', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(853, '1525', 24, 'Swidnik_Audi_Przebudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(854, '1841', 29, 'Winkelmann_I_DKJ_biuro', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(855, '1217', 31, 'Mercedes', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(856, '1908', 31, 'Poznan_VW_Bentley', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(857, '1919', 24, 'Olesnica_przelew_awaryjny', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(858, '1709', 24, 'Winkelmann_I_kontenery', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(859, '1701', 24, 'Zlotoryja_Schneider_Hala_Namiotowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(860, '1432', 24, 'Winkelmann_I_przelozenie_stezenia', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(861, '1613', 31, 'Dlugoleka_Polmotors', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(862, '1450', 24, 'Turek_Messer_zaklad_Napelniania_Butli', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(863, '1305', 24, 'VW_Polczynska_zabudowa_szachtu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(864, '1636', 24, 'Dlugoleka_Toyota_Namiot', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(865, '1738', 29, 'Winkelmann_II_fundament_prasy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(866, '1429', 29, 'Leszno_Renault_Przebudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(867, '1417', 29, 'Poznan_Salon_Audi', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(868, '1804', 24, 'Winkelmann_IVc_fundamenty_prasa_suwnica', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(869, '1616', 31, 'Olesnica_Alucrom', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(870, '1306', 29, 'Aminopieliki', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(871, '1601', 24, 'Zlotoryja_gabinet_stomatologiczny', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(872, '1617', 24, 'Lear_openspace_w_DD', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(873, '1901', 24, 'Winkelmann_Automotive_Parking', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(874, '1715', 29, 'Infiniti_Gdansk', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(875, '1318', 24, 'Winkelmann_Elewacje', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(876, '1704', 24, 'Winkelmann_I_plac_i_namiot_logistyki', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(877, '1716', 25, 'Wroclaw_Audi', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(878, '1323', 29, 'Warszawa_Porsche_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(879, '1303', 24, 'Porsche_rozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(880, '1429', 25, 'Leszno_Renault_Przebudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(881, '1521', 29, 'Poznan_VW_Rebrending', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(882, '1237', 24, 'VW_Group_Parking_przy_biurowcu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(883, '1716', 29, 'Wroclaw_Audi', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(884, '1340', 29, 'Warszawa_Porsche_Wciagnik', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(885, '1349', 35, 'Warszawa_Inter_Auto', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(886, '1836', 38, 'PoznanSkodaRozbudowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(887, '1510', 24, 'Walbrzych_DWA', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(888, '2011', 29, 'WinkelmannIV_fundamentpodregal', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(889, '1632', 24, 'Winkelmann_I_rozbudowa_budynku_konstruktorow', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(890, '2010', 24, 'Legnica_Partner_zbiornikzywicy', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(891, '1348', 24, 'Winkelmann_I_rozbudowa_1401', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(892, '1416', 25, 'Turek_Messer_110', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(893, '1416', 24, 'Turek_Messer_110', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(894, '1540', 29, 'Poznan_TCS_Zwiekszenie_nosnosci_stropu_serwerowni', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(895, '1604', 24, 'Winkelmann_przejscie_WIII_WIV', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(896, '1609', 24, '1609PB_Legnica_Lear_posadowienie_pras300_400', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(897, '2022', 25, 'Koscielec_chlodnia_rozbudowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(898, '2026', 25, 'Koscielec_Hala_magazynowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(899, '2110', 29, 'Polkowice_przychodnia_remont_wc', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(900, '2111', 24, 'Myslinow_Domek', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(901, '2111', 29, 'Myslinow_Domek', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(902, '2112', 24, 'Winkelmann_II_Rozbudowa_Parkingu', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(903, '2112', 29, 'Winkelmann_II_Rozbudowa_Parkingu', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(904, '2113', 24, 'Gorzow_KIM_Rozbudowa_Salonu', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(905, '2114', 24, 'Lubin_Dobrygowski_Uzywane', NULL, 2, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(910, '2116', 24, 'Winkelmann_I_schody', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(911, '2116', 29, 'Winkelmann_I_schody', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(913, '2117', 29, 'Winkelmann_I_Czyszczenie_Laserem', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(914, '2118', 35, 'Winkelmann_I_sciana_ppoz', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(916, '2119', 29, 'Lear_pomieszczenie_szkoleniowe', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(917, '1801', 24, 'Winkelmann_droga_dojazdowa_do', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(918, '2120', 24, 'Zlotniki_plantacja', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(919, '2121', 39, 'Winkelmann_Separatory', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(920, '2122', 24, 'Wroclaw_UTC_Kantyna', 'Zleceniodawca:\r\nUTC AEROSPACE SYSTEMS WROCŁAW SP. Z O.O.\r\nBIERUTOWSKA 65-67\r\nWROCŁAW 51-317', 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(921, '2123', 31, 'Winkelmann_I_Hala_Magazynowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(922, '2124', 24, 'Zielona_Gora_KIM', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(923, '2125', 40, 'Winkelmann_Organizacja_Ruchu', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(924, '2126', 24, 'Winkelmann_II_przestawienie_namiotu', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(925, '2127', 29, 'Winkelmann_II_rozbudowa_oswietlenia', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(926, '2128', 24, 'Turek_Messer_Parking', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(927, '2129', 33, 'Piotrowek_fotowoltaika', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(928, '2130', 24, 'Koscielec_Budynek_Nadbudowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(929, '2114', 29, 'Lubin_Dobrygowski_Uzywane', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(930, '2108', 29, 'Lubin_Dobrygowski_Lexus', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-25 09:48:00.974431', '', NULL),
	(931, '2131', 24, 'Lubin_WiK_Hala_magazynowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(932, '2131', 29, 'Lubin_WiK_Hala_magazynowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(933, '2132', 29, 'Lisowice_Dagmar_Rozbudowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(934, '2133', 33, 'Legnica_Polbruk_rozbudowa', NULL, 2, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-02-02 10:40:13.430229', '', NULL),
	(935, '2130', 29, 'Koscielec_Budynek_Nadbudowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(936, '2122', 29, 'Wroclaw_UTC_Kantyna', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(937, '2201', 24, 'Dlugoleka_Team_wiaty', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(938, '2202', 24, 'Winkelmann_I_Szatnie_nad_TS', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(939, '2203', 35, 'Winkelmann_Analiza_nosnosci_dachu', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(940, '2023', 29, 'Wilczyce_chlodnia', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(941, '2204', 24, 'Winkelmann_Bilbord', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(942, '2205', 29, 'Winkelmann_Domki_IS_deszczowa', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-24 12:50:58.565665', '', NULL),
	(943, '2206', 31, 'Dlugoleka_Dobrygowski_Lexus', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(944, '2207', 24, 'Boleslawiec_Parafia_Przebudowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(945, '1909', 34, 'Wroclaw_Toyota', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '2023-01-23 15:02:42.896271', '', NULL),
	(946, '2208', 24, 'Winkelmann_I_fundament_pod_filtry', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(947, '2209', 24, 'Zielona_Gora_Sw_Pa_Blacharnia_rozbudowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(948, '2209', 29, 'Zielona_Gora_Sw_Pa_Blacharnia_rozbudowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(950, '2211', 29, 'Swieradow_Ski_Sun', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(951, '2212', 24, 'Swieradow_baseny', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(953, '2202', 29, 'Winkelmann_I_Szatnie_nad_TS', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(954, '2214', 32, 'Polkowice_CSS', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(955, '2214', 24, 'Polkowice_CSS', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(956, '2201', 29, 'Dlugoleka_Team_wiaty', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(959, '2215', 24, 'Swieradow_PSZOK1_waga', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(960, '2216', 24, 'Swieradow_PSZOK2', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(961, '2212', 29, 'Swieradow_baseny', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(962, '2217', 29, 'Poznan_PIA_Krancowa_Podnosnik', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(963, '2218', 29, 'Wroclaw_Lexus', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(964, '2219', 24, 'Rabczyn_Messer', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(965, '2030', 24, 'Prostynia_hala', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(966, '2030', 29, 'Prostynia_hala', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(967, '2220', 31, 'Osla_rozbudowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(969, '2221', 29, 'Wojcieszow_Dach_OSW', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(970, '2222', 29, 'Winkelmann_II_Lab_Wentylacja', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(971, '2223', 24, 'Lear_waga_najazdowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(972, '2224', 24, 'Swieradow_Ski_Sun_Wyciag_Przestawienie', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(973, '2225', 29, 'Winkelmann_I_drabiny', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(974, '2226', 33, 'Wena_projekt_fotowoltaina', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(975, '2227', 24, 'Zanam_Zbiornik_Tlen', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(976, '2228', 35, 'Legnica_Rileta_Nosnosc_Dachu', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(978, '2229', 31, 'Winkelmann_I_koncepcja_ogrzewania', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(979, '2230', 35, 'Winkelmann_I_posadzka', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(980, '2231', 29, 'Lubin_Cupra', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(981, '2232', 24, 'Legnica_Wezi_Hale_Magazynowe', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(982, '2233', 24, 'Boleslawiec_Parafia_Wieza', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(983, '2234', 29, 'Winkelmann_III_przebudowa_biur', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(984, '2234', 34, 'Winkelmann_III_przebudowa_biur', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(985, '2235', 31, 'Lubin_Dobrygowski_Uzytkowe', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(986, '2236', 31, 'Dlugoleka_Dobrygowski_rozbudowa_wiaty', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1001, '2237', 31, 'Dlugoleka_Team_Ineos', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1008, '2122', 34, 'Wroclaw_UTC_Kantyna', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1009, '2015', 25, 'Dlugoleka_Dobrygowski_wiata', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1011, '2239', 24, 'Wroclaw_VW_PV', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1012, '2240', 24, 'Wroclaw_Audi_PV', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1013, '2238', 24, 'Wroclaw_MotorPol_PV', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1014, '2241', 24, 'Polkowice_ZWR_Rudna_Przesiewacze', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1016, '1107', 39, 'Legnica_Viessmann_hala_fundamenty', NULL, 1, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1017, '2242', 31, 'Lear_zmiana_ogrzewania', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1018, '2024', 25, 'Legnica_Zaklad_Produkcji_Lodu', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1019, '2243', 29, 'Legnica_Wezi_Przebudowa_SNNN', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1020, '2244', 24, 'Dlugoleka_Team_Wiata_Magazynowa', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1021, '2232', 29, 'Legnica_Wezi_Hale_Magazynowe', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1045, '2245', 24, 'Winkelmann_Magazyn_Tryskacze', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1046, '2246', 37, 'Koscielec_Chlodnia_Rozbudowa_II_etap', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1047, '2247', 29, 'Zelazny_Most_Instalacje', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1048, '2248', 24, 'Swieradow_deptak', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1050, '2250', 33, 'Lear_przeglady', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1051, '2249', 24, 'Swieradow_Park', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1052, '2251', 24, 'Winkelmann_I_posadowienie_agregatu', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1053, '2252', 35, 'Winkelmann_I_ekspertyza', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1054, '2253', 35, 'Winkelmann_II_III_IV_ekspertyza', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1055, '2254', 24, 'Winkelmann_II_stacja_testow', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1056, '2017', 24, 'Poznan_PIA_kontenery', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1057, '2255', 24, 'Dlugoleka_Team_PV', NULL, 0, '', '', '', '0001-01-01 00:00:00.000000', '', '', '0001-01-01 00:00:00.000000', '', NULL),
	(1058, '2256', 24, 'Dlugoleka_Dobrygowski_Wiata_PV', '', 0, '', '', '', '2022-12-21 12:32:00.754233', '', '', '2022-12-21 12:33:10.439253', '', NULL),
	(1059, '2257', 24, 'Legnica_WPEC_KD', '', 0, '', '', '', '2022-12-21 12:58:12.359480', '', '', '2022-12-21 11:58:12.225800', '', NULL),
	(1061, '2301', 24, 'Winkelmann_III_IV_Posadowienie_Agregatow', '', 0, '', '', '', '2023-01-10 08:05:19.441544', '', '', '2023-01-10 08:05:39.996208', '', NULL),
	(1063, '2302', 24, 'Winkelmann_II_Posadowienie_Agregatu', '', 0, '', '', '', '2023-01-17 08:42:54.706493', '', '', '2023-01-17 07:42:51.206165', '', NULL),
	(1069, '2303', 35, 'Winkelmann_Studnia_PPOZ', '', 0, '', '', '', '2023-01-19 10:55:03.564585', '', '', '2023-01-19 09:55:01.741858', '', NULL),
	(1070, '2304', 24, 'Dlugoleka_Dobrygowski_Serwis_Blacharski', '', 0, '', '', '', '2023-01-19 14:24:49.643953', '', '', '2023-01-19 13:24:47.148622', '', NULL),
	(1071, '2305', 24, 'Glogow_hala_wanien', '', 0, '', '', '', '2023-01-23 09:21:02.144874', '', '', '2023-01-23 08:21:00.217368', '', NULL),
	(1073, '2306', 29, 'Turek_Messer_Kontener', '', 0, '', '', '', '2023-01-23 13:33:26.717591', '', '', '2023-01-23 12:33:23.711940', '', NULL),
	(1075, '2307', 24, 'Polkowice_TEAM_organizacja_ruchu', '', 0, '', '', '', '2023-02-22 13:07:41.013498', '', '', '2023-02-22 12:07:39.230278', '', NULL),
	(1076, '2308', 24, 'Poznan_PIA_Audi_przebudowa', '', 0, '', '', '', '2023-02-22 13:39:56.391669', '', '', '2023-02-22 12:39:56.077530', '', NULL),
	(1077, '2308', 29, 'Poznan_PIA_Audi_przebudowa', '', 0, '', '', '', '2023-02-22 13:40:20.325236', '', '', '2023-02-22 12:40:19.858852', '', NULL),
	(1078, '2309', 35, 'Winkelmann_I_zuraw_I4', '', 0, '', '', '', '2023-03-02 09:11:34.950421', '', '', '2023-03-02 08:11:33.109840', '', NULL),
	(1079, '2248', 38, 'Swieradow_deptak', '', 0, '', '', '', '2023-03-02 12:39:47.283534', '', '', '2023-03-02 11:39:46.988909', '', NULL),
	(1080, '2246', 24, 'Koscielec_Chlodnia_Rozbudowa_II', '', 0, '', '', '', '2023-03-03 11:44:50.018883', '', '', '2023-03-03 10:44:46.999913', '', NULL),
	(1082, '2310', 24, 'Koscielec_Rozbudowa_Magazynu', '', 0, '', '', '', '2023-03-03 11:45:42.487626', '', '', '2023-03-03 10:45:42.165434', '', NULL);

-- Zrzut struktury tabela SODP.Roles
CREATE TABLE IF NOT EXISTS `Roles` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RolesIX_NormalizedName` (`NormalizedName`),
  UNIQUE KEY `RolesIX_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.Roles: ~3 rows (około)
REPLACE INTO `Roles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`) VALUES
	(1, 'Administrator', 'ADMINISTRATOR', '3ab726e5-2325-464c-bb9c-835bf22d67ce'),
	(2, 'User', 'USER', 'a5f67dca-a9c6-4ab7-9d30-f04713ad6294'),
	(3, 'ProjectManager', 'PROJECTMANAGER', 'a4b532a6-9583-4ae0-9bb4-4e69b8d7c362');

-- Zrzut struktury tabela SODP.Stages
CREATE TABLE IF NOT EXISTS `Stages` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Sign` varchar(10) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL DEFAULT '',
  `ActiveStatus` tinyint(1) NOT NULL DEFAULT '1',
  `CreateTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `ModifyTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `Order` int NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.Stages: ~21 rows (około)
REPLACE INTO `Stages` (`Id`, `Sign`, `Name`, `ActiveStatus`, `CreateTimeStamp`, `ModifyTimeStamp`, `Order`) VALUES
	(24, 'PB', 'PROJEKT BUDOWLANY', 1, '0001-01-01 00:00:00.000000', '2022-12-08 23:21:56.015795', 1),
	(25, 'PBZ', 'PROJEKT BUDOWLANY ZAMIENNY', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(26, 'PBZII', 'PROJEKT BUDOWLANY ZAMIENNY', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(27, 'PAB', 'PROJEKT ARCHITEKTONICZNO-BUDOWLANY', 1, '0001-01-01 00:00:00.000000', '2022-12-08 23:21:53.384011', 1),
	(28, 'PT', 'PROJEKT TECHNICZNY', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(29, 'PW', 'PROJEKT WYKONAWCZY', 1, '0001-01-01 00:00:00.000000', '2022-12-08 23:22:14.647545', 1),
	(30, 'PWKS', 'PROJEKT WYKONAWCZY KONSTRUKCJI STALOWEJ', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(31, 'PK', 'PROJEKT KONCEPCYJNY', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(32, 'PR', 'PROJEKT ROZBIÓRKI', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(33, 'NI', 'NADZÓR INWESTORSKI', 1, '0001-01-01 00:00:00.000000', '2022-12-20 13:45:19.677603', 1),
	(34, 'NA', 'NADZÓR AUTORSKI', 0, '0001-01-01 00:00:00.000000', '2022-12-09 09:01:09.005559', 1),
	(35, 'OT', 'OPINIA TECHNICZNA', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(36, 'RE', 'PROJEKT REMONTU', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(37, 'WZ', 'WARUNKI ZABUDOWY', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(38, 'PP', 'PROJEKT PRZETARGOWY', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(39, 'IB', 'INWENTARYZACJA BUDOWLANA', 1, '0001-01-01 00:00:00.000000', '2022-12-20 13:45:18.777469', 1),
	(40, 'PO', 'PROJEKT ORGANIZACJI', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(41, 'PBZIII', 'PROJEKT BUDOWLANY ZAMIENNY', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(42, 'PBW', 'PROJEKT BUDOWLANO-WYKONAWCZY', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1),
	(43, 'IO', 'INSTRUKCJA ODŚNIEŻANIA', 0, '0001-01-01 00:00:00.000000', '2022-12-20 13:45:17.820450', 1),
	(44, 'WZII', 'WARUNKI ZABUDOWY', 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 1);

-- Zrzut struktury tabela SODP.Tokens
CREATE TABLE IF NOT EXISTS `Tokens` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `RefreshTokenKey` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Refresh` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `CreateTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `ModifyTimeStamp` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`),
  KEY `TokensIX_User` (`UserId`),
  CONSTRAINT `FK_Tokens_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.Tokens: ~0 rows (około)

-- Zrzut struktury tabela SODP.Users
CREATE TABLE IF NOT EXISTS `Users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `SecurityStamp` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `ConcurrencyStamp` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `PhoneNumber` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  `Firstname` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT '',
  `Lastname` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UsersIX_UserName` (`UserName`),
  UNIQUE KEY `UsersIX_MormalizedUserName` (`NormalizedUserName`),
  KEY `UsersIX_NormalizedEmail` (`NormalizedEmail`),
  KEY `UsersIX_Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.Users: ~6 rows (około)
REPLACE INTO `Users` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`, `Firstname`, `Lastname`) VALUES
	(1, 'Administrator', 'ADMINISTRATOR', NULL, NULL, 0, 'AQAAAAEAACcQAAAAEIH1DwIg4DoR4EW6Itlb7QrXJTg5rHDGnP991vhPGk/gZ5VKGDNVRgO0mxfIV4FTCQ==', 'EKBMQ4LBY36AUXR4TOI43O6JSDDVIEFK', NULL, NULL, 0, 0, NULL, 1, 0, NULL, NULL),
	(2, 'PManager', 'PMANAGER', NULL, NULL, 0, 'AQAAAAEAACcQAAAAEG7cTR8JTnx4bh4TqaJzAb/nS/QWAGffmjza/qREDWlmGlvWGaJzcx0Nz6KHc/Vobg==', 'VKIDL7EMW2OOXMDU4OMIHQXC73WTQ7Z2', NULL, NULL, 0, 0, NULL, 1, 0, NULL, NULL),
	(3, 'Kasia', 'KASIA', NULL, NULL, 0, 'AQAAAAEAACcQAAAAEA5OjX6C3KKYtM4ap5NGrOl1ClG15DNoczKV1HTY7bjAqOX+lYIFS6Pkx4Xsdp+hzw==', '7X4ZPQUPW6ZBTN2DQAMWLEPSJI2DKQFL', NULL, NULL, 0, 0, NULL, 1, 0, 'Katarzyna', 'Koczenasz'),
	(4, 'Krystian', 'KRYSTIAN', NULL, NULL, 0, 'AQAAAAEAACcQAAAAEJrM1KZNgy7bf0qsL2oVPCt4dKvefn+uEqMIScZ+7+Zx0yln9p9Va8HBVJZMJU46eA==', 'WVLSLQR3EO5CLGTJK3F7G5OJHAB747WE', NULL, NULL, 0, 0, NULL, 1, 0, NULL, NULL),
	(11, 'Tomek', 'TOMEK', 'tomasz.tkaczyk@unipromax.pl', 'TOMASZ.TKACZYK@UNIPROMAX.PL', 0, 'AQAAAAEAACcQAAAAEN8n15r75PXGlvthp3Ouoh2CaoE6Pl6NulYkwPeuPmdCbJTFCZHRYi2t2rzG1J8Ryw==', 'MVDWXV3U7WQACSXNQGMWAVNM3TVWYTAS', NULL, NULL, 0, 0, NULL, 1, 0, NULL, NULL),
	(12, 'patrycja_malczyk', 'PATRYCJA_MALCZYK', 'p.malczyk@handbud.net', 'P.MALCZYK@HANDBUD.NET', 0, 'AQAAAAEAACcQAAAAEGwZhegLZdD9lff0rEVOHnm1JM393MdQUQA4j3d2L8QImeiKVyA0jMrP12mPaqsKng==', '2V4255HLLGRHBATJVUQM6QNBLP6MQ6Z2', NULL, NULL, 0, 0, NULL, 1, 0, '', '');

-- Zrzut struktury tabela SODP.__EFMigrationsHistory
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
  `MigrationId` varchar(95) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Zrzucanie danych dla tabeli SODP.__EFMigrationsHistory: ~3 rows (około)
REPLACE INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
	('20210321211553_CreateInit', '3.1.11'),
	('20221115214427_ModifyProjectProperties', '3.1.15'),
	('20221201094233_AddBuildingPermit', '3.1.15'),
	('20230213205524_AddParts', '3.1.15');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;