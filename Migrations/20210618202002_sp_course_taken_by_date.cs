/******Created by D.Perry | dd.perry @hotmail.com******/
using Microsoft.EntityFrameworkCore.Migrations;

namespace CenedexUniversityWebSystem.Migrations
{
    public partial class Sp_course_taken_by_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"
                CREATE OR ALTER PROC [Identity].[sp_course_taken_by_date](
                    @fName VARCHAR,
	                @lName VARCHAR,
	                @cName VARCHAR,
	                @StartDate DATE,
	                @EndDate DATE
	                )
                AS
                SET NOCOUNT ON
                BEGIN
	                ---- Select table columns for report. ----
                    SELECT
                        g2.*, s1.FirstName, s1.LastName
                    FROM 
                        [Identity].[Student] AS s1 JOIN [Identity].StudentGrade AS g2 on s1.Id = g2.StudentId
		                LEFT JOIN [Identity].Course AS c3 ON g2.CourseId = c3.Id
                    WHERE
	                    ---- Compare string  variables for Student's name. -----
                        FirstName LIKE '%' + @fName + '%' AND LastName LIKE '%' + @LName + '%'
	                AND
	                ---- Compare string  variables for Course taken by Student name. -----
	                c3.Name LIKE '%' + @cName + '%'
	                AND g2.DateTaken BETWEEN @StartDate AND @EndDate;
                END";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP PROC[Identity].[sp_course_taken_by_date]";
            migrationBuilder.Sql(procedure);
        }
    }
}
