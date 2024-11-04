USE FreecodecampSQLTutorial;

-- Find all branches and the name of their managers
SELECT e.EmpID, e.FirstName, e.LastName, b.BranchName
FROM Employee AS e
INNER JOIN Branch AS b
	ON e.EmpID = b.MgrID;

SELECT e.EmpID, e.FirstName, e.LastName, b.BranchName
FROM Employee AS e
LEFT OUTER JOIN Branch AS b
	ON e.EmpID = b.MgrID;

SELECT e.EmpID, e.FirstName, e.LastName, b.BranchName
FROM Employee AS e
RIGHT OUTER JOIN Branch AS b
	ON e.EmpID = b.MgrID;

SELECT e.EmpID, e.FirstName, e.LastName, b.BranchName
FROM Employee AS e
FULL OUTER JOIN Branch AS b
	ON e.EmpID = b.MgrID;