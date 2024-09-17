<?php
    namespace App\Controller;

    use App\Cache\TranslationCache;
    use App\Repository\TranslationRepository;
    use App\Repository\LanguageRepository;

    /**
     * Manages actions on the index page.
     * NOTE: not using any framework.
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
            $languageId = (int)$_POST['language'];
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
            $cache = new TranslationCache($repository);
            $translation = $cache->findForLanguage($languageId, $phrase) ?: "No translation found...";

            return $translation;
        }
    }
?>