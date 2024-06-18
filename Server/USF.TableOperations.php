<?php
namespace USF;

use PDO;

class USFTableOperations
{
	public USFConnectionOperations $usfConnectionOperations;
	function GetTableNames()
	{
		$conn = $this->usfConnectionOperations->connection;
		$conn->beginTransaction();
		$sql = 'SHOW TABLES';
		$result = $conn->prepare($sql);
		try {
			$result->execute();
			$conn->commit();
			return $result;
		} catch (\Exception $e) {
			$conn->commit();
			return $this->usfConnectionOperations->UsfLogger(0) . $e->getMessage(); //return exception
		}
	}

	function TryInsertToTable(string $TableName, array $columns, array $values, string $whereClause = null)
	{
		$conn = $this->usfConnectionOperations->connection;
		try {
			if (!$conn->inTransaction())
				$conn->beginTransaction();
			for ($c = 0; $c < count($columns); $c++) {
				if ($this->CheckColumnExistence($TableName, $columns[$c]) == false) {
					$sql = "ALTER TABLE $TableName ADD COLUMN IF NOT EXISTS $columns[$c] VARCHAR(255) NULL";
					$result = $conn->prepare($sql);
					$result->execute() . " " . $this->usfConnectionOperations->UsfLogger(998);
				}
			}
			$sql = "INSERT INTO " . $TableName . " ";
			if ($columns != null) {
				$sql .= "(";
				for ($i2 = 0; $i2 < count($columns); $i2++) {
					if ($i2 < count($columns) - 1)
						$sql .= $columns[$i2] . ", ";
					if ($i2 == count($columns) - 1)
						$sql .= $columns[$i2];
				}
				$sql .= ") ";
			}
			$sql .= "VALUES(";
			$insertMarks = "";
			for ($i = 0; $i < count($values); $i++) {
				if ($i < count($values) - 1)
					$insertMarks .= "?,";
				if ($i == count($values) - 1)
					$insertMarks .= "?)";
			}
			$arr = array();
			$keys = array_keys($values);
			for ($i = 0; $i < count($values); $i++) {
				$arr[] = "" . $values[$keys[$i]] . "";
			}
			$sql .= $insertMarks;
			if ($whereClause != null) {
				$sql .= " WHERE " . $whereClause;
			}
			$result = $conn->prepare($sql);
			$result->execute($arr);
			$InsertedId = $conn->lastInsertId();
			$conn->commit();
			return $InsertedId;
		} catch (\PDOException $ex) {
			return $this->usfConnectionOperations->UsfLogger(8) . $ex->getMessage();
		}
	}

	function TryInsertToTableWithColumnTypes(string $TableName, array $columns, array $columnTypes, array $values, string $whereClause = null)
	{
		$conn = $this->usfConnectionOperations->connection;
		if (!$conn->inTransaction())
			$conn->beginTransaction();
		try {
			for ($c = 0; $c < count($columns); $c++) {
				if ($this->CheckColumnExistence($TableName, $columns[$c]) == false) {
					$sql = "ALTER TABLE $TableName ADD COLUMN IF NOT EXISTS $columns[$c] $columnTypes[$c] NULL";
					$result = $conn->prepare($sql);
					$result->execute() . " " . $this->usfConnectionOperations->UsfLogger(998);
				}
			}
		} catch (\PDOException $ex) {
			$conn->commit();
			return $this->usfConnectionOperations->UsfLogger(8) . $ex->getMessage();
		}
		try {
			$sql = "INSERT INTO " . $TableName . " ";
			if ($columns != null) {
				$sql .= "(";
				for ($i2 = 0; $i2 < count($columns); $i2++) {
					if ($i2 < count($columns) - 1)
						$sql .= $columns[$i2] . ", ";
					if ($i2 == count($columns) - 1)
						$sql .= $columns[$i2];
				}
				$sql .= ") ";
			}
			$sql .= "VALUES(";
			$insertMarks = "";
			for ($i = 0; $i < count($values); $i++) {
				if ($i < count($values) - 1)
					$insertMarks .= "?,";
				if ($i == count($values) - 1)
					$insertMarks .= "?)";
			}
			$arr = array();
			$keys = array_keys($values);
			for ($i = 0; $i < count($values); $i++) {
				$arr[] = "" . $values[$keys[$i]] . "";
			}
			$sql .= $insertMarks;
			if ($whereClause != null) {
				$sql .= " WHERE " . $whereClause;
			}
			$result = $conn->prepare($sql);
			$result->execute($arr);
			$InsertedId = $conn->lastInsertId();
			$conn->commit();
			return $InsertedId;
		} catch (\PDOException $ex) {
			$conn->commit();
			return $this->usfConnectionOperations->UsfLogger(8) . $ex->getMessage();
		}
	}

	function TryUpdateTable($TableName, $columns, $values, $whereClause = null)
	{
		$conn = $this->usfConnectionOperations->connection;
		try {
			for ($c = 0; $c < count($columns); $c++) {
				$check = $this->CheckColumnExistence($TableName, $columns[$c]);
				if ($check == false) {
					$sql = "ALTER TABLE $TableName ADD COLUMN IF NOT EXISTS  $columns[$c] VARCHAR(255) NULL";
					$result = $conn->prepare($sql);
					$result->execute() . " " . $this->usfConnectionOperations->UsfLogger(998);
				}
			}
			if (!$conn->inTransaction())
				$conn->beginTransaction();
			$sql = "UPDATE " . $TableName . " SET ";
			$arr = array();
			$keys = array_keys($values);

			if ($columns != null && $values != null) {
				for ($i2 = 0; $i2 < count($columns); $i2++) {
					if ($i2 < count($columns) - 1)
						$sql .= $columns[$i2] . " =?, ";
					if ($i2 == count($columns) - 1)
						$sql .= $columns[$i2] . " =?";
					$arr[] = "" . $values[$keys[$i2]] . "";
				}
			}

			if ($whereClause != null) {
				$sql .= " WHERE " . $whereClause;
			}
			$result = $conn->prepare($sql);
			$result->execute($arr);
			$InsertedId = $conn->lastInsertId();
			$conn->commit();
			return $InsertedId;
		} catch (\PDOException $ex) {
			$conn->commit();
			return $this->usfConnectionOperations->UsfLogger(22) . $ex->getMessage(); // . $sql;
		}
	}

	function TryGetRowFromTable($TableName, $column, $whereClause = null)
	{
		$conn = $this->usfConnectionOperations->connection;
		try {
			if (!$conn->inTransaction())
				$conn->beginTransaction();
			$sql = "SELECT $column FROM " . $TableName;
			if ($whereClause != null) {
				$sql .= " WHERE " . $whereClause;
			}
			$result = $conn->prepare($sql);
			$result->execute();
			$rowCount = $result->rowCount();
			if ($rowCount > 0) {
				$rows = $result->fetch(PDO::FETCH_ASSOC);
				$res = $rows[$column];
			} else {
				$conn->commit();
				return $this->usfConnectionOperations->UsfLogger(13) . "No information found.";
			}
			$conn->commit();
			return $res;
		} catch (\PDOException $ex) {
			return $this->usfConnectionOperations->UsfLogger(13) . $ex->getMessage(); // . $sql;
		}
	}

	function TryGetAllRowDataFromTable($TableName, $whereClause = null)
	{
		$conn = $this->usfConnectionOperations->connection;
		try {
			if (!$conn->inTransaction())
				$conn->beginTransaction();
			$sql = "SELECT * FROM " . $TableName;
			if ($whereClause != null) {
				$sql .= " WHERE " . $whereClause;
			}
			$result = $conn->prepare($sql);
			$result->execute();
			$rowCount = $result->rowCount();
			if ($rowCount > 0) {
				$rows = $result->fetch(PDO::FETCH_ASSOC);
			} else {
				$conn->commit();
				return $this->usfConnectionOperations->UsfLogger(16) . "No information found.";
			}
			$conn->commit();
			return $rows;
		} catch (\PDOException $ex) {
			$conn->rollBack();
			return $this->usfConnectionOperations->UsfLogger(15) . $ex->getMessage();
		}
	}

	function TryGetAllRowsFromTable($TableName, $whereClause = null)
	{
		$conn = $this->usfConnectionOperations->connection;
		try {
			if (!$conn->inTransaction())
				$conn->beginTransaction();
			$sql = "SELECT * FROM " . $TableName;
			if ($whereClause != null) {
				$sql .= " WHERE " . $whereClause;
			}
			$result = $conn->prepare($sql);
			$result->execute();
			$rowCount = $result->rowCount();
			if ($rowCount > 0) {
				$rows = $result->fetchAll(PDO::FETCH_ASSOC);
			} else {
				$conn->commit();
				return $this->usfConnectionOperations->UsfLogger(16) . "No information found.";
			}
			$conn->commit();
			return $rows;
		} catch (\PDOException $ex) {
			$conn->rollBack();
			return $this->usfConnectionOperations->UsfLogger(15) . $ex->getMessage();
		}
	}

	function TryGetCountFromTable($TableName, $column, $whereClause = null)
	{
		$conn = $this->usfConnectionOperations->connection;
		try {
			if (!$conn->inTransaction())
				$conn->beginTransaction();
			$sql = "SELECT COUNT($column) as total FROM " . $TableName;
			if ($whereClause != null) {
				$sql .= " WHERE " . $whereClause;
			}
			$result = $conn->prepare($sql);
			$result->execute();
			$rowCount = $result->rowCount();
			if ($rowCount > 0) {
				$rows = $result->fetch(PDO::FETCH_ASSOC);
				$res = $rows[$column];
			} else {
				$conn->commit();
				return $this->usfConnectionOperations->UsfLogger(13) . "No information found.";
			}
			$conn->commit();
			return $rowCount;
		} catch (\PDOException $ex) {
			$conn->rollBack();
			return co(4) . $ex->getMessage(); // . $sql;
		}
	}

	function TryDeleteFromTable($TableName, $whereClause)
	{
		$conn = $this->usfConnectionOperations->connection;
		try {
			if (!$conn->inTransaction())
				$conn->beginTransaction();
			$sql = "DELETE FROM " . $TableName . " WHERE " . $whereClause;
			$result = $conn->prepare($sql);
			$result->execute();
			$conn->commit();
			return $this->usfConnectionOperations->UsfLogger(0);
		} catch (\PDOException $ex) {
			return $this->usfConnectionOperations->UsfLogger(12) . $ex->getMessage(); // . $sql;
		}
	}

	function CheckColumnExistence($TableName, $field)
	{
		$conn = $this->usfConnectionOperations->connection;
		try {
			$sql = "SHOW COLUMNS FROM $TableName LIKE '?'";
			$result = $conn->prepare($sql);
			$result->execute(array($field));
			$rows = $result->rowCount();
			if ($rows > 0) {
				return true;
			} else {
				return false;
			}
		} catch (\PDOException $ex) {
			return $this->usfConnectionOperations->UsfLogger(9) . $ex->getMessage();
		}
	}

	function CheckFieldsExistence($TableName, $fields, $values) {
		$conn = $this->usfConnectionOperations->connection;
		try {
			$sql = "SELECT COUNT(*) FROM $TableName WHERE ";
			for ($c = 0; $c < count($fields); $c++) {
				$sql .= "$fields[$c] = :value$c";
				if ($c != count($fields) - 1) {
					$sql .= " AND ";
				}
			}
			$result = $conn->prepare($sql);
			for ($c = 0; $c < count($fields); $c++) {
				$result->bindValue(":value$c", $values[$c]);
			}
			$result->execute();
			$rows = $result->fetchColumn();
			if ($rows > 0) {
				return true;
			} else {
				return false;
			}
		} catch (\PDOException $ex) {
			return $this->usfConnectionOperations->UsfLogger(9) . $ex->getMessage();
		}
	}
}

?>