<?php

namespace USF;

include "USF.ConnectionOperations.php";
include "USF.TableOperations.php";
include "USF.FriendshipOperations.php";
include "USF.AchievementOperations.php";
include "USF.Encryption.php";
include "USF.AccountOperations.php";
include "USF.Utilities.php";
include "USF.MessageOperations.php";
include "USF.InventoryOperations.php";

global $usfConnectionOperations;
$usfConnectionOperations = new USFConnectionOperations();
global $usfTableOperations;
$usfTableOperations = new USFTableOperations();
global $usfFriendshipOperations;
$usfFriendshipOperations = new USFFriendshipOperations();
global $usfAchievementOperations;
$usfAchievementOperations = new USFAchievementOperations();
global $usfEncrytpion;
$usfEncrytpion = new USFEncryption();
global $usfUtilities;
$usfUtilities = new USFUtilities();
global $usfAccountOperations;
$usfAccountOperations = new USFAccountOperations();
global $usfMessageOperations;
$usfMessageOperations = new USFMessageOperations();
global $usfInventoryOperations;
$usfInventoryOperations = new USFInventoryOperations();

class USFWrapper
{
	function main()
	{
		global $usfEncrytpion;
		global $usfTableOperations;
		global $usfConnectionOperations;
		global $usfUtilities;
		global $decodedUserData;
		global $usfAccountOperations;
		global $usfFriendshipOperations;
		global $usfAchievementOperations;
		global $usfMessageOperations;
		global $usfInventoryOperations;
		if (isset($_POST["route"])) {
			$initializeResult = $this->initializeConnection();
			$token = null;
			if (isset($_POST["token"]) && isset($_POST["userData"])) {
				$token = $usfAccountOperations->checkToken($decodedUserData->ID, $usfEncrytpion->decrypt($_POST["token"]));
			}
			switch ($usfEncrytpion->decrypt($_POST["route"])) {
				case "InitialRequest":
					return $initializeResult;
				case "LoginRequest":
					return $usfAccountOperations->login();
				case "DeleteAccountRequest":
					return $usfAccountOperations->deleteUser();
				case "RegisterRequest":
					return $usfAccountOperations->register();
				case "HeartbeatRequest":
					return $usfAccountOperations->heartbeat();
				case "UpdateUserRequest":
					if ($token != true)
						return $token;
					return $usfAccountOperations->updateUser();
				case "ContactGetterRequest":
					if ($token != true)
						return $token;
					$usfFriendshipOperations->getContacts($usfEncrytpion->decrypt($_POST["forUserId"]), $usfEncrytpion->decrypt($_POST["page"]), $usfEncrytpion->decrypt($_POST["limit"]));
				case "ContactStatusChangerRequest":
					if ($token != true)
						return $token;
					return $usfFriendshipOperations->changeFriendshipRequestStatus($usfEncrytpion->decrypt($_POST["fromUserId"]), $usfEncrytpion->decrypt($_POST["toUserId"]), $usfEncrytpion->decrypt($_POST["toStatus"]));
				case "MySentRequestsGetterRequest":
					if ($token != true)
						return $token;
					return $usfFriendshipOperations->getMyRequests($decodedUserData->ID, $usfEncrytpion->decrypt($_POST["page"]), $usfEncrytpion->decrypt($_POST["limit"]));
				case "MyRequestsGetterRequest":
					if ($token != true)
						return $token;
					return $usfFriendshipOperations->getRequestsForMe($decodedUserData->ID, $usfEncrytpion->decrypt($_POST["page"]), $usfEncrytpion->decrypt($_POST["limit"]));
				case "ChangeAchievementStatusForUserRequest":
					if ($token != true)
						return $token;
					return $usfAchievementOperations->changeAchievementStatusForUser($usfEncrytpion->decrypt($_POST["forUserId"]), $usfEncrytpion->decrypt($_POST["achievementId"]), $usfEncrytpion->decrypt($_POST["progress"]), $usfEncrytpion->decrypt($_POST["status"]));
				case "CheckAchievementStatusForUserRequest":
					if ($token != true)
						return $token;
					$result = $usfAchievementOperations->checkAchievementStatusForUser($usfEncrytpion->decrypt($_POST["forUserId"]), $usfEncrytpion->decrypt($_POST["achievementId"]));
					if (is_array($result))
						return $result;
					else
						return $usfConnectionOperations->UsfLogger(997) . $result;
				case "GetAchievementsListRequest":
					if ($token != true)
						return $token;
					$result = $usfAchievementOperations->retrieveAchievementsList();
					if (is_array($result))
						return $result;
					else
						return $usfConnectionOperations->UsfLogger(997) . $result;
				case "AddAchievementRequest":
					if ($token != true)
						return $token;
					return $usfAchievementOperations->addAchievement($usfEncrytpion->decrypt($_POST["achievementName"]), $usfEncrytpion->decrypt($_POST["description"]), $usfEncrytpion->decrypt($_POST["shortDescription"]), $usfEncrytpion->decrypt($_POST["requiredPoints"]));
				case "RemoveAchievementRequest":
					if ($token != true)
						return $token;
					$achiev = null;
					if (isset($_POST["achievementId"]))
						$achiev = $usfEncrytpion->decrypt($_POST["achievementId"]);
					else
						$achiev = $usfEncrytpion->decrypt($_POST["achievementName"]);
					return $usfAchievementOperations->removeAchievement($achiev);
				case "GetMessagesRequest":
					if ($token != true)
						return $token;
					$result = $usfMessageOperations->getMessages($usfEncrytpion->decrypt($_POST["forUserId"]), $usfEncrytpion->decrypt($_POST["page"]), $usfEncrytpion->decrypt($_POST["limit"]));
					if (is_array($result))
						return $result;
					else
						return $usfConnectionOperations->UsfLogger(997) . $result;
				case "SendMessageRequest":
					if ($token != true)
						return $token;
					$result = $usfMessageOperations->sendMessage($usfEncrytpion->decrypt($_POST["fromUserId"]), $usfEncrytpion->decrypt($_POST["toUserId"]), $usfEncrytpion->decrypt($_POST["message"]));
					if (is_array($result))
						return $result;
					else
						return $usfConnectionOperations->UsfLogger(997) . $result;
				case "ReadMessageRequest":
					if ($token != true)
						return $token;
					return $usfMessageOperations->readMessage($usfEncrytpion->decrypt($_POST["messageId"]));
				case "DeleteMessageRequest":
					if ($token != true)
						return $token;
					return $usfMessageOperations->deleteMessage($usfEncrytpion->decrypt($_POST["messageId"]));
				case "GetMessagesWithUserRequest":
					if ($token != true)
						return $token;
					$usfMessageOperations->getMessagesWithUser($usfEncrytpion->decrypt($_POST["fromUserId"]), $usfEncrytpion->decrypt($_POST["toUserId"]), $usfEncrytpion->decrypt($_POST["page"]), $usfEncrytpion->decrypt($_POST["limit"]));
				case "AddItemToUserRequest":
					if ($token != true)
						return $token;
					return $usfInventoryOperations->addItemToUser($usfEncrytpion->decrypt($_POST["forUserId"]), $usfEncrytpion->decrypt($_POST["itemId"]), $usfEncrytpion->decrypt($_POST["quantity"]));
				case "RemoveItemFromUserRequest":
					if ($token != true)
						return $token;
					return $usfInventoryOperations->removeItemFromUser($usfEncrytpion->decrypt($_POST["forUserId"]), $usfEncrytpion->decrypt($_POST["itemId"]), $usfEncrytpion->decrypt($_POST["quantity"]));
				case "GetUserItemsRequest":
					if ($token != true)
						return $token;
					return $usfInventoryOperations->getUserInventory($usfEncrytpion->decrypt($_POST["forUserId"]), $usfEncrytpion->decrypt($_POST["page"]), $usfEncrytpion->decrypt($_POST["limit"]));
				case "CheckIfUserHasItemRequest":
					if ($token != true)
						return $token;
					return $usfInventoryOperations->checkIfUserHasItem($usfEncrytpion->decrypt($_POST["forUserId"]), $usfEncrytpion->decrypt($_POST["itemId"]));
				case "GetItemsRequest":
					if ($token != true)
						return $token;
					$result = $usfInventoryOperations->getItemsList();
					case "GetTimeRequest":
						if ($token != true)
							return $token;
						$result = [
							'Time' => date("Y-m-d H:i:s")
						];
				default:
					return "yeah, right 1";
			}
		} else
			return "yeah, right 2";
	}
	function initializeConnection()
	{
		global $usfEncrytpion;
		global $usfTableOperations;
		global $usfConnectionOperations;
		try {
			$result = "";
			$usfTableOperations->usfConnectionOperations = $usfConnectionOperations;
			$setDbConnectionResult = $usfConnectionOperations->SetDbConnection("localhost", "root", "GZEqmfz8v890");
			$result = $selectDatabaseResult = $usfConnectionOperations->SelectDatabase($usfEncrytpion->decrypt($_POST["dbName"]), $usfEncrytpion->decrypt($_POST["dbEnv"]), $usfEncrytpion->decrypt($_POST["dbVer"]));
			if (\strpos($selectDatabaseResult, "[3]")) {
				$result = $usfConnectionOperations->UsfLogger(7) . " :" . $usfConnectionOperations->connection->exec(file_get_contents("DefaultDB.sql"));
			} else {
				$rowCount = $usfTableOperations->GetTableNames()->rowCount();
				if ($rowCount < 3) {
					$result = $usfConnectionOperations->UsfLogger(7) . " :" . $usfConnectionOperations->connection->exec(file_get_contents("DefaultDB.sql"));
				} else {
					$result = $usfConnectionOperations->UsfLogger(6) . " Table count:$rowCount";
				}
			}
			return $result;
		} catch (\Exception $e) {
			return $usfConnectionOperations->UsfLogger(999) . $e;
		}
	}
}

$usfWrapper = new USFWrapper;
//test
if (isset($_GET["test"])) {
	$_POST["route"] = $usfEncrytpion->encrypt("InitialRequest");
	$_POST["dbName"] = $usfEncrytpion->encrypt("WebDB");
	$_POST["dbEnv"] = $usfEncrytpion->encrypt("WebTestEnv");
	$_POST["dbVer"] = $usfEncrytpion->encrypt("1");
	$_POST["token"] = $usfEncrytpion->encrypt("asd");
	$_POST["fromUserId"] = $usfEncrytpion->encrypt("8");
	$_POST["toUserId"] = $usfEncrytpion->encrypt("9");
	$_POST["forUserId"] = $usfEncrytpion->encrypt("8");
	$_POST["limit"] = $usfEncrytpion->encrypt("5000");
	$_POST["page"] = $usfEncrytpion->encrypt("0");
	$_POST["toStatus"] = $usfEncrytpion->encrypt("2");
	$_POST["userData"] = $usfEncrytpion->encrypt('{
		"IsLoggedIn":false,
		"IsGuestAccount":false,
		"CustomUserData":[
		   {
			  "field":0,
			  "fieldName":"testInteger",
			  "FieldValue":null
		   },
		   {
			  "field":1,
			  "fieldName":"testString",
			  "FieldValue":null
		   }
		],
		"ID":-1,
		"UserName":"Guest-2bro6ttl",
		"Email":"Guest-8ua6ftse",
		"Password":"ik3/d1eNnSkmquMl7Z+7fDfNP56s8RjJ3Bql9kjnhlw=",
		"FirstName":null,
		"MiddleName":null,
		"LastName":null,
		"Phone":null,
		"CoinBalance":0,
		"ClanId":-1,
		"LocalTimeZone":"(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna",
		"LocalTimeZoneOffset":3600000.0,
		"Language":null,
		"Created":"2023-01-16 00:57:38",
		"LastUpdated":"2023-01-16 00:57:38",
		"Status":-1,
		"UserToken":null,
		"UpdateRequest":{
		   "IsSet":false,
		   "Token":"",
		   "ConnectionStartTime":"0001-01-01 00:00:00",
		   "ConnectionFinishTime":"0001-01-01 00:00:00",
		   "ConnectionEndpoint":null,
		   "ConnectionTotalTime":0.0,
		   "ConnectionParameters":null,
		   "Configuration":null,
		   "ConnectionResultText":null,
		   "ConnectionResponseHeaders":null
		}
	 }');
}
//
global $decodedUserData;
if (isset($_POST["userData"]))
	$decodedUserData = json_decode($usfEncrytpion->decrypt($_POST["userData"]));

$result = $usfWrapper->main();
if (isset($_GET["test"])) {
	if (is_array($result)) {
		echo "isarray";
		if (array_key_exists('inserted', $result)) {
			echo "exists";
		}
	}
	echo json_encode($result);
	die;
}

if (is_array($result)) {
	if (array_key_exists('inserted', $result)) {
		if(array_key_exists('userData', $result))
		$result['inserted'] .= "|" . $result['userData'];
		$encryptedResult = $usfEncrytpion->encrypt($result['inserted']);
		$usfUtilities->returnHeaders(
			array(
				'RequestResult: ' => $encryptedResult,
				'userToken: ' => $result['token']
			)
		);
	} else {
		$encryptedResult = $usfEncrytpion->encrypt(json_encode($result));
		$usfUtilities->returnHeaders(
			array(
				'RequestResult: ' => $encryptedResult,
			)
		);
	}
} else {
	$encryptedResult = $usfEncrytpion->encrypt($result);
	$usfUtilities->returnHeaders(
		array(
			'RequestResult: ' => $encryptedResult,
		)
	);
}
?>