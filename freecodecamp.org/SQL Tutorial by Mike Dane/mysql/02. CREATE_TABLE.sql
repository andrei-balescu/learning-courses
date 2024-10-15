USE freecodecamp_sql_tutorial;

DROP TABLE IF EXISTS student;

CREATE TABLE student(
	student_id	INT PRIMARY KEY,
    name		VARCHAR(20),
    major		VARCHAR(20)
--    ,PRIMARY KEY (student_id) -- alternative syntax
);

-- IF NOT EXISTS not working for adding columns
ALTER TABLE student ADD COLUMN gpa DECIMAL(3,2);

-- ALTER TABLE student DROP COLUMN gpa;

DESCRIBE student;

SELECT * 
	FROM information_schema.COLUMNS 
	WHERE TABLE_SCHEMA = 'freecodecamp_sql_tutorial' 
	AND TABLE_NAME = 'student' 
	AND COLUMN_NAME = 'gpa'
