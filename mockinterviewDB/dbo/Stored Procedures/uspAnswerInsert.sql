            
/*            
 DECLARE @data as bulkQuestionInsert                
INSERT INTO @data            
VALUES ( 'A1 2','c#'),    
('b1v','c1#')            
 exec [uspQuestionInsert] '' , @data            
*/            
CREATE PROCEDURE [dbo].[uspAnswerInsert]              
@CreatedBy VARCHAR(50),  
@CandidateID VARCHAR(50),
@NumberOfInterviewID BIGINT,
@AnswerInsertType AnswerInsertType READONLY              
             
AS              
BEGIN              
SET NOCOUNT ON;              
DECLARE @Status bit              
DECLARE @Message varchar(500)              
DECLARE @Data varchar(50)            
 BEGIN TRY    
 
 INSERT INTO [dbo].[tblAnswer]            
 (QuestionID,
  AnswerText,
  CandidateID,
  NumberOfInterviewID
 )              
 (SELECT               
 QuestionID,
 AnswerText,
 @CandidateID,
 @NumberOfInterviewID
 from @AnswerInsertType)  
 
 SELECT  1 AS [Status], 'Insert Successfully' AS [Message], 1 AS TotalRecords;    

             
  END TRY              
           
              
  BEGIN CATCH  
  
 SET @Message = ERROR_MESSAGE();                
                
  DECLARE @ErrorSeverity INT = ERROR_SEVERITY();                
  DECLARE @ErrorState INT = ERROR_STATE();                  
  RAISERROR(@Message, @ErrorSeverity, @ErrorState);              
  END CATCH              
 SELECT @Status [Status], @Message [Message] , 1 [Data]                
END