USE freecodecamp_sql_tutorial;

-- Error Code: 1175. You are using safe update mode and you tried to update a table without a WHERE that uses a KEY column.  To disable safe mode, toggle the option in Preferences -> SQL Editor and reconnect.
SET SQL_SAFE_UPDATES = 0;

DELETE FROM employee
WHERE first_name = 'Michael'
	AND last_name = 'Scott';
    
SELECT * FROM employee; -- ON DELETE SET NULL
SELECT * FROM branch; -- ON DELETE SET NULL
SELECT * FROM works_with; -- ON DELETE CASCADE (FOREIGN KEY is also PRIMARY KEY)

DELETE FROM branch
WHERE branch_name = 'Stamford';

SELECT * FROM branch_supplier; -- ON DELETE CASCADE (FOREIGN KEY is also PRIMARY KEY)