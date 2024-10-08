<?php

namespace App\Repository;

/**
 * Manages translations from database;
 */
class TranslationRepository extends Repository
{
    /**
     * Translates a phrase based on the given language.
     */
    public function getForLanguage(int $languageId, string $phrase): ?string
    {
        $sql = "SELECT translation FROM translation WHERE language_id = :language AND phrase = :phrase";
        $statement = $this->connection->prepare($sql);

        $statement->execute(['language' => $languageId, 'phrase' => $phrase]);
        $response = $statement->fetchColumn();

        return $response;
    }
}

?>