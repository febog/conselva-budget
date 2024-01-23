# ConselvaBudget

## Contents

This repository contains Visual Studio Solution with 2 projects:

1. **ConselvaBudget** [ASP.NET Core Razor Pages](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/) web app. This is the main application and provides a website for managing the budget for Conselva. It uses [Entity Framework Core](https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro) and a SQL database for storing the business data.
2. **ConselvaBudget.Tests** Tests project for the main web application.

## Database migrations

I commonly reset and add migrations as I develop the data model. For reference, here are the steps that I follow to reset the migrations and start from a fresh database.

Note that I specify the `-Context` since this solution has more than one `DBContext`.

### To reset database migrations

This drops the database and starts fresh.

- `Drop-Database -Context ConselvaBudgetContext`
- Delete Migrations folder.
- `Add-Migration InitialCreate -Context ConselvaBudgetContext -OutputDir Data/ConselvaMigrations`
- `Update-Database -Context ConselvaBudgetContext`

### Add Migration

- `Add-Migration AddDatesToProjects -Context ConselvaBudgetContext`
- `Update-Database -Context ConselvaBudgetContext`
