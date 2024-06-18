CREATE TABLE  IF NOT EXISTS `users`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `userName` VARCHAR(255) NOT NULL,
    `email` VARCHAR(255) NOT NULL,
    `password` VARCHAR(255) NOT NULL,
    `salt` VARCHAR(255) NOT NULL,
    `googleId` VARCHAR(255) NOT NULL,
    `facebookId` VARCHAR(255) NOT NULL,
    `firstName` VARCHAR(255) NOT NULL,
    `middleName` VARCHAR(255) NOT NULL,
    `lastName` VARCHAR(255) NOT NULL,
    `phone` VARCHAR(255) NOT NULL,
    `coinBalance` INT NOT NULL,
    `clanId` INT NOT NULL,
    `localTimeZone` VARCHAR(255) NOT NULL,
    `localTimeZoneOffset` INT NOT NULL,
    `language` VARCHAR(255) NOT NULL,
    `userToken` VARCHAR(255) NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `friends`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `userId` INT NOT NULL,
    `friendId` INT NOT NULL,
    `relationshipStatus` INT NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `achievements`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `achievementName` VARCHAR(255) NOT NULL,
    `description` VARCHAR(255) NOT NULL,
    `shortDescription` VARCHAR(255) NOT NULL,
    `requiredPoints` INT NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `userAchievements`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `achievementId` INT NOT NULL,
    `userId` INT NOT NULL,
    `progress` INT NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `userMessages`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `message` VARCHAR(255) NOT NULL,
    `fromUser` INT NOT NULL,
    `toUser` INT NOT NULL,
    `sentTime` TIMESTAMP NOT NULL DEFAULT NOW(),
    `seenTime` TIMESTAMP NOT NULL DEFAULT NOW(),
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `systemMessages`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `message` VARCHAR(255) NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `systemMessageQueue`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `userId` INT NOT NULL,
    `messageId` INT NOT NULL,
    `status` INT NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW()
);
CREATE TABLE  IF NOT EXISTS `tradeItems`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `itemId` INT NOT NULL,
    `price` INT NOT NULL,
    `sellingUserId` INT NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `items`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `itemName` VARCHAR(255) NOT NULL,
    `description` VARCHAR(255) NOT NULL,
    `shortDescription` VARCHAR(255) NOT NULL,
    `itemType` INT NOT NULL,
    `itemRarity` INT NOT NULL,
    `stackable` INT NOT NULL,
    `stackLimit` INT NOT NULL,
    `tradable` INT NOT NULL,
    `consumable` INT NOT NULL,
    `equippable` INT NOT NULL,
    `customData` VARCHAR(255) NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `userItems`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `userId` INT NOT NULL,
    `itemId` INT NOT NULL,
    `quantity` INT NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `userAchievements`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `userId` INT NOT NULL,
    `achievementId` INT NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `clans`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `clanName` VARCHAR(255) NOT NULL,
    `description` VARCHAR(255) NOT NULL,
    `shortDescription` VARCHAR(255) NOT NULL,
    `clanType` INT NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `itemProperties`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `itemId` INT NOT NULL,
    `propertyName` INT NOT NULL,
    `propertyValue` VARCHAR(255) NOT NULL,
    `category` INT NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `currencies`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `currencyName` VARCHAR(255) NOT NULL,
    `description` VARCHAR(255) NOT NULL,
    `shortDescription` VARCHAR(255) NOT NULL,
    `category` INT NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `itemCosts`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `itemId` INT NOT NULL,
    `currencyId` INT NOT NULL,
    `cost` INT NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdated` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);
CREATE TABLE  IF NOT EXISTS `clanRanks`(
    `id` INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `rank` INT NOT NULL,
    `userId` INT NOT NULL,
    `clanId` INT NOT NULL,
    `created` TIMESTAMP NOT NULL DEFAULT NOW(),
    `lastUpdate` TIMESTAMP NOT NULL DEFAULT NOW(),
    `status` INT NOT NULL
);