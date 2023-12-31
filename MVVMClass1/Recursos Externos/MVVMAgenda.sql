CREATE DATABASE MVVMAgenda;
USE [MVVMAgenda]
GO
/****** Object:  Table [dbo].[Recordatorio]    Script Date: 29/07/2023 10:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recordatorio](
	[IdRecordatorio] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[Recordatorio] [varchar](2000) NULL,
	[Fecha] [datetime] NULL,
	[Llamado] [int] NULL,
 CONSTRAINT [PK_Recordatorio] PRIMARY KEY CLUSTERED 
(
	[IdRecordatorio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 29/07/2023 10:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Correo] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Imagen] [varchar](500) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Recordatorio] ON 

INSERT [dbo].[Recordatorio] ([IdRecordatorio], [IdUsuario], [Recordatorio], [Fecha], [Llamado]) VALUES (1012, 1, N'Este es un recordatorio de pruebas dentro de esta cuenta registrada dentro de la app de agenda, este recordatorio esta pensado para recordarse en una semana', CAST(N'2023-08-05T18:47:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Recordatorio] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Correo], [Password], [Imagen]) VALUES (1, N'pipelonso', N'andresfelipeibanezcuta2@gmail.com', N'lolxd24', N'~/Imagenes/UserPicks/10411620_1974.jpg')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
ALTER TABLE [dbo].[Recordatorio]  WITH CHECK ADD  CONSTRAINT [FK_Recordatorio_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Recordatorio] CHECK CONSTRAINT [FK_Recordatorio_Usuario]
GO
/****** Object:  StoredProcedure [dbo].[DeleteTaskWithId]    Script Date: 29/07/2023 10:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DeleteTaskWithId] 
@id int
as 
BEGIN
DELETE Recordatorio WHERE IdRecordatorio = @id
END
GO
/****** Object:  StoredProcedure [dbo].[EditTaskWithId]    Script Date: 29/07/2023 10:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[EditTaskWithId]
@id int,
@Recordatorio varchar(2000),
@Fecha datetime
as
BEGIN
UPDATE Recordatorio SET Recordatorio = @Recordatorio , Fecha = @Fecha , Llamado = 0 WHERE IdRecordatorio = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[GetExpired]    Script Date: 29/07/2023 10:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetExpired]
@CorreoUsuario VARCHAR(100)
AS
BEGIN
DECLARE @USERID INT = (SELECT IdUsuario FROM Usuario WHERE Correo =  @CorreoUsuario);


SELECT * FROM Recordatorio WHERE IdUsuario = @USERID AND Fecha <= GETDATE() AND Llamado <= 3
END

GO
/****** Object:  StoredProcedure [dbo].[getTaskByMail]    Script Date: 29/07/2023 10:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[getTaskByMail] 
@correo varchar(50)
as 
begin
SELECT Recordatorio.* FROM Usuario , Recordatorio WHERE Recordatorio.IdUsuario = Usuario.IdUsuario AND Usuario.Correo = @correo;
end
GO
/****** Object:  StoredProcedure [dbo].[InsertTasks]    Script Date: 29/07/2023 10:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[InsertTasks]
@correo varchar(100),
@Recordatorio VARCHAR(5000),
@Fecha Datetime

as
BEGIN
DECLARE @idUsuario int;

SELECT @idUsuario = Usuario.IdUsuario FROM Usuario WHERE Usuario.Correo = @correo;

INSERT INTO Recordatorio (IdUsuario , Recordatorio , Fecha , Llamado) 
VALUES (@idUsuario , @Recordatorio , @Fecha , 1);
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateExpired]    Script Date: 29/07/2023 10:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateExpired]
@idRecordatorio INT,
@LlamadoSet INT
as
BEGIN

	UPDATE Recordatorio SET Llamado = @LlamadoSet WHERE IdRecordatorio = @idRecordatorio

END
GO
