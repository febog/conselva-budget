# ConselvaBudget

[![Deploy workflow](https://github.com/febog/conselva-budget/actions/workflows/main_conselvanet(staging).yml/badge.svg)](https://github.com/febog/conselva-budget/actions/workflows/main_conselvanet(staging).yml)

Business ASP.NET Core app hosted in Azure App service.

## Table of contents

  - [Introduction](#introduction)
  - [Solution contents](#solution-contents)
  - [Database migrations](#database-migrations)
    - [Drop database and reset migrations](#drop-database-and-reset-migrations)
    - [Add a migration](#add-a-migration)

## Introduction

Conselva is a non-profit organization that works in the field of water conservation. As a non-profit working with different partners and projects, they have unique requirements for their budget management and the organization of their day-to-day activities. This project addresses their need for a tool that allows them to control and report on their budget spending effectively.

## Solution contents

This repository contains a Visual Studio Solution with 3 projects:

1. **ConselvaBudget** [ASP.NET Core Razor Pages](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/) web app. This is the main application and provides a website for managing the budget for Conselva. It uses [Entity Framework Core](https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro) and a SQL database for storing the business data.
2. **ConselvaBudget.Tests** Tests project for the main web application.
3. **ConselvaBudget.Seed** Console app to seed the database with business data. It contains a reference to the main project to reuse the model and `DBContext`. The data itself is not included in the repo.

## Database migrations

I commonly reset and add EF Core [database migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs) as I develop the data model. For reference, here are the steps that I follow.

These commands are to be executed in Visual Studio's *Package Manger Console* with the main web application project selected. Note that I specify the `-Context` since this project has more than one `DBContext`.

### Drop database and reset migrations

Deletes all migrations and creates a new migration with all the model information. This is especially useful early in development when the model can change drastically.

1. Delete the database.

```
Drop-Database -Context ConselvaBudgetContext
```

2. Delete the Migrations folder, `Data/ConselvaMigrations`.

3. Create a fresh migration with all the model data.

```
Add-Migration InitialCreate -Context ConselvaBudgetContext -OutputDir Data/ConselvaMigrations
```

4. Create the database and apply the migration.

```
Update-Database -Context ConselvaBudgetContext
```

### Add a migration

1. Crate a new migration with the model changes.

```
Add-Migration ChangeDescription -Context ConselvaBudgetContext
```

2. Push the changes to the database.

```
Update-Database -Context ConselvaBudgetContext
```

### Deploying migrations

To run the database migrations on the App Service using SSH:

1. Navigate to the deployed files.

```
cd /home/site/wwwroot
```

2. Run the migration bundle created by the [deployment workflow](https://learn.microsoft.com/en-us/azure/app-service/tutorial-dotnetcore-sqldb-app?toc=%2Faspnet%2Fcore%2Ftoc.json&bc=%2Faspnet%2Fcore%2Fbreadcrumb%2Ftoc.json&view=aspnetcore-8.0#4-generate-database-schema).

```
./migrate
```
