<?php

namespace App\Repository;

/**
 * Fetches translations from database;
 */
class TranslationRepository
{
    /**
     * Translates a phrase based on the given language.
     */
    public function getForLanguage($languageId, $phrase)
    {
        return "No translation found for: " . $phrase;
    }
}

?>