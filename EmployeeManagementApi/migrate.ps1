param(
    [ValidateSet("Local", "Azure")]
    [string]$Environment = "Local"
)

Push-Location $PSScriptRoot
try {
    if ($Environment -eq "Azure") {
        $connection = (dotnet user-secrets list |
            Select-String "ConnectionStrings:AzureConnection").ToString()

        $connection = $connection -replace '^.*?=\s*', ''

        dotnet ef database update --project . --startup-project . --connection $connection
    }
    else {
        dotnet ef database update --project . --startup-project .
    }
}
finally {
    Pop-Location
}
