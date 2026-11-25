IF OBJECT_ID(N'[dbo].[AspNetRoles]', N'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM [dbo].[AspNetRoles]
        WHERE [Id] = N'1'
    )
    BEGIN
        INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName])
        VALUES (N'1', N'Admin', N'ADMIN');
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM [dbo].[AspNetRoles]
        WHERE [Id] = N'2'
    )
    BEGIN
        INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName])
        VALUES (N'2', N'Employee', N'EMPLOYEE');
    END;
END;

IF OBJECT_ID(N'[dbo].[Employees]', N'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM [dbo].[Employees]
        WHERE [Id] = 'd43395fd-4f7a-4218-b302-70fae3356483'
    )
    BEGIN
        INSERT INTO [dbo].[Employees] ([Id], [Name], [Email], [Department], [Salary])
        VALUES ('d43395fd-4f7a-4218-b302-70fae3356483', N'Darshan S', N'darshan.s@email.com', N'Dev', 50000);
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM [dbo].[Employees]
        WHERE [Id] = 'a11195fd-4f7a-4218-b302-70fae3356483'
    )
    BEGIN
        INSERT INTO [dbo].[Employees] ([Id], [Name], [Email], [Department], [Salary])
        VALUES ('a11195fd-4f7a-4218-b302-70fae3356483', N'Takan', N'takan@email.com', N'Dev', 70000);
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM [dbo].[Employees]
        WHERE [Id] = 'a22295fd-4f7a-4218-b302-70fae3356483'
    )
    BEGIN
        INSERT INTO [dbo].[Employees] ([Id], [Name], [Email], [Department], [Salary])
        VALUES ('a22295fd-4f7a-4218-b302-70fae3356483', N'Pavan L', N'pavan.l@email.com', N'Dev', 70000);
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM [dbo].[Employees]
        WHERE [Id] = 'a33395fd-4f7a-4218-b302-70fae3356483'
    )
    BEGIN
        INSERT INTO [dbo].[Employees] ([Id], [Name], [Email], [Department], [Salary])
        VALUES ('a33395fd-4f7a-4218-b302-70fae3356483', N'Pavan H', N'pavan.h@email.com', N'Dev', 70000);
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM [dbo].[Employees]
        WHERE [Id] = 'a44495fd-4f7a-4218-b302-70fae3356483'
    )
    BEGIN
        INSERT INTO [dbo].[Employees] ([Id], [Name], [Email], [Department], [Salary])
        VALUES ('a44495fd-4f7a-4218-b302-70fae3356483', N'John Doe', N'john.doe@email.com', N'Sales', 50000);
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM [dbo].[Employees]
        WHERE [Id] = 'a55595fd-4f7a-4218-b302-70fae3356483'
    )
    BEGIN
        INSERT INTO [dbo].[Employees] ([Id], [Name], [Email], [Department], [Salary])
        VALUES ('a55595fd-4f7a-4218-b302-70fae3356483', N'Jane Doe', N'jane.doe@email.com', N'Sales', 60000);
    END;
END;
