<?php
namespace USF;

global $usfConnectionOperations;
global $usfTableOperations;
global $usfEncrytpion;
global $usfUtilities;
global $decodedUserData;

class USFInventoryOperations
{
	function addItemToUser($user, $temId, $quantity)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		$check = $usfTableOperations->TryGetAllRowDataFromTable("userItems", "userId=$user && itemId=$temId AND status >= 1");
		if (is_array($check)) {
			$quantity = $check["quantity"] + $quantity;
			return $usfConnectionOperations->UsfLogger(0) . $usfTableOperations->TryUpdateTable("userItems", array("quantity"), array($quantity));
		} else {
			return $usfConnectionOperations->UsfLogger(0) . $usfTableOperations->TryInsertToTable("userItems", array("userId", "itemId", "quantity", "status"), array($user, $temId, $quantity, "1"));
		}
	}
	function checkIfUserHasItem($user, $temId, $quantity)
	{
		global $usfTableOperations;
		return $usfTableOperations->TryGetAllRowsFromTable("userItems", "userId=$user && itemId=$temId AND status >= 1 AND quantity >= $quantity");
	}
	function removeItemFromUser($user, $temId)
	{
		global $usfTableOperations;
		global $usfConnectionOperations;
		return $usfConnectionOperations->UsfLogger(0) . $usfTableOperations->TryDeleteFromTable("userItems", "userId=$user");
	}
	function getUserInventory($user, $page, $limit)
	{
		global $usfTableOperations;
		$offset = $page * $limit;
		return $usfTableOperations->TryGetAllRowsFromTable("userItems", "userId=$user AND status >= 1 LIMIT $limit OFFSET $offset");
	}
	function getAllItems( $page, $limit)
	{
		global $usfTableOperations;
		$offset = $page * $limit;
		return $usfTableOperations->TryGetAllRowsFromTable("items", "status >= 1 LIMIT $limit OFFSET $offset");
	}
}

?>