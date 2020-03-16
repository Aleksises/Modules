--Выдать всех продавцов, которые имеют более 150 заказов. Использовать вложенный SELECT
SELECT [EmployeeID]  AS 'EmployeeID'
FROM [dbo].[Employees]
WHERE (SELECT COUNT([OrderID]) 
        FROM [dbo].[Orders] 
        WHERE [Orders].[EmployeeID] = [Employees].[EmployeeID]) > 150;