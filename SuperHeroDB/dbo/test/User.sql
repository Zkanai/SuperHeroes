SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Name], [UserName], [Email], [PassWord], [Salt]) VALUES (1, N'Main Smith', N'admin', N'mail@mail.com', N'EBBD74FC7FFB0398F7BB6792440EA664415622D811369CBCF58B89A63905D86A', N'ZDpF7bsAr3ZmiPyoXHKIyw==')
INSERT [dbo].[User] ([Id], [Name], [UserName], [Email], [PassWord], [Salt]) VALUES (2, N'Marek Zelinski', N'Maze83', N'mazer@nomail.com', N'A175831046488AAFD60E4BA957941425EE0FA4A3443FAE44D9CDD8D6B85EC7A2', N'fpcAazmgk7zkdi07+M5Ofw==')
INSERT [dbo].[User] ([Id], [Name], [UserName], [Email], [PassWord], [Salt]) VALUES (3, N'George Drake', N'SkyRow', N'gdrake@tomail.com', N'DD9AFC93E3A9F020CD4260E9784663170BD87CB2C7CAC4FA3F0F859AC9D58E2F', N'745GCFmM5jlTdGsyGx6wNg==')
SET IDENTITY_INSERT [dbo].[User] OFF
