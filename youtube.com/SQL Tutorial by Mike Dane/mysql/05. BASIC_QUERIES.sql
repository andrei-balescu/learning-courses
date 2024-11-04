USE freecodecamp_sql_tutorial;

SELECT name, gpa 
FROM student
ORDER BY gpa DESC, name
LIMIT 3;

SELECT name, major, gpa 
FROM student
WHERE gpa IS NOT NULL
ORDER BY gpa DESC;

SELECT name, major 
FROM student
WHERE major IN ('Biology', 'undecided');

-- <, >, <=. >=, <>, AND, OR, IN