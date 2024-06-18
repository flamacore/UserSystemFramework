<?php
namespace USF;

//Includes
include "USF.ErrorCodes.php";

//Uses
use PDO;

//Globals
$GLOBALS["enableBackTrace"] = true;

///Class
class USFConnectionOperations
{
    public $connection;
    ///Function that sets up the DB connection. Returns a status.
    public function SetDbConnection(string $databaseAddress, string $databaseUser, string $databasePassword): string
    {
        try {
            $this->connection = new PDO('mysql:host=' . $databaseAddress . ';charset=utf8', $databaseUser, $databasePassword, array(PDO::ATTR_EMULATE_PREPARES => false, PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION));
            $this->connection->beginTransaction();
            $this->connection->exec("SET time_zone='+00:00';");
            $GLOBALS["dbUser"] = $databaseUser;
            $this->connection->commit();
            return $this->UsfLogger(0);
        } catch (\Exception $ex) {
            return $this->UsfLogger(1) . ":" . $ex;
        }
    }
    ///Function that selects the database we're working on. Mainly for environments.
    public function SelectDatabase(string $databaseName, string $environment, string $version)
    {
        $db = $databaseName . "_" . $environment . "_" . $version;
        try {
            $this->connection->exec("use " . $db);
            return $this->UsfLogger(0);
        } catch (\Exception $ex) {
            $message = $this->UsfLogger(3);
            try {
                $this->connection->exec("CREATE DATABASE ".$db."; GRANT ALL ON ".$db.".* TO ".$GLOBALS["dbUser"]."@'localhost'; FLUSH PRIVILEGES;");
                $this->connection->exec("use " . $db);
                return $message . "\n" . $this->UsfLogger(0);
            } catch (\Exception $th) {
                return $this->UsfLogger(2) . ":" . $ex;
            }
        }
    }

    public function DropDatabase(string $databaseName, string $environment,  string $version)
    {
        $db = $databaseName . "_" . $environment . "_" . $version;
        try {
            $this->connection->exec("DROP DATABASE [IF EXISTS] " . $db);
            return $this->UsfLogger(0);
        } catch (\Exception $ex) {
            return $this->UsfLogger(5) . $ex;
        }
    }


    public function UsfLogger(int $no)
    {
        $r = "Status[" . $no . "] : ";
        if ($GLOBALS["enableBackTrace"] == true) {
            $r .= "(" . debug_backtrace(DEBUG_BACKTRACE_IGNORE_ARGS, 2)[1]['function'] . "):";
        }
        return $r . $GLOBALS["errorCodes"][$no];
    }
}

?>