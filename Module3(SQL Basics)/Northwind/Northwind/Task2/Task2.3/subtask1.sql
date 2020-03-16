--Определить продавцов, которые обслуживают регион 'Western' (таблица Region). 
SELECT DISTINCT 
    [Employees].[EmployeeID] AS 'EmployeeId',
    [Employees].[FirstName] AS 'First name'
FROM [dbo].[Employees]
    JOIN [dbo].[EmployeeTerritories] 
        ON [Employees].[EmployeeID] = [EmployeeTerritories].[EmployeeID]
    JOIN [dbo].[Territories] 
        ON [EmployeeTerritories].[TerritoryID] = [Territories].[TerritoryID]
    JOIN [dbo].[Region] 
        ON [Region].[RegionID] = [Territories].[RegionID]
WHERE [RegionDescription] = 'Western';