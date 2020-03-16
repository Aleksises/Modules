--Выдать всех поставщиков (колонка CompanyName в таблице Suppliers), у которых нет хотя бы одного продукта 
--на складе (UnitsInStock в таблице Products равно 0). Использовать вложенный SELECT для этого запроса с использованием оператора IN
SELECT [CompanyName] AS 'CompanyName'
FROM [dbo].[Suppliers]
WHERE [SupplierID] IN 
	(SELECT [SupplierID]
	FROM [dbo].[Products]
	WHERE [UnitsInStock] = 0);