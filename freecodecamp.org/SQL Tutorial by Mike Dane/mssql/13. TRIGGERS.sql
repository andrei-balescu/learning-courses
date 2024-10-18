USE FreecodecampSqlTutorial;

CREATE TABLE TriggerTest(
	Message NVARCHAR(100)
);
GO

CREATE TRIGGER TR_Employee_INSERT
ON Employee
AFTER INSERT 
AS
BEGIN
    INSERT INTO TriggerTest
    SELECT CASE
        WHEN Sex = 'M' THEN CONCAT('Added male employee: ', FirstName, ' ', LastName)
        WHEN Sex = 'F' THEN CONCAT('Added female employee: ', FirstName, ' ', LastName)
        ELSE CONCAT('Added other employee: ', FirstName, ' ', LastName)
    END
    FROM INSERTED;
END;
GO

INSERT INTO Employee VALUES 
	(109, 'Oscar', 'Martinez', '1968-02-19', 'M', 69000, 106, 3),
    (110, 'Sarah', 'Mallone', '1978-02-19', 'F', 69000, 106, 3),
    (111, 'Max', '', '1978-02-19', 'A', 1000, 106, 3);

SELECT * FROM TriggerTest;

DELETE FROM Employee
WHERE EmpID IN (109, 110, 111);

DROP TRIGGER TR_Employee_INSERT;
DROP TABLE TriggerTest;