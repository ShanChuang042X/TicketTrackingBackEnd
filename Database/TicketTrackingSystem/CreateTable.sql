USE [TicketTrackingSystem]
GO

/****** 物件: Table [dbo].[Ticket] 指令碼日期: 2022/5/2 上午 01:23:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ticket] (
    [TicketID]    NVARCHAR (50)  NOT NULL,
    [Status]      NVARCHAR (50)  NULL,
    [TicketType]  NVARCHAR (50)  NULL,
    [Applicant]   NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (200) NOT NULL,
    [Summary]     NVARCHAR (50)  NOT NULL,
    [Solver]      NVARCHAR (50)  NULL,
    [CreateTime]  DATETIME       NOT NULL,
    [UpdateTime]  DATETIME       NULL,
    [Severity]    NVARCHAR (50)  NULL,
    [Priority]    NVARCHAR (50)  NULL,
    [RDRemark]    NVARCHAR (200) NULL
);
