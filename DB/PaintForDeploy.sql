set ansi_padding on
go
set ansi_nulls on
go
set quoted_identifier on
go

create database [Paint]
use [Paint]

create table [dbo].[Role]
(
[ID_Role] [int] not null identity(1,1),
[Name_Role] [nvarchar] (50) not null unique,

constraint [PK_Role] primary key clustered ([ID_Role])
)
go

insert into [Role] values 
('Admin')
go
insert into [Role] values 
('Delivery')
go
insert into [Role] values 
('Product')
go
insert into [Role] values 
('ClientManager')
go
insert into [Role] values 
('Warehouse')
go

create table [dbo].[User]
(
[ID_User] [int] not null identity(1,1),
[Login] [nvarchar] (30) not null unique,
[Password] [nvarchar] (max),
[Name] [nvarchar] (50) not null,
[Surname] [nvarchar] (50) not null,
[Patromymic] [nvarchar] (50) not null,
[E_mail] [nvarchar] (50) not null unique,
[Number_Telephone] [varchar] (50) not null unique,
[Priority] [float] not null,
[Salt] [nvarchar] (max),
[Verification] [bit] not null default (0),
[Photo] [varbinary] (max),
[Role_ID] [int] not null

constraint [PK_User] primary key clustered ([ID_User]),
constraint [Len_Login] check (len([Login]) >= 8),
constraint [Len_Password] check (len([Password]) >= 8),
constraint [Dog_E_mail] check ([E_mail] like ('%@%')),
constraint [Dot_E_mail] check ([E_mail] like ('%.%')),
constraint [FK_Role_User] foreign key ([Role_ID]) references [Role]([ID_Role])
)
go

create table [dbo].[Token]
(
[ID_Token] [int] not null identity(1,1),
[Token] [nvarchar] (max),
[User_ID] [int] not null,

constraint [PK_Token] primary key clustered ([ID_Token]),
constraint [FK_Token_User] foreign key ([User_ID]) references [User]([ID_User]),
)
go

create table [dbo].[FeedBack]
(
[ID_FeedBack] [int] not null identity(1,1),
[Number_Or_E_mail] [nvarchar] (50) not null,
[End] [bit] not null default (0),
[Name_User] [nvarchar] (50) not null,
[Message] [varbinary] (max),
[User_ID] [int] null,
[Date] [datetime] not null default getdate(),
[Category] [nvarchar] (50) not null,

constraint [FK_FeedBack_User] foreign key ([User_ID]) references [User]([ID_User]),
constraint [PK_FeedBack] primary key clustered ([ID_FeedBack])
)
go

create table [dbo].[News]
(
[ID_News] [int] not null identity(1,1),
[Heading_News] [nvarchar] (150) not null,
[Text_News] [varbinary] (max) not null,
[Date] [datetime] not null default getdate(),
[User_ID] [int] not null

constraint [PK_News] primary key clustered ([ID_News]),
constraint [FK_News_User] foreign key ([User_ID]) references [User]([ID_User])
)
go

create table [dbo].[Photo_News]
(
[ID_Photo_News] [int] not null identity(1,1),
[News_ID] [int] not null,
[Photo] [varbinary] (max)

constraint [FK_News] foreign key ([News_ID]) references [News]([ID_News]),
constraint [PK_Photo_News] primary key clustered ([ID_Photo_News]),
)
go

create table [dbo].[Status_Delivery]
(
[ID_Status_Order] [int] not null identity(1,1),
[Name_Status_Order] [nvarchar] (50) unique,

constraint [PK_Status_Order] primary key clustered ([ID_Status_Order])
)
go

create table [dbo].[Ral_Catalog]
(
[ID_Ral_Catalog] [int] not null identity(1,1),
[Name_Ral] [nvarchar] (100) not null unique,
[Color_Ral] [nvarchar] (7) not null unique,
[Color_HTML] [nvarchar] (8) not null unique,

constraint [PK_Ral_Catalog] primary key clustered ([ID_Ral_Catalog])
)
go

create table [dbo].[Temp_Pulverization]
(
[ID_Temp_Pulverization] [int] not null identity(1,1),
[Degree] [int] not null,
[Time] [int] not null

constraint [PK_Temp_Pulverization] primary key clustered ([ID_Temp_Pulverization])
)
go

create table [dbo].[Type_Application]
(
[ID_Type_Application] [int] not null identity(1,1),
[Name_Type_Application] [nvarchar] (100) not null unique,

constraint [PK_Type_Application] primary key clustered ([ID_Type_Application])
)
go

create table [dbo].[Type_Surface]
(
[ID_Type_Surface] [int] not null identity(1,1),
[Name_Type_Surface] [nvarchar] (50) not null unique,

constraint [PK_Type_Surface] primary key clustered ([ID_Type_Surface])
)
go

create table [dbo].[Shine]
(
[ID_Shine] [int] not null identity(1,1),
[First_Procent] [int] not null,
[End_Procent] [int] not null,
[Name_Shine] [nvarchar] (50) not null unique,

constraint [PK_Shine] primary key clustered ([ID_Shine]),
constraint [CK_First_Procent] check ([First_Procent] >= 0 and [First_Procent] <= 100),
constraint [CK_End_Procent] check ([End_Procent] >= 0 and [End_Procent] <= 100)
)
go

create table [dbo].[City]
(
[ID_City] [int] not null identity(1,1),
[Name_City] [nvarchar] (50) not null unique,

constraint [PK_City] primary key clustered ([ID_City])
)
go

create table [dbo].[WareHouse]
(
[ID_WareHouse] [int] not null identity(1,1),
[City_ID] [int] not null,
[Adress] [nvarchar] (max),

constraint [PK_WareHouse] primary key clustered ([ID_WareHouse]),
constraint [FK_City] foreign key ([City_ID]) references [City]([ID_City])
)
go

create table [dbo].[Color]
(
[ID_Color] [int] not null identity(1,1),
[Certificate] [varbinary] (max),
[Photo] [varbinary] (max),
[Priority] [float] not null,
[Type_Application_ID] [int] not null,
[Temp_Pulverization_ID] [int] not null,
[Shine_ID] [int] not null,
[Type_Surface_ID] [int] not null,
[Ral_catalog_ID] [int] not null

constraint [PK_Color] primary key clustered ([ID_Color]),
constraint [FK_Type_Application] foreign key ([Type_Application_ID]) references [Type_Application]([ID_Type_Application]),
constraint [FK_Temp_Pulverization] foreign key ([Temp_Pulverization_ID]) references [Temp_Pulverization]([ID_Temp_Pulverization]),
constraint [FK_Shine] foreign key ([Shine_ID]) references [Shine]([ID_Shine]),
constraint [FK_Type_Surface] foreign key ([Type_Surface_ID]) references [Type_Surface]([ID_Type_Surface]),
constraint [FK_Ral_catalog] foreign key ([Ral_catalog_ID]) references [Ral_catalog]([ID_Ral_catalog])
)
go

create table [dbo].[Quantity_Colors]
(
[ID_Quantity_Colors] [int] not null identity(1,1),
[Color_ID] [int] not null unique,
[WareHouse_ID] [int] not null,
[Quantity] [int] not null,
[Price_For_KG] [float] not null,
[Date] [datetime] not null default getdate(),
[Shelf_Life] [int] not null default 1000

constraint [FK_Color] foreign key ([Color_ID]) references [Color]([ID_Color]),
constraint [FK_WareHouse] foreign key ([WareHouse_ID]) references [WareHouse]([ID_WareHouse]),
constraint [PK_Quantity_Colors] primary key clustered ([ID_Quantity_Colors]),
)
go

create table [dbo].[Discount]
(
[ID_Discount] [int] not null identity(1,1),
[Color_ID] [int] not null unique,
[Size_Dicsount] [int] not null,

constraint [FK_Discount_Color] foreign key ([Color_ID]) references [Color]([ID_Color]),
constraint [PK_Discount] primary key clustered ([ID_Discount]),
)
go

create table [dbo].[Delivery]
(
[ID_Delivery] [int] not null identity(1,1),
[Adress] [nvarchar] (max),
[Salt] [nvarchar] (max),
[Cheque] [varbinary] (max),
[City_ID] [int] not null,
[Status_Order_ID] [int] not null,
[User_ID] [int] not null,

constraint [PK_Delivery] primary key clustered ([ID_Delivery]),
constraint [FK_Delivery_City] foreign key ([City_ID]) references [City]([ID_City]),
constraint [FK_Status_Order] foreign key ([Status_Order_ID]) references [Status_Delivery]([ID_Status_Order]),
)
go

create table [dbo].[Color_Delivery]
(
[ID_Color_Delivery] [int] not null identity(1,1),
[Delivery_ID] [int] not null,
[Color_ID] [int] not null,

constraint [FK_Delivery] foreign key ([Delivery_ID]) references [Delivery]([ID_Delivery]),
constraint [FK_Delivery_Color] foreign key ([Color_ID]) references [Color]([ID_Color]),
constraint [PK_Color_Delivery] primary key clustered ([ID_Color_Delivery]),
)
go

create table [dbo].[Sold_Color]
(
[ID_Sold_Color] [int] not null identity(1,1),
[Date] [datetime] not null default getdate(),
[Delivery_ID] [int] not null,
[Price_Delivery] [float] not null

constraint [PK_Sold_Color] primary key clustered ([ID_Sold_Color]),
constraint [FK_Sold_Delivery] foreign key ([Delivery_ID]) references [Delivery]([ID_Delivery]),
)
go

drop table [Sold_Color]
go

drop table [Quantity_Colors]
go