USE [master]
GO
/****** Object:  Database [DB_Doctorsforum]    Script Date: 7/3/2022 9:26:04 AM ******/
CREATE DATABASE [DB_Doctorsforum]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_Doctorsforum', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DB_Doctorsforum.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DB_Doctorsforum_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DB_Doctorsforum_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DB_Doctorsforum] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_Doctorsforum].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_Doctorsforum] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_Doctorsforum] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_Doctorsforum] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DB_Doctorsforum] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_Doctorsforum] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET RECOVERY FULL 
GO
ALTER DATABASE [DB_Doctorsforum] SET  MULTI_USER 
GO
ALTER DATABASE [DB_Doctorsforum] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_Doctorsforum] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_Doctorsforum] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_Doctorsforum] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DB_Doctorsforum] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DB_Doctorsforum] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DB_Doctorsforum', N'ON'
GO
ALTER DATABASE [DB_Doctorsforum] SET QUERY_STORE = OFF
GO
USE [DB_Doctorsforum]
GO
/****** Object:  Table [dbo].[Achievement]    Script Date: 7/3/2022 9:26:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achievement](
	[AchID] [int] IDENTITY(1,1) NOT NULL,
	[User_id] [varchar](50) NOT NULL,
	[AchName] [varchar](250) NOT NULL,
	[Issued_by] [varchar](50) NOT NULL,
	[Date_Range] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_user]    Script Date: 7/3/2022 9:26:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_user](
	[User_id] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[First_name] [varchar](50) NOT NULL,
	[Last_name] [varchar](50) NOT NULL,
	[Image] [varchar](250) NULL,
	[Address] [varchar](250) NOT NULL,
	[Phone_number] [varchar](12) NOT NULL,
	[Email_address] [varchar](100) NOT NULL,
	[Sex] [varchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[Experience] [varchar](50) NULL,
	[profession] [varchar](50) NOT NULL,
	[public_profile] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[User_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post_Detail]    Script Date: 7/3/2022 9:26:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post_Detail](
	[Post_id] [int] IDENTITY(1,1) NOT NULL,
	[Level] [int] NULL,
	[forid] [varchar](50) NULL,
	[Image] [varchar](250) NULL,
	[NumericalOrder] [int] NULL,
	[Topic_id] [int] NOT NULL,
	[Email] [varchar](100) NULL,
	[TimePost] [datetime] NOT NULL,
	[Content] [ntext] NOT NULL,
	[Userpost] [varchar](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Post_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Professional]    Script Date: 7/3/2022 9:26:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Professional](
	[ProID] [int] IDENTITY(1,1) NOT NULL,
	[User_id] [varchar](50) NOT NULL,
	[ProName] [varchar](50) NOT NULL,
	[Date_Range] [date] NOT NULL,
	[Issued_by] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Qualification]    Script Date: 7/3/2022 9:26:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Qualification](
	[QualiID] [int] IDENTITY(1,1) NOT NULL,
	[User_id] [varchar](50) NOT NULL,
	[QualiName] [varchar](50) NOT NULL,
	[Date_Range] [date] NOT NULL,
	[Issued_by] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[QualiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topic]    Script Date: 7/3/2022 9:26:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topic](
	[Topic_id] [int] IDENTITY(1,1) NOT NULL,
	[TopicName] [varchar](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Topic_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Achievement] ON 

INSERT [dbo].[Achievement] ([AchID], [User_id], [AchName], [Issued_by], [Date_Range]) VALUES (1, N'doctor1', N'Certificate of merit by the Prime Minister on performance.', N'Prime Minister', CAST(N'2003-01-01' AS Date))
SET IDENTITY_INSERT [dbo].[Achievement] OFF
GO
INSERT [dbo].[DB_user] ([User_id], [Password], [First_name], [Last_name], [Image], [Address], [Phone_number], [Email_address], [Sex], [Age], [Experience], [profession], [public_profile], [Status]) VALUES (N'admin', N'123456', N'Administrator', N'Doctors Forum', N'admin.png', N'Cantho, Vietnam', N'0123456789', N'info.doctors-forum@gmail.com ', N'Male', 25, NULL, N'Doctor', 0, N'Enable')
INSERT [dbo].[DB_user] ([User_id], [Password], [First_name], [Last_name], [Image], [Address], [Phone_number], [Email_address], [Sex], [Age], [Experience], [profession], [public_profile], [Status]) VALUES (N'customer1', N'123456', N'An', N'Nguyen', N'customer1.png', N'Cantho, Vietnam', N'0932654127', N'annguyen@gmail.com', N'Female', 21, NULL, N'Customer', 0, N'Enable')
INSERT [dbo].[DB_user] ([User_id], [Password], [First_name], [Last_name], [Image], [Address], [Phone_number], [Email_address], [Sex], [Age], [Experience], [profession], [public_profile], [Status]) VALUES (N'customer2', N'123456', N'Yae', N'Cotta', N'customer2.png', N'Cantho, Vietnam', N'0236412594', N'yae@gmail.com', N'Female', 20, NULL, N'Customer', 0, N'Distable')
INSERT [dbo].[DB_user] ([User_id], [Password], [First_name], [Last_name], [Image], [Address], [Phone_number], [Email_address], [Sex], [Age], [Experience], [profession], [public_profile], [Status]) VALUES (N'doctor1', N'123456', N'John', N'Mache', N'doctor1.png', N'Cantho, Vietnam', N'0123456752', N'john@gmail.com', N'Male', 50, N'Neurology', N'Doctor', 1, N'Enable')
INSERT [dbo].[DB_user] ([User_id], [Password], [First_name], [Last_name], [Image], [Address], [Phone_number], [Email_address], [Sex], [Age], [Experience], [profession], [public_profile], [Status]) VALUES (N'doctor2', N'123456', N'Meha', N'Nata', N'doctor2.png', N'Cantho, Vietnam', N'0258369741', N'meha@gmail.com', N'Female', 25, N'Heart', N'Doctor', 1, N'Distable')
INSERT [dbo].[DB_user] ([User_id], [Password], [First_name], [Last_name], [Image], [Address], [Phone_number], [Email_address], [Sex], [Age], [Experience], [profession], [public_profile], [Status]) VALUES (N'doctor3', N'123456', N'Nameha', N'Kazuha', N'doctor3.png', N'Cantho, Vietnam', N'0528741369', N'kazuha@gmail.com', N'Male', 35, N'Orthopedics', N'Doctor', 1, N'Enable')
INSERT [dbo].[DB_user] ([User_id], [Password], [First_name], [Last_name], [Image], [Address], [Phone_number], [Email_address], [Sex], [Age], [Experience], [profession], [public_profile], [Status]) VALUES (N'hoang', N'123456', N'Hoang', N'Le Thien', N'hoang.png', N'Cantho, Vietnam', N'0123456789', N'lthoanga17061@cusc.ctu.edu.vn', N'Male', 21, NULL, N'Customer', 1, N'Distable')
INSERT [dbo].[DB_user] ([User_id], [Password], [First_name], [Last_name], [Image], [Address], [Phone_number], [Email_address], [Sex], [Age], [Experience], [profession], [public_profile], [Status]) VALUES (N'thanh', N'123456', N'Thanh', N'Le Hoang Tri', N'thanh.png', N'Cantho, Vietnam', N'0258963147', N'	lhtthanha20176@cusc.ctu.edu.vn', N'Male', 28, NULL, N'Customer', 1, N'Distable')
INSERT [dbo].[DB_user] ([User_id], [Password], [First_name], [Last_name], [Image], [Address], [Phone_number], [Email_address], [Sex], [Age], [Experience], [profession], [public_profile], [Status]) VALUES (N'thuan', N'123456', N'Thuan', N'Dao Minh Trung', N'thuan.png', N'Cantho, Vietnam', N'0258741369', N'dmtthuana18088@cusc.ctu.edu.vn', N'Male', 23, NULL, N'Customer', 1, N'Distable')
GO
SET IDENTITY_INSERT [dbo].[Post_Detail] ON 

INSERT [dbo].[Post_Detail] ([Post_id], [Level], [forid], [Image], [NumericalOrder], [Topic_id], [Email], [TimePost], [Content], [Userpost]) VALUES (1, 0, NULL, N'1.jpg', 1744854431, 1, N'hana@gmail.com', CAST(N'2022-07-03T04:36:04.693' AS DateTime), N'Hospital communication during the pandemic is a question Vu and his team receive every day, from senior management to staff. Stemming from those questions and concerns, Vu synthesizes, builds and shares his practical knowledge with the Vietnamese health system through this article.', N'doctor1')
INSERT [dbo].[Post_Detail] ([Post_id], [Level], [forid], [Image], [NumericalOrder], [Topic_id], [Email], [TimePost], [Content], [Userpost]) VALUES (2, 0, NULL, N'2.jpg', 1955695859, 2, NULL, CAST(N'2022-07-03T05:20:37.120' AS DateTime), N'Hospital human resources are the core to help the Hospital develop successfully and sustainably. Doctor William Osler – recognized as the first person to bring medical students out of the lecture hall to experiment at the bedside once shared: “A good doctor can cure all diseases, but a great doctor can cure all diseases. can cure every patient.” This famous statement of a Canadian doctor is more than enough for us to realize that the importance of hospital human resources is completely real.
In our country, not only the public hospital brands in particular and the medical brand in general, even many corporate brands are also going in the wrong direction in the process of spreading the positive inspiration that the brand image brings. . Most are focusing on service quality, product quality or coverage of product images as well as sales locations.', N'doctor1')
INSERT [dbo].[Post_Detail] ([Post_id], [Level], [forid], [Image], [NumericalOrder], [Topic_id], [Email], [TimePost], [Content], [Userpost]) VALUES (3, 0, NULL, N'3.jpg', 1745409802, 3, NULL, CAST(N'2022-07-03T05:22:07.830' AS DateTime), N'Applying the PDCA cycle in quality improvement is the basic method applied in all quality models that can be applied to all organizations and medical facilities at different levels. The document is released by GIZ organization, part of German development cooperation in Vietnam.', N'doctor2')
INSERT [dbo].[Post_Detail] ([Post_id], [Level], [forid], [Image], [NumericalOrder], [Topic_id], [Email], [TimePost], [Content], [Userpost]) VALUES (4, 0, NULL, N'4.png', 1299745655, 4, NULL, CAST(N'2022-07-03T05:28:07.247' AS DateTime), N'Read through some quality improvement plans at some hospitals. I think, we need to distinguish two groups of work: Work plan for quality management department (always in coordination with other departments) and Quality improvement plan, more accurately called management. improvement projects.', N'doctor3')
INSERT [dbo].[Post_Detail] ([Post_id], [Level], [forid], [Image], [NumericalOrder], [Topic_id], [Email], [TimePost], [Content], [Userpost]) VALUES (5, 1, N'doctor3', NULL, 4, 4, NULL, CAST(N'2022-07-03T05:43:35.580' AS DateTime), N'Finish one project and move on to another. There are always 5-7 operational quality improvement projects in an organization --> that is continuous improvement.', N'doctor1')
INSERT [dbo].[Post_Detail] ([Post_id], [Level], [forid], [Image], [NumericalOrder], [Topic_id], [Email], [TimePost], [Content], [Userpost]) VALUES (6, 1, N'doctor1', NULL, 1, 1, NULL, CAST(N'2022-07-03T06:11:28.707' AS DateTime), N'That''s so great', N'doctor2')
INSERT [dbo].[Post_Detail] ([Post_id], [Level], [forid], [Image], [NumericalOrder], [Topic_id], [Email], [TimePost], [Content], [Userpost]) VALUES (7, 1, N'doctor1', NULL, 1, 1, NULL, CAST(N'2022-07-03T06:20:55.333' AS DateTime), N'So useful!', N'customer1')
SET IDENTITY_INSERT [dbo].[Post_Detail] OFF
GO
SET IDENTITY_INSERT [dbo].[Professional] ON 

INSERT [dbo].[Professional] ([ProID], [User_id], [ProName], [Date_Range], [Issued_by]) VALUES (1, N'doctor1', N'Health professional', CAST(N'2001-07-22' AS Date), N'RAND Corporation')
SET IDENTITY_INSERT [dbo].[Professional] OFF
GO
SET IDENTITY_INSERT [dbo].[Qualification] ON 

INSERT [dbo].[Qualification] ([QualiID], [User_id], [QualiName], [Date_Range], [Issued_by]) VALUES (1, N'doctor1', N'Doctors'' Qualifications', CAST(N'2000-02-03' AS Date), N'CanTho Uni')
SET IDENTITY_INSERT [dbo].[Qualification] OFF
GO
SET IDENTITY_INSERT [dbo].[Topic] ON 

INSERT [dbo].[Topic] ([Topic_id], [TopicName]) VALUES (1, N'Guidelines for hospital communication in the midst of a pandemic')
INSERT [dbo].[Topic] ([Topic_id], [TopicName]) VALUES (2, N'Human resource development Hospital applying Maslow''s hierarchy of needs')
INSERT [dbo].[Topic] ([Topic_id], [TopicName]) VALUES (3, N'Applying PDCA cycle in Vietnamese hospitals')
INSERT [dbo].[Topic] ([Topic_id], [TopicName]) VALUES (4, N'Share the Quality Improvement Plan.')
SET IDENTITY_INSERT [dbo].[Topic] OFF
GO
ALTER TABLE [dbo].[Achievement]  WITH CHECK ADD FOREIGN KEY([User_id])
REFERENCES [dbo].[DB_user] ([User_id])
GO
ALTER TABLE [dbo].[Post_Detail]  WITH CHECK ADD FOREIGN KEY([Topic_id])
REFERENCES [dbo].[Topic] ([Topic_id])
GO
ALTER TABLE [dbo].[Professional]  WITH CHECK ADD FOREIGN KEY([User_id])
REFERENCES [dbo].[DB_user] ([User_id])
GO
ALTER TABLE [dbo].[Qualification]  WITH CHECK ADD FOREIGN KEY([User_id])
REFERENCES [dbo].[DB_user] ([User_id])
GO
USE [master]
GO
ALTER DATABASE [DB_Doctorsforum] SET  READ_WRITE 
GO
