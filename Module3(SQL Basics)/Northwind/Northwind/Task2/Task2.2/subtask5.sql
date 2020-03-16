--Найти всех покупателей, которые живут в одном городе
SELECT 
    CustomersL.[CustomerID] AS 'Customer id',
    CustomersR.[CustomerID] AS 'Neighbor id',
    CustomersL.[City] AS 'City'
FROM [dbo].[Customers] CustomersL
    LEFT JOIN [dbo].[Customers] CustomersR 
        ON CustomersL.[CustomerID] <> CustomersR.[CustomerID] AND CustomersL.[City] = CustomersR.[City]
ORDER BY CustomersL.[CustomerID];