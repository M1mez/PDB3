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
/****** Object:  StoredProcedure [dbo].[usp_SavePicture]    Script Date: 22.02.2018 21:51:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[usp_SavePicture]
    -- Add the parameters for the stored procedure here
	@FileName VARCHAR(64),
	@IPTC INT = null,
	@EXIF INT = null,
	@Photog INT = null,
	@Camera INT = null
	
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ;
	BEGIN TRAN tran_insert_pics
		BEGIN TRY
    -- Insert statements for procedure here
			IF EXISTS(SELECT FileName FROM Pictures WHERE FileName = @FileName)
				UPDATE Pictures SET FK_IPTC_ID = @IPTC, FK_EXIF_ID = @EXIF, FK_Cam_ID = @Camera WHERE FileName = @FileName
			ELSE
				INSERT INTO Pictures(FileName, FK_IPTC_ID, FK_EXIF_ID, FK_PG_ID, FK_Cam_ID)
				VALUES (@FileName, @IPTC, @EXIF, @Photog, @Camera)
			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			SELECT
				ERROR_NUMBER() as ErrorNR,
				ERROR_LINE() as ErrorLine,
				ERROR_MESSAGE() as ErrorMessage;
			ROLLBACK TRANSACTION
		END CATCH
END
