<?php

namespace App\Repository;

/**
 * Fetches translations from database;
 */
class TranslationRepository extends Repository
{
    /**
     * Translates a phrase based on the given language.
     */
    public function getForLanguage($languageId, $phrase)
    {
        $sql = "SELECT translation FROM translation WHERE language_id = :language AND phrase = :phrase";
        $statement = $this->connection->prepare($sql);

        $statement->execute(['language' => $languageId, 'phrase' => $phrase]);
        $response = $statement->fetchColumn();

        return $response;
    }
}

?>