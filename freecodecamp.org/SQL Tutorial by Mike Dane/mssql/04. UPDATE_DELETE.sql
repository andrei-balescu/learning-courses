USE FreecodecampSqlTutorial;

UPDATE Student
    SET Gpa = 8.5
WHERE Major = 'Biology';

UPDATE Student 
    SET Gpa = 9.5
WHERE StudentId = 5;

DELETE FROM Student
WHERE StudentId = 4;

SELECT * FROM Student;