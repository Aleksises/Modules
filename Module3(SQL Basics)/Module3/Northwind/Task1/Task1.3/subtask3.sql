--Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается 
--на буквы из диапазона b и g, не используя оператор BETWEEN. 

--version 1
SELECT 
    [CustomerId] AS 'CustomerId',
    [Country] AS 'Country'
FROM [dbo].[Customers] WHERE SUBSTRING([Country], 1, 1) IN ('b', 'c', 'd', 'e', 'f', 'g')
ORDER BY [Country];

--version 2
SELECT 
    [CustomerId] AS 'CustomerId',
    [Country] AS 'Country'
FROM [dbo].[Customers] WHERE SUBSTRING(Country, 1, 1) >= 'b' AND SUBSTRING(Country, 1, 1) <= 'g'
ORDER BY [Country];