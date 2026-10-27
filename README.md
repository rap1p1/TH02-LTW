# TH02 - Lap Trinh Web

## Comic Store - ASP.NET Core Web API

Bai thuc hanh 02 mon Lap Trinh Web.

---

## Cau truc du an

```
TH02-LTW/
├── backend/
│   └── ComicStore.API/          # ASP.NET Core Web API
│       ├── Controllers/
│       │   ├── ComicsController.cs   # Endpoint quan ly truyen
│       │   └── OrdersController.cs   # Endpoint quan ly don hang
│       ├── Data/
│       │   └── AppDbContext.cs        # EF Core DbContext
│       ├── Models/
│       │   ├── Comic.cs               # Model truyen
│       │   └── Order.cs               # Model don hang
│       ├── Migrations/
│       │   └── InitialCreate.cs       # Migration tao database
│       ├── Program.cs                 # Cau hinh dich vu va middleware
│       ├── appsettings.json           # Cau hinh ung dung (connection string)
│       └── ComicStore.API.csproj      # File du an .NET
└── frontend/
    ├── index.html     # Giao dien nguoi dung
    └── app.js         # Goi API bang Axios (GET, POST, PUT, DELETE)
```

---

## Yeu cau

- .NET 8 SDK: https://dotnet.microsoft.com/download
- Trinh duyet web (de chay frontend)

---

## Cach chay du an

### 1. Backend

Mo terminal, di chuyen vao thu muc backend:

```bash
cd backend/ComicStore.API
```

Restore cac thu vien:

```bash
dotnet restore
```

Cai EF Core Tools (chi can cai 1 lan):

```bash
dotnet tool install --global dotnet-ef
```

Tao migration:

```bash
dotnet ef migrations add InitialCreate
```

Tao database va ap dung migration:

```bash
dotnet ef database update
```

Chay server:

```bash
dotnet run
```

API se chay tai: `http://localhost:5000`

Swagger UI: `http://localhost:5000/swagger`

### 2. Frontend

Mo file `frontend/index.html` bang trinh duyet hoac dung Live Server trong VS Code.

---

## Cac Endpoint API

### Truyen (Comics)

| Phuong thuc | URL               | Chuc nang            |
|-------------|-------------------|----------------------|
| GET         | /api/comics       | Lay tat ca truyen    |
| GET         | /api/comics/{id}  | Lay truyen theo ID   |
| POST        | /api/comics       | Them truyen moi      |
| PUT         | /api/comics/{id}  | Cap nhat truyen      |
| DELETE      | /api/comics/{id}  | Xoa truyen           |

### Don hang (Orders)

| Phuong thuc | URL               | Chuc nang            |
|-------------|-------------------|----------------------|
| GET         | /api/orders       | Lay tat ca don hang  |
| GET         | /api/orders/{id}  | Lay don theo ID      |
| POST        | /api/orders       | Tao don hang moi     |
| PUT         | /api/orders/{id}  | Cap nhat don hang    |
| DELETE      | /api/orders/{id}  | Xoa don hang         |

---

## Cau hinh Database

File: `backend/ComicStore.API/appsettings.json`

Mac dinh dung SQLite (khong can cai them gi):

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=comicstore.db"
}
```

Doi sang SQL Server (them trong appsettings.json va chinh Program.cs):

```json
"ConnectionStrings": {
  "SqlServerConnection": "Server=localhost;Database=ComicStoreDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

---

## Cai dat va cau hinh EF Core

### Cac thu vien duoc su dung

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0" />
```

### Cac lenh EF Core can biet

```bash
# Tao migration moi
dotnet ef migrations add TenMigration

# Ap dung migration vao database
dotnet ef database update

# Xem danh sach migration
dotnet ef migrations list

# Hoan tac migration cuoi
dotnet ef migrations remove

# Tao lai database tu dau
dotnet ef database drop
dotnet ef database update
```

---

## Axios - Goi API tu Frontend

File `frontend/app.js` minh hoa cach goi API bang Axios:

```javascript
// GET
const response = await axios.get("http://localhost:5000/api/comics");

// POST
await axios.post("http://localhost:5000/api/comics", { title: "...", author: "..." });

// PUT
await axios.put("http://localhost:5000/api/comics/1", { id: 1, title: "..." });

// DELETE
await axios.delete("http://localhost:5000/api/comics/1");
```
