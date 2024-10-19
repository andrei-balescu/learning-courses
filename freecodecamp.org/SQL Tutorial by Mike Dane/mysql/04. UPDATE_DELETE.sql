USE freecodecamp_sql_tutorial;

-- Error Code: 1175. You are using safe update mode and you tried to update a table without a WHERE that uses a KEY column.  To disable safe mode, toggle the option in Preferences -> SQL Editor and reconnect.
-- requires declaring primary key in where clause
SET SQL_SAFE_UPDATES = 0; 
UPDATE student
	SET gpa = 8.5
WHERE major = 'Biology';

SET SQL_SAFE_UPDATES = 1;
UPDATE student
	SET gpa = 9.5
WHERE student_id = 5;

DELETE FROM student
WHERE student_id = 4;

SELECT * FROM student;