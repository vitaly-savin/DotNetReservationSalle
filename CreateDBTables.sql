create table dbo.AspNetRoles
(
    Id               nvarchar(450) not null
        constraint PK_AspNetRoles
        primary key,
    Name             nvarchar(256),
    NormalizedName   nvarchar(256),
    ConcurrencyStamp nvarchar(max)
)
    go

create table dbo.AspNetRoleClaims
(
    Id         int identity
        constraint PK_AspNetRoleClaims
        primary key,
    RoleId     nvarchar(450) not null
        constraint FK_AspNetRoleClaims_AspNetRoles_RoleId
        references dbo.AspNetRoles
        on delete cascade,
    ClaimType  nvarchar(max),
    ClaimValue nvarchar(max)
)
    go

create index IX_AspNetRoleClaims_RoleId
    on dbo.AspNetRoleClaims (RoleId)
    go

create unique index RoleNameIndex
    on dbo.AspNetRoles (NormalizedName)
    where [NormalizedName] IS NOT NULL
go

create table dbo.AspNetUsers
(
    Id                   nvarchar(450) not null
        constraint PK_AspNetUsers
        primary key,
    FirstName            nvarchar(max) not null,
    LastName             nvarchar(max) not null,
    UserName             nvarchar(256),
    NormalizedUserName   nvarchar(256),
    Email                nvarchar(256),
    NormalizedEmail      nvarchar(256),
    EmailConfirmed       bit           not null,
    PasswordHash         nvarchar(max),
    SecurityStamp        nvarchar(max),
    ConcurrencyStamp     nvarchar(max),
    PhoneNumber          nvarchar(max),
    PhoneNumberConfirmed bit           not null,
    TwoFactorEnabled     bit           not null,
    LockoutEnd           datetimeoffset,
    LockoutEnabled       bit           not null,
    AccessFailedCount    int           not null
)
    go

create table dbo.AspNetUserClaims
(
    Id         int identity
        constraint PK_AspNetUserClaims
        primary key,
    UserId     nvarchar(450) not null
        constraint FK_AspNetUserClaims_AspNetUsers_UserId
        references dbo.AspNetUsers
        on delete cascade,
    ClaimType  nvarchar(max),
    ClaimValue nvarchar(max)
)
    go

create index IX_AspNetUserClaims_UserId
    on dbo.AspNetUserClaims (UserId)
    go

create table dbo.AspNetUserLogins
(
    LoginProvider       nvarchar(450) not null,
    ProviderKey         nvarchar(450) not null,
    ProviderDisplayName nvarchar(max),
    UserId              nvarchar(450) not null
        constraint FK_AspNetUserLogins_AspNetUsers_UserId
        references dbo.AspNetUsers
        on delete cascade,
    constraint PK_AspNetUserLogins
        primary key (LoginProvider, ProviderKey)
)
    go

create index IX_AspNetUserLogins_UserId
    on dbo.AspNetUserLogins (UserId)
    go

create table dbo.AspNetUserRoles
(
    UserId nvarchar(450) not null
        constraint FK_AspNetUserRoles_AspNetUsers_UserId
        references dbo.AspNetUsers
        on delete cascade,
    RoleId nvarchar(450) not null
        constraint FK_AspNetUserRoles_AspNetRoles_RoleId
        references dbo.AspNetRoles
        on delete cascade,
    constraint PK_AspNetUserRoles
        primary key (UserId, RoleId)
)
    go

create index IX_AspNetUserRoles_RoleId
    on dbo.AspNetUserRoles (RoleId)
    go

create table dbo.AspNetUserTokens
(
    UserId        nvarchar(450) not null
        constraint FK_AspNetUserTokens_AspNetUsers_UserId
        references dbo.AspNetUsers
        on delete cascade,
    LoginProvider nvarchar(450) not null,
    Name          nvarchar(450) not null,
    Value         nvarchar(max),
    constraint PK_AspNetUserTokens
        primary key (UserId, LoginProvider, Name)
)
    go

create index EmailIndex
    on dbo.AspNetUsers (NormalizedEmail)
    go

create unique index UserNameIndex
    on dbo.AspNetUsers (NormalizedUserName)
    where [NormalizedUserName] IS NOT NULL
go

create table dbo.EtatInvitation
(
    idEtatInvitation  int identity
        constraint PK_etatInvitation
        primary key,
    nomEtatInvitation varchar(20) not null
)
    go

create table dbo.EtatReservation
(
    idEtatReservation  int identity
        constraint PK_etatReservation
        primary key,
    nomEtatReservation varchar(20) not null
)
    go

create table dbo.Personne
(
    courriel varchar(50) not null
        constraint PK_Personne
            primary key,
    nom      varchar(30) not null,
    prenom   varchar(30) not null
)
    go

create table dbo.Administrateur
(
    courriel  varchar(50) not null
        constraint PK_Administrateur
            primary key
        constraint FK_Administrateur_Personne
            references dbo.Personne,
    matricule varchar(30) not null
)
    go

create table dbo.Membre
(
    courriel                              varchar(50) not null
        constraint PK_Membre
            primary key
        constraint FK_Membre_Personne
            references dbo.Personne,
    adresse                               varchar(50),
    province                              varchar(25),
    codePostal                            varchar(7),
    telephone                             varchar(10),
    estActif                              bit
        constraint DF_Membre_estActif default 0,
    etatModifierParAdministrateurCourriel varchar(50)
        constraint FK_Membre_Administrateur
            references dbo.Administrateur
)
    go

create table dbo.SalleLaboratoire
(
    noSalle                        int                    not null
        constraint PK_SalleLaboratoire
            primary key,
    capacite                       int,
    description                    varchar(255),
    estActif                       bit
        constraint DF_SalleLaboratoire_estActif default 1 not null,
    creerParAdministrateurCourriel varchar(50)            not null
        constraint FK_SalleLaboratoire_Administrateur
            references dbo.Administrateur
)
    go

create table dbo.TypeActivite
(
    nomActivite                    varchar(20)        not null
        constraint PK_TypeActivite
            primary key,
    description                    varchar(100)       not null,
    estActif                       bit
        constraint DF_TypeActivite_estActif default 1 not null,
    creerParAdministrateurCourriel varchar(50)        not null
        constraint FK_TypeActivite_Administrateur
            references dbo.Administrateur
)
    go

create table dbo.Reservation
(
    noReservation                    int identity
        constraint PK_Reservation
        primary key,
    dateHeureDebut                   datetime    not null,
    dateHeureFin                     datetime    not null,
    traiterParAdministrateurCourriel varchar(50)
        constraint FK_Reservation_Administrateur
            references dbo.Administrateur,
    traiterLe                        date,
    idEtatReservation                int         not null
        constraint FK_Reservation_etatReservation
            references dbo.EtatReservation,
    creerParMembreCourriel           varchar(50) not null
        constraint FK_Reservation_Membre
            references dbo.Membre,
    noSalle                          int         not null
        constraint FK_Reservation_SalleLaboratoire
            references dbo.SalleLaboratoire,
    nomActivite                      varchar(20) not null
        constraint FK_Reservation_TypeActivite
            references dbo.TypeActivite
)
    go

create table dbo.Invitation
(
    noReservation    int         not null
        constraint FK_Invitation_Reservation
            references dbo.Reservation,
    membreCourriel   varchar(50) not null
        constraint FK_Invitation_Membre
            references dbo.Membre,
    idEtatInvitation int         not null
        constraint FK_Invitation_etatInvitation
            references dbo.EtatInvitation,
    dateReponse      date,
    constraint PK_Invitation
        primary key (noReservation, membreCourriel)
)
    go

create table dbo.Plainte
(
    noReservation          int          not null
        constraint FK_Plainte_Reservation
            references dbo.Reservation,
    membreCourriel         varchar(50)  not null
        constraint FK_Plainte_Membre
            references dbo.Membre,
    datePlainte            date         not null,
    description            varchar(max) not null,
    administrateurCourriel varchar(50)
        constraint FK_Plainte_Administrateur
            references dbo.Administrateur,
    constraint PK_Plainte
        primary key (noReservation, membreCourriel)
)
    go

CREATE TRIGGER afterInsertReservation
    ON  [dbo].[Reservation]
    AFTER INSERT
    AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @noReservation int
    DECLARE @creerParMembreCourriel varchar(50)

SELECT @noReservation= INSERTED.[noReservation],
       @creerParMembreCourriel= INSERTED.[creerParMembreCourriel]
FROM INSERTED

     --Ajouter l'invitation pour le créateur de la réservation.
    INSERT INTO [dbo].[Invitation]
([noReservation]
    ,[membreCourriel]
    ,[idEtatInvitation])
VALUES
    (@noReservation
        ,@creerParMembreCourriel
        ,1)

END
go

disable trigger dbo.afterInsertReservation on dbo.Reservation
go

create table dbo.SalleLaboratoire_TypeActivite
(
    noSalle     int         not null
        constraint FK_SalleLaboratoire_TypeActivite_SalleLaboratoire
            references dbo.SalleLaboratoire
            on update cascade on delete cascade,
    nomActivite varchar(20) not null
        constraint FK_SalleLaboratoire_TypeActivite_TypeActivite
            references dbo.TypeActivite
            on update cascade on delete cascade,
    constraint PK_SalleLaboratoire_TypeActivite
        primary key (noSalle, nomActivite)
)
    go

create table dbo.__EFMigrationsHistory
(
    MigrationId    nvarchar(150) not null
        constraint PK___EFMigrationsHistory
        primary key,
    ProductVersion nvarchar(32)  not null
)
    go

create table dbo.sysdiagrams
(
    name         sysname not null,
    principal_id int     not null,
    diagram_id   int identity
        primary key,
    version      int,
    definition   varbinary(max),
    constraint UK_principal_name
        unique (principal_id, name)
)
    go

exec sp_addextendedproperty 'microsoft_database_tools_support', 1, 'SCHEMA', 'dbo', 'TABLE', 'sysdiagrams'
go

