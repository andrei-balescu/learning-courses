USE freecodecamp_sql_tutorial;

-- Find names of all employees that sold over $30,000 to a single client
SELECT first_name, last_name
FROM employee
WHERE emp_id IN (
	SELECT emp_id
	FROM works_with
	WHERE total_sales > 30000
);

-- using common table expressions (CTEs)
WITH ww AS(
	SELECT emp_id, total_sales
	FROM works_with
	WHERE total_sales > 30000
)
SELECT e.first_name, e.last_name, SUM(ww.total_sales) AS total_sales
FROM employee AS e
INNER JOIN ww
	ON e.emp_id = ww.emp_id
GROUP BY e.first_name, e.last_name;
    
-- Find all clients that are managed byt the branch that Michael Scott manages.
WITH br AS (
	SELECT b.branch_id
	FROM employee AS e
	INNER JOIN branch b
		ON e.emp_id = b.mgr_id
	WHERE e.first_name = 'Michael'
		AND e.last_name = 'Scott'
)
SELECT c.client_name
FROM client AS c
INNER JOIN br
	ON c.branch_id = br.branch_id;
    
-- using multiple joins
SELECT c.client_name
FROM employee AS e
INNER JOIN branch b
	ON e.emp_id = b.mgr_id
INNER JOIN client AS c
	ON b.branch_id = c.branch_id
WHERE e.first_name = 'Michael'
	AND e.last_name = 'Scott'

