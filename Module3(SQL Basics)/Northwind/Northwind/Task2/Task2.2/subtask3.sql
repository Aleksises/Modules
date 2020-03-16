--По таблице Orders найти количество заказов, сделанных каждым продавцом и для каждого покупателя. 
--Необходимо определить это только для заказов, сделанных в 1998 году. 
DECLARE 
    @year INT = 1998;
SELECT 
    [EmployeeID] AS 'EmployeeId',
    [CustomerID] AS 'CustomerId',
    COUNT([OrderID]) AS 'Amount'
FROM [dbo].[Orders] 
WHERE YEAR([OrderDate]) = @year
GROUP BY [EmployeeID], [CustomerID];
