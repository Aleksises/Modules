--Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g. 
--Использовать оператор BETWEEN. Проверить, что в результаты запроса попадает Germany. Запрос должен возвращать 
--только колонки CustomerID и Country и отсортирован по Country.

SELECT 
    [CustomerId] AS 'CustomerId',
    [Country] AS 'Country'
FROM [dbo].[Customers]
WHERE SUBSTRING([Country], 1, 1) BETWEEN 'b' AND 'g'
ORDER BY [Country];