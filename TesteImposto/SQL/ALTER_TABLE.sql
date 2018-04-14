use [Teste]
go

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

ALTER TABLE NotaFiscalItem
	ADD BaseIpi decimal(18,5) null,
		AliquotaIpi decimal(18,5) null,
		ValorIpi decimal(18,5) null,
		Desconto decimal(18,5) null;

GO

SET ANSI_PADDING OFF
GO