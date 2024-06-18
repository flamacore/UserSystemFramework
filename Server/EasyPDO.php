<?php
function TryInsertToTable($TableName, $columns, $values, $whereClause = null)
{
    global $conn;
    $conn->beginTransaction();
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
        return co(0) . $InsertedId;
    } catch (PDOException $ex) {
        $conn->rollBack();
        return co(1) . $ex->getMessage();
    }
}

function TryUpdateTable($TableName, $columns, $values, $whereClause = null)
{
    global $conn;
    $conn->beginTransaction();
    try {
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
        echo $sql;
        print_r($arr);
        $result = $conn->prepare($sql);
        $result->execute($arr);
        $InsertedId = $conn->lastInsertId();
        $conn->commit();
        return co(0) . $InsertedId;
    } catch (PDOException $ex) {
        $conn->rollBack();
        return co(2) . $ex->getMessage(); // . $sql;
    }
}

function TryDeleteFromTable($TableName, $whereClause)
{
    global $conn;
    $conn->beginTransaction();
    try {
        $sql = "DELETE FROM " . $TableName . " WHERE " . $whereClause;
        echo $sql;
        $result = $conn->prepare($sql);
        $result->execute();
        $conn->commit();
        return co(0);
    } catch (PDOException $ex) {
        $conn->rollBack();
        return co(2) . $ex->getMessage(); // . $sql;
    }
}

function TryGetDataFromTable($TableName, $column, $whereClause = null)
{
    global $conn;
    $conn->beginTransaction();
    try {
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
            return co(3) . "No information found.";
        }
        $conn->commit();
        return $res;
    } catch (PDOException $ex) {
        $conn->rollBack();
        return co(4) . $ex->getMessage(); // . $sql;
    }
}

function TryGetCountFromTable($TableName, $column, $whereClause = null)
{
    global $conn;
    $conn->beginTransaction();
    try {
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
            return co(3) . "No information found.";
        }
        $conn->commit();
        return $rowCount;
    } catch (PDOException $ex) {
        $conn->rollBack();
        return co(4) . $ex->getMessage(); // . $sql;
    }
}

function TryGetAllRowDataFromTable($TableName, $whereClause = null)
{
    global $conn;
    $conn->beginTransaction();
    try {
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
            return co(5) . "No information found.";
        }
        $conn->commit();
        return $rows;
    } catch (PDOException $ex) {
        $conn->rollBack();
        return co(6) . $ex->getMessage(); // . $sql;
    }
}

function TryGetEntireDataFromTable($TableName, $whereClause = null)
{
    global $conn;
    $conn->beginTransaction();
    try {
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
            return co(5) . "No information found.";
        }
        $conn->commit();
        return $rows;
    } catch (PDOException $ex) {
        $conn->rollBack();
        return co(6) . $ex->getMessage(); // . $sql;
    }
}

function CheckFieldExistence($TableName, $field, $value)
{
    global $conn;
    $conn->beginTransaction();
    try {
        $sql = "SELECT * FROM " . $TableName . " Where " . $field . " = ?";
        $result = $conn->prepare($sql);
        $result->execute(array($value));
        $rows = $result->rowCount();
        $conn->commit();
        if ($rows > 0)
            return true;
        else
            return false;
    } catch (PDOException $ex) {
        $conn->rollBack();
        return co(10) . $ex->getMessage();
    }
}

function GetColumnNames($TableName)
{
    global $conn;
    $conn->beginTransaction();
    $sql = "SELECT * FROM " . $TableName . " LIMIT 1";
    $result = $conn->prepare($sql);
    try {
        $result->execute();
        return array_keys($result->fetch(PDO::FETCH_ASSOC));
    }
    catch (Exception $e)
    {
        return co(55) . $e->getMessage(); //return exception
    }
}

function GetTableNames()
{
    global $conn;
    $conn->beginTransaction();
    $sql = 'SHOW TABLES';
    $result = $conn->prepare($sql);
    try {
        $result->execute();
        return $result->fetchAll(PDO::FETCH_COLUMN);
    }
    catch (Exception $e)
    {
        return co(56) . $e->getMessage(); //return exception
    }
}

function co($no)
{
    return "Status[" . $no . "] by(" . debug_backtrace(DEBUG_BACKTRACE_IGNORE_ARGS, 2)[1]['function'] . "):";
}
function addlog($l, $i)
{
    $log = "User: " . $_SERVER['REMOTE_ADDR'] . ' - ' . date("F j, Y, g:i a") . PHP_EOL .
        "Attempt: " . ($i) . PHP_EOL .
        "Result: " . ($l) . PHP_EOL .
        "-------------------------" . PHP_EOL;
    file_put_contents('./logs/log_' . date("j.n.Y") . '.txt', $log, FILE_APPEND);
    $tryLog = TryInsertToTable("actionlog", array("ipAddress", "userAgent", "log"), array($_SERVER['REMOTE_ADDR'], $_SERVER['HTTP_USER_AGENT'], $log));
}
