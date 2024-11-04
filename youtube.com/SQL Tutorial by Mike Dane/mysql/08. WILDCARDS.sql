USE freecodecamp_sql_tutorial;

-- % = any number of characters 
-- _ = any one character

-- Find all clients that are an LLC
SELECT *
FROM client
WHERE client_name LIKE '%LLC';

-- Find any branch suppliers who are in the label business
SELECT *
FROM branch_supplier
WHERE supplier_name LIKE '%Label%';

-- Find any employees born in October
SELECT *
FROM employee
WHERE birth_day LIKE '____-02-__';

-- Find any clients who are schools
SELECT *
FROM client
WHERE client_name LIKE '%school%';