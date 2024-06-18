<?php
namespace USF;

global $usfConnectionOperations;
global $usfTableOperations;
global $usfEncrytpion;
global $usfUtilities;
global $decodedUserData;

class USFMessageOperations
{
	function sendMessage($fromUser, $toUser, $message)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		$check = $usfTableOperations->TryGetAllRowDataFromTable("friends", "(userId=$fromUser AND friendId=$toUser) OR (userId=$toUser AND friendId=$fromUser)");
		if (is_array($check)) {
			if ($check["relationshipStatus"] != 3 && $check["relationshipStatus"] != 4) {
				$insert = $usfConnectionOperations->UsfLogger(0) . $usfTableOperations->TryInsertToTable("userMessages", array("fromUser", "toUser", "message", "messageStatus", "status"), array($fromUser, $toUser, $message, "1", "1"));
				return $insert;
			} else {
				return $usfConnectionOperations->UsfLogger(997);
			}
		} else {
			return $check;
		}
	}

	function readMessage($message)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		return $usfConnectionOperations->UsfLogger(0) . $usfTableOperations->TryUpdateTable("userMessages", array("messageStatus"), array("2"));
	}

	function deleteMessage($message)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		return $usfConnectionOperations->UsfLogger(0) . $usfTableOperations->TryDeleteFromTable("userMessages", "message=$message");
	}

	function getMessages($forUser, $page, $limit)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		$offset = $page * $limit;
		return $usfTableOperations->TryGetAllRowsFromTable("userMessages", "fromUser=$forUser OR toUser=$forUser LIMIT $limit OFFSET $offset");
	}

	function getMessagesWithUser($fromUser, $toUser, $page, $limit)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		global $usfUtilities;
		global $usfEncrytpion;
		$offset = $page * $limit;
		$result = $usfTableOperations->TryGetAllRowsFromTable("userMessages", "(fromUser=$fromUser AND toUser=$toUser) OR (fromUser=$toUser AND toUser=$fromUser) LIMIT $limit OFFSET $offset");
		$otherUser = $usfTableOperations->TryGetAllRowDataFromTable("users", "ID=$toUser");
		if (is_array($result)) {
			$usfUtilities->returnHeaders(
				array(
					'RequestResultMessages: ' => $usfEncrytpion->encrypt(json_encode($result)),
					'RequestResultOtherUser: ' => $usfEncrytpion->encrypt(json_encode($otherUser)),
					'RequestResult: ' => $usfConnectionOperations->UsfLogger(0),
				)
			);
		} else {
			$usfUtilities->returnHeaders(
				array(
					'RequestResult: ' => $usfEncrytpion->encrypt($result)
				)
			);
		}
		exit;
	}
}

?>