USE [ESB]
GO
/****** Object:  Table [dbo].[Bomberos]    Script Date: 22/3/2023 2:38:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bomberos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[Fecha_Nacimiento] [datetime] NOT NULL,
 CONSTRAINT [PK_Bomberos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registro_Incendios]    Script Date: 22/3/2023 2:38:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registro_Incendios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Direccion] [nvarchar](250) NOT NULL,
	[Id_BomberoCargo] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Id_Usuarios_registros] [int] NOT NULL,
 CONSTRAINT [PK_Registro_Incendios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 22/3/2023 2:38:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NombreRoles] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuarioRoles]    Script Date: 22/3/2023 2:38:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Usuario] [int] NOT NULL,
	[Id_Roles] [int] NOT NULL,
 CONSTRAINT [PK_UsuarioRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 22/3/2023 2:38:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[CorreoElectronico] [nvarchar](250) NOT NULL,
	[Contraseña] [nvarchar](250) NOT NULL,
	[ClaveSeguridad] [nvarchar](250) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[UltimoAcceso] [datetime] NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bomberos] ON 
GO
INSERT [dbo].[Bomberos] ([Id], [Nombre], [Apellido], [Fecha_Nacimiento]) VALUES (1, N'Luis Eduardo', N'Beltre', CAST(N'2023-03-22T17:20:23.717' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Bomberos] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([Id], [NombreRoles]) VALUES (1, N'Administrador')
GO
INSERT [dbo].[Roles] ([Id], [NombreRoles]) VALUES (2, N'Bombero')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[UsuarioRoles] ON 
GO
INSERT [dbo].[UsuarioRoles] ([Id], [Id_Usuario], [Id_Roles]) VALUES (1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[UsuarioRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 
GO
INSERT [dbo].[Usuarios] ([Id], [Nombre], [Apellido], [CorreoElectronico], [Contraseña], [ClaveSeguridad], [FechaCreacion], [UltimoAcceso], [Estado]) VALUES (1, N'Luis', N'Beltre', N'compaq_eduardo@hotmail.com', N'Db/HBHl1BFaAD1sNueDA2x8q4GbKwtmbRNL6uONNfAg=', N'2203YY30*$#dsf354#587!@78e5d732-df98-4a19-9ce4-3fce6361a61fddfdsff435#$', CAST(N'2023-03-22T13:54:30.693' AS DateTime), CAST(N'2023-03-22T14:24:04.303' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[Registro_Incendios]  WITH CHECK ADD  CONSTRAINT [FK_Registro_Incendios_Bomberos] FOREIGN KEY([Id_BomberoCargo])
REFERENCES [dbo].[Bomberos] ([Id])
GO
ALTER TABLE [dbo].[Registro_Incendios] CHECK CONSTRAINT [FK_Registro_Incendios_Bomberos]
GO
ALTER TABLE [dbo].[Registro_Incendios]  WITH CHECK ADD  CONSTRAINT [FK_Registro_Incendios_Usuarios] FOREIGN KEY([Id_Usuarios_registros])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Registro_Incendios] CHECK CONSTRAINT [FK_Registro_Incendios_Usuarios]
GO
ALTER TABLE [dbo].[UsuarioRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioRoles_Roles] FOREIGN KEY([Id_Roles])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UsuarioRoles] CHECK CONSTRAINT [FK_UsuarioRoles_Roles]
GO
ALTER TABLE [dbo].[UsuarioRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioRoles_Usuarios] FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[UsuarioRoles] CHECK CONSTRAINT [FK_UsuarioRoles_Usuarios]
GO
