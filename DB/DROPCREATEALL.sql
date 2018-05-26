USE [PicDB]
GO

/****** Object:  Trigger [t_DeletedPicture]    Script Date: 01/04/2018 20:09:25 ******/
DROP TRIGGER [dbo].[t_DeletedPicture]
GO

/****** Object:  Trigger [t_UpdatePictureEXIF]    Script Date: 01/04/2018 20:09:25 ******/
DROP TRIGGER [dbo].[t_UpdatePictureEXIF]
GO

DECLARE @sql NVARCHAR(MAX);
SET @sql = N'';

SELECT @sql = @sql + N'
  ALTER TABLE ' + QUOTENAME(s.name) + N'.'
  + QUOTENAME(t.name) + N' DROP CONSTRAINT '
  + QUOTENAME(c.name) + ';'
FROM sys.objects AS c
INNER JOIN sys.tables AS t
ON c.parent_object_id = t.[object_id]
INNER JOIN sys.schemas AS s 
ON t.[schema_id] = s.[schema_id]
WHERE c.[type] IN ('D','C','F','PK','UQ')
ORDER BY c.[type];

EXEC sys.sp_executesql @sql;

/****** Object:  Table [dbo].[Pictures]    Script Date: 01/04/2018 20:09:25 ******/
DROP TABLE [dbo].[Pictures]
GO

/****** Object:  Table [dbo].[EXIF]    Script Date: 01/04/2018 20:09:25 ******/
DROP TABLE [dbo].[EXIF]
GO

/****** Object:  Table [dbo].[Cameras]    Script Date: 01/04/2018 20:09:25 ******/
DROP TABLE [dbo].[Cameras]
GO

/****** Object:  Table [dbo].[IPTC]    Script Date: 01/04/2018 20:09:25 ******/
DROP TABLE [dbo].[IPTC]
GO

/****** Object:  Table [dbo].[Photographers]    Script Date: 01/04/2018 20:10:37 ******/
DROP TABLE [dbo].[Photographers]
GO
 
/****** Object:  Table [dbo].[Photographers]    Script Date: 01/04/2018 20:10:37 ******/
SET ANSI_NULLS ON
GO
 
SET QUOTED_IDENTIFIER ON
GO
 
CREATE TABLE [dbo].[Photographers](
    [PG_ID] [int] IDENTITY(1,1) NOT NULL,
    [FirstName] [varchar](100) NULL,
    [LastName] [varchar](50) NOT NULL,
    [BirthDay] [date] NULL,
    [Notes_PG] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
    [PG_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[IPTC]    Script Date: 01/04/2018 20:09:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IPTC](
	[IPTC_ID] [int] IDENTITY(1,1) NOT NULL,
	[Keywords] [varchar](256) NULL,
	[ByLine] [varchar](32) NULL,
	[CopyrightNotice] [varchar](128) NULL,
	[Headline] [varchar](256) NULL,
	[Caption] [varchar](2000) NULL,
	[IPTC_FK_Pic_ID] [int] NOT NULL UNIQUE,
PRIMARY KEY CLUSTERED 
(
	[IPTC_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[IPTC_FK_Pic_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Cameras]    Script Date: 01/04/2018 20:09:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cameras](
	[Cam_ID] [int] IDENTITY(1,1) NOT NULL,
	[Producer] [varchar](64) NOT NULL,
	[Make_Cam] [varchar](64) NULL,
	[BoughtOn] [datetime] NULL,
	[Notes_Cam] [varchar](2000) NULL,
	[ISOLimitGood] [decimal] NULL,
	[ISOLimitAcceptable] [decimal] NULL,
PRIMARY KEY CLUSTERED 
(
	[Cam_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[EXIF]    Script Date: 01/04/2018 20:09:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EXIF](
	[EXIF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Make_EXIF] [varchar](128) NULL,
	[FNumber] [decimal](18, 0) NULL,
	[ExposureTime] [decimal](18, 0) NULL,
	[ISOValue] [decimal](18, 0) NULL,
	[Flash] [bit] NULL,
	[ExposureProgram] [int] NULL,
	[EXIF_FK_Pic_ID] [int] NOT NULL UNIQUE,
PRIMARY KEY CLUSTERED 
(
	[EXIF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[EXIF_FK_Pic_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Pictures]    Script Date: 01/04/2018 20:09:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pictures](
	[Pic_ID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](128) NOT NULL,
	[FK_PG_ID] [int] NULL,
	[FK_IPTC_ID] [int] NULL,
	[FK_EXIF_ID] [int] NULL,
	[FK_Cam_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Pic_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Pictures]  WITH CHECK ADD FOREIGN KEY([FK_Cam_ID])
REFERENCES [dbo].[Cameras] ([Cam_ID])
GO

ALTER TABLE [dbo].[Pictures]  WITH CHECK ADD FOREIGN KEY([FK_EXIF_ID])
REFERENCES [dbo].[EXIF] ([EXIF_ID])
GO

ALTER TABLE [dbo].[Pictures]  WITH CHECK ADD FOREIGN KEY([FK_IPTC_ID])
REFERENCES [dbo].[IPTC] ([IPTC_ID])
GO

/****** Object:  Trigger [dbo].[t_UpdatePictureEXIF]    Script Date: 01/04/2018 20:09:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [dbo].[t_UpdatePictureEXIF] ON [dbo].[EXIF]
AFTER INSERT
AS 
BEGIN
	DECLARE @EXIF_ID INT;
	DECLARE @FK_Pic_ID INT;
	DECLARE updateCursor CURSOR FOR
	SELECT EXIF_ID, EXIF_FK_Pic_ID FROM inserted as INS

	OPEN updateCursor
	FETCH NEXT FROM updateCursor INTO @EXIF_ID, @FK_Pic_ID;
	UPDATE Pictures
			SET Pictures.FK_EXIF_ID = @EXIF_ID
			WHERE Pictures.Pic_ID = @FK_Pic_ID;
	WHILE @@FETCH_STATUS = 0
		BEGIN
			
			FETCH NEXT FROM updateCursor INTO @EXIF_ID, @FK_Pic_ID;
		END
	CLOSE updateCursor;
	DEALLOCATE updateCursor;
END
GO

ALTER TABLE [dbo].[EXIF] ENABLE TRIGGER [t_UpdatePictureEXIF]
GO

/****** Object:  Trigger [dbo].[t_UpdatePictureIPTC]    Script Date: 01/04/2018 20:09:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [dbo].[t_UpdatePictureIPTC] ON [dbo].[IPTC]
AFTER INSERT
AS 
BEGIN
	DECLARE @IPTC_ID INT;
	DECLARE @FK_Pic_ID INT;
	DECLARE updateCursor CURSOR FOR
	SELECT IPTC_ID, IPTC_FK_Pic_ID FROM inserted as INS

	OPEN updateCursor
	FETCH NEXT FROM updateCursor INTO @IPTC_ID, @FK_Pic_ID;
	UPDATE Pictures
			SET Pictures.FK_IPTC_ID = @IPTC_ID
			WHERE Pictures.Pic_ID = @FK_Pic_ID;
	WHILE @@FETCH_STATUS = 0
		BEGIN
			
			FETCH NEXT FROM updateCursor INTO @IPTC_ID, @FK_Pic_ID;
		END
	CLOSE updateCursor;
	DEALLOCATE updateCursor;
END
GO

ALTER TABLE [dbo].[IPTC] ENABLE TRIGGER [t_UpdatePictureIPTC]
GO

/****** Object:  Trigger [dbo].[t_DeletedPicture]    Script Date: 01/04/2018 20:09:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[t_DeletedPicture] ON [dbo].[Pictures]
AFTER DELETE
AS
BEGIN
	DECLARE @FK_IPTC_ID INT;
	DECLARE @FK_EXIF_ID INT;
	DECLARE deleteCursor CURSOR FOR
	SELECT FK_IPTC_ID, FK_EXIF_ID FROM deleted as DEL

	OPEN deleteCursor
	FETCH NEXT FROM deleteCursor INTO @FK_IPTC_ID, @FK_EXIF_ID;
	WHILE @@FETCH_STATUS = 0
		BEGIN
			DELETE FROM IPTC WHERE @FK_IPTC_ID = IPTC_ID
			DELETE FROM EXIF WHERE @FK_EXIF_ID = EXIF_ID
			FETCH NEXT FROM deleteCursor INTO @FK_IPTC_ID, @FK_EXIF_ID;
		END
	CLOSE deleteCursor;
	DEALLOCATE deleteCursor;
END 
GO

ALTER TABLE [dbo].[Pictures] ENABLE TRIGGER [t_DeletedPicture]
GO

/****** Object:  Trigger [dbo].[t_DeletedPicture]    Script Date: 01/04/2018 20:09:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[t_DeletedPhotographer] ON [dbo].[Photographers]
AFTER DELETE
AS
BEGIN
	DECLARE @PG_ID INT;
	DECLARE deleteCursor CURSOR FOR
	SELECT PG_ID FROM deleted as DEL

	OPEN deleteCursor
	FETCH NEXT FROM deleteCursor INTO @PG_ID;
	WHILE @@FETCH_STATUS = 0
		BEGIN
			UPDATE Pictures SET Pictures.FK_PG_ID = NULL WHERE Pictures.FK_PG_ID = @PG_ID;
			FETCH NEXT FROM deleteCursor INTO @PG_ID;
		END
	CLOSE deleteCursor;
	DEALLOCATE deleteCursor;
END 
GO

ALTER TABLE [dbo].[Pictures] ENABLE TRIGGER [t_DeletedPicture]
GO
