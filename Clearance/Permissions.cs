using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CenedexUniversityWebSystem.Clearance
{
    public class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.Details",
                $"Permissions.{module}.Index",
             };
        }

        public static class SudentGrade
        { 
            public const string Create = "Permissions.SudentGrade.Create";
            public const string Edit = "Permissions.SudentGrade.Edit";
            public const string Delete = "Permissions.SudentGrade.Delete";
            public const string Details = "Permissions.SudentGrade.Details";
            public const string Index = "Permissions.SudentGrade.Index";
        }

        public static class Course
        {
            public const string Create = "Permissions.Course.Create";
            public const string Edit = "Permissions.Course.Edit";
            public const string Delete = "Permissions.Course.Delete";
            public const string Details = "Permissions.Course.Details";
            public const string Index = "Permissions.Course.Index";
        }

    }
}
