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


-- Introducing FOREIGN KEY constraint 'FK_Employee_Employee_SuperID' on table 'Employee' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
ALTER TABLE Employee
ADD CONSTRAINT FK_Employee_Employee_SuperID
FOREIGN KEY (SuperID)
REFERENCES Employee(EmpID)
ON DELETE NO ACTION; 
GO

CREATE TRIGGER TR_Employee_Delete
ON Employee
AFTER DELETE 
AS
    UPDATE Employee
        SET SuperID = NULL
    WHERE SuperID IN (SELECT deleted.EmpID FROM deleted)
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
    ON DELETE CASCADE,

    CONSTRAINT FK_WorksWith_Client
    FOREIGN KEY (ClientID)
    REFERENCES Client(ClientID)
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
    ON DELETE CASCADE
);