<?php

namespace App\Cache;

use Symfony\Component\Cache\Adapter\AbstractAdapter;
use Symfony\Component\Cache\Adapter\RedisAdapter;
use Symfony\Contracts\Cache\ItemInterface;

use App\Repository\TranslationRepository;

/**
 * Manage cache via Redis server;
 */
class TranslationCache
{
    private TranslationRepository $repository;
    private AbstractAdapter $cache;

    /**
     * New translation cache.
     */
    public function __construct(TranslationRepository $repository)
    {
        $this->repository = $repository;

        $uri = "redis://{$_ENV['REDIS_HOST']}:{$_ENV['REDIS_PORT']}";
        $connection = RedisAdapter::createConnection($uri);

        $this->cache = new RedisAdapter($connection);
    }

    /**
     * Get translation from cache or repository.
     */
    public function findForLanguage(int $languageId, string $phrase): ?string
    {
        $key = sprintf("translation;%d;%s", $languageId, $phrase);
        $translation = $this->cache->get($key, function(ItemInterface $item) use ($languageId, $phrase) {
            echo "Adding item to cache...";
            $item->expiresAfter(3600);
            $translatedPhrase = $this->repository->getForLanguage($languageId, $phrase);
            return $translatedPhrase;
        });

        return $translation;
    }
}

?>