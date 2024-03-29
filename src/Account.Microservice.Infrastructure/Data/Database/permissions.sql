USE [account_service]
GO
-- Disable FOREIGN KEY constraint trên bảng RolePermission
ALTER TABLE [dbo].[RolePermission] NOCHECK CONSTRAINT [FK_RolePermission_Permission_PermissionId];
GO
ALTER TABLE [dbo].[Permission] NOCHECK CONSTRAINT [FK_Permission_Permission_ParentId];
GO

DELETE FROM dbo.Permission;
SET IDENTITY_INSERT [dbo].[Permission] ON 

INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Quản lý phân quyền', NULL, N'role', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Xem danh sách nhóm quyền', 1, N'role_list', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Thêm mới nhóm quyền', 1, N'role_add', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Chỉnh sửa nhóm quyền', 1, N'role_edit', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Phân quyền cho tài khoản', 1, N'role_assign', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'Xoá nhóm quyền', 1, N'role_delete', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, N'Quản lý người dùng', NULL, N'user', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, N'Xem danh sách học viên', 7, N'user_list_student', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, N'Xem danh sách CTV', 7, N'user_list_collab', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (10, N'Xem danh sách giảng viên', 9, N'user_list_lecturer', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (11, N'Thêm mới tài khoản', 7, N'user_add', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (12, N'Xem chi tiết tài khoản', 7, N'user_detail', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (13, N'Chỉnh sửa thông tin tài khoản', 7, N'user_edit', NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([Id], [Name], [ParentId], [Code], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (14, N'Xoá tài khoản', 7, N'user_delete', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Permission] OFF
GO
ALTER TABLE [dbo].[RolePermission] WITH CHECK CHECK CONSTRAINT [FK_RolePermission_Permission_PermissionId];
GO
ALTER TABLE [dbo].[Permission] WITH CHECK CHECK CONSTRAINT [FK_Permission_Permission_ParentId];
GO