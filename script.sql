USE [RealPlaza]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 21/9/2022 09:57:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Producto]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Producto](
	[PKID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](500) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[PKID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 97) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  StoredProcedure [dbo].[pListarProducto]    Script Date: 21/9/2022 09:57:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pListarProducto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pListarProducto] AS' 
END
GO


ALTER PROCEDURE [dbo].[pListarProducto]
@ParametroJSON NVARCHAR(MAX)
AS
	DECLARE @Table AS Table(
		NumeroPagina INT,
		RegistrosPagina INT,
		NumeroPaginas INT,
		Filter INT
	)

	INSERT  @Table
	SELECT NumeroPagina, RegistrosPagina, NumeroPaginas, Filter FROM 
	OPENJSON(@ParametroJSON)
	WITH (
		NumeroPagina INT,
		RegistrosPagina INT,
		NumeroPaginas INT,
		Filter INT
	)

	UPDATE @Table 
		SET NumeroPaginas = 
		(SELECT 
			(CASE 
				WHEN (COUNT(Producto.PKID)% (SELECT RegistrosPagina FROM @Table) >0) 
				THEN (COUNT(Producto.PKID)/ (SELECT RegistrosPagina FROM @Table) +1)
				ELSE (COUNT(Producto.PKID)/ (SELECT RegistrosPagina FROM @Table))
			END) 
		FROM Producto)

	SET NOCOUNT ON
	SELECT
		JSON_QUERY((
			SELECT NumeroPagina, RegistrosPagina, NumeroPaginas, Filter
			FROM @Table
			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)) AS Paginacion,
		(
			SELECT PKID, Nombre, Precio, FechaCreacion
			FROM(
					SELECT *, ROW_NUMBER() 
					OVER ( ORDER BY
						CASE WHEN (SELECT Filter FROM @Table) = 0 THEN Producto.Precio END ASC,
						CASE WHEN (SELECT Filter FROM @Table) = 1 THEN Producto.Precio END DESC
					) AS Fila 
					FROM Producto
				) as Producto 
				WHERE (Fila >((SELECT NumeroPagina FROM @Table) - 1 ) *(SELECT RegistrosPagina FROM @Table) AND Fila<= ((SELECT NumeroPagina FROM @Table)*(SELECT RegistrosPagina FROM @Table))
			)				
			FOR JSON PATH 
		) AS Producto
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
GO
