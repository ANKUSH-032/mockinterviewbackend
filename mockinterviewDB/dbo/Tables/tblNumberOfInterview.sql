CREATE TABLE [dbo].[tblNumberOfInterview] (
    [NumberOfInterviewID] INT            IDENTITY (1, 1) NOT NULL,
    [CandidateID]         INT            NOT NULL,
    [Score]               DECIMAL (5, 2) NOT NULL,
    [CreatedOn]           DATETIME       NULL,
    [CreatedBy]           VARCHAR (50)   NULL,
    [CheckedBy]           VARCHAR (50)   NULL,
    [UpdatedOn]           DATETIME       NULL,
    [UpdatedBy]           VARCHAR (50)   NULL,
    [DeletedOn]           DATETIME       NULL,
    [DeletedBy]           VARCHAR (50)   NULL,
    [IsDeleted]           BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([NumberOfInterviewID] ASC)
);

