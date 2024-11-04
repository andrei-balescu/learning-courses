USE FreecodecampSqlTutorial;

-- % = any number of characters 
-- _ = any one character

-- Find all clients that are an LLC
SELECT *
FROM Client
WHERE ClientName LIKE '%LLC';

-- Find any branch suppliers who are in the label business
SELECT *
FROM BranchSupplier
WHERE SupplierName LIKE '%Label%';

-- Find any employees born in October
SELECT *
FROM Employee
WHERE BirthDay LIKE '____-02-__';

-- Find any clients who are schools
SELECT *
FROM Client
WHERE ClientName LIKE '%school%';