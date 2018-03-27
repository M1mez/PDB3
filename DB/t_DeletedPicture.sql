/*    ==Skriptparameter==

    Quellserverversion : SQL Server 2016 (13.0.4001)
    Edition des Quelldatenbankmoduls : Microsoft SQL Server Express Edition
    Typ des Quelldatenbankmoduls : Eigenständige SQL Server-Instanz

    Zielserverversion : SQL Server 2017
    Edition des Zieldatenbankmoduls : Microsoft SQL Server Standard Edition
    Typ des Zieldatenbankmoduls : Eigenständige SQL Server-Instanz
*/

USE [PicDB]
GO
/****** Object:  Trigger [dbo].[t_DeletedPicture]    Script Date: 22.02.2018 17:01:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TRIGGER [dbo].[t_DeletedPicture]
ON [dbo].[Pictures]
AFTER DELETE
AS
BEGIN
	
	DECLARE @IPTC INT;
	DECLARE @EXIF INT;
	DECLARE deleteCursor CURSOR FOR
	SELECT FK_IPTC_ID, FK_EXIF_ID FROM deleted as DEL

	OPEN deleteCursor
	FETCH NEXT FROM deleteCursor INTO @IPTC, @EXIF;
	WHILE @@FETCH_STATUS = 0
		BEGIN
			DELETE FROM IPTC WHERE @IPTC = IPTC_ID
			DELETE FROM EXIF WHERE @EXIF = EXIF_ID
			FETCH NEXT FROM deleteCursor INTO @IPTC, @EXIF;
		END
	CLOSE deleteCursor;
	DEALLOCATE deleteCursor;
END 