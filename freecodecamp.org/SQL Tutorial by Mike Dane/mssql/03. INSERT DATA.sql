USE FreecodecampSqlTutorial;

CREATE TABLE #Student
(
    StudentId   INT,
    Name        NVARCHAR(20),
    Major       NVARCHAR(20)
)

INSERT INTO #Student
VALUES
    (1, 'Jack', 'Biology'),
    (2, 'Kate', 'Sociology'),
    (3, 'Claire', 'English'),
    (4, 'Jack', 'Biology'),
    (5, 'Mike', 'Computer Science');

MERGE INTO Student AS s
USING #Student AS ts
ON s.StudentId = ts.StudentId
WHEN MATCHED THEN
    UPDATE
        SET Name = ts.Name, 
            Major = ts.Major
WHEN NOT MATCHED THEN
    INSERT (StudentId, Name, Major) 
    VALUES (ts.StudentId, ts.Name, ts.Major);

SELECT * FROM Student;

DROP TABLE #Student;