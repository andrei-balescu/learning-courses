USE FreecodecampDotnet6MvcTutorial;

DECLARE @Categories TABLE(
    [Name]          VARCHAR(100),
    DisplayOrder    INT,
    CreatedDateTime DATETIME
);

INSERT INTO @Categories 
    ([Name], DisplayOrder, CreatedDateTime)
VALUES
    ('Laptop', 1, GETDATE()),
    ('Telefoane', 3, GETDATE()),
    ('Tablete', 2, GETDATE())

MERGE INTO Categories AS c
USING @Categories AS tc
ON c.[Name] = tc.[Name]
WHEN NOT MATCHED THEN 
    INSERT ([Name], DisplayOrder, CreatedDateTime)
    VALUES (tc.[Name], tc.DisplayOrder, tc.CreatedDateTime)
OUTPUT $action, INSERTED.[Name];