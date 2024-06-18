<?php
namespace USF;

global $usfConnectionOperations;
global $usfTableOperations;

class USFAchievementOperations
{
	function changeAchievementStatusForUser($userId, $achievementId, $progress, $toStatus)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		$check = $usfTableOperations->CheckFieldsExistence("userAchievements", array("userId", "achievementId"), array($userId, $achievementId));
		if (!$check) {
			return $usfConnectionOperations->UsfLogger(0) . $usfTableOperations->TryInsertToTable("userAchievements", array("userId", "achievementId", "progress", "status"), array($userId, $achievementId, $progress, $toStatus));
		}
		return $usfConnectionOperations->UsfLogger(0) . $usfTableOperations->TryUpdateTable("userAchievements", array("status", "progress"), array("$toStatus", "$progress"), "userId=$userId AND achievementId=$achievementId");
	}

	function checkAchievementStatusForUser($userId, $achievementId)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		$check = $usfTableOperations->CheckFieldsExistence("userAchievements", array("userId", "achievementId"), array($userId, $achievementId));
		if (!$check) {
			$insert = $usfTableOperations->TryInsertToTable("userAchievements", array("userId", "achievementId", "progress", "status"), array($userId, $achievementId, 0, 0));
			if (is_numeric($insert)) {
				return $usfTableOperations->TryGetAllRowsFromTable("userAchievements", "userId=$userId AND achievementId=$achievementId");
			} else
				return $usfConnectionOperations->UsfLogger(997);
		} else {
			return $usfTableOperations->TryGetAllRowsFromTable("userAchievements", "userId=$userId AND achievementId=$achievementId");
		}
	}
	function retrieveAchievementsList()
	{
		global $usfTableOperations;
		return $usfTableOperations->TryGetAllRowsFromTable("achievements");
	}
	function addAchievement($achievementName, $description, $shortDescription, $requiredPoints)
	{
		global $usfTableOperations;
		$result = $usfTableOperations->TryInsertToTable("achievements", array("achievementName", "description", "shortDescription", "requiredPoints", "status"), array($achievementName, $description, $shortDescription, $requiredPoints, 1));
		return $result;
	}
	function removeAchievement($achievementId)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		if (isset($_POST["achievementId"]))
			$result = $usfTableOperations->TryDeleteFromTable("achievements", "id='$achievementId'");
		else
			$result = $usfTableOperations->TryDeleteFromTable("achievements", "achievementName='$achievementId'");
		return $result;
	}
}

?>