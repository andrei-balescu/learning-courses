USE FreecodecampSqlTutorial;

IF OBJECT_ID('FK_Branch_Employee', 'F') IS NOT NULL
    ALTER TABLE Branch
    DROP CONSTRAINT FK_Branch_Employee;

IF OBJECT_ID('FK_Client_Branch', 'F') IS NOT NULL
    ALTER TABLE Client
    DROP CONSTRAINT FK_Client_Branch;

IF OBJECT_ID('FK_WorksWith_Employee', 'F') IS NOT NULL
    ALTER TABLE WorksWith
    DROP CONSTRAINT FK_WorksWith_Employee;

IF OBJECT_ID('FK_WorksWith_Client', 'F') IS NOT NULL
    ALTER TABLE WorksWith
    DROP CONSTRAINT FK_WorksWith_Client;

IF OBJECT_ID('FK_BranchSupplier_Branch', 'F') IS NOT NULL
    ALTER TABLE BranchSupplier
    DROP CONSTRAINT FK_BranchSupplier_Branch;

IF OBJECT_ID('Employee', 'U') IS NOT NULL
    DROP TABLE Employee;
CREATE TABLE Employee(
    EmpID       INT PRIMARY KEY,
    FirstName   NVARCHAR(40),
    LastName    NVARCHAR(40),
    BirthDay    DATE,
    Sex         NVARCHAR(1),
    Salary      INT,
    SuperID     INT NULL,
    BranchID    INT
);

IF OBJECT_ID('Branch', 'U') IS NOT NULL
    DROP TABLE Branch;
CREATE TABLE Branch(
    BranchID        INT PRIMARY KEY,
    BranchName      NVARCHAR(40),
    MgrID           INT,
    MgrStartDate    DATE,

    CONSTRAINT FK_Branch_Employee
    FOREIGN KEY (MgrID)
    REFERENCES Employee(EmpID)
    ON DELETE SET NULL
);

ALTER TABLE Employee
ADD CONSTRAINT FK_Employee_Branch
FOREIGN KEY (BranchID)
REFERENCES Branch(BranchID)
ON DELETE SET NULL;
GO

-- Introducing FOREIGN KEY constraint 'FK_Employee_Employee_SuperID' on table 'Employee' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
-- ALTER TABLE Employee
-- ADD CONSTRAINT FK_Employee_Employee_SuperID
-- FOREIGN KEY (SuperID)
-- REFERENCES Employee(EmpID)
-- ON DELETE NO ACTION; -- blocks deletion of row that is referenced

CREATE TRIGGER TR_Employee_Delete
ON Employee
AFTER DELETE 
AS
    UPDATE Employee
        SET SuperID = NULL
    WHERE SuperID IN (SELECT deleted.EmpID FROM deleted) -- workaround for ON DELETE SET NULL
GO

IF OBJECT_ID('Client', 'U') IS NOT NULL
    DROP TABLE Client;
CREATE TABLE Client(
    ClientID    INT PRIMARY KEY,
    ClientName  NVARCHAR(40),
    BranchID    INT

    CONSTRAINT FK_Client_Branch
    FOREIGN KEY (BranchID)
    REFERENCES Branch(BranchID)
    ON DELETE SET NULL
);

IF OBJECT_ID('WorksWith', 'U') IS NOT NULL
    DROP TABLE WorksWith;
CREATE TABLE WorksWith(
    EmpID       INT,
    ClientID    INT,
    TotalSales  INT,

    PRIMARY KEY(EmpID, ClientID),

    CONSTRAINT FK_WorksWith_Employee
    FOREIGN KEY (EmpID)
    REFERENCES Employee(EmpID)
    -- FOREIGN KEY is also PRIMARY KEY
    ON DELETE CASCADE,

    CONSTRAINT FK_WorksWith_Client
    FOREIGN KEY (ClientID)
    REFERENCES Client(ClientID)
    -- FOREIGN KEY is also PRIMARY KEY
    ON DELETE CASCADE
);

IF OBJECT_ID('BranchSupplier', 'U') IS NOT NULL
    DROP TABLE BranchSupplier;
CREATE TABLE BranchSupplier(
    BranchID        INT,
    SupplierName    NVARCHAR(40),
    SupplyType      NVARCHAR(40),

    PRIMARY KEY (BranchID, SupplierName),

    CONSTRAINT FK_BranchSupplier_Branch
    FOREIGN KEY (BranchID)
    REFERENCES Branch(BranchID)
    -- FOREIGN KEY is also PRIMARY KEY
    ON DELETE CASCADE
);

-- Seed Company DB

-- Corporate branch
INSERT INTO Employee VALUES (100, 'David', 'Wallace', '1967-11-17', 'M', 250000, NULL, NULL);
INSERT INTO Branch VALUES (1, 'Corporate', 100, '2006-02-09');
UPDATE Employee
    SET BranchID = 1
    WHERE EmpID = 100;
INSERT INTO Employee VALUES(101, 'Jan', 'Levinson', '1961-05-11', 'F', 110000, 100, 1);

-- Scranton branch
INSERT INTO Employee VALUES(102, 'Michael', 'Scott', '1964-03-15', 'M', 75000, 100, NULL);
INSERT INTO Branch VALUES(2, 'Scranton', 102, '1992-04-06');
UPDATE Employee
	SET BranchID = 2
    WHERE EmpID = 102;
INSERT INTO Employee VALUES
	(103, 'Angela', 'Martin', '1971-06-25', 'F', 63000, 102, 2),
    (104, 'Kelly', 'Kapoor', '1980-02-05', 'F', 55000, 102, 2),
    (105, 'Stanley', 'Hudson', '1958-02-19', 'M', 69000, 102, 2);

-- Stamford branch
INSERT INTO Employee VALUES (106, 'Josh', 'Porter', '1969-09-05', 'M', 78000, 100, NULL);
INSERT INTO Branch VALUES (3, 'Stamford', 106, '1998-02-13');
UPDATE Employee
	SET BranchID = 3
    WHERE EmpID = 106;
INSERT INTO Employee VALUES
	(107, 'Andy', 'Bernard', '1973-07-22', 'M', 65000, 106, 3),
    (108, 'Jim', 'Halpert', '1978-10-01', 'M', 71000, 106, 3);

-- Buffalo branch
INSERT INTO Branch VALUES (4, 'Buffalo', NULL, NULL);

-- Branch supplier
INSERT INTO BranchSupplier VALUES
	(2, 'Hammer Mill', 'Paper'),
    (2, 'Uni-ball', 'Writing Utensils'),
    (3, 'Patriot Paper', 'Paper'),
    (2, 'J.T. Forms & Labels', 'Custom Forms'),
    (3, 'Uni-ball', 'Writing Utensils'),
    (3, 'Hammer Mill', 'Paper'),
    (3, 'Stamford Lables', 'Custom Forms');

-- Client
INSERT INTO Client VALUES
	(400, 'Dunmore Highschool', 2),
    (401, 'Lackawana Country', 2),
    (402, 'FedEx', 3),
    (403, 'John Daly Law, LLC', 3),
    (404, 'Scranton Whitepages', 2),
    (405, 'Times Newspaper', 3),
    (406, 'FedEx', 2);

-- Works with
INSERT INTO WorksWith VALUES
	(105, 400, 55000),
    (102, 401, 267000),
    (108, 402, 22500),
    (107, 403, 5000),
    (108, 403, 12000),
    (105, 404, 33000),
    (107, 405, 26000),
    (102, 406, 15000),
    (105, 406, 130000);

SELECT * FROM Employee;