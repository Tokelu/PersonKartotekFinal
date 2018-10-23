CREATE TABLE [dbo].[PersonAddressRelations]
(
	[PersonID] INT NOT NULL , 
    [AddressID] INT NOT NULL, 
    PRIMARY KEY ([PersonID], [AddressID])
)
GO
ALTER TABLE [dbo].[PersonAddressRelations]  ADD  CONSTRAINT [FK_PersonAddressRelations_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([AddressID])
GO

ALTER TABLE [dbo].[PersonAddressRelations] CHECK CONSTRAINT [FK_PersonAddressRelations_Address]
GO

ALTER TABLE [dbo].[PersonAddressRelations]  ADD  CONSTRAINT [FK_PersonAddressRelations_Person] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO

ALTER TABLE [dbo].[PersonAddressRelations] CHECK CONSTRAINT [FK_PersonAddressRelations_Person]
GO


