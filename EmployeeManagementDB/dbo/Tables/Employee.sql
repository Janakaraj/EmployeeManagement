CREATE TABLE dbo.Employee
(
	Id INT NOT NULL PRIMARY KEY, 
    Name NVARCHAR(20) NOT NULL, 
    Surname NVARCHAR(20) NOT NULL, 
    Address NVARCHAR(100) NOT NULL, 
    Qualification NVARCHAR(MAX) NOT NULL, 
    ContactNumber BIGINT NOT NULL, 
    DepartmentId INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES dbo.Department(DepartmentId) 
)
