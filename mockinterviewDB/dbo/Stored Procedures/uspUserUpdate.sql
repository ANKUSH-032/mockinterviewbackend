﻿  
CREATE PROCEDURE [dbo].[uspUserUpdate]    
 @UserId VARCHAR(50),  
 @FirstName VARCHAR(50),   
 @LastName VARCHAR(50),   
 @ContactNo VARCHAR(15),  
 @Email VARCHAR(150),      
 @Address VARCHAR(500),   
 @Zipcode VARCHAR(10),  
 @Gender VARCHAR(10),  
 @UpdatedBy VARCHAR(50) = null  
   
AS  
BEGIN    
SET NOCOUNT ON;  
  
 DECLARE @Status BIT = 0;    
 DECLARE @Message VARCHAR(MAX);    
  
  
 BEGIN TRY       
 BEGIN   
  
  IF EXISTS( SELECT 1 FROM [dbo].[tblUsers] WITH(NOLOCK) WHERE Email = @UserId AND IsDeleted = 1)  
  BEGIN  
  SELECT 0 AS [Status], 'User is not register in this system' AS [Message]  
  RETURN  
  END  
  
  IF  EXISTS( SELECT 1 FROM [dbo].[tblUsers] WITH(NOLOCK) WHERE UserId = @UserId AND IsDeleted=0)  
  BEGIN   
   Update [dbo].[tblUsers]  
   SET  
      
      FirstName = @FirstName,  
      LastName = @LastName,  
      Email= @Email,  
      ContactNo = @ContactNo,  
      Address = @Address,  
                 
               Zipcode = @Zipcode,  
               Gender = @Gender,  
      UpdatedOn = GETUTCDATE(),   
      UpdatedBy = @UpdatedBy  
      WHERE UserId = @UserId AND IsDeleted=0
   SET @Status = 1;    
   SET @Message = 'Record updated successfully.';  
  END  
  ElSE  
  BEGIN  
    SELECT 0 AS [Status], 'Record with same name is already existed.' AS [Message]  
    RETURN  
  END  
 END     
 END TRY     
 BEGIN CATCH    
  SET @Message = ERROR_MESSAGE();    
    
  DECLARE @ErrorSeverity INT = ERROR_SEVERITY();    
  DECLARE @ErrorState INT = ERROR_STATE();      
  RAISERROR(@Message, @ErrorSeverity, @ErrorState);    
 END CATCH    
  
 SELECT @Status [Status], @Message [Message] , @UserId [Data]    
END