USE freecodecamp_sql_tutorial;

CREATE TABLE trigger_test(
	message VARCHAR(100)
);

-- DELIMITER $$ -- replaces semicolon(;) default statement delimiter - used inside trigger code;
-- only works from mysql command line, NOT in editor window: https://stackoverflow.com/questions/10259504/delimiters-in-mysql
CREATE TRIGGER tr_employee_insert
AFTER INSERT
ON employee
FOR EACH ROW 
-- BEGIN
	INSERT INTO trigger_test VALUES (CONCAT('added new employee: ', NEW.first_name, ' ', NEW.last_name));
-- END$$
-- DELIMITER ;
SHOW TRIGGERS;

INSERT INTO employee VALUES 
	(109, 'Oscar', 'Martinez', '1968-02-19', 'M', 69000, 106, 3),
    (110, 'Kevin', 'Mallone', '1978-02-19', 'M', 69000, 106, 3);

SELECT * FROM trigger_test;

DELETE FROM employee
WHERE emp_id IN (109, 110);

DROP TRIGGER tr_employee_insert;
DROP TABLE trigger_test;