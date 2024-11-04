USE freecodecamp_sql_tutorial;

-- Find a list of employee branch and client names
SELECT CONCAT(first_name, ' ', last_name) AS company_names
FROM employee
UNION
SELECT branch_name
FROM branch
UNION 
SELECT client_name
FROM client;

-- Find a list of all suppliers and branch names
SELECT client_name, client.branch_id
FROM client
UNION
SELECT supplier_name, branch_supplier.branch_id
FROM branch_supplier;

-- Find a list of all money spent or earned by the company
SELECT (-salary)
FROM employee
UNION
SELECT total_sales
FROM works_with