--По таблице Employees найти для каждого продавца его руководителя.
SELECT 
    [EmployeeID] AS 'EmployeeID',
    [FirstName] AS 'Seller name',
    (SELECT Managers.[FirstName] 
        FROM [dbo].[Employees] Managers
        WHERE Managers.[EmployeeID] = [Employees].[ReportsTo]) AS 'Manager'
FROM [dbo].[Employees];