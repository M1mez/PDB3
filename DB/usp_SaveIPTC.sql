-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_SaveIPTC
	-- Add the parameters for the stored procedure here
	@Keywords VARCHAR(64) = NULL,
	@ByLine VARCHAR(32) = NULL,
	@CopyrightNotice VARCHAR(128) = NULL,
	@Headline VARCHAR(256) = NULL,
	@Caption VARCHAR(2000) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO IPTC(Keywords, ByLine, CopyrightNotice, Headline, Caption)
	VALUES (@Keywords, @ByLine, @CopyrightNotice, @Headline, @Caption)
END
GO
