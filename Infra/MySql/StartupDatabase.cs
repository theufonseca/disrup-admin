using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.MySql
{
    public static class StartupDatabase
    {
        public static void CreateTables(this IApplicationBuilder app)
        {
            if (!ExistsTable(app, Company))
                CreateTable(app, Company);

            if (!ExistsTable(app, Photo))
                CreateTable(app, Photo);
        }

        private static bool ExistsTable(IApplicationBuilder app, string tableName)
        {
            var serviceScopeFactory = (IServiceScopeFactory)app.ApplicationServices.GetService(typeof(IServiceScopeFactory))!;
            using var scope = serviceScopeFactory.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            if (context == null)
                throw new Exception("Error when startup database");

            using var connection = context.Database.GetDbConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @$"SELECT 1 FROM INFORMATION_SCHEMA.TABLES
                                    where table_name like '{tableName}';";
            context.Database.OpenConnection();
            using var result = command.ExecuteReader();
            return result.HasRows;
        }

        private static void CreateTable(IApplicationBuilder app, string table)
        {
            var serviceScopeFactory = (IServiceScopeFactory)app.ApplicationServices.GetService(typeof(IServiceScopeFactory))!;
            using var scope = serviceScopeFactory.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            if (context == null)
                throw new Exception("Error when startup database");

            using var connection = context.Database.GetDbConnection();
            using var command = connection.CreateCommand();
            command.CommandText = scripts[table];
            context.Database.OpenConnection();
            command.ExecuteNonQuery();
        }

        private static readonly string Company = "Company";
        private static readonly string Photo = "Photo";

        private static Dictionary<string, string> scripts = new Dictionary<string, string>
        {
            { "Company",
             @"
                use disrup;
                Create table Company
                (
	                Id int not null auto_increment,
                    CreateDate Datetime not null, 
                    Status int not null,
                    Document varchar(50) not null, 
                    LegalName varchar(200) not null, 
                    FantasyName varchar(200) not null,
                    Name varchar(200) not null,
                    Site varchar(500),
                    Email varchar(500),
                    Phone varchar(20),
	                PlaystoreLink varchar(500),
	                AppleStoreLink varchar(500),
                    LinkedInLink varchar(500),
                    InstagramLink varchar(500),
                    FacebookLink varchar(500),
                    TwitterLink varchar(500),
                    YoutubeChannelLink varchar(500),
                    SolvedProblemDescription text,
                    Mission text,
                    Vision text,
                    CompanyValues text,
                    DisrupIdeia text,
                    SocietyContribuition text,
                    WorkEnvironment text,
                    Category1 varchar(50),
                    Category2 varchar(50),
                    Category3 varchar(50),
                    Category4 varchar(50),
                    Category5 varchar(50),
                    primary key (Id)
                );"
            },
            { 
                "Photo",
                @"
                use disrup;        
                Create Table Photo
                (
	                Id int not null auto_increment primary key,
                    CompanyId int not null,
                    Name varchar(100) not null,
                    Url varchar(300) not null,
                    IsThumb bit not null,
                    CreateDate datetime,
                    foreign key (CompanyId) references Company(Id)
                );"
            },
        };
    }
}
