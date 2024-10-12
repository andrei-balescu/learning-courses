RESTORE DATABASE [AdventureWorks] FROM DISK = '/tmp/AdventureWorksLT2022.bak' -- make sure to copy .bak file inside docker image at this location
WITH FILE = 1,
MOVE 'AdventureWorksLT2022_Data' TO '/var/opt/mssql/data/AdventureWorks.mdf',
MOVE 'AdventureWorksLT2022_Log' TO '/var/opt/mssql/data/AdventureWorks.ldf',
NOUNLOAD, REPLACE, STATS = 5
GO

-- Sanitize DB
USE AdventureWorks
GO

UPDATE SalesLT.Customer
SET EmailAddress = FirstName + 'example.com',
    LastName = 'Customer',
    Phone = '000-000-0000',
    PasswordHash = '',
    PasswordSalt = '';