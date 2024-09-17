<?php

namespace App\Repository;

use App\Database\Connection;

/**
 * Base class for all repositories.
 */
abstract class Repository
{
    protected \PDO $connection;

    /**
     * Create new repository.
     */
    public function __construct()
    {
        $this->connection = Connection::getInstance()->getPdo();
    }
}

?>