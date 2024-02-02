- Câu lệnh chạy khi cập nhật table hoặc entity

dotnet ef database update -c AppDbContext -p ../Account.Microservice.Infrastructure/Account.Microservice.Infrastructure.csproj -s Account.Microservice.Web.csproj

- Câu lệnh này nếu khởi tạo project

dotnet ef migrations add InitialModel --context AppDbContext -p ../Account.Microservice.Infrastructure/Account.Microservice.Infrastructure.csproj -s Account.Microservice.Web.csproj


- Câu lệnh xoá migration gần nhất
dotnet ef migrations remove -p ../Account.Microservice.Infrastructure/Account.Microservice.Infrastructure.csproj -s Account.Microservice.Web.csproj