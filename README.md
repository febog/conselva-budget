# ConselvaBudget

## Database migrations

For reference, these are the steps that I use to reset and add database migrations during development. I specify the `-Context` since this solution has more than one `DBContext`.

### To reset database migrations

This drops the database and starts fresh.

- Drop-Database -Context ConselvaBudgetContext
- Delete Migrations folder
- Add-Migration InitialCreate -Context ConselvaBudgetContext -OutputDir Data/ConselvaMigrations
- Update-Database -Context ConselvaBudgetContext

### Add Migration

- Add-Migration AddDatesToProjects -Context ConselvaBudgetContext
- Update-Database -Context ConselvaBudgetContext
