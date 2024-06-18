<?php
namespace USF;
global $usfConnectionOperations;
global $usfTableOperations;
global $usfEncrytpion;
global $usfUtilities;
global $decodedUserData;

class USFAccountOperations
{
	function deleteUser()
	{
		global $usfTableOperations;
		global $decodedUserData;
		return $dbData = $usfTableOperations->TryDeleteFromTable("users", "userName='$decodedUserData->UserName'");
	}
	function login()
	{
		global $usfEncrytpion;
		global $usfTableOperations;
		global $usfConnectionOperations;
		global $usfUtilities;
		global $decodedUserData;
		$dbData = $usfTableOperations->TryGetAllRowDataFromTable("users", "userName='".$decodedUserData->UserName."'");
		$passAndSalt = $usfUtilities->createSaltedPasswordHash($decodedUserData->Password);
		if ($usfUtilities->checkPassword($decodedUserData->Password, $dbData["password"], $dbData["salt"])) {
			$res = [
				'inserted' => ($usfConnectionOperations->UsfLogger(0) . $dbData["id"]), 
				'token' => $this->handleToken($dbData["id"], $usfEncrytpion->decrypt($_POST["token"])),
				'userData' => json_encode($dbData)
			];
			return $res;
		} else {
			return $usfConnectionOperations->UsfLogger(17);
		}
	}

	function updateUser()
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		global $usfUtilities;
		global $decodedUserData;
		$columns = array(
			"userName",
			"email",
			"googleId",
			"facebookId",
			"firstName",
			"middleName",
			"lastName",
			"phone",
			"coinBalance",
			"clanId",
			"localTimeZone",
			"localTimeZoneOffset",
			"language",
			"userToken",
			"status"
		);
		$values = array(
			$decodedUserData->UserName,
			$decodedUserData->Email,
			"undefined",
			"undefined",
			$decodedUserData->FirstName,
			$decodedUserData->MiddleName,
			$decodedUserData->LastName,
			$decodedUserData->Phone,
			$decodedUserData->CoinBalance,
			$decodedUserData->ClanId,
			$decodedUserData->LocalTimeZone,
			$decodedUserData->LocalTimeZoneOffset,
			$decodedUserData->Language,
			$decodedUserData->UserToken,
			$decodedUserData->Status
		);
		for ($i = 0; $i < count($decodedUserData->CustomUserData); $i++) {
			$val = $decodedUserData->CustomUserData[$i]->FieldValueString;
			if (strlen($val) == 0)
				$val = "-99";
			$columns[] = $decodedUserData->CustomUserData[$i]->fieldName;
			$values[] = $val;
		}
		$updateResult = $usfTableOperations->TryUpdateTable("users", $columns, $values, "userName='".$decodedUserData->UserName."'");
		return ($usfConnectionOperations->UsfLogger(0) . $updateResult);
	}
	function heartbeat()
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		global $decodedUserData;
		$token = $decodedUserData->UserToken;
		$dbData = $usfTableOperations->TryGetAllRowDataFromTable("users", "userName='".$decodedUserData->UserName."'");
		if ($token == $dbData["userToken"]) {
			$res = [
				'inserted' => ($usfConnectionOperations->UsfLogger(0) . $dbData["id"]), 
				'token' => $token,
				'userData' => json_encode($dbData)
			];
			return $res;
		} else {
			return $usfConnectionOperations->UsfLogger(18);
		}
	}

	function register()
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		global $usfUtilities;
		global $decodedUserData;
		$dbData = $usfTableOperations->TryGetAllRowDataFromTable("users", "userName='".$decodedUserData->UserName."'");
		if(!\strpos($dbData, "[16]") && !\strpos($dbData, "[15]"))
		{
			$loginResult = $this->login();
			if(\strpos($loginResult, "[0]"))
			{
				return $loginResult;
			}
			else
			{
				return $usfConnectionOperations->UsfLogger(23);
			}
		}
		$columns = array(
			"userName",
			"email",
			"password",
			"salt",
			"googleId",
			"facebookId",
			"firstName",
			"middleName",
			"lastName",
			"phone",
			"coinBalance",
			"clanId",
			"localTimeZone",
			"localTimeZoneOffset",
			"language",
			"userToken",
			"created",
			"lastUpdated",
			"status"
		);
		for ($i = 0; $i < count($decodedUserData->CustomUserData); $i++) {
			$columns[] = $decodedUserData->CustomUserData[$i]->fieldName;
		}
		$columnTypes = array(
			"VARCHAR(255)",
			"VARCHAR(255)",
			"VARCHAR(255)",
			"VARCHAR(255)",
			"VARCHAR(255)",
			"VARCHAR(255)",
			"VARCHAR(255)",
			"VARCHAR(255)",
			"VARCHAR(255)",
			"VARCHAR(255)",
			"INT(255)",
			"INT(255)",
			"VARCHAR(255)",
			"INT(255)",
			"VARCHAR(255)",
			"VARCHAR(255)",
			"TIMESTAMP(6)",
			"TIMESTAMP(6)",
			"INT(3)"
		);
		for ($i = 0; $i < count($decodedUserData->CustomUserData); $i++) {
			switch ($decodedUserData->CustomUserData[$i]->field) {
				case 0:
					$columnTypes[] = "INT(255)";
					break;
				case 1:
					$columnTypes[] = "VARCHAR(255)";
					break;
				case 2:
					$columnTypes[] = "TIMESTAMP(6)";
					break;
				case 3:
					$columnTypes[] = "INT(1)";
					break;
			}

		}
		$passAndSalt = $usfUtilities->createSaltedPasswordHash($decodedUserData->Password);
		$values = array(
			$decodedUserData->UserName,
			$decodedUserData->Email,
			$passAndSalt[0],
			$passAndSalt[1],
			"undefined",
			"undefined",
			$decodedUserData->FirstName,
			$decodedUserData->MiddleName,
			$decodedUserData->LastName,
			$decodedUserData->Phone,
			$decodedUserData->CoinBalance,
			$decodedUserData->ClanId,
			$decodedUserData->LocalTimeZone,
			$decodedUserData->LocalTimeZoneOffset,
			$decodedUserData->Language,
			$decodedUserData->UserToken,
			$decodedUserData->Created,
			$decodedUserData->LastUpdated,
			$decodedUserData->Status
		);
		for ($i = 0; $i < count($decodedUserData->CustomUserData); $i++) {
			$val = $decodedUserData->CustomUserData[$i]->FieldValue;
			if (strlen($val) == 0)
				$val = "-99";
			$values[] = $val;
		}
		$insertResult = $usfTableOperations->TryInsertToTableWithColumnTypes("users", $columns, $columnTypes, $values);
		$res = [
			'inserted' => ($usfConnectionOperations->UsfLogger(0) . $insertResult),
			'token' => $this->handleToken($insertResult, "newToken"),
			'userData' => json_encode($usfTableOperations->TryGetAllRowDataFromTable("users", "userName='".$decodedUserData->UserName."'"))
		];
		return $res;
	}

	function handleToken(int $id, string $compareToken)
	{
		global $usfTableOperations;
		global $usfUtilities;
		$currentToken = $usfTableOperations->TryGetRowFromTable("users", array("userToken"), "id=" . $id);
		if ($currentToken != $compareToken) {
			$token = $usfUtilities->guidv4();
			$usfTableOperations->TryUpdateTable("users", array("userToken"), array($token), "id=" . $id);
			return $token;
		} else {
			return $currentToken;
		}
	}

	function checkToken(int $id, string $compareToken)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		$currentToken = $usfTableOperations->TryGetRowFromTable("users", array("userToken"), "id=" . $id);
		if ($currentToken != $compareToken) {
			return $usfConnectionOperations->UsfLogger(20);
		} else {
			return true;
		}
	}
}
?>