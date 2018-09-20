namespace DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.MyDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DAL.MyDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Areas.AddOrUpdate(
                new Area { ID = 1, Name = "Tầng 1" },
                new Area { ID = 2, Name = "Tầng 2" }
            );

            context.Tables.AddOrUpdate(
                new Table { ID = 1, Name = "Bàn 101", AreaID = 1 },
                new Table { ID = 2, Name = "Bàn 102", AreaID = 1 },
                new Table { ID = 3, Name = "Bàn 103", AreaID = 1 },
                new Table { ID = 4, Name = "Bàn 104", AreaID = 1 },
                new Table { ID = 5, Name = "Bàn 201", AreaID = 2 },
                new Table { ID = 6, Name = "Bàn 202", AreaID = 2 },
                new Table { ID = 7, Name = "Bàn 203", AreaID = 2 },
                new Table { ID = 8, Name = "Bàn 204", AreaID = 2 }
            );

            context.Departments.AddOrUpdate(
                new Department { ID = 1, Name = "Quản lý" },
                new Department { ID = 2, Name = "Thu ngân" }
            );

            context.Employees.AddOrUpdate(
                new Employee
                {
                    ID = 1,
                    Name = "Hiếu Đẹp Trai 1",
                    Username = "hieudeptrai1",
                    Password = "d85969d8a3f9dfe24dc27ecd7546a912"
                },
                new Employee
                {
                    ID = 2,
                    Name = "Hiếu Đẹp Trai 2",
                    Username = "hieudeptrai2",
                    Password = "d85969d8a3f9dfe24dc27ecd7546a912"
                }
            );

            context.EmployeeDepartments.AddOrUpdate(
                new EmployeeDepartment
                {
                    EmployeeID = 1,
                    DepartmentID = 1
                },
                new EmployeeDepartment
                {
                    EmployeeID = 1,
                    DepartmentID = 2
                },
                new EmployeeDepartment
                {
                    EmployeeID = 2,
                    DepartmentID = 2
                }
            );
        }
    }
}
