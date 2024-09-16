<?php

namespace App\Database;

use PDO;

/**
 * MySQL DB connection.
 */
class Connection
{
    private static $instance = null;
    private PDO $pdo;

    private const OPTIONS = [
        PDO::ATTR_ERRMODE               => PDO::ERRMODE_EXCEPTION,
        PDO::ATTR_DEFAULT_FETCH_MODE    => PDO::FETCH_ASSOC
    ];

    /**
     * Create a DB connection
     */
    private function __construct()
    {
        $dsn = "mysql:host={$_ENV['MYSQL_HOST']};dbname={$_ENV['MYSQL_DATABASE']};port={$_ENV['MYSQL_PORT']}";

        $user = $_ENV['MYSQL_USER'];
        $passwword = $_ENV['MYSQL_PASSWORD'];
        $this->pdo = new PDO($dsn, $user, $passwword, self::OPTIONS);
    }

    /**
     * Returns static instance.
     */
    public static function getInstance() :self {
        if (self::$instance == null) {
            self::$instance = new self();
        }

        return self::$instance;
    }

    /**
     * Get connection PDO.
     */
    public function getPdo() {
        return $this->pdo;
    }
}

?>