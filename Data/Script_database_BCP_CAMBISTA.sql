USE [BCP_CAMBISTA]
GO
/****** Object:  Schema [Authorization]    Script Date: 08/04/2022 11:23:27 a.m. ******/
CREATE SCHEMA [Authorization]
GO
/****** Object:  Schema [Exchange]    Script Date: 08/04/2022 11:23:27 a.m. ******/
CREATE SCHEMA [Exchange]
GO
/****** Object:  Table [Authorization].[TbUsersJwt]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Authorization].[TbUsersJwt](
	[StCodUserJwt] [nvarchar](50) NOT NULL,
	[StUserName] [nvarchar](250) NOT NULL,
	[StPassword] [nvarchar](250) NOT NULL,
	[StSalt] [nvarchar](250) NOT NULL,
	[BlIsActive] [bit] NOT NULL,
	[BlIsDeleted] [bit] NOT NULL,
 CONSTRAINT [pk_usersjwt] PRIMARY KEY CLUSTERED 
(
	[StCodUserJwt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Exchange].[TbCurrency]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Exchange].[TbCurrency](
	[StCodCurrency] [nvarchar](50) NOT NULL,
	[StDescription] [varchar](3) NULL,
	[BlIsDeleted] [bit] NOT NULL,
 CONSTRAINT [Pk_currency] PRIMARY KEY CLUSTERED 
(
	[StCodCurrency] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Exchange].[TbExchangeRate]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Exchange].[TbExchangeRate](
	[StCodExchangeRate] [nvarchar](50) NOT NULL,
	[StDate] [varchar](10) NOT NULL,
	[StCodCurrencyOrigin] [nvarchar](50) NOT NULL,
	[StCodCurrencyDestination] [nvarchar](50) NOT NULL,
	[DcAmountBuy] [decimal](10, 4) NOT NULL,
	[DcAmountSale] [decimal](10, 4) NOT NULL,
	[BlIsDeleted] [bit] NOT NULL,
 CONSTRAINT [Pk_exchangerate] PRIMARY KEY CLUSTERED 
(
	[StCodExchangeRate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [Authorization].[TbUsersJwt] ADD  DEFAULT ((0)) FOR [BlIsActive]
GO
ALTER TABLE [Authorization].[TbUsersJwt] ADD  DEFAULT ((0)) FOR [BlIsDeleted]
GO
ALTER TABLE [Exchange].[TbCurrency] ADD  DEFAULT ((0)) FOR [BlIsDeleted]
GO
ALTER TABLE [Exchange].[TbExchangeRate] ADD  CONSTRAINT [DF__TbExchang__BlIsD__173876EA]  DEFAULT ((0)) FOR [BlIsDeleted]
GO
/****** Object:  StoredProcedure [Authorization].[sp_TbUsersJwt_del]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
create procedure [Authorization].[sp_TbUsersJwt_del]
(
	@StCodUserJwt nVarchar(50),
	@InRowsAffected int output
)
as
begin
set nocount on

update [Authorization].[TbUsersJwt]
set
	BlIsDeleted = 1
where
	BlIsDeleted = 0
	and StCodUserJwt = @StCodUserJwt

set @InRowsAffected = @@rowcount

end

GO
/****** Object:  StoredProcedure [Authorization].[sp_TbUsersJwt_ins]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
create procedure [Authorization].[sp_TbUsersJwt_ins]
(
	@StCodUserJwt nVarchar(50),
	@StUserName nVarchar(250),
	@StPassword nVarchar(250),
	@StSalt nVarchar(250),
	@BlIsActive Bit,
	@InRowsAffected int output
)
as
begin
set nocount on

insert into [Authorization].[TbUsersJwt]
(
	StCodUserJwt,
	StUserName,
	StPassword,
	StSalt,
	BlIsActive
)
values
(
	@StCodUserJwt,
	@StUserName,
	@StPassword,
	@StSalt,
	@BlIsActive
)

set @InRowsAffected = @@rowcount

end

GO
/****** Object:  StoredProcedure [Authorization].[sp_TbUsersJwt_sel]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
create procedure [Authorization].[sp_TbUsersJwt_sel]
as
begin
set nocount on

select
	StCodUserJwt,
	StUserName,
	StPassword,
	StSalt,
	BlIsActive
from [Authorization].[TbUsersJwt]
where
	BlIsDeleted = 0

end

GO
/****** Object:  StoredProcedure [Authorization].[sp_TbUsersJwt_selby]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
create procedure [Authorization].[sp_TbUsersJwt_selby]
(
	@StCodUserJwt nVarchar(50)
)
as
begin
set nocount on

select
	StCodUserJwt,
	StUserName,
	StPassword,
	StSalt,
	BlIsActive
from [Authorization].[TbUsersJwt]
where
	BlIsDeleted = 0
	and StCodUserJwt = @StCodUserJwt
end

GO
/****** Object:  StoredProcedure [Authorization].[sp_TbUsersJwt_upd]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
create procedure [Authorization].[sp_TbUsersJwt_upd]
(
	@StCodUserJwt nVarchar(50),
	@StUserName nVarchar(250),
	@StPassword nVarchar(250),
	@StSalt nVarchar(250),
	@BlIsActive Bit,
	@InRowsAffected int output
)
as
begin
set nocount on

update [Authorization].[TbUsersJwt]
set
	StUserName = @StUserName,
	StPassword = @StPassword,
	StSalt = @StSalt,
	BlIsActive = @BlIsActive
where
	BlIsDeleted = 0
	and StCodUserJwt = @StCodUserJwt

set @InRowsAffected = @@rowcount

end

GO
/****** Object:  StoredProcedure [Exchange].[sp_TbCurrency_del]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
create procedure [Exchange].[sp_TbCurrency_del]
(
	@StCodCurrency nVarchar(50),
	@InRowsAffected int output
)
as
begin
set nocount on

update [Exchange].[TbCurrency]
set
	BlIsDeleted = 1
where
	BlIsDeleted = 0
	and StCodCurrency = @StCodCurrency

set @InRowsAffected = @@rowcount

end

GO
/****** Object:  StoredProcedure [Exchange].[sp_TbCurrency_ins]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
create procedure [Exchange].[sp_TbCurrency_ins]
(
	@StCodCurrency nVarchar(50),
	@StDescription Varchar(3),
	@InRowsAffected int output
)
as
begin
set nocount on

insert into [Exchange].[TbCurrency]
(
	StCodCurrency,
	StDescription
)
values
(
	@StCodCurrency,
	@StDescription
)

set @InRowsAffected = @@rowcount

end

GO
/****** Object:  StoredProcedure [Exchange].[sp_TbCurrency_sel]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
create procedure [Exchange].[sp_TbCurrency_sel]
as
begin
set nocount on

select
	StCodCurrency,
	StDescription
from [Exchange].[TbCurrency]
where
	BlIsDeleted = 0

end

GO
/****** Object:  StoredProcedure [Exchange].[sp_TbCurrency_selby]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
create procedure [Exchange].[sp_TbCurrency_selby]
(
	@StCodCurrency nVarchar(50)
)
as
begin
set nocount on

select
	StCodCurrency,
	StDescription
from [Exchange].[TbCurrency]
where
	BlIsDeleted = 0
	and StCodCurrency = @StCodCurrency
end

GO
/****** Object:  StoredProcedure [Exchange].[sp_TbCurrency_upd]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
create procedure [Exchange].[sp_TbCurrency_upd]
(
	@StCodCurrency nVarchar(50),
	@StDescription Varchar(3),
	@InRowsAffected int output
)
as
begin
set nocount on

update [Exchange].[TbCurrency]
set
	StDescription = @StDescription
where
	BlIsDeleted = 0
	and StCodCurrency = @StCodCurrency

set @InRowsAffected = @@rowcount

end

GO
/****** Object:  StoredProcedure [Exchange].[sp_TbExchangeRate_del]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
create procedure [Exchange].[sp_TbExchangeRate_del]
(
	@StCodExchangeRate nVarchar(50),
	@InRowsAffected int output
)
as
begin
set nocount on

update [Exchange].[TbExchangeRate]
set
	BlIsDeleted = 1
where
	BlIsDeleted = 0
	and StCodExchangeRate = @StCodExchangeRate

set @InRowsAffected = @@rowcount

end

GO
/****** Object:  StoredProcedure [Exchange].[sp_TbExchangeRate_ins]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
CREATE procedure [Exchange].[sp_TbExchangeRate_ins]
(
	@StCodExchangeRate nVarchar(50),
	@StDate Varchar(10),
	@StCodCurrencyOrigin nVarchar(50),
	@StCodCurrencyDestination nVarchar(50),
	@DcAmountBuy Decimal(10,4),
	@DcAmountSale Decimal(10,4),
	@InRowsAffected int output
)
as
begin
set nocount on

insert into [Exchange].[TbExchangeRate]
(
	StCodExchangeRate,
	StDate,
	StCodCurrencyOrigin,
	StCodCurrencyDestination,
	DcAmountBuy,
	DcAmountSale
)
values
(
	@StCodExchangeRate,
	@StDate,
	@StCodCurrencyOrigin,
	@StCodCurrencyDestination,
	@DcAmountBuy,
	@DcAmountSale
)

set @InRowsAffected = @@rowcount

end

GO
/****** Object:  StoredProcedure [Exchange].[sp_TbExchangeRate_sel]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
CREATE procedure [Exchange].[sp_TbExchangeRate_sel]
as
begin
set nocount on

select
	t1.StCodExchangeRate,
	t1.StDate,
	t1.StCodCurrencyOrigin,
	t2.StDescription as StCurrencyOrigin,
	t1.StCodCurrencyDestination,
	t3.StDescription as StCurrencyDestination,
	t1.DcAmountBuy,
	t1.DcAmountSale
from [Exchange].[TbExchangeRate] t1
inner join [Exchange].[TbCurrency] t2 on t1.StCodCurrencyOrigin = t2.StCodCurrency and t2.BlIsDeleted = 0
inner join [Exchange].[TbCurrency] t3 on t1.StCodCurrencyDestination = t3.StCodCurrency and t3.BlIsDeleted = 0
where
	t1.BlIsDeleted = 0
order by convert(datetime, t1.StDate) desc

end

GO
/****** Object:  StoredProcedure [Exchange].[sp_TbExchangeRate_selby]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
CREATE procedure [Exchange].[sp_TbExchangeRate_selby]
(
	@StCodExchangeRate nVarchar(50)
)
as
begin
set nocount on

select
	t1.StCodExchangeRate,
	t1.StDate,
	t1.StCodCurrencyOrigin,
	t2.StDescription as StCurrencyOrigin,
	t1.StCodCurrencyDestination,
	t3.StDescription as StCurrencyDestination,
	t1.DcAmountBuy,
	t1.DcAmountSale
from [Exchange].[TbExchangeRate] t1
inner join [Exchange].[TbCurrency] t2 on t1.StCodCurrencyOrigin = t2.StCodCurrency and t2.BlIsDeleted = 0
inner join [Exchange].[TbCurrency] t3 on t1.StCodCurrencyDestination = t3.StCodCurrency and t3.BlIsDeleted = 0
where
	t1.BlIsDeleted = 0
	and t1.StCodExchangeRate = @StCodExchangeRate
end

GO
/****** Object:  StoredProcedure [Exchange].[sp_TbExchangeRate_selbyDate]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
CREATE procedure [Exchange].[sp_TbExchangeRate_selbyDate]
(
	@StDate Varchar(10),
	@StCodCurrencyOrigin nVarchar(50),
	@StCodCurrencyDestination nVarchar(50)
)
as
begin
set nocount on

select
	t1.StCodExchangeRate,
	t1.StDate,
	t1.StCodCurrencyOrigin,
	t2.StDescription as StCurrencyOrigin,
	t1.StCodCurrencyDestination,
	t3.StDescription as StCurrencyDestination,
	t1.DcAmountBuy,
	t1.DcAmountSale
from [Exchange].[TbExchangeRate] t1
inner join [Exchange].[TbCurrency] t2 on t1.StCodCurrencyOrigin = t2.StCodCurrency and t2.BlIsDeleted = 0
inner join [Exchange].[TbCurrency] t3 on t1.StCodCurrencyDestination = t3.StCodCurrency and t3.BlIsDeleted = 0
where
	t1.BlIsDeleted = 0
	and t1.StDate = @StDate
	and (
		(t1.StCodCurrencyOrigin = @StCodCurrencyOrigin and t1.StCodCurrencyDestination = @StCodCurrencyDestination)
		or
		(t1.StCodCurrencyDestination = @StCodCurrencyOrigin and t1.StCodCurrencyOrigin = @StCodCurrencyDestination)
		)
end

GO
/****** Object:  StoredProcedure [Exchange].[sp_TbExchangeRate_upd]    Script Date: 08/04/2022 11:23:28 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************/
-- Responsable : David Bringas
-- Fecha de creación : 31/03/2022 11:33:02
/********************************************************************/
CREATE procedure [Exchange].[sp_TbExchangeRate_upd]
(
	@StCodExchangeRate nVarchar(50),
	@StDate Varchar(10),
	@StCodCurrencyOrigin nVarchar(50),
	@StCodCurrencyDestination nVarchar(50),
	@DcAmountBuy Decimal(10,4),
	@DcAmountSale Decimal(10,4),
	@InRowsAffected int output
)
as
begin
set nocount on

update [Exchange].[TbExchangeRate]
set
	StDate = @StDate,
	StCodCurrencyOrigin = @StCodCurrencyOrigin,
	StCodCurrencyDestination = @StCodCurrencyDestination,
	DcAmountBuy = @DcAmountBuy,
	DcAmountSale = @DcAmountSale
where
	BlIsDeleted = 0
	and StCodExchangeRate = @StCodExchangeRate

set @InRowsAffected = @@rowcount

end

GO
