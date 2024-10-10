IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_InsertUpdateCustomers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].SP_InsertUpdateCustomers
GO

CREATE PROCEDURE dbo.SP_InsertUpdateCustomers
@CustomerNo bigint,
@CustomerName Nvarchar(255)=NULL,
@Dob Date=NULL,
@Gender varchar(100)=NULL
AS
declare @maxCustNo bigint=0
declare @gend bigint=1;
BEGIN
select @gend=id from Genders where LOWER(Descriptions)=LOWER(@Gender)
IF EXISTS(SELECT 1 FROM Customers WHERE CustomerNumber=@CustomerNo)
	BEGIN
		UPDATE Customers SET CustomerName =@CustomerName,DOB=@Dob,Gender=@gend  WHERE CustomerNumber=@CustomerNo
	END
ELSE
BEGIN 
	SELECT @maxCustNo = MAX(ISNULL(CustomerNumber,0)) FROM Customers
	INSERT INTO Customers (CustomerNumber,CustomerName,DOB,Gender)
	VALUES(@maxCustNo+1,@CustomerName,@Dob,@gend)
END
END