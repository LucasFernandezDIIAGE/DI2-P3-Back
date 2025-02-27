using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

public partial class UpdatePasswordRelation : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Ajout de la colonne ApplicationId dans Passwords
        migrationBuilder.AddColumn<int>(
            name: "ApplicationId",
            table: "Passwords",
            type: "int",
            nullable: false,
            defaultValue: 0);

        // Suppression de la table de liaison ApplicationPasswords
        migrationBuilder.DropTable(name: "ApplicationPasswords");

        // Ajout de la contrainte de clé étrangère sur ApplicationId
        migrationBuilder.CreateIndex(
            name: "IX_Passwords_ApplicationId",
            table: "Passwords",
            column: "ApplicationId");

        migrationBuilder.AddForeignKey(
            name: "FK_Passwords_Applications_ApplicationId",
            table: "Passwords",
            column: "ApplicationId",
            principalTable: "Applications",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Suppression de la contrainte de clé étrangère
        migrationBuilder.DropForeignKey(
            name: "FK_Passwords_Applications_ApplicationId",
            table: "Passwords");

        // Suppression de l'index
        migrationBuilder.DropIndex(
            name: "IX_Passwords_ApplicationId",
            table: "Passwords");

        // Suppression de la colonne ApplicationId
        migrationBuilder.DropColumn(
            name: "ApplicationId",
            table: "Passwords");

        // Récréation de la table de liaison ApplicationPasswords
        migrationBuilder.CreateTable(
            name: "ApplicationPasswords",
            columns: table => new
            {
                ApplicationId = table.Column<int>(type: "int", nullable: false),
                PasswordId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationPasswords", x => new { x.ApplicationId, x.PasswordId });
                table.ForeignKey(
                    name: "FK_ApplicationPasswords_Applications_ApplicationId",
                    column: x => x.ApplicationId,
                    principalTable: "Applications",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ApplicationPasswords_Passwords_PasswordId",
                    column: x => x.PasswordId,
                    principalTable: "Passwords",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ApplicationPasswords_PasswordId",
            table: "ApplicationPasswords",
            column: "PasswordId");
    }
}
