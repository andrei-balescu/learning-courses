<?php

namespace App\Model;

/**
 * Represents language data;
 */
class Language
{
    private int $id;
    private string $name;

    /**
     * DB table name.
     */
    public const TABLE = "language";

    /**
     * Language ID
     */
    public function getId(): int 
    {
        return $this->id;
    }

    /**
     * Language name.
     */
    public function getName(): string 
    {
        return $this->name;
    }
}

?>