USE FreecodecampSqlTutorial;

DELETE FROM Employee
WHERE FirstName = 'Michael'
	AND LastName = 'Scott';
    
SELECT * FROM Employee; -- ON DELETE SET NULL (trigger)
SELECT * FROM Branch; -- ON DELETE SET NULL
SELECT * FROM WorksWith; -- ON DELETE CASCADE (FOREIGN KEY is also PRIMARY KEY)

DELETE FROM Branch
WHERE BranchName = 'Stamford';

SELECT * FROM BranchSupplier; -- ON DELETE CASCADE (FOREIGN KEY is also PRIMARY KEY)