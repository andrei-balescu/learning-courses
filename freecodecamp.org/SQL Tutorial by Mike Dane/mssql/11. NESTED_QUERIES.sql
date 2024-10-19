USE FreecodecampSqlTutorial;

-- Find names of all employees that sold over $30,000 to a single client
SELECT FirstName, LastName
FROM Employee
WHERE EmpID IN (
	SELECT EmpID
	FROM WorksWith
	WHERE TotalSales > 30000
);

-- using common table expressions (CTEs)
WITH ww AS(
	SELECT EmpID, TotalSales
	FROM WorksWith
	WHERE TotalSales > 30000
)
SELECT e.FirstName, e.LastName, SUM(ww.TotalSales) AS TotalSales
FROM employee AS e
INNER JOIN ww
	ON e.EmpID = ww.EmpID
GROUP BY e.FirstName, e.LastName;

-- Find all clients that are managed byt the branch that Michael Scott manages.
WITH br AS (
	SELECT b.BranchID
	FROM Employee AS e
	INNER JOIN Branch b
		ON e.EmpID = b.MgrID
	WHERE e.FirstName = 'Michael'
		AND e.LastName = 'Scott'
)
SELECT c.ClientName
FROM Client AS c
INNER JOIN br
	ON c.BranchID = br.BranchID;

-- using multiple joins
SELECT c.ClientName
FROM Employee AS e
INNER JOIN Branch b
	ON e.EmpID = b.MgrID
INNER JOIN Client AS c
	ON b.BranchID = c.BranchID
WHERE e.FirstName = 'Michael'
	AND e.LastName = 'Scott'