USE FreecodecampSqlTutorial;

CREATE TABLE #Student
(
    StudentId   INT IDENTITY,
    Name        NVARCHAR(20) NOT NULL,
    Major       NVARCHAR(20) DEFAULT 'undecided'
)

INSERT INTO #Student
VALUES
    ('Jack', 'Biology'),
    ('Kate', 'Sociology');
INSERT INTO #Student (Name)
VALUES 
    ('Claire');
INSERT INTO #Student
VALUES
    ('Jack', 'Biology'),
    ('Mike', 'Computer Science');

MERGE INTO Student AS s
USING #Student AS ts
ON s.StudentId = ts.StudentId
WHEN MATCHED THEN
    UPDATE
        SET Name = ts.Name, 
            Major = ts.Major
WHEN NOT MATCHED THEN
    INSERT (Name, Major) 
    VALUES (ts.Name, ts.Major)
WHEN NOT MATCHED BY SOURCE THEN
    DELETE
OUTPUT $action, INSERTED.Name;

SELECT * FROM Student;

DROP TABLE #Student;