-- Server Version: 5.5.40-0+wheezy1
-- PHP-Version: 5.4.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_accounting`
--

CREATE TABLE IF NOT EXISTS `gk_accounting` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `date` datetime NOT NULL COMMENT 'time of accounting',
  `user` varchar(255) NOT NULL COMMENT 'username',
  `price` decimal(60,2) NOT NULL,
  `comment` text,
  `device` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_devices`
--

CREATE TABLE IF NOT EXISTS `gk_devices` (
  `ID` int(11) NOT NULL,
  `deviceID` varchar(255) NOT NULL,
  `OS` varchar(255) DEFAULT NULL,
  `comment` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_keydates`
--

CREATE TABLE IF NOT EXISTS `gk_keydates` (
  `keydate` datetime NOT NULL,
  `comment` text NOT NULL,
  PRIMARY KEY (`keydate`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_mail`
--

CREATE TABLE IF NOT EXISTS `gk_mail` (
  `username` varchar(255) NOT NULL COMMENT 'the user name',
  `adress` varchar(255) NOT NULL COMMENT 'the mail adress',
  `active` varchar(8) NOT NULL DEFAULT 'true' COMMENT 'indicate whether the mail should be active',
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_pricing`
--

CREATE TABLE IF NOT EXISTS `gk_pricing` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `product` varchar(255) NOT NULL COMMENT 'product name',
  `price` decimal(60,2) NOT NULL COMMENT 'product price',
  `comment` text,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `product` (`product`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_user`
--

CREATE TABLE IF NOT EXISTS `gk_user` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) NOT NULL COMMENT 'name of the user',
  `debt` decimal(60,2) NOT NULL COMMENT 'debt the user has',
  `state` varchar(255) NOT NULL DEFAULT 'active' COMMENT 'state of the user - maybe sth. like inactive, blacked, etc.)',
  `comment` text,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
