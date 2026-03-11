Команды чтобы не забыть:
- запустить psql: `docker exec -it college_postgres psql -U admin -d college_db`
- запустить миграцию: `docker exec -it college_postgres psql -U admin -d college_db -f /Migrations/<миграция>`
- локальная установка ef tools: `dotnet tool install dotnet-ef`
- подключение БД к приложению:
`dotnet ef dbcontext scaffold \
  "Host=localhost;Port=5000;Database=college_db;Username=admin;Password=secret" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --output-dir Data \
  --context AppDBContext`
