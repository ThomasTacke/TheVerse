# BaiHu

BaiHi -- White Tiger -- is a HTTP(S) to MQTT wrapper. For example it is used with Sleep as Android to switch on/off lights when the alarm rings or sleep tracking is activated.

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
docker run --rm -p 8802:80 --name qing-long rootthekid/bai-hu:latest
```
