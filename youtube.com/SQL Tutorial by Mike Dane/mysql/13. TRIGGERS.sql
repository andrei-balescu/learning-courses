USE freecodecamp_sql_tutorial;

CREATE TABLE trigger_test(
	message VARCHAR(100)
);

CREATE TRIGGER tr_employee_insert
AFTER INSERT
ON employee
FOR EACH ROW 
	INSERT INTO trigger_test VALUES (CONCAT('added new employee: ', NEW.first_name, ' ', NEW.last_name));

DELIMITER $$ -- replaces semicolon(;) statement delimiter - used inside trigger code;
-- only works from mysql command line, NOT in editor window (ignored): https://stackoverflow.com/questions/10259504/delimiters-in-mysql
-- see docker-compose.yaml for instructions.
CREATE TRIGGER tr_employee_insert2
AFTER INSERT
ON employee
FOR EACH ROW BEGIN
	IF NEW.sex = 'M' THEN
		INSERT INTO trigger_test VALUES ('added male employee');
	ELSEIF NEW.sex = 'F' THEN
		INSERT INTO trigger_test VALUES ('added female employee');
	ELSE
		INSERT INTO trigger_test VALUES ('added other employee');
	END IF;
END$$
DELIMITER ;

SHOW TRIGGERS;

INSERT INTO employee VALUES 
	(109, 'Oscar', 'Martinez', '1968-02-19', 'M', 69000, 106, 3),
    (110, 'Kevin', 'Mallone', '1978-02-19', 'M', 69000, 106, 3),
    (111, 'Pam', 'Beesly', '1988-02-19', 'F', 69000, 106, 3);

SELECT * FROM trigger_test;

DELETE FROM employee
WHERE emp_id IN (109, 110, 111);

DROP TRIGGER tr_employee_insert;
DROP TABLE trigger_test;