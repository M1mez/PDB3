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
/****** Object:  StoredProcedure [dbo].[usp_GetPictures]    Script Date: 22.02.2018 17:18:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[usp_GetPictures]
	-- Add the parameters for the stored procedure here
	@NamePart VARCHAR(32) = null,
	@PhotoGrapherPart VARCHAR(32) = NULL,
	@IPTCPart VARCHAR(32) = NULL,
	@EXIFPart VARCHAR(32) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	IF(@NamePart IS NOT NULL AND @PhotoGrapherPart IS NULL AND @IPTCPart IS NULL AND @EXIFPart IS NULL)
		SELECT * FROM Pictures WHERE FileName LIKE '%'+@NamePart+'%'

	ELSE IF(@NamePart IS NULL AND @PhotoGrapherPart IS NOT NULL AND @IPTCPart IS NULL AND @EXIFPart IS NULL)
		SELECT * FROM Pictures WHERE FK_PG_ID = (SELECT PG_ID FROM Photographer WHERE FirstName LIKE '%'+@PhotoGrapherPart+'%' OR LastName LIKE '%'+@PhotoGrapherPart+'%' OR Notes LIKE '%'+@PhotoGrapherPart+'%')

	ELSE IF (@NamePart IS NULL AND @PhotoGrapherPart IS NULL AND @IPTCPart IS NOT NULL AND @EXIFPart IS NULL)
		SELECT * FROM Pictures WHERE FK_IPTC_ID = (SELECT IPTC_ID FROM IPTC WHERE Keywords LIKE '%'+@IPTCPart+'%' OR ByLine LIKE '%'+@IPTCPart+'%' OR CopyrightNotice LIKE '%'+@IPTCPart+'%' OR Headline LIKE '%'+@IPTCPart+'%' OR Caption LIKE '%'+@IPTCPart+'%')

	ELSE IF (@NamePart IS NULL AND @PhotoGrapherPart IS NULL AND @IPTCPart IS NULL AND @EXIFPart IS NOT NULL)
		SELECT * FROM Pictures WHERE FK_EXIF_ID = (SELECT EXIF_ID FROM EXIF WHERE MAKE like '%'+@EXIFPart+'%' OR FNumber like '%'+@EXIFPart+'%' OR ExposureTime like '%'+@EXIFPart+'%' OR ISOValue like '%'+@EXIFPart+'%' OR Flash like '%'+@EXIFPart+'%' OR ExposureProgram like '%'+@EXIFPart+'%')
	
	ELSE
		SELECT * FROM Pictures
END
