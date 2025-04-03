using CSharp_Lab13.Data;

namespace CSharp_Lab13;

public partial class MainPage : ContentPage
{
    private readonly LocalDbService _dbService;
    private readonly ClientService _clientService;
    private readonly TourService _tourService;
    private readonly BookingService _bookingService;
    public MainPage()
    {
        InitializeComponent();
        _dbService = new LocalDbService();
        _clientService = new ClientService(_dbService);
        _tourService = new TourService(_dbService);
        _bookingService = new BookingService(_dbService);
        InitializeDatabaseAsync();
    }
    private async void InitializeDatabaseAsync()
    {
        await _dbService.InitializeDatabaseAsync();
    }
    private async void OnClientClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ClientPage(_clientService));
    }

    private async void OnTourClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new TourPage(_tourService));
    }

    private async void OnBookingClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new BookingPage(_bookingService, _clientService, _tourService));
    }
    private async void OnShowTablesClicked(object sender, EventArgs e)
    {
        try
        {
            var tableNames = await _dbService.GetTableNamesAsync();
            if (tableNames.Any())
            {
                var tables = string.Join("\n", tableNames);
                await DisplayAlert("Список таблиц", tables, "OK");
            }
            else
            {
                await DisplayAlert("Список таблиц", "Таблицы отсутствуют в базе данных.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Ошибка получения таблиц: {ex.Message}", "OK");
        }
    }
}