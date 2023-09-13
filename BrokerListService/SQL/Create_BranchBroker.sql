USE BrokerList

CREATE TABLE [dbo].[BranchBroker](
	[BranchBrokerId] [int] IDENTITY(1,1) NOT NULL,
	[HeadquarterBrokerId] [int] NOT NULL,
	[PublicationDate] [datetime],
	[Code] [varchar](20) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[EstablishmentDate] [datetime],
	[Address] [nvarchar](100),
	[Telephone] [varchar](20),
	[CreateUser] [varchar](20),
	[CreateDate] [datetime],
	[UpdateUser] [varchar](20),
	[UpdateDate] [datetime],
	PRIMARY KEY (BranchBrokerId), /*PK*/
);

ALTER TABLE [dbo].[BranchBroker] WITH CHECK ADD
CONSTRAINT [FK_BranchBroker_HeadquarterBrokerId] FOREIGN KEY([HeadquarterBrokerId])
REFERENCES [dbo].[HeadquarterBroker] ([HeadquarterBrokerId]);