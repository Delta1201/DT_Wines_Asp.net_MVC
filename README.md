# DT_Wines_Asp.net_MVC
 This is an ASP.NET MVC Test practice of PROG1322 given by D.Stovell for managing wines and wine types. It implements a one-to-many relationship where a single wine belongs to only one wine type, but multiple wines can belong to the same type. The project includes validation, error handling, CRUD operations, sorting, filtering, and basic auditing.

1. Database Schema

- Wine\_Type Table
  - `ID` (Primary Key, Auto-increment)
  - `WineTypeName` (Required, Max length: 50)
- Wine Table
  - `ID` (Primary Key, Auto-increment)
  - `WineName` (Required, Max length: 70)
  - `WineYear` (Required, Exactly 4 digits)
  - `WinePrice` (Required, Double, Less than \$1000, Currency format)
  - `WineHarvest` (Optional, Short Date format)
  - `Wine_TypeID` (Foreign Key, Required)
  - `WineDescription` (Computed property: `WineName - WineYear`)
  - **Unique Constraint**: Combination of `WineName` and `WineYear`

2. Validations & Error Handling

- User-friendly validation messages for incorrect inputs.
- Ensures `WineHarvest` (if provided) falls within the designated `WineYear`.
- Prevents duplicate wines with the same name in the same year.
- Prevents deletion of `Wine_Type` if associated wines exist.

3. CRUD Operations

- Full Create, Read, Update, and Delete operations for both `Wine` and `Wine_Type`.
- Dropdown for selecting `Wine_Type` when creating a wine (with validation prompt).
- User-friendly UI with formatted labels.
- Navigation updates in `_Layout.cshtml`.

4. Sorting & Filtering

- **Filtering**: By wine type and characters in wine name.
- **Sorting**: By `WineName`, `WineYear`, and `WinePrice`.

5. Auditing
- Basic auditing added to track changes in `Wine` and `Wine_Type` records.

Prerequisites
- .NET SDK (Latest version)
- SQL Server or LocalDB
- Visual Studio with ASP.NET MVC support

Technologies Used
- ASP.NET MVC
- Entity Framework Core
- SQL Server
- Bootstrap (for UI styling)

License:This project is licensed under the MIT License.

Credits Developed by: Dhaval Tailor Course: PROG 1332 Advanced Web Development Fall 2024 Instructor: Dave Stovell

