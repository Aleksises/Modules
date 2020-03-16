--Найти покупателей и продавцов, которые живут в одном городе. Если в городе живут только один или несколько продавцов,
--или только один или несколько покупателей, то информация о таких покупателя и продавцах не должна попадать в результирующий набор.
--Не использовать конструкцию JOIN

SELECT 
    [CustomerId] AS 'CustomerId',
    [EmployeeID] AS 'EmployeeId',
    [City] AS 'City'
FROM [dbo].[Customers]
CROSS APPLY (SELECT [EmployeeId] 
                FROM [dbo].[Employees] 
                WHERE [Employees].[City] = [Customers].[City]) EmployeesT;