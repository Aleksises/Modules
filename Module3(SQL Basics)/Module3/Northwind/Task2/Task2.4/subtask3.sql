--Выдать всех заказчиков (таблица Customers), которые не имеют ни одного заказа 
--(подзапрос по таблице Orders). Использовать оператор EXISTS
SELECT [CustomerId ]
FROM [dbo].[Customers]
WHERE NOT EXISTS (SELECT [OrderId] 
                    FROM [dbo].[Orders]
                    WHERE [Orders].[CustomerID] = [Customers].[CustomerID]);