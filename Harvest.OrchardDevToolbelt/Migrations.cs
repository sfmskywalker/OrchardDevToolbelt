using System.Data;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Roles.Services;

namespace Harvest.OrchardDevToolbelt {
    public class Migrations : DataMigrationImpl {
        private readonly IRoleService _roleService;

        public Migrations(IRoleService roleService) {
            _roleService = roleService;
        }

        public int Create() {

            SchemaBuilder.CreateTable("Recipe", table => table
                .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                .Column("Name", DbType.String, column => column.WithLength(50))
                .Column("Category_Id", DbType.Int32));

            SchemaBuilder.CreateTable("RecipeCategory", table => table
                .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                .Column("Name", DbType.String, column => column.WithLength(50)));

            SchemaBuilder.CreateTable("Ingredient", table => table
                .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                .Column("Name", DbType.String, column => column.WithLength(50))
                .Column("Recipe_Id", DbType.Int32)
                .Column("Quantity", DbType.Single)
                .Column("Unit", DbType.String));


            SchemaBuilder.CreateTable("ContactFormEntry", table => table
                .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                .Column("Name", DbType.String, column => column.WithLength(50))
                .Column("Email", DbType.String, column => column.WithLength(50))
                .Column("Subject", DbType.String, column => column.WithLength(256))
                .Column("MessageBody", DbType.String, column => column.Unlimited())
                .Column("CreatedUtc", DbType.DateTime));

            return 1;
        }

        public int UpdateFrom1() {

            SchemaBuilder.CreateTable("ContactFormEntryPartRecord", table => table
                .ContentPartRecord()
                .Column("SenderName", DbType.String, column => column.WithLength(50))
                .Column("SenderEmail", DbType.String, column => column.WithLength(50))
                .Column("Subject", DbType.String, column => column.WithLength(256)));

            ContentDefinitionManager.AlterPartDefinition("ContactFormEntryPart", part => part
                .Attachable(false));

            ContentDefinitionManager.AlterTypeDefinition("ContactFormEntry", type => type
                .WithPart("CommonPart")
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("ContactFormEntryPart")
                .DisplayedAs("Contact Form Entry"));

            return 2;
        }

        public int UpdateFrom2() {
            SchemaBuilder.CreateTable("UserProfilePartRecord", table => table
                .ContentPartRecord()
                .Column("TwitterAlias", DbType.String, column => column.WithLength(50)));

            return 3;
        }

    }
}