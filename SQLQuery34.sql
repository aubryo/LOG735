USE [LOG735]
GO

ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK__Users__UserProfi__08B54D69]
GO

ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK__Users__ChosenSch__09A971A2]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 29/07/2018 22:49:18 ******/
DROP TABLE [dbo].[Users]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 29/07/2018 22:49:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationNumber] [varchar](15) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[UserPassword] [varchar](50) NOT NULL,
	[UserProfileId] [int] NOT NULL,
	[ChosenScheduleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([ChosenScheduleId])
REFERENCES [dbo].[Schedules] ([ScheduleId])
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfiles] ([UserProfileId])
GO


