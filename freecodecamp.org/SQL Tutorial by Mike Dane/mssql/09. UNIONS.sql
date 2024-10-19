USE FreecodeCampSqlTutorial;

-- Find a list of employee branch and client names
SELECT CONCAT(FirstName, ' ', LastName) AS CompanyNames
FROM Employee
UNION
SELECT BranchName
FROM Branch
UNION 
SELECT ClientName
FROM Client;

-- Find a list of all suppliers and branch names
SELECT ClientName, Client.BranchID
FROM Client
UNION
SELECT SupplierName, BranchSupplier.BranchID
FROM BranchSupplier;

-- Find a list of all money spent or earned by the company
SELECT (-Salary)
FROM Employee
UNION
SELECT TotalSales
FROM WorksWith