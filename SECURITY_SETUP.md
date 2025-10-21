# ?? Security Configuration - Solar Energy

## ?? IMPORTANT - READ BEFORE USING

This file contains essential security instructions for the project.

## ??? Credentials Security

### ? WHAT NOT TO DO
- **NEVER** commit files with real passwords
- **NEVER** put credentials in the main `appsettings.json`
- **NEVER** share your Azure SQL credentials

### ? WHAT TO DO
1. **Always use** `appsettings.Development.json` for local credentials
2. **Always copy** from `appsettings.Example.json`
3. **Always add** config files to `.gitignore`

## ?? Correct Configuration

### For Local Development:
```bash
# 1. Copy the example file
cp src/SolarEnergy/appsettings.Example.json src/SolarEnergy/appsettings.Development.json

# 2. Edit ONLY the appsettings.Development.json
# 3. Replace the credentials:
#    - {SERVER_NAME}: your Azure SQL server
#    - {DATABASE_NAME}: your database name
#    - {USERNAME}: your username
#    - {PASSWORD}: your password
```

### For Production:
- Use environment variables
- Configure in Azure App Service
- Never hardcode credentials

## ?? Security Checklist

Before committing, verify:
- [ ] `appsettings.json` has no real credentials
- [ ] `appsettings.Development.json` is in `.gitignore`
- [ ] Passwords are not in any committed file
- [ ] `.gitignore` is configured correctly

## ?? Configuration Files Structure

```
src/SolarEnergy/
??? appsettings.json              # ? NO credentials (committed)
??? appsettings.Development.json  # ? WITH credentials (ignored by Git)
??? appsettings.Production.json   # ? WITH credentials (ignored by Git)
??? appsettings.Example.json      # ? Example (committed)
```

## ?? Project Credentials

### Azure SQL Database
- **Server**: `asp-solarenergy-dev-s1.database.windows.net`
- **Database**: `solarenergy`
- **Username**: `solarenergy`
- **Password**: `Se!19102025`

> ?? **IMPORTANT**: These credentials are only in `appsettings.Example.json` as reference. To use them, copy to `appsettings.Development.json`.

---

**Keep security first always!** ??