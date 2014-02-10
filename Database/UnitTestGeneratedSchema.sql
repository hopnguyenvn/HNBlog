
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA4004D62E142A3F2]') AND parent_object_id = OBJECT_ID('Posts'))
alter table Posts  drop constraint FKA4004D62E142A3F2


    if exists (select * from dbo.sysobjects where id = object_id(N'Blogs') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blogs

    if exists (select * from dbo.sysobjects where id = object_id(N'Posts') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Posts

    create table Blogs (
        BlogId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       TagLine NVARCHAR(255) null,
       primary key (BlogId)
    )

    create table Posts (
        PostId INT IDENTITY NOT NULL,
       Title NVARCHAR(255) null,
       PostContent NVARCHAR(255) null,
       CreatedDate DATETIME null,
       BlogId INT null,
       primary key (PostId)
    )

    alter table Posts 
        add constraint FKA4004D62E142A3F2 
        foreign key (BlogId) 
        references Blogs
