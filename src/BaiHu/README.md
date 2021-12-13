# BaiHu

BaiHi -- White Tiger -- is the interface between the MQTT-Topics and the database. Data is uploaded via QingLong to the database.

## Getting Started

```PowerShell
dotnet restore
```

```PowerShell
dotnet run
```

## Docker Notes

### Build

```PowerShell
docker build -f Dockerfile -t rootthekid/bai-hu:latest .
```

### Push

```PowerShell
docker push rootthekid/bai-hu:latest
```

### Run

```PowerShell
docker run --rm -p 8802:80 --name BaiHu rootthekid/bai-hu:latest
```
