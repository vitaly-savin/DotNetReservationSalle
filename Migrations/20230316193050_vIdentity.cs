using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class vIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });
/*
            migrationBuilder.CreateTable(
                name: "etatInvitation",
                columns: table => new
                {
                    idEtatInvitation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomEtatInvitation = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etatInvitation", x => x.idEtatInvitation);
                });


            migrationBuilder.CreateTable(
                name: "etatReservation",
                columns: table => new
                {
                    idEtatReservation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomEtatReservation = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etatReservation", x => x.idEtatReservation);
                });

            migrationBuilder.CreateTable(
                name: "Personne",
                columns: table => new
                {
                    courriel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    motDePasse = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    nom = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    prenom = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personne", x => x.courriel);
                });
*/
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
/*
            migrationBuilder.CreateTable(
                name: "Administrateur",
                columns: table => new
                {
                    courriel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    matricule = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrateur", x => x.courriel);
                    table.ForeignKey(
                        name: "FK_Administrateur_Personne",
                        column: x => x.courriel,
                        principalTable: "Personne",
                        principalColumn: "courriel");
                });

            migrationBuilder.CreateTable(
                name: "Membre",
                columns: table => new
                {
                    courriel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    adresse = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    province = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    codePostal = table.Column<string>(type: "varchar(7)", unicode: false, maxLength: 7, nullable: true),
                    telephone = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    estActif = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    etatModifierParAdministrateurCourriel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membre", x => x.courriel);
                    table.ForeignKey(
                        name: "FK_Membre_Administrateur",
                        column: x => x.etatModifierParAdministrateurCourriel,
                        principalTable: "Administrateur",
                        principalColumn: "courriel");
                    table.ForeignKey(
                        name: "FK_Membre_Personne",
                        column: x => x.courriel,
                        principalTable: "Personne",
                        principalColumn: "courriel");
                });

            migrationBuilder.CreateTable(
                name: "SalleLaboratoire",
                columns: table => new
                {
                    noSalle = table.Column<int>(type: "int", nullable: false),
                    capacite = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    estActif = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    creerParAdministrateurCourriel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalleLaboratoire", x => x.noSalle);
                    table.ForeignKey(
                        name: "FK_SalleLaboratoire_Administrateur",
                        column: x => x.creerParAdministrateurCourriel,
                        principalTable: "Administrateur",
                        principalColumn: "courriel");
                });

            migrationBuilder.CreateTable(
                name: "TypeActivite",
                columns: table => new
                {
                    nomActivite = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    estActif = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    creerParAdministrateurCourriel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeActivite", x => x.nomActivite);
                    table.ForeignKey(
                        name: "FK_TypeActivite_Administrateur",
                        column: x => x.creerParAdministrateurCourriel,
                        principalTable: "Administrateur",
                        principalColumn: "courriel");
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    noReservation = table.Column<int>(type: "int", nullable: false),
                    dateHeureDebut = table.Column<DateTime>(type: "datetime", nullable: false),
                    dateHeureFin = table.Column<DateTime>(type: "datetime", nullable: false),
                    traiterParAdministrateurCourriel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    traiterLe = table.Column<DateTime>(type: "date", nullable: true),
                    idEtatReservation = table.Column<int>(type: "int", nullable: false),
                    creerParMembreCourriel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    noSalle = table.Column<int>(type: "int", nullable: false),
                    nomActivite = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.noReservation);
                    table.ForeignKey(
                        name: "FK_Reservation_Administrateur",
                        column: x => x.traiterParAdministrateurCourriel,
                        principalTable: "Administrateur",
                        principalColumn: "courriel");
                    table.ForeignKey(
                        name: "FK_Reservation_Membre",
                        column: x => x.creerParMembreCourriel,
                        principalTable: "Membre",
                        principalColumn: "courriel");
                    table.ForeignKey(
                        name: "FK_Reservation_SalleLaboratoire",
                        column: x => x.noSalle,
                        principalTable: "SalleLaboratoire",
                        principalColumn: "noSalle");
                    table.ForeignKey(
                        name: "FK_Reservation_TypeActivite",
                        column: x => x.nomActivite,
                        principalTable: "TypeActivite",
                        principalColumn: "nomActivite");
                    table.ForeignKey(
                        name: "FK_Reservation_etatReservation",
                        column: x => x.idEtatReservation,
                        principalTable: "etatReservation",
                        principalColumn: "idEtatReservation");
                });

            migrationBuilder.CreateTable(
                name: "SalleLaboratoire_TypeActivite",
                columns: table => new
                {
                    NoSalle = table.Column<int>(type: "int", nullable: false),
                    NomActivite = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalleLaboratoire_TypeActivite", x => new { x.NoSalle, x.NomActivite });
                    table.ForeignKey(
                        name: "FK_SalleLaboratoire_TypeActivite_SalleLaboratoire",
                        column: x => x.NoSalle,
                        principalTable: "SalleLaboratoire",
                        principalColumn: "noSalle");
                    table.ForeignKey(
                        name: "FK_SalleLaboratoire_TypeActivite_TypeActivite",
                        column: x => x.NomActivite,
                        principalTable: "TypeActivite",
                        principalColumn: "nomActivite");
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    noReservation = table.Column<int>(type: "int", nullable: false),
                    membreCourriel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    idEtatInvitation = table.Column<int>(type: "int", nullable: false),
                    dateReponse = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => new { x.noReservation, x.membreCourriel });
                    table.ForeignKey(
                        name: "FK_Invitation_Membre",
                        column: x => x.membreCourriel,
                        principalTable: "Membre",
                        principalColumn: "courriel");
                    table.ForeignKey(
                        name: "FK_Invitation_Reservation",
                        column: x => x.noReservation,
                        principalTable: "Reservation",
                        principalColumn: "noReservation");
                    table.ForeignKey(
                        name: "FK_Invitation_etatInvitation",
                        column: x => x.idEtatInvitation,
                        principalTable: "etatInvitation",
                        principalColumn: "idEtatInvitation");
                });

            migrationBuilder.CreateTable(
                name: "Plainte",
                columns: table => new
                {
                    noReservation = table.Column<int>(type: "int", nullable: false),
                    membreCourriel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    datePlainte = table.Column<DateTime>(type: "date", nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    administrateurCourriel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plainte", x => new { x.noReservation, x.membreCourriel });
                    table.ForeignKey(
                        name: "FK_Plainte_Administrateur",
                        column: x => x.administrateurCourriel,
                        principalTable: "Administrateur",
                        principalColumn: "courriel");
                    table.ForeignKey(
                        name: "FK_Plainte_Membre",
                        column: x => x.membreCourriel,
                        principalTable: "Membre",
                        principalColumn: "courriel");
                    table.ForeignKey(
                        name: "FK_Plainte_Reservation",
                        column: x => x.noReservation,
                        principalTable: "Reservation",
                        principalColumn: "noReservation");
                });
*/
            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
/*
            migrationBuilder.CreateIndex(
                name: "IX_Invitation_idEtatInvitation",
                table: "Invitation",
                column: "idEtatInvitation");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_membreCourriel",
                table: "Invitation",
                column: "membreCourriel");

            migrationBuilder.CreateIndex(
                name: "IX_Membre_etatModifierParAdministrateurCourriel",
                table: "Membre",
                column: "etatModifierParAdministrateurCourriel");

            migrationBuilder.CreateIndex(
                name: "IX_Plainte_administrateurCourriel",
                table: "Plainte",
                column: "administrateurCourriel");

            migrationBuilder.CreateIndex(
                name: "IX_Plainte_membreCourriel",
                table: "Plainte",
                column: "membreCourriel");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_creerParMembreCourriel",
                table: "Reservation",
                column: "creerParMembreCourriel");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_idEtatReservation",
                table: "Reservation",
                column: "idEtatReservation");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_nomActivite",
                table: "Reservation",
                column: "nomActivite");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_noSalle",
                table: "Reservation",
                column: "noSalle");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_traiterParAdministrateurCourriel",
                table: "Reservation",
                column: "traiterParAdministrateurCourriel");

            migrationBuilder.CreateIndex(
                name: "IX_SalleLaboratoire_creerParAdministrateurCourriel",
                table: "SalleLaboratoire",
                column: "creerParAdministrateurCourriel");

            migrationBuilder.CreateIndex(
                name: "IX_SalleLaboratoire_TypeActivite_NomActivite",
                table: "SalleLaboratoire_TypeActivite",
                column: "NomActivite");

            migrationBuilder.CreateIndex(
                name: "IX_TypeActivite_creerParAdministrateurCourriel",
                table: "TypeActivite",
                column: "creerParAdministrateurCourriel");
*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");
            /*
            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "Plainte");

            migrationBuilder.DropTable(
                name: "SalleLaboratoire_TypeActivite");
            */
            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
            /*
            migrationBuilder.DropTable(
                name: "etatInvitation");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Membre");

            migrationBuilder.DropTable(
                name: "SalleLaboratoire");

            migrationBuilder.DropTable(
                name: "TypeActivite");

            migrationBuilder.DropTable(
                name: "etatReservation");

            migrationBuilder.DropTable(
                name: "Administrateur");

            migrationBuilder.DropTable(
                name: "Personne");
            */
        }
    }
}
