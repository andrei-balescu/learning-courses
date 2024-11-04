USE freecodecamp_sql_tutorial;

-- Find all branches and the name of their managers
SELECT e.emp_id, e.first_name, e.last_name, b.branch_name
FROM employee AS e
INNER JOIN branch AS b
	ON e.emp_id = b.mgr_id;
    
SELECT e.emp_id, e.first_name, e.last_name, b.branch_name
FROM employee AS e
LEFT OUTER JOIN branch AS b
	ON e.emp_id = b.mgr_id;
    
SELECT e.emp_id, e.first_name, e.last_name, b.branch_name
FROM employee AS e
RIGHT OUTER JOIN branch AS b
	ON e.emp_id = b.mgr_id;
    
-- MySQL does not support full outer joins; use following to get same result
SELECT e.emp_id, e.first_name, e.last_name, b.branch_name
FROM employee AS e
LEFT OUTER JOIN branch AS b
	ON e.emp_id = b.mgr_id
UNION ALL
SELECT e.emp_id, e.first_name, e.last_name, b.branch_name
FROM employee AS e
RIGHT OUTER JOIN branch AS b
	ON e.emp_id = b.mgr_id
WHERE e.emp_id IS NULL;