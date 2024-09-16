<?php

namespace App\Repository;

use App\Model\Language;

/**
 * Manages language data from DB.
 */
class LanguageRepository extends Repository
{
    private string $table = Language::TABLE;

    /**
     * Get all languages.
     */
    public function findAll(): array 
    {
        $sql = "SELECT * FROM {$this->table}";
        $statement = $this->connection->query($sql);
        $result = $statement->fetchAll(\PDO::FETCH_CLASS, Language::class);

        return $result;
    }
}

?>