USE [EmpresaDB]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[_DetailsAccionVM]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_DetailsAccionVM](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Formato] [nvarchar](max) NULL,
	[Estandar] [nvarchar](max) NULL,
	[Titulo] [nvarchar](max) NULL,
	[Fecha] [nvarchar](max) NULL,
	[Version] [nvarchar](max) NULL,
	[FechaSolicitud] [nvarchar](max) NULL,
	[Categoria] [int] NOT NULL,
	[Responsable] [nvarchar](max) NULL,
	[Cargo] [nvarchar](max) NULL,
	[Proceso] [nvarchar](max) NULL,
	[FuenteAccion] [nvarchar](max) NULL,
	[Descripcion] [nvarchar](max) NULL,
	[EficaciaAntes] [int] NOT NULL,
	[EficaciaDespues] [int] NOT NULL,
	[FechaCierre] [nvarchar](max) NULL,
	[Efectiva] [bit] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_dbo._DetailsAccionVM] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accidentadoes]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accidentadoes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TrabajadorID] [int] NOT NULL,
	[IncidenteID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Accidentadoes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accions]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ZonaID] [int] NOT NULL,
	[ProcesoID] [int] NOT NULL,
	[ActividadID] [int] NOT NULL,
	[TareaID] [int] NOT NULL,
	[FechaSolicitud] [datetime] NOT NULL,
	[Categoria] [int] NOT NULL,
	[TrabajadorID] [int] NOT NULL,
	[FuenteAccion] [int] NOT NULL,
	[Descripcion] [nvarchar](200) NOT NULL,
	[EficaciaAntes] [int] NOT NULL,
	[EficaciaDespues] [int] NOT NULL,
	[FechaCierre] [datetime] NOT NULL,
	[Efectiva] [bit] NOT NULL,
	[Estado] [bit] NOT NULL,
	[RiesgoID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Actions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Actividads]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actividads](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Aplicacions]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aplicacions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RiesgoID] [int] NOT NULL,
	[FechaInicial] [datetime] NOT NULL,
	[FechaFinal] [datetime] NOT NULL,
	[Presupuesto] [decimal](18, 2) NOT NULL,
	[Observaciones] [nvarchar](max) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[CategoriaAplicacion] [int] NOT NULL,
	[Finalidad] [int] NOT NULL,
	[Intervencion] [int] NOT NULL,
	[Beneficios] [nvarchar](max) NULL,
	[TrabajadorID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Aplicacions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AplicacionVMs]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AplicacionVMs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RiesgoID] [int] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[CategoriaAplicacion] [int] NOT NULL,
	[Intervencion] [int] NOT NULL,
	[Beneficios] [nvarchar](100) NULL,
	[Presupuesto] [decimal](18, 2) NOT NULL,
	[TrabajadorID] [int] NOT NULL,
	[FechaInicial] [datetime] NOT NULL,
	[FechaFinal] [datetime] NOT NULL,
	[Observaciones] [nvarchar](100) NOT NULL,
	[Responsable] [nvarchar](max) NULL,
	[TextFechaInicial] [nvarchar](max) NULL,
	[TextFechaFinal] [nvarchar](max) NULL,
	[TextCategoria] [nvarchar](max) NULL,
	[TextIntervencion] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AplicacionVMs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditaAccions]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditaAccions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Usuario] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.AuditaAccions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditRiesgoes]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditRiesgoes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Usuario] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.AuditRiesgoes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cargoes]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cargoes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [nvarchar](max) NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Cargoes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoriaPeligroes]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriaPeligroes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Controls]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Controls](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [nvarchar](max) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Finalidad] [int] NOT NULL,
	[Intervencion] [int] NOT NULL,
	[Beneficios] [nvarchar](max) NULL,
	[Presupuesto] [decimal](18, 2) NOT NULL,
	[Efectividad] [int] NOT NULL,
	[RiesgoID] [int] NOT NULL,
	[CategoriaAplicacion] [int] NOT NULL,
	[AplicacionID] [int] NOT NULL,
	[CategoriaEfectividad] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Controls] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreateIndicatorsViewModels]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreateIndicatorsViewModels](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FechaInicial] [datetime] NOT NULL,
	[FechaFinal] [datetime] NOT NULL,
	[IndicadorID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.CreateIndicatorsViewModels] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Formato] [nvarchar](max) NULL,
	[Estandar] [nvarchar](max) NULL,
	[Titulo] [nvarchar](max) NULL,
	[Version] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Documents] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Incidentes]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Incidentes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FechaReporte] [datetime] NOT NULL,
	[FechaIncidente] [datetime] NOT NULL,
	[IncapacidadMedica] [bit] NOT NULL,
	[DiasIncapacidad] [int] NOT NULL,
	[NaturalezaLesion] [nvarchar](150) NOT NULL,
	[PartesAfectadas] [nvarchar](150) NOT NULL,
	[TipoIncidente] [nvarchar](150) NOT NULL,
	[AgenteLesion] [nvarchar](150) NOT NULL,
	[ActosInseguros] [nvarchar](150) NULL,
	[CondicionesInsegura] [nvarchar](150) NULL,
	[TipoDaño] [nvarchar](250) NULL,
	[Afectacion] [nvarchar](150) NULL,
	[DañosOcasionados] [nvarchar](150) NULL,
	[TipoVehiculo] [nvarchar](20) NULL,
	[MarcaVehiculo] [nvarchar](20) NULL,
	[ModeloVehiculo] [nvarchar](20) NULL,
	[KilometrajeVehiculo] [int] NOT NULL,
	[CostosEstimados] [decimal](18, 2) NOT NULL,
	[DescripcionIncidente] [nvarchar](250) NOT NULL,
	[EvitarIncidente] [nvarchar](150) NULL,
	[AccionesInmediatas] [nvarchar](150) NULL,
	[ComentariosAdicionales] [nvarchar](150) NULL,
	[AtencionBrindada] [nvarchar](150) NOT NULL,
	[LesionPersonal] [int] NOT NULL,
	[DañoMaterial] [int] NOT NULL,
	[MedioAmbiente] [int] NOT NULL,
	[Imagen] [int] NOT NULL,
	[RequiereInvestigacion] [bit] NOT NULL,
	[ConsecuenciasLesion] [int] NOT NULL,
	[ConsecuenciasDaño] [int] NOT NULL,
	[ConsecuenciasMedio] [int] NOT NULL,
	[ConsecuenciasImagen] [int] NOT NULL,
	[Probabilidad] [int] NOT NULL,
	[ActividadID] [int] NOT NULL,
	[ZonaID] [int] NOT NULL,
	[ProcesoID] [int] NOT NULL,
	[TareaID] [int] NOT NULL,
	[RiesgoID] [int] NOT NULL,
	[AccionID] [int] NOT NULL,
	[CategoriasIncidente] [int] NOT NULL,
	[TrabajadorID] [int] NOT NULL,
	[EquiposInvestigador] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Incidentes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IncidenteViewModels]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IncidenteViewModels](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FechaReporte] [datetime] NOT NULL,
	[FechaIncidente] [datetime] NOT NULL,
	[CategoriasIncidente] [int] NOT NULL,
	[IncapacidadMedica] [bit] NOT NULL,
	[DiasIncapacidad] [int] NOT NULL,
	[NaturalezaLesion] [nvarchar](150) NOT NULL,
	[PartesAfectadas] [nvarchar](150) NULL,
	[TipoIncidente] [nvarchar](150) NOT NULL,
	[AgenteLesion] [nvarchar](150) NOT NULL,
	[ActosInseguros] [nvarchar](150) NULL,
	[CondicionesInsegura] [nvarchar](150) NULL,
	[TipoDaño] [nvarchar](100) NULL,
	[Afectacion] [nvarchar](100) NULL,
	[DañosOcasionados] [nvarchar](100) NULL,
	[TipoVehiculo] [nvarchar](20) NULL,
	[MarcaVehiculo] [nvarchar](20) NULL,
	[ModeloVehiculo] [nvarchar](20) NULL,
	[KilometrajeVehiculo] [int] NOT NULL,
	[CostosEstimados] [decimal](18, 2) NOT NULL,
	[DescripcionIncidente] [nvarchar](250) NULL,
	[EvitarIncidente] [nvarchar](150) NULL,
	[AccionesInmediatas] [nvarchar](150) NULL,
	[ComentariosAdicionales] [nvarchar](150) NULL,
	[AtencionBrindada] [nvarchar](150) NULL,
	[EquiposInvestigador] [int] NOT NULL,
	[LesionPersonal] [int] NOT NULL,
	[DañoMaterial] [int] NOT NULL,
	[MedioAmbiente] [int] NOT NULL,
	[Imagen] [int] NOT NULL,
	[RequiereInvestigacion] [bit] NOT NULL,
	[ConsecuenciasLesion] [int] NOT NULL,
	[ConsecuenciasDaño] [int] NOT NULL,
	[ConsecuenciasMedio] [int] NOT NULL,
	[ConsecuenciasImagen] [int] NOT NULL,
	[Probabilidad] [int] NOT NULL,
	[ZonaID] [int] NOT NULL,
	[ProcesoID] [int] NOT NULL,
	[ActividadID] [int] NOT NULL,
	[TareaID] [int] NOT NULL,
	[TrabajadorID] [int] NOT NULL,
	[FechaIncidentStr] [nvarchar](max) NULL,
	[FechaReportStr] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.IncidenteViewModels] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Indicadors]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Indicadors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Definicion] [nvarchar](max) NOT NULL,
	[Numerador] [nvarchar](max) NOT NULL,
	[Denominador] [nvarchar](max) NOT NULL,
	[Formula] [nvarchar](max) NOT NULL,
	[Interpretacion] [nvarchar](max) NOT NULL,
	[Periodicidad] [nvarchar](max) NOT NULL,
	[TipoChart] [nvarchar](max) NULL,
	[Meta] [decimal](18, 2) NOT NULL,
	[Fuente] [nvarchar](max) NULL,
	[Tipo] [nvarchar](max) NULL,
	[Responsable] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Indicadors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IndicadorViewModels]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IndicadorViewModels](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Definicion] [nvarchar](max) NOT NULL,
	[Numerador] [nvarchar](max) NOT NULL,
	[Denominador] [nvarchar](max) NOT NULL,
	[Formula] [nvarchar](max) NOT NULL,
	[Interpretacion] [nvarchar](max) NOT NULL,
	[Periodicidad] [nvarchar](max) NOT NULL,
	[Imagen] [nvarchar](max) NULL,
	[Year] [int] NOT NULL,
	[Periodo] [nvarchar](max) NULL,
	[Titulo] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.IndicadorViewModels] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MatrizRiesgosVMs]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatrizRiesgosVMs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Proceso] [nvarchar](max) NULL,
	[Zona] [nvarchar](max) NULL,
	[Actividad] [nvarchar](max) NULL,
	[Rutinaria] [nvarchar](max) NULL,
	[CategoriaPeligro] [nvarchar](max) NULL,
	[Peligro] [nvarchar](max) NULL,
	[EfectosPosibles] [nvarchar](max) NULL,
	[Fuente] [nvarchar](max) NULL,
	[Medio] [nvarchar](max) NULL,
	[Individuo] [nvarchar](max) NULL,
	[NivelDeficiencia] [int] NOT NULL,
	[NivelExposicion] [int] NOT NULL,
	[NivelProbabilidad] [int] NOT NULL,
	[InterpretaNP] [nvarchar](max) NULL,
	[NivelConsecuencia] [int] NOT NULL,
	[NivelRiesgo] [int] NOT NULL,
	[CategoriaRiesgo] [nvarchar](max) NULL,
	[AceptabilidadNR] [nvarchar](max) NULL,
	[SignificadoNR] [nvarchar](max) NULL,
	[NroExpuestos] [int] NOT NULL,
	[PeorConsecuencia] [nvarchar](max) NULL,
	[RequisitoLegal] [nvarchar](max) NULL,
	[Eliminacion] [nvarchar](max) NULL,
	[Sustitucion] [nvarchar](max) NULL,
	[Ingenieria] [nvarchar](max) NULL,
	[Administracion] [nvarchar](max) NULL,
	[Señalizacion] [nvarchar](max) NULL,
	[EPP] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.MatrizRiesgosVMs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organizations]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NIT] [nvarchar](max) NOT NULL,
	[RazonSocial] [nvarchar](50) NOT NULL,
	[Direccion] [nvarchar](50) NOT NULL,
	[Municip] [nvarchar](50) NOT NULL,
	[Department] [nvarchar](50) NOT NULL,
	[Telefono] [nvarchar](50) NOT NULL,
	[ARL] [nvarchar](50) NOT NULL,
	[ClaseRiesgo] [nvarchar](10) NOT NULL,
	[DocumentRepresent] [nvarchar](20) NOT NULL,
	[NameRepresent] [nvarchar](50) NOT NULL,
	[EconomicActivity] [nvarchar](100) NOT NULL,
	[NumeroTrabajadores] [int] NOT NULL,
	[Products] [nvarchar](150) NOT NULL,
	[Mision] [nvarchar](150) NOT NULL,
	[Vision] [nvarchar](150) NOT NULL,
	[Objetivos] [nvarchar](150) NOT NULL,
	[Procesos] [nvarchar](150) NULL,
	[Organigrama] [nvarchar](150) NULL,
	[TurnosAdministrativo] [nvarchar](150) NULL,
	[TurnosOperativo] [nvarchar](150) NULL,
 CONSTRAINT [PK_dbo.Organizations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Peligroes]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Peligroes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[CategoriaPeligroID] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlanAccions]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanAccions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccionID] [int] NOT NULL,
	[FechaInicial] [datetime] NOT NULL,
	[FechaFinal] [datetime] NOT NULL,
	[Causa] [int] NOT NULL,
	[Accion] [nvarchar](100) NOT NULL,
	[TrabajadorID] [int] NOT NULL,
	[Prioritaria] [int] NOT NULL,
	[Costos] [decimal](18, 0) NULL,
	[Responsable] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.PlanAccions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlanActions]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanActions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccionID] [int] NOT NULL,
	[FechaInicial] [datetime] NOT NULL,
	[FechaFinal] [datetime] NOT NULL,
	[Causa] [int] NOT NULL,
	[Accion] [nvarchar](100) NOT NULL,
	[TrabajadorID] [int] NOT NULL,
	[Prioritaria] [bit] NOT NULL,
	[Costos] [decimal](18, 2) NOT NULL,
	[Responsable] [nvarchar](100) NULL,
	[AccionViewModel_ID] [int] NULL,
	[_DetailsAccionVM_ID] [int] NULL,
 CONSTRAINT [PK_dbo.PlanActions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Procesoes]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Procesoes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Riesgoes]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Riesgoes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Rutinaria] [bit] NOT NULL,
	[EfectosPosibles] [int] NOT NULL,
	[NivelDeficiencia] [int] NOT NULL,
	[NivelExposicion] [int] NOT NULL,
	[NivelConsecuencia] [int] NOT NULL,
	[Aceptabilidad] [int] NOT NULL,
	[NroExpuestos] [int] NOT NULL,
	[RequisitoLegal] [bit] NOT NULL,
	[NivelProbabilidad] [int] NOT NULL,
	[NivelRiesgo] [int] NOT NULL,
	[CategoriaRiesgo] [nvarchar](max) NOT NULL,
	[IncidenteID] [int] NOT NULL,
	[PeorConsecuencia] [nvarchar](max) NULL,
	[AccionID] [int] NOT NULL,
	[ZonaID] [int] NOT NULL,
	[ProcesoID] [int] NOT NULL,
	[ActividadID] [int] NOT NULL,
	[TareaID] [int] NOT NULL,
	[CategoriaPeligroID] [int] NOT NULL,
	[PeligroID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Riesgoes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RiesgoViewModels]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RiesgoViewModels](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ZonaID] [int] NOT NULL,
	[ProcesoID] [int] NOT NULL,
	[ActividadID] [int] NOT NULL,
	[TareaID] [int] NOT NULL,
	[Rutinaria] [bit] NOT NULL,
	[CategoriaPeligroID] [int] NOT NULL,
	[PeligroID] [int] NOT NULL,
	[EfectosPosibles] [int] NOT NULL,
	[NivelesDeficiencia] [int] NOT NULL,
	[NivelesExposicion] [int] NOT NULL,
	[NivelesConsecuencia] [int] NOT NULL,
	[AceptabilidadNR] [int] NOT NULL,
	[NroExpuestos] [int] NOT NULL,
	[RequisitoLegal] [bit] NOT NULL,
	[NivelDeficiencia] [int] NOT NULL,
	[NivelExposicion] [int] NOT NULL,
	[NivelConsecuencia] [int] NOT NULL,
	[IncidenteID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.RiesgoViewModels] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleOperations]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[Operation] [int] NOT NULL,
	[Component] [int] NOT NULL,
 CONSTRAINT [PK_dbo.RoleOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleUserVMs]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleUserVMs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.RoleUserVMs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeguimientoAccions]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeguimientoAccions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccionID] [int] NOT NULL,
	[Resultado] [nvarchar](max) NOT NULL,
	[FechaSeguimiento] [datetime] NOT NULL,
	[TrabajadorID] [int] NOT NULL,
	[Responsable] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.SeguimientoAccions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seguimientoes]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seguimientoes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccionID] [int] NOT NULL,
	[Resultado] [nvarchar](max) NOT NULL,
	[FechaSeguimiento] [datetime] NOT NULL,
	[TrabajadorID] [int] NOT NULL,
	[Responsable] [nvarchar](max) NULL,
	[AccionViewModel_ID] [int] NULL,
	[_DetailsAccionVM_ID] [int] NULL,
 CONSTRAINT [PK_dbo.Seguimientoes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tareas]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tareas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trabajadors]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trabajadors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PrimerApellido] [nvarchar](max) NULL,
	[SegundoApellido] [nvarchar](max) NULL,
	[Documento] [nvarchar](max) NULL,
	[FechaNacimiento] [datetime] NOT NULL,
	[Genero] [int] NOT NULL,
	[EstadoCivil] [int] NOT NULL,
	[Nombres] [nvarchar](max) NOT NULL,
	[FechaNomina] [datetime] NOT NULL,
	[DiasPago] [int] NOT NULL,
	[TipoVinculacion] [int] NOT NULL,
	[EPS] [nvarchar](20) NULL,
	[AFP] [nvarchar](20) NULL,
	[ARL] [nvarchar](20) NULL,
	[CargoID] [int] NOT NULL,
	[Firma] [nvarchar](20) NULL,
	[Foto] [nvarchar](20) NULL,
	[Direccion] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trazas]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trazas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ControlID] [int] NOT NULL,
	[FechaInicial] [datetime] NOT NULL,
	[FechaFinal] [datetime] NOT NULL,
	[Efectividad] [int] NOT NULL,
	[Presupuesto] [decimal](18, 2) NOT NULL,
	[EstadoActual] [nvarchar](max) NOT NULL,
	[AplicacionID] [int] NOT NULL,
	[RiesgoolID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Trazas] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserViewModels]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserViewModels](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Role] [nvarchar](max) NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UserViewModels] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zonas]    Script Date: 10/07/2022 2:51:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zonas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Aplicacions] ADD  DEFAULT ((0)) FOR [CategoriaAplicacion]
GO
ALTER TABLE [dbo].[Aplicacions] ADD  DEFAULT ((0)) FOR [Finalidad]
GO
ALTER TABLE [dbo].[Aplicacions] ADD  DEFAULT ((0)) FOR [Intervencion]
GO
ALTER TABLE [dbo].[Aplicacions] ADD  DEFAULT ((0)) FOR [TrabajadorID]
GO
ALTER TABLE [dbo].[AuditRiesgoes] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [Fecha]
GO
ALTER TABLE [dbo].[AuditRiesgoes] ADD  DEFAULT ('') FOR [Descripcion]
GO
ALTER TABLE [dbo].[AuditRiesgoes] ADD  DEFAULT ('') FOR [Usuario]
GO
ALTER TABLE [dbo].[Controls] ADD  DEFAULT ((0)) FOR [RiesgoID]
GO
ALTER TABLE [dbo].[Controls] ADD  DEFAULT ((0)) FOR [CategoriaAplicacion]
GO
ALTER TABLE [dbo].[Controls] ADD  DEFAULT ((0)) FOR [AplicacionID]
GO
ALTER TABLE [dbo].[Controls] ADD  DEFAULT ((0)) FOR [CategoriaEfectividad]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [ActividadID]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [ZonaID]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [ProcesoID]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [TareaID]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [RiesgoID]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [AccionID]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [CategoriasIncidente]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [TrabajadorID]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [EquiposInvestigador]
GO
ALTER TABLE [dbo].[IncidenteViewModels] ADD  DEFAULT ((0)) FOR [ZonaID]
GO
ALTER TABLE [dbo].[IncidenteViewModels] ADD  DEFAULT ((0)) FOR [ProcesoID]
GO
ALTER TABLE [dbo].[IncidenteViewModels] ADD  DEFAULT ((0)) FOR [ActividadID]
GO
ALTER TABLE [dbo].[IncidenteViewModels] ADD  DEFAULT ((0)) FOR [TareaID]
GO
ALTER TABLE [dbo].[IncidenteViewModels] ADD  DEFAULT ((0)) FOR [TrabajadorID]
GO
ALTER TABLE [dbo].[IncidenteViewModels] ADD  DEFAULT ('') FOR [FechaIncidentStr]
GO
ALTER TABLE [dbo].[Indicadors] ADD  DEFAULT ((0)) FOR [Meta]
GO
ALTER TABLE [dbo].[IndicadorViewModels] ADD  DEFAULT ((0)) FOR [Year]
GO
ALTER TABLE [dbo].[Riesgoes] ADD  DEFAULT ((0)) FOR [NivelProbabilidad]
GO
ALTER TABLE [dbo].[Riesgoes] ADD  DEFAULT ((0)) FOR [NivelRiesgo]
GO
ALTER TABLE [dbo].[Riesgoes] ADD  DEFAULT ('') FOR [CategoriaRiesgo]
GO
ALTER TABLE [dbo].[Riesgoes] ADD  DEFAULT ((0)) FOR [IncidenteID]
GO
ALTER TABLE [dbo].[Riesgoes] ADD  DEFAULT ((0)) FOR [AccionID]
GO
ALTER TABLE [dbo].[Riesgoes] ADD  DEFAULT ((0)) FOR [ZonaID]
GO
ALTER TABLE [dbo].[Riesgoes] ADD  DEFAULT ((0)) FOR [ProcesoID]
GO
ALTER TABLE [dbo].[Riesgoes] ADD  DEFAULT ((0)) FOR [ActividadID]
GO
ALTER TABLE [dbo].[Riesgoes] ADD  DEFAULT ((0)) FOR [TareaID]
GO
ALTER TABLE [dbo].[Riesgoes] ADD  DEFAULT ((0)) FOR [CategoriaPeligroID]
GO
ALTER TABLE [dbo].[Riesgoes] ADD  DEFAULT ((0)) FOR [PeligroID]
GO
ALTER TABLE [dbo].[RiesgoViewModels] ADD  DEFAULT ((0)) FOR [NivelDeficiencia]
GO
ALTER TABLE [dbo].[RiesgoViewModels] ADD  DEFAULT ((0)) FOR [NivelExposicion]
GO
ALTER TABLE [dbo].[RiesgoViewModels] ADD  DEFAULT ((0)) FOR [NivelConsecuencia]
GO
ALTER TABLE [dbo].[RiesgoViewModels] ADD  DEFAULT ((0)) FOR [IncidenteID]
GO
ALTER TABLE [dbo].[RoleOperations] ADD  DEFAULT ((0)) FOR [RoleID]
GO
ALTER TABLE [dbo].[RoleOperations] ADD  DEFAULT ((0)) FOR [Operation]
GO
ALTER TABLE [dbo].[RoleOperations] ADD  DEFAULT ((0)) FOR [Component]
GO
ALTER TABLE [dbo].[Trabajadors] ADD  DEFAULT ('') FOR [Nombres]
GO
ALTER TABLE [dbo].[Trabajadors] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [FechaNomina]
GO
ALTER TABLE [dbo].[Trabajadors] ADD  DEFAULT ((0)) FOR [DiasPago]
GO
ALTER TABLE [dbo].[Trabajadors] ADD  DEFAULT ((0)) FOR [TipoVinculacion]
GO
ALTER TABLE [dbo].[Trabajadors] ADD  DEFAULT ((0)) FOR [CargoID]
GO
ALTER TABLE [dbo].[Trazas] ADD  DEFAULT ((0)) FOR [AplicacionID]
GO
ALTER TABLE [dbo].[Trazas] ADD  DEFAULT ((0)) FOR [RiesgoolID]
GO
ALTER TABLE [dbo].[Accidentadoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Accidentadoes_dbo.Incidentes_IncidenteID] FOREIGN KEY([IncidenteID])
REFERENCES [dbo].[Incidentes] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accidentadoes] CHECK CONSTRAINT [FK_dbo.Accidentadoes_dbo.Incidentes_IncidenteID]
GO
ALTER TABLE [dbo].[Aplicacions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Aplicacions_dbo.Riesgoes_RiesgoID] FOREIGN KEY([RiesgoID])
REFERENCES [dbo].[Riesgoes] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Aplicacions] CHECK CONSTRAINT [FK_dbo.Aplicacions_dbo.Riesgoes_RiesgoID]
GO
ALTER TABLE [dbo].[PlanAccions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PlanAccions_dbo.Accions_AccionID] FOREIGN KEY([AccionID])
REFERENCES [dbo].[Accions] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlanAccions] CHECK CONSTRAINT [FK_dbo.PlanAccions_dbo.Accions_AccionID]
GO
ALTER TABLE [dbo].[SeguimientoAccions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SeguimientoAccions_dbo.Accions_AccionID] FOREIGN KEY([AccionID])
REFERENCES [dbo].[Accions] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SeguimientoAccions] CHECK CONSTRAINT [FK_dbo.SeguimientoAccions_dbo.Accions_AccionID]
GO
ALTER TABLE [dbo].[Trazas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Trazas_dbo.Controls_ControlID] FOREIGN KEY([ControlID])
REFERENCES [dbo].[Controls] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Trazas] CHECK CONSTRAINT [FK_dbo.Trazas_dbo.Controls_ControlID]
GO
