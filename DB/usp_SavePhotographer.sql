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
/****** Object:  StoredProcedure [dbo].[usp_SavePhotographer]    Script Date: 24.02.2018 18:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[usp_SavePhotographer]
	-- Add the parameters for the stored procedure here
	@FirstName VARCHAR(64) = null,
	@LastName VARCHAR(64),
	@BirthDay VARCHAR(32) = null,
	@Notes VARCHAR(2000) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT FirstName FROM Photographer WHERE (FirstName = @FirstName))
		UPDATE Photographer SET BirthDay = CAST(NULLIF(@BirthDay,'')as datetime), Notes = @Notes WHERE LastName = @LastName AND FirstName = @FirstName
	ELSE
		INSERT INTO Photographer(FirstName, LastName, BirthDay, Notes)
		VALUES (@FirstName, @LastName, CAST(NULLIF(@BirthDay,'')as datetime), @Notes)
END
