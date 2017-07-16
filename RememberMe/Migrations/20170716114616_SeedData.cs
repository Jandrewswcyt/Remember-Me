using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RememberMe.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Friends (Name) VALUES ('Sundip Dogra')"); 
            migrationBuilder.Sql("INSERT INTO Friends (Name) VALUES ('Eszter Mandli')"); 
            migrationBuilder.Sql("INSERT INTO Friends (Name) VALUES ('Gurpreet Kalsi')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Friends WHERE Name In ('Sundip Dogra','Eszter Mandli','Gurpreet Kalsi')"); 
        }
    }
}
