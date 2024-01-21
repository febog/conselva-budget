namespace ConselvaBudget.Authorization
{
    public class Roles
    {
        public static readonly string Administrator = "Administrator";
        public static readonly string Management = "Management";
        public static readonly string Employee = "Employee";
    }

    public class Policies
    {
        public static readonly string RequireAdministratorRole = "RequireAdministratorRole";
        public static readonly string RequireManagementRole = "RequireManagementRole";
        public static readonly string RequireEmployeeRole = "RequireEmployeeRole";
    }
}
