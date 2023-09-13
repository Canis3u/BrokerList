USE BrokerList
GO

CREATE TABLE [dbo].[HeadquarterBroker](
	[HeadquarterBrokerId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[EstablishmentDate] [datetime],
	[Address] [nvarchar](100),
	[Telephone] [varchar](20),
	[CreateUser] [varchar](20),
	[CreateDate] [datetime],
	[UpdateUser] [varchar](20),
	[UpdateDate] [datetime],
	PRIMARY KEY (HeadquarterBrokerId) /*PK*/
)
GO