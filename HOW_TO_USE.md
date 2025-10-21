# ?? How to Use - Solar Energy Platform

## ?? MANDATORY CONFIGURATION BEFORE RUNNING

### 1. Configure Database

The project uses **Azure SQL Database**. You need to configure your credentials:

1. **Copy the example file:**
   ```bash
   cp src/SolarEnergy/appsettings.Example.json src/SolarEnergy/appsettings.Development.json
   ```

2. **Edit the file `src/SolarEnergy/appsettings.Development.json`** with your real credentials:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=tcp:YOUR_SERVER.database.windows.net,1433;Initial Catalog=YOUR_DATABASE;User ID=YOUR_USERNAME;Password=YOUR_PASSWORD;..."
     }
   }
   ```

### 2. Run Migrations

```bash
cd src/SolarEnergy
dotnet ef database update
```

### 3. Run the Project

```bash
dotnet run
```

## ?? Access the Application

- **Local URL**: https://localhost:5001
- **Login Page**: https://localhost:5001/Auth/Login
- **Register Page**: https://localhost:5001/Auth/Register

## ?? User Types

1. **CONSUMER**: Individual looking for solar energy solutions
2. **COMPANY**: Business offering solar energy services
3. **ADMINISTRATOR**: Platform manager

## ?? Implemented Design System

### Solar Energy Colors
- **Primary Blue**: `#1e3a8a`
- **Highlight Orange**: `#ff6b35`
- **Gradient**: `linear-gradient(135deg, #1e3a8a 0%, #3b82f6 100%)`

### Ready Features
- ? Complete authentication system
- ? Responsive login and register pages
- ? Homepage with solar energy information
- ? Personalized dashboard
- ? Real-time validations
- ? Azure SQL Database integration

## ?? Technologies Used

- **Backend**: ASP.NET Core MVC + C#
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap 5
- **Database**: Azure SQL Database
- **ORM**: Entity Framework Core
- **Auth**: ASP.NET Identity

## ?? Responsiveness

The design is fully responsive and works on:
- ??? Desktop
- ?? Mobile
- ?? Tablet

## ?? Common Issues

### Database Connection Error
- Check if you configured `appsettings.Development.json`
- Confirm your Azure SQL credentials are correct

### Migrations Not Applied
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Port in Use
If port 5001 is busy, the project will try 5002, 5003, etc.

---

**Developed by Solar Energy team** ???