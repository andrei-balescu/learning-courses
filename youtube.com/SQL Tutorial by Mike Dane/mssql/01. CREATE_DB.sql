IF NOT EXISTS (
    SELECT * FROM sys.databases 
        WHERE name = 'FreecodecampSqlTutorial'
)
    CREATE DATABASE "FreecodecampSqlTutorial";

SELECT * FROM sys.databases 
    WHERE name = 'FreecodecampSqlTutorial'