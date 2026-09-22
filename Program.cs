using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Services;
using GesHomeLibrary.Views;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// Services
services.AddSingleton<IBookService, BookService>();
services.AddSingleton<IFilterBookService, FilterBookService>();
services.AddSingleton<ISortBookService, SortBookService>();
services.AddSingleton<IStatisticsService, StatisticsService>();

// Validators
services.AddTransient<BookValidator>();
services.AddTransient<UserInputValidator>();

// Main views
services.AddTransient<MainView>();
services.AddTransient<BookCrudView>();
services.AddTransient<FilterView>();
services.AddTransient<SortView>();
services.AddTransient<StatisticsView>();

// Helping views
services.AddTransient<AddingBookView>();
services.AddTransient<UpdatingBookView>();
services.AddTransient<DeletingBookView>();
services.AddTransient<UpdatingGenreView>();

var serviceProvider = services.BuildServiceProvider(validateScopes: true);

Console.Clear();
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