--Найти общую сумму всех заказов из таблицы Order Details с учетом количества закупленных товаров и скидок по ним. 
--Результатом запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.

SELECT SUM([Quantity] * [UnitPrice] * (1 - [Discount])) AS 'Totals'
FROM [dbo].[Order Details];