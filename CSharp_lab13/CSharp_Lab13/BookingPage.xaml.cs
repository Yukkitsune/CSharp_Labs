using CSharp_Lab13.Data;
using CSharp_Lab13.Models;
using System.Diagnostics.Metrics;

namespace CSharp_Lab13
{
    public partial class BookingPage : ContentPage
    {
        private readonly BookingService _bookingService;
        private readonly ClientService _clientService;
        private readonly TourService _tourService;
        private int _editBookingId;
        private int _selectedClientId;
        private int _selectedTourId;
        private int counter = 0;

        public BookingPage(BookingService bookingService, ClientService clientService, TourService tourService)
        {
            InitializeComponent();
            _bookingService = bookingService;
            _clientService = clientService;
            _tourService = tourService;
            LoadData();
            LoadPickersData();
            Console.WriteLine("Переход к бронированию!!!!!!!!!!!!!!!!!!!");
        }

        private async Task LoadData()
        {
            try
            {
                var bookings = await _bookingService.GetBookingsAsync();
                listView.ItemsSource = bookings;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error loading data: {ex.Message}", "OK");
            }
        }
        private async Task LoadPickersData()
        {
            try
            {
                var clients = await _clientService.GetClientsAsync();
                clientPicker.ItemsSource = clients.Select(c => $"{c.ClientId}: {c.FirstName} {c.LastName}").ToList();

                var tours = await _tourService.GetToursAsync();
                tourPicker.ItemsSource = tours.Select(t => $"{t.TourId}: {t.Destination}").ToList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error loading pickers data: {ex.Message}", "OK");
            }
        }
        private void ClientPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clientPicker.SelectedIndex >= 0)
            {
                var selectedText = clientPicker.Items[clientPicker.SelectedIndex];
                _selectedClientId = int.Parse(selectedText.Split(':')[0]);
            }
        }
        private void TourPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tourPicker.SelectedIndex >= 0)
            {
                var selectedText = tourPicker.Items[tourPicker.SelectedIndex];
                _selectedTourId = int.Parse(selectedText.Split(':')[0]);
            }
        }
        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            counter++;
            CounterLabel.Text = counter.ToString();
            Console.WriteLine($"Счётчик: {counter}"); 
        }
        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Save", "Save button clicked", "OK");
            Console.WriteLine("Кнопка нажата!!!!!!!!!!!!!!!!!!!");
            try
            {
                Console.WriteLine("Блок трай!!!!!!!!!!!!!!!!!!!");
                var numberOfPeople = int.TryParse(numberOfPeopleEntryField.Text?.Trim(), out int parsedNumberOfPeople) ? parsedNumberOfPeople : 0;

                if (_selectedClientId == 0 || _selectedTourId == 0 || numberOfPeople == 0)
                {
                    await DisplayAlert("Ошибка", "Все поля должны быть заполнены корректно.", "OK");
                    return;
                }

                if (_editBookingId == 0)
                {
                    var newBooking = new Booking
                    {
                        ClientId = _selectedClientId,
                        TourId = _selectedTourId,
                        NumberOfPeople = numberOfPeople,
                        Description = descriptionEntryField.Text?.Trim()
                    };

                    try
                    {
                        await _bookingService.AddBookingAsync(newBooking);
                        await DisplayAlert("Успех", "Запись добавлена.", "OK");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Ошибка", $"Ошибка при добавлении: {ex.Message}", "OK");
                        Console.WriteLine($"Exception: {ex}");
                    }
                }
                else
                {
                    var updatedBooking = new Booking
                    {
                        BookingId = _editBookingId,
                        ClientId = _selectedClientId,
                        TourId = _selectedTourId,
                        NumberOfPeople = numberOfPeople,
                        Description = descriptionEntryField.Text?.Trim()
                    };

                    await _bookingService.UpdateBookingAsync(updatedBooking);
                    _editBookingId = 0;
                    await DisplayAlert("Успех", "Запись обновлена.", "OK");
                }

                clientPicker.SelectedIndex = -1;
                tourPicker.SelectedIndex = -1;
                numberOfPeopleEntryField.Text = string.Empty;
                descriptionEntryField.Text = string.Empty;

                await LoadData();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Ошибка при сохранении: {ex.Message}", "OK");
            }
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var booking = (Booking)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editBookingId = booking.BookingId;

                    clientPicker.SelectedIndex = clientPicker.Items.IndexOf($"{booking.ClientId}:");
                    tourPicker.SelectedIndex = tourPicker.Items.IndexOf($"{booking.TourId}:");

                    numberOfPeopleEntryField.Text = booking.NumberOfPeople.ToString();
                    descriptionEntryField.Text = booking.Description;
                    break;
                case "Delete":
                    try
                    {
                        await _bookingService.DeleteBookingAsync(booking.BookingId);
                        await LoadData();
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", $"Error deleting data: {ex.Message}", "OK");
                    }
                    break;
            }
        }
    }
}