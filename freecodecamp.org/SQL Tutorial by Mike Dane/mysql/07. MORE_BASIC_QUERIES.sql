USE freecodecamp_sql_tutorial;

-- Find all employees
SELECT first_name AS forename, last_name AS surname, sex
FROM employee
ORDER BY sex, last_name;

-- Find all employee branches
SELECT DISTINCT branch_id
FROM employee;

-- Functions

-- Find the number of employees, employees that have a superviser
SELECT COUNT(emp_id), COUNT(super_id)
FROM employee;

-- Find the number of employees born after 1970
SELECT COUNT(emp_id)
FROM employee
WHERE birth_day > '1970-01-01';
    
-- Find the average of all employee's salaries for a given branch
SELECT AVG(salary)
FROM employee
WHERE branch_id = 2;

-- Find the sum of all employee's salary
SELECT SUM(salary)
FROM employee;

-- Find how many males and females there are
SELECT COUNT(sex), sex
FROM employee
GROUP BY sex;

-- Find the total sales of each salesman
SELECT e.first_name, e.last_name, SUM(ww.total_sales)
FROM employee e
	INNER JOIN works_with ww
    ON e.emp_id = ww.emp_id
GROUP BY ww.emp_id;

-- Find the total sales to each client_id
SELECT c.client_name, c.client_id, SUM(ww.total_sales)
FROM client c
	INNER JOIN works_with ww
    ON ww.client_id = c.client_id
GROUP BY ww.client_id;