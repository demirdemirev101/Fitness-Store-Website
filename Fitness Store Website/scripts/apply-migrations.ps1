param(
    [string]$MigrationName = "AddOrdersAndProducts"
)

Write-Host "Creating migration: $MigrationName"
dotnet ef migrations add $MigrationName

Write-Host "Applying database updates"
dotnet ef database update

Write-Host "Done"
