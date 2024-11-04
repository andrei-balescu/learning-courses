USE FreecodecampSqlTutorial;

SELECT TOP 3 [Name], [Gpa]
FROM [Student]
ORDER BY [Gpa] DESC, [name];

SELECT [Name], [Major], [Gpa]
FROM [Student]
WHERE [Gpa] IS NOT NULL
ORDER BY [Gpa] DESC

SELECT [Name], [Major]
FROM [Student]
WHERE [Major] IN ('Biology', 'undecided')

-- <, >, <=. >=, <>, AND, OR, IN
