--Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. 
--Запрос сделать с только помощью оператора IN. Возвращать колонки с именем
--пользователя и названием страны в результатах запроса. Упорядочить результаты 
--запроса по имени заказчиков и по месту проживания.

SELECT 
    [ContactName] AS 'Contact Name',
    [Country] AS 'Country'
FROM [dbo].[Customers]
WHERE [Country] IN ('USA', 'Canada')
ORDER BY [ContactName], [Address];