SET IDENTITY_INSERT Suppliers ON
IF NOT EXISTS (SELECT SupplierID FROM Suppliers WHERE SupplierID = 1)
BEGIN    
    INSERT Suppliers(SupplierID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage) VALUES(1,'Exotic Liquids','Charlotte Cooper','Purchasing Manager','49 Gilbert St.','London',NULL,'EC1 4SD','UK','(171) 555-2222',NULL,NULL)
END
IF NOT EXISTS (SELECT SupplierID FROM Suppliers WHERE SupplierID = 2)
BEGIN    
    INSERT Suppliers(SupplierID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage) VALUES(2,'New Orleans Cajun Delights','Shelley Burke','Order Administrator','P.O. Box 78934','New Orleans','LA','70117','USA','(100) 555-4822',NULL,'#CAJUN.HTM#')
END
IF NOT EXISTS (SELECT SupplierID FROM Suppliers WHERE SupplierID = 3)
BEGIN    
    INSERT Suppliers(SupplierID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage) VALUES(3,'Grandma Kelly''s Homestead','Regina Murphy','Sales Representative','707 Oxford Rd.','Ann Arbor','MI','48104','USA','(313) 555-5735','(313) 555-3349',NULL)
END
IF NOT EXISTS (SELECT SupplierID FROM Suppliers WHERE SupplierID = 4)
BEGIN    
    INSERT Suppliers(SupplierID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage) VALUES(4,'Tokyo Traders','Yoshi Nagase','Marketing Manager','9-8 Sekimai Musashino-shi','Tokyo',NULL,'100','Japan','(03) 3555-5011',NULL,NULL)
END
IF NOT EXISTS (SELECT SupplierID FROM Suppliers WHERE SupplierID = 5)
BEGIN    
    INSERT Suppliers(SupplierID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage) VALUES(5,'Cooperativa de Quesos ''Las Cabras''','Antonio del Valle Saavedra','Export Administrator','Calle del Rosal 4','Oviedo','Asturias','33007','Spain','(98) 598 76 54',NULL,NULL)
END
SET IDENTITY_INSERT Suppliers OFF