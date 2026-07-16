# Todo List

Angular arayüzü ve ASP.NET Core Web API'den oluşan, CQRS ve Repository Pattern kullanan basit bir görev yönetimi uygulamasıdır.

## Teknolojiler

- .NET 8, ASP.NET Core Web API, Entity Framework Core 8
- Microsoft SQL Server / LocalDB
- Angular 22, TypeScript, HTML ve SCSS
- Swagger / OpenAPI

## Proje yapısı

```text
TodoList/
├── todo-backend/         ASP.NET Core Web API
│   ├── Application/      Commands, Queries ve bunların handler sınıfları
│   ├── Controllers/      REST API controller
│   ├── Domain/           Todo entity
│   ├── Infrastructure/   EF Core DbContext ve repository
│   ├── Migrations/       EF Core migration dosyaları
│   ├── TodoList.Api.csproj
│   └── TodoList.Api.slnx
├── todo-frontend/        Angular uygulaması
├── README.md
└── .gitignore
```

Controller veritabanına doğrudan erişmez. İstekleri command/query handler sınıflarına, handler sınıfları da `IToDoRepository` üzerinden EF Core repository'sine iletir.

## Backend'i çalıştırma

Gereksinimler: .NET 8 SDK ve SQL Server LocalDB veya erişilebilir bir SQL Server örneği.

```powershell
cd todo-backend
dotnet restore
dotnet ef database update
dotnet run --launch-profile https
```

Swagger geliştirme ortamında `https://localhost:7075/swagger` adresindedir.

## Veritabanı ve migration

Bağlantı `todo-backend/appsettings.json` içindeki `ConnectionStrings:DefaultConnection` alanından okunur. Depodaki varsayılan değer Windows kimlik doğrulamalı LocalDB içindir ve parola içermez. Kendi SQL Server ortamınız için bu değeri User Secrets veya ortam değişkeniyle geçersiz kılın:

```powershell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=SUNUCU;Database=Todolist2Db;Trusted_Connection=True;TrustServerCertificate=True"
```

Migration komutları:

```powershell
dotnet tool install --global dotnet-ef
dotnet ef migrations add MigrationAdi
dotnet ef database update
dotnet ef migrations list
```

Migration uygulamak mevcut kayıtları silmez. Ancak `AddTodoLengthConstraints` migration'ından önce mevcut Title/Description değerlerinin yeni uzunluk sınırlarını aşmadığı kontrol edilmelidir.

## Frontend'i çalıştırma

Gereksinimler: Güncel Node.js LTS ve npm. API adresi `todo-frontend/src/app/services/todo.service.ts` içinde tanımlıdır.

```powershell
cd todo-frontend
npm ci
npm start
```

Arayüz `http://localhost:4200` adresinde açılır. Production build için:

```powershell
npm run build
```

## API endpointleri

| Metot | Adres | Açıklama | Başarılı sonuç |
|---|---|---|---|
| GET | `/api/todo` | Tüm görevler | `200 OK` |
| GET | `/api/todo/{id}` | Guid ile görev | `200 OK`, yoksa `404` |
| POST | `/api/todo` | Görev oluşturur | `201 Created` |
| PUT | `/api/todo/{id}` | Görevi günceller | `204 No Content`, yoksa `404` |
| DELETE | `/api/todo/{id}` | Görevi siler | `204 No Content`, yoksa `404` |

POST örneği:

```json
{
  "title": "Raporu tamamla",
  "description": "Staj raporunun son kontrolünü yap"
}
```

PUT örneği (`id` yalnızca URL'de gönderilir):

```json
{
  "title": "Raporu teslim et",
  "description": "Son sürümü danışmana gönder",
  "isCompleted": true
}
```

GET ve DELETE isteklerinin gövdesi yoktur. `Title` zorunlu ve en fazla 100, `Description` en fazla 500 karakterdir. Yeni kayıtlarda Guid, `CreatedAt` ve başlangıç tamamlanma durumu backend tarafından atanır.

## CORS ve güvenlik

Backend geliştirme ortamında `http://localhost:4200` kaynağına izin verir. Gerçek kullanıcı adı veya parola repoya eklenmemelidir; production bağlantıları ortam değişkenleri veya secret yönetimiyle sağlanmalıdır. Projede Docker veya Railway yapılandırması bulunmamaktadır.
