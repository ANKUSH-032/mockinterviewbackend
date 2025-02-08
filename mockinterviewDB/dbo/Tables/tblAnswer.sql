CREATE TABLE [dbo].[tblAnswer] (
    [AnswerID]            INT            IDENTITY (1, 1) NOT NULL,
    [CandidateID]         INT            NOT NULL,
    [QuestionID]          INT            NOT NULL,
    [NumberOfInterviewID] INT            NOT NULL,
    [AnswerText]          NVARCHAR (MAX) NULL,
    [CreatedOn]           DATETIME       NULL,
    [CreatedBy]           VARCHAR (50)   NULL,
    [UpdatedOn]           DATETIME       NULL,
    [UpdatedBy]           VARCHAR (50)   NULL,
    [DeletedOn]           DATETIME       NULL,
    [DeletedBy]           VARCHAR (50)   NULL,
    [IsDeleted]           BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([AnswerID] ASC),
    CONSTRAINT [FK_tblAnswer_tblNumberOfInterview] FOREIGN KEY ([NumberOfInterviewID]) REFERENCES [dbo].[tblNumberOfInterview] ([NumberOfInterviewID])
);

