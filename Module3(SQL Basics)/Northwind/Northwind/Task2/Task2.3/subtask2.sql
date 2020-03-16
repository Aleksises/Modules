--Выдать в результатах запроса имена всех заказчиков из таблицы Customers и суммарное количество их заказов из таблицы Orders. 
--Принять во внимание, что у некоторых заказчиков нет заказов, но они также должны быть выведены в результатах запроса. 
--Упорядочить результаты запроса по возрастанию количества заказов.
SELECT
    [ContactName] AS 'ContactName',
    COUNT([OrderId]) AS 'OrdersCount'
FROM [dbo].[Customers]
    LEFT JOIN [dbo].[Orders] 
        ON [Customers].[CustomerId] = [Orders].[CustomerId]
GROUP BY [Customers].[CustomerID], [ContactName]
ORDER BY 'OrdersCount';