USE freecodecamp_sql_tutorial;

SET SQL_SAFE_UPDATES = 0; -- requires setting primary key in where clause
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