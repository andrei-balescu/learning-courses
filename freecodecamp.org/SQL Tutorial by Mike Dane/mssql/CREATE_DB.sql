IF NOT EXISTS (
    SELECT * FROM sys.databases 
        WHERE name = 'freecodecamp_sql_tutorial'
)
    CREATE DATABASE "freecodecamp_sql_tutorial";

SELECT * FROM sys.databases 
    WHERE name = 'freecodecamp_sql_tutorial'