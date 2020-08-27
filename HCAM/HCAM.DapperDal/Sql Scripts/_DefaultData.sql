

INSERT INTO [dbo].[CodeMappers]
           ([MapperName], [IsDefault])
     VALUES
           ('Default Mapping', -1)
GO
INSERT INTO [dbo].[ActionCodes]
			([Code], [Name], [Description], [IsFee], [IsAsset], [IsLiability], [IsEquity])
     VALUES
           (10000, 'Depenses', 'Depenses: beaucoup beaucoup', 1, 0, 0, 0)
GO

