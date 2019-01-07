namespace DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
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

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relative = @"..\..\";
            string absolute = Path.GetFullPath(Path.Combine(baseDirectory, relative));
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);

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
                    Name = "Admin",
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin", BCrypt.Net.BCrypt.GenerateSalt())
                },
                new Employee
                {
                    ID = 2,
                    Name = "User",
                    Username = "user",
                    Password = BCrypt.Net.BCrypt.HashPassword("user", BCrypt.Net.BCrypt.GenerateSalt())
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

            context.Menus.AddOrUpdate(
                new Menu{ ID = 1, Name = "Ăn sáng" },
                new Menu { ID = 2, Name = "Ăn trưa" },
                new Menu { ID = 3, Name = "Ăn tối" },
                new Menu { ID = 4, Name = "Nước" }
            );

            string rootPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            context.MenuItems.AddOrUpdate(
                new MenuItem {
                    ID = 1,
                    MenuID = 1,
                    Name = "Hủ tiếu nam vang",
                    Price = 20000,
                    PriceAfter = 20000,
                    Image = File.ReadAllBytes(rootPath+"/data/images/hu-tieu-nam-vang.jpg")
                },
                new MenuItem
                {
                    ID = 2,
                    MenuID = 1,
                    Name = "Bún mắm",
                    Price = 25000,
                    PriceAfter = 20000,
                    Image = File.ReadAllBytes(rootPath + "/data/images/bun-mam.jpg")
                },
                new MenuItem
                {
                    ID = 3,
                    MenuID = 1,
                    Name = "Cơm sườn",
                    Price = 15000,
                    PriceAfter = 15000,
                    Image = File.ReadAllBytes(rootPath + "/data/images/com-suon.jpg")
                },
                new MenuItem
                {
                    ID = 4,
                    MenuID = 1,
                    Name = "Bún riêu cua",
                    Price = 15000,
                    PriceAfter = 15000,
                    Image = File.ReadAllBytes(rootPath + "/data/images/bun-rieu-cua.jpg")
                },

                new MenuItem
                {
                    ID = 5,
                    MenuID = 2,
                    Name = "Sườn non xào chua ngọt",
                    Price = 25000,
                    PriceAfter = 20000,
                    Image = File.ReadAllBytes(rootPath + "/data/images/suon-non-xao-chua-ngot.jpg")
                },
                new MenuItem
                {
                    ID = 6,
                    MenuID = 2,
                    Name = "Canh chua cá lóc",
                    Price = 25000,
                    PriceAfter = 20000,
                    Image = File.ReadAllBytes(rootPath + "/data/images/canh-chua-ca-loc.jpg")
                },
                new MenuItem
                {
                    ID = 7,
                    MenuID = 2,
                    Name = "Cơm chiên cá mặn",
                    Price = 30000,
                    PriceAfter = 25000,
                    Image = File.ReadAllBytes(rootPath + "/data/images/com-chien-ca-man.jpg")
                },
                new MenuItem
                {
                    ID = 8,
                    MenuID = 2,
                    Name = "Sườn cây chiên xả",
                    Price = 30000,
                    PriceAfter = 25000,
                    Image = File.ReadAllBytes(rootPath + "/data/images/suon-cay-chien-xa.jpg")
                },
                new MenuItem
                {
                    ID = 9,
                    MenuID = 3,
                    Name = "Lẫu bò",
                    Price = 99000,
                    PriceAfter = 98000,
                    Image = File.ReadAllBytes(rootPath + "/data/images/lau-bo.jpg")
                },
                new MenuItem
                {
                    ID = 10,
                    MenuID = 3,
                    Name = "Lẫu mắm",
                    Price = 55000,
                    PriceAfter = 50000,
                    Image = File.ReadAllBytes(rootPath + "/data/images/lau-mam.jpg")
                },
                new MenuItem
                {
                    ID = 11,
                    MenuID = 3,
                    Name = "Bánh xèo",
                    Price = 35000,
                    PriceAfter = 30000,
                    Image = File.ReadAllBytes(rootPath + "/data/images/banh-xeo.jpg")
                },
                new MenuItem
                {
                    ID = 12,
                    MenuID = 3,
                    Name = "Cá kho tộ",
                    Price = 25000,
                    PriceAfter = 20000,
                    Image = File.ReadAllBytes(rootPath + "/data/images/ca-kho-to.jpg")
                },
                new MenuItem
                {
                    ID = 13,
                    MenuID = 4,
                    Name = "Coca Cola",
                    Price = 10000,
                    PriceAfter = 9500,
                    Image = File.ReadAllBytes(rootPath + "/data/images/cocacola.jpg")
                },
                new MenuItem
                {
                    ID = 14,
                    MenuID = 4,
                    Name = "Mirinda Cam",
                    Price = 10000,
                    PriceAfter = 9500,
                    Image = File.ReadAllBytes(rootPath + "/data/images/mirinda-cam.jpg")
                },
                new MenuItem
                {
                    ID = 15,
                    MenuID = 4,
                    Name = "Mirinda Soda Kem",
                    Price = 10000,
                    PriceAfter = 9500,
                    Image = File.ReadAllBytes(rootPath + "/data/images/mirinda-soda-kem.jpg")
                }
            );

            
        }
    }
}
