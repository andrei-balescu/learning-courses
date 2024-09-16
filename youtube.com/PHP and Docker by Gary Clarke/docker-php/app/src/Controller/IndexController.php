<?php
    namespace App\Controller;

    use App\Repository\TranslationRepository;
    use App\Repository\LanguageRepository;

    /**
     * Manages actions on the index page.
     */
    class IndexController
    {
        /**
         * Fetch all languages.
         */
        public function getLanguages(): array
        {
            $repository = new LanguageRepository();
            $languages = $repository->findAll();

            return $languages;
        }

        /**
         * Retrieve translation for the current request.
         */
        public function getTranslation() : string
        {
            $languageId = $_POST['language'];
            if (empty($languageId))
            {
                return "Language not available.";
            }
            
            $phrase = $_POST['phrase'];
            if (empty($phrase))
            {
                return "Nothing to translate.";
            }

            $repository = new TranslationRepository();
            $translation = $repository->getForLanguage($languageId, $phrase) ?: "No translation found...";

            return $translation;
        }
    }
?>