using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Services;
using GesHomeLibrary.Views;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IBookService, BookService>();
services.AddSingleton<IFilterBookService, FilterBookService>();
services.AddSingleton<ISortBookService, SortBookService>();
services.AddSingleton<IStatisticsService, StatisticsService>();

services.AddTransient<StatusParseService>();
services.AddTransient<GenreParseService>();
services.AddTransient<BookValidator>();

services.AddTransient<MainView>();
services.AddTransient<BookCRUDView>();

var serviceProvider = services.BuildServiceProvider(validateScopes: true);

Console.WriteLine("Добро пожаловать в Вашу личную библиотеку!");

try 
{
    var app = serviceProvider.GetRequiredService<MainView>();
    app.StartMainView();
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка запуска приложения: {ex.Message}");
}