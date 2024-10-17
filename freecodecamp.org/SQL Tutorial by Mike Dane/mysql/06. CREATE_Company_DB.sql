USE freecodecamp_sql_tutorial;

-- view foreign keys for a table
SELECT TABLE_NAME, COLUMN_NAME, CONSTRAINT_NAME, REFERENCED_TABLE_NAME, REFERENCED_COLUMN_NAME
    FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
    WHERE
      REFERENCED_TABLE_NAME = 'employee';

-- comment out DROP FOREN KEY statements when first creating DB
ALTER TABLE branch
DROP FOREIGN KEY fk_branch_employee;
ALTER TABLE client
DROP FOREIGN KEY fk_client_branch;
ALTER TABLE works_with
DROP FOREIGN KEY fk_works_with_employee;
ALTER TABLE works_with
DROP FOREIGN KEY fk_works_with_client;
ALTER TABLE branch_supplier
DROP FOREIGN KEY fk_branch_supplier_branch;

-- create Company database tables
DROP TABLE IF EXISTS employee;
CREATE TABLE employee(
	emp_id 		INT PRIMARY KEY,
    first_name 	VARCHAR(40),
    last_name 	VARCHAR(40),
    birth_day 	DATE,
    sex 		VARCHAR(1),
    salary 		INT,
    super_id 	INT,
    branch_id 	INT
);

DROP TABLE IF EXISTS branch;
CREATE TABLE branch(
	branch_id 		INT PRIMARY KEY,
    branch_name 	VARCHAR(40),
    mgr_id			INT,
    mgr_start_date	DATE,
    
    CONSTRAINT fk_branch_employee 
    FOREIGN KEY (mgr_id) 
    REFERENCES employee(emp_id) 
    ON DELETE SET NULL
);

ALTER TABLE employee
ADD CONSTRAINT fk_employee_branch FOREIGN KEY (branch_id)
REFERENCES branch(branch_id)
ON DELETE SET NULL;

ALTER TABLE employee
ADD CONSTRAINT fk_employee_employee_super_id FOREIGN KEY (super_id)
REFERENCES employee(emp_id)
ON DELETE SET NULL;

DROP TABLE IF EXISTS client;
CREATE TABLE client (
	client_id 	INT PRIMARY KEY,
    client_name VARCHAR(40),
    branch_id	INT,
    
    CONSTRAINT fk_client_branch
    FOREIGN KEY (branch_id)
    REFERENCES branch(branch_id)
    ON DELETE SET NULL
);

DROP TABLE IF EXISTS works_with;
CREATE TABLE works_with(
	emp_id		INT,
    client_id	INT,
    total_sales	INT,
    
    PRIMARY KEY(emp_id, client_id),
    
    CONSTRAINT fk_works_with_employee
    FOREIGN KEY(emp_id)
    REFERENCES employee(emp_id)
    ON DELETE CASCADE,
    
    CONSTRAINT fk_works_with_client
    FOREIGN KEY (client_id)
    REFERENCES client(client_id)
    ON DELETE CASCADE
);

DROP TABLE IF EXISTS branch_supplier;
CREATE TABLE branch_supplier(
	branch_id		INT,
    supplier_name	VARCHAR(40),
    supply_type		VARCHAR(40),
    
    PRIMARY KEY (branch_id, supplier_name),
    
    CONSTRAINT fk_branch_supplier_branch
    FOREIGN KEY(branch_id)
    REFERENCES branch(branch_id)
    ON DELETE CASCADE
);