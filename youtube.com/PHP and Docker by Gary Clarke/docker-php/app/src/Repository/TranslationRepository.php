<?php

namespace App\Repository;

/**
 * Fetches translations from database;
 */
class TranslationRepository
{
    public function getForLanguage($languageId, $phrase)
    {
        return "No translation found for: " . $phrase;
    }
}

?>