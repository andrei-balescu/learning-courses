USE FreecodecampDotnet6MvcTutorial;

DECLARE @Categories TABLE(
    [Name]          VARCHAR(100),
    DisplayOrder    INT,
    CreatedDateTime DATETIME
);

INSERT INTO @Categories 
    ([Name], DisplayOrder)
VALUES
    ('Laptop', 1),
    ('Telefoane', 3),
    ('Tablete', 2)

MERGE INTO Categories AS c
USING @Categories AS tc
ON c.[Name] = tc.[Name]
WHEN NOT MATCHED THEN 
    INSERT ([Name], DisplayOrder)
    VALUES (tc.[Name], tc.DisplayOrder)
OUTPUT $action, INSERTED.[Name];