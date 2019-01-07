
# Restaurant Management

## Migrate Database
- Install SQLServer Express
- Create a database with name "QLNhaHang"
- In `DAL`, update `connectionStrings` in `App.config` (if necessary)
- Go to View > Other Windows > Package Manager Console
- At `Package Manager Console`, select default project is "DAL", run `Update-Database`

## Login

Admin

	Username: admin
	Password: admin

Normal user

	Username: user
	Password: user
## Main feature
- Manage areas/tables
- Manage menus
- Manage employees
- Create order (food, discount, vat, extra)
- Cancel food
- Change order's table
- Merge tables
- Print bill
- Analytics
- Export to excel file
