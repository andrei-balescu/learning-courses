USE FreecodecampSqlTutorial;

-- Find all employees
SELECT FirstName AS forename, LastName AS surname, Sex
FROM Employee
ORDER BY Sex, LastName;

-- Find all employee branches
SELECT DISTINCT BranchID
FROM Employee;

-- Functions

-- Find the number of employees, employees that have a superviser
SELECT COUNT(EmpID), COUNT(SuperID)
FROM employee;

-- Find the number of employees born after 1970
SELECT COUNT(empID)
FROM employee
WHERE BirthDay > '1970-01-01';

-- Find the average of all employee's salaries for a given branch
SELECT AVG(salary)
FROM Employee
WHERE BranchID = 2;

-- Find the sum of all employee's salary
SELECT SUM(Salary)
FROM Employee;

-- Find how many males and females there are
SELECT COUNT(Sex), Sex
FROM Employee
GROUP BY Sex;

-- Find the total sales of each salesman
SELECT e.FirstName, e.LastName, SUM(ww.TotalSales)
FROM Employee e
	INNER JOIN WorksWith ww
    ON e.EmpID = ww.EmpID
GROUP BY ww.EmpID, e.FirstName, e.LastName;

-- Find the total sales to each client_id
SELECT c.ClientName, c.ClientID, SUM(ww.TotalSales)
FROM client c
	INNER JOIN WorksWith ww
    ON ww.ClientID = c.ClientID
GROUP BY c.ClientID, c.ClientName;