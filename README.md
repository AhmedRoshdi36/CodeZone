# CodeZone.MVC

A warehouse management system built with ASP.NET Core MVC (.NET 9.0) that allows you to manage warehouses, products, and stock transactions.

##  Architecture

This project follows **Clean Architecture** principles with a three-layer structure:

- **CodeZone.DAL** (Data Access Layer) - Entities, Repositories, and DbContext
- **CodeZone.BLL** (Business Logic Layer) - Services and business logic
- **CodeZone.MVC** (Presentation Layer) - Controllers, Views, and UI

##  Features

- **Warehouse Management**: Create, read, update, and delete warehouses
- **Product Management**: Manage products with SKU, name, and description
- **Stock Transactions**: Track stock movements (add/remove) between warehouses and products
- **Repository Pattern**: Implements Unit of Work pattern for data access
- **Dependency Injection**: Fully configured DI container
- **Database Seeding**: Automatic database initialization with sample data

##  Technology Stack

- **.NET 9.0**
- **ASP.NET Core MVC**
- **Entity Framework Core** (In-Memory Database)
- **Bootstrap** (UI Framework)
- **jQuery** (Client-side scripting)

##  Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- Visual Studio 2022, Visual Studio Code, or any compatible IDE

##  Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd CodeZone.MVC
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Build the Solution

```bash
dotnet build
```

### 4. Run the Application

```bash
cd CodeZone.MVC
dotnet run
```

## Project Structure

```
CodeZone.MVC/
├── CodeZone.DAL/              # Data Access Layer
│   ├── Entities/              # Domain entities (Product, Warehouse, StockTransaction)
│   ├── Repositories/          # Repository implementations and interfaces
│   ├── Configurations/        # EF Core entity configurations
│   ├── Persistence/           # DbContext
│   └── Seed/                  # Database seeding
│
├── CodeZone.BLL/              # Business Logic Layer
│   ├── Services/              # Business services
│   ├── ViewModels/            # View models
│   └── Results/               # Result patterns
│
└── CodeZone.MVC/              # Presentation Layer
    ├── Controllers/          # MVC controllers
    ├── Views/                  # Razor views
    ├── Models/                # View models
    └── wwwroot/               # Static files (CSS, JS, images)
```

##  Database

The application uses **Entity Framework Core In-Memory Database** for development. The database is automatically seeded with sample data when the application starts.

### Entities

- **Warehouse**: Represents a storage location
  - `Id`: Unique identifier
  - `Name`: Warehouse name (unique)

- **Product**: Represents a product/item
  - `Id`: Unique identifier
  - `Name`: Product name
  - `SKU`: Stock Keeping Unit (unique identifier)
  - `Description`: Product description

- **StockTransaction**: Represents stock movements
  - `Id`: Unique identifier
  - `WarehouseId`: Foreign key to Warehouse
  - `ProductId`: Foreign key to Product
  - `Quantity`: Stock quantity (positive to add, negative to remove)





### Warehouse Management

- Navigate to `/Warehouse` to view all warehouses
- Create new warehouses with unique names
- Edit or delete existing warehouses
- View warehouse details including associated stock transactions

### Product Management

- Navigate to `/Product` to manage products
- Create products with SKU, name, and description
- Edit or delete products
- View product details

### Stock Transactions

- Navigate to `/StockTransaction` to manage stock movements
- Create transactions to add or remove stock
- Associate transactions with warehouses and products
- Quantity must be non-zero (positive for additions, negative for removals)

##  Dependencies

### Main Packages

- `Microsoft.EntityFrameworkCore.Tools` (9.0.11)
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` (9.0.11)



##  Acknowledgments

- ASP.NET Core team for the excellent framework
- Entity Framework Core for robust data access

---

**Note**: This application uses an in-memory database, so all data will be lost when the application restarts. 
