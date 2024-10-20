USE FreecodecampSqlTutorial;

IF (object_id('Student', 'U')) IS NOT NULL
    DROP TABLE Student;

CREATE TABLE Student(
    StudentId   INT PRIMARY KEY IDENTITY,
    Name        NVARCHAR(20) NOT NULL,
    Major       NVARCHAR(20) DEFAULT 'undecided'
);

IF NOT EXISTS (
    SELECT * FROM sys.columns
    WHERE object_id = object_id(N'[dbo].[Student]')
        AND name = 'Gpa'
)
ALTER TABLE Student
ADD Gpa DECIMAL(3,2);

-- EXEC sp_columns Student;
EXEC sp_help Student;