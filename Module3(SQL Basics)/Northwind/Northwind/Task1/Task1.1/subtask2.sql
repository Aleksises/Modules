--Написать запрос, который выводит только недоставленные заказы из таблицы Orders. 
--В результатах запроса возвращать для колонки ShippedDate вместо значений NULL 
--строку ‘Not Shipped’ (использовать системную функцию CASЕ). Запрос должен возвращать 
--только колонки OrderID и ShippedDate.

SELECT 
    [OrderID] AS 'OrderID',
    CASE 
        WHEN [ShippedDate] IS NULL 
        THEN 'Not shipped' END 
    AS 'ShippedDate'
FROM [dbo].[Orders]
WHERE [ShippedDate] IS NULL;