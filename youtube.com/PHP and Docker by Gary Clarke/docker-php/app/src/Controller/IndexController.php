<?php
    namespace App\Controller;

    use App\Repository\TranslationRepository;
    use \Exception;

    /**
     * Manages actions on the index page.
     */
    class IndexController
    {
        /**
         * [POST] Retrieve translation for the current request.
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