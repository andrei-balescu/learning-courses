USE freecodecamp_sql_tutorial;

INSERT IGNORE INTO student VALUES -- ignore existing values
	(1, 'Jack', 'Biology', NULL), 
	(2, 'Kate', 'Sociology', NULL);
    
INSERT IGNORE INTO student 
	(student_id, name)
VALUES (3, 'Claire');

REPLACE INTO student (student_id, name, major) -- replace existing values
VALUES 
(4, 'Jack', 'Biology'),
(5, 'Mike', 'Computer Science');

SELECT * FROM student;