Fitness Store Website
Кратко описание

Razor Pages / ASP.NET Core приложение (.NET 8) за онлайн магазин с продукти, админ панел и количка.
Поддържа качване на изображения за продукти (wwwroot/images), Stripe конфигурация за плащания (настройки в appsettings.json).
Бърз старт (локално)

Клонирай репото и влез в папката проекта:

git clone https://github.com/demirdemirev101/Fitness-Store-Website.git
cd "Fitness Store Website"
Настройки на connection string:

Редактирай appsettings.json или използвай User Secrets за DefaultConnection и Stripe ключове.
Създай и приложи миграции (EF Core):

Увери се, че имаш инсталиран dotnet-ef: dotnet tool install --global dotnet-ef
Създай миграция: dotnet ef migrations add Init
Приложи миграции: dotnet ef database update
Алтернативно може да използваш скрипта: .\scripts\apply-migrations.ps1 (PowerShell)
Стартирай приложението:

dotnet run
Отвори браузър: https://localhost:5001 (или порта, на който е стартирано)
Админ панел

При първо стартиране (ако е включен seed) се създава админ потребител по данните в appsettings.json -> AdminSeed (Enable/Email/Password).
Уеб линкове:
Admin Products: /AdminProduct/Index
Admin Orders: /AdminOrder/Index
Админ панел е custom UI, sidebar, upload на изображения за продукти (файловете се запазват в wwwroot/images).
Изображения / Upload

Качените изображения се записват в wwwroot/images и Product.URL се записва като /images/<file>.
Ако няма изображение, се използва wwwroot/images/placeholder.svg.
Полезни бележки

Премини към production внимателно: не оставяй AdminSeed:Enable включен в production.
Сложи Stripe ключовете в User Secrets или environment variables.
За по-добра сигурност и мащабируемост използвай blob storage за изображения.
