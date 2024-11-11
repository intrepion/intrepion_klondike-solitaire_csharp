# Intrepion.KlondikeSolitaire

## Build

dotnet build

## Update database

```bash
dotnet tool install --global dotnet-ef
dotnet ef --project Intrepion.KlondikeSolitaire.BusinessLogic --startup-project Intrepion.KlondikeSolitaire database update
```

## Run

```bash
dotnet run --project Intrepion.KlondikeSolitaire --urls http://localhost:8765
```

## Test

### terminal window 1

```bash
dotnet tool install --global PowerShell
cd Intrepion.KlondikeSolitaire.AcceptanceTests
pwsh ./bin/Debug/net8.0/playwright.ps1 install --with-deps
cd ..
dotnet run --environment Test --project Intrepion.KlondikeSolitaire --urls http://localhost:8765
```

### terminal window 2

```bash
BASE_URL=http://localhost:8765 HEADED=1 dotnet test
```

## Generate Test Code

### terminal window 2

```bash
cd Intrepion.KlondikeSolitaire.AcceptanceTests
pwsh bin/Debug/net8.0/playwright.ps1 codegen http://localhost:8765
cd ..
```
