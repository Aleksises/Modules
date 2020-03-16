SET IDENTITY_INSERT Products ON
IF NOT EXISTS (SELECT ProductID FROM Products WHERE ProductID = 1)
BEGIN    
    INSERT Products(ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued) VALUES(1,'Chai',1,1,'10 boxes x 20 bags',18,39,0,10,0)
END
IF NOT EXISTS (SELECT ProductID FROM Products WHERE ProductID = 2)
BEGIN    
    INSERT Products(ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued) VALUES(2,'Chang',1,1,'24 - 12 oz bottles',19,17,40,25,0)
END
IF NOT EXISTS (SELECT ProductID FROM Products WHERE ProductID = 3)
BEGIN    
    INSERT Products(ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued) VALUES(3,'Aniseed Syrup',1,2,'12 - 550 ml bottles',10,13,70,25,0)
END
IF NOT EXISTS (SELECT ProductID FROM Products WHERE ProductID = 4)
BEGIN    
    INSERT Products(ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued) VALUES(4,'Chef Anton''s Cajun Seasoning',2,2,'48 - 6 oz jars',22,53,0,0,0)
END
IF NOT EXISTS (SELECT ProductID FROM Products WHERE ProductID = 5)
BEGIN    
    INSERT Products(ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued) VALUES(5,'Chef Anton''s Gumbo Mix',2,2,'36 boxes',21.35,0,0,0,1)
END
SET IDENTITY_INSERT Products OFF