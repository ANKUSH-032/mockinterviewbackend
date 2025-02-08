﻿CREATE TABLE [dbo].[tblCandidate] (
    [UserId]       VARCHAR (50)    NOT NULL,
    [FirstName]    VARCHAR (50)    NOT NULL,
    [LastName]     VARCHAR (50)    NOT NULL,
    [Email]        VARCHAR (50)    NOT NULL,
    [Gender]       VARCHAR (10)    NOT NULL,
    [ContactNo]    NVARCHAR (50)   NOT NULL,
    [PasswordHash] VARBINARY (MAX) NOT NULL,
    [PasswordSalt] VARBINARY (MAX) NOT NULL,
    [RoleId]       VARCHAR (50)    NOT NULL,
    [Active]       BIT             CONSTRAINT [DF_tblCandidate_Active] DEFAULT ((1)) NOT NULL,
    [Address]      VARCHAR (500)   NOT NULL,
    [Zipcode]      VARCHAR (10)    NOT NULL,
    [CreatedOn]    DATETIME        NULL,
    [CreatedBy]    VARCHAR (50)    NULL,
    [UpdatedOn]    DATETIME        NULL,
    [UpdatedBy]    VARCHAR (50)    NULL,
    [DeletedOn]    DATETIME        NULL,
    [DeletedBy]    VARCHAR (50)    NULL,
    [IsDeleted]    BIT             NOT NULL,
    CONSTRAINT [PK_tblCandidate] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

