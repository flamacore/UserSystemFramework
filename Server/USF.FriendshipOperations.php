<?php
namespace USF;

global $usfTableOperations;
global $usfEncryption;
global $usfUtilities;
global $usfConnectionOperations;
global $decodedUserData;

class USFFriendshipOperations
{
	function changeFriendshipRequestStatus($fromUser, $toUser, $status)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		$check = $usfTableOperations->CheckFieldsExistence("friends", array("userId", "friendId"), array($fromUser, $toUser));
		if (!$check) {
			return $usfConnectionOperations->UsfLogger(0) . $usfTableOperations->TryInsertToTable("friends", array("userId", "friendId", "relationshipStatus", "status"), array($fromUser, $toUser, $status, "1"));
		}
		return $usfConnectionOperations->UsfLogger(0) . $usfTableOperations->TryUpdateTable("friends", array("relationshipStatus"), array("$status"), "userId=$fromUser AND friendId=$toUser");
	}

	function getContacts($forUser, $page, $limit)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		global $usfEncrytpion;
		global $decodedUserData;
		global $usfUtilities;
		$offset = $page * $limit;
		$allContacts = $usfTableOperations->TryGetAllRowsFromTable("friends", "userId=$forUser LIMIT $limit OFFSET $offset");
		$allOtherContacts = $usfTableOperations->TryGetAllRowsFromTable("friends", "friendId=$forUser LIMIT $limit OFFSET $offset");
		$ids = [];
		foreach ($allContacts as $row) {
			$ids[] = $row["friendId"];
		}
		foreach ($allOtherContacts as $row) {
			$ids[] = $row["userId"];
			$allContacts[] = $row;
		}
		$usersFromContacts = $usfTableOperations->TryGetAllRowsFromTable("users", "id IN(" . \implode(",", $ids) . ")");
		$RequestedUsers = $usfEncrytpion->encrypt(json_encode($usersFromContacts));
		$RequestedStatuses = $usfEncrytpion->encrypt(json_encode($allContacts));
		$usfUtilities->returnHeaders(
			array(
				'RequestResultUsers: ' => $RequestedUsers,
				'RequestResultContactData: ' => $RequestedStatuses,
				'RequestResult: ' => $usfConnectionOperations->UsfLogger(0),
			)
		);
		exit;
	}

	function getMyRequests($forUser, $page, $limit)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		$offset = $page * $limit;
		return $usfTableOperations->TryGetAllRowsFromTable("friends", "userId=$forUser AND relationshipStatus = 1 LIMIT $limit OFFSET $offset");
	}

	function getRequestsForMe($forUser, $page, $limit)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		$offset = $page * $limit;
		return $usfTableOperations->TryGetAllRowsFromTable("friends", "friendId=$forUser AND relationshipStatus = 1 LIMIT $limit OFFSET $offset");
	}
}

?>