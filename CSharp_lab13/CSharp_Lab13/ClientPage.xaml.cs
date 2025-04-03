using CSharp_Lab13.Data;
using CSharp_Lab13.Models;
using System.Data.Entity;

namespace CSharp_Lab13;

public partial class ClientPage : ContentPage
{
	private readonly ClientService _clientService;
	private int _editClientId;
    public ClientPage(ClientService clientService)
	{
		InitializeComponent();
        _clientService = clientService;
        LoadData();
    }
	private async Task LoadData()
	{
		try
		{
			var clients = await _clientService.GetClientsAsync();
			await Dispatcher.DispatchAsync(() => listView.ItemsSource = clients);
        }
		catch (Exception ex)
        {
            await DisplayAlert("Error loading data", ex.Message, "OK");
        }
    }
	private async void saveButton_Clicked(object sender, EventArgs e)
	{
        
        try
		{
			if (_editClientId == 0)
			{
				var newClient = new Client
				{
					FirstName = firstNameEntryField.Text,
					LastName = lastNameEntryField.Text,
					Description = descriptionEntryField.Text
				};
				await _clientService.AddClientAsync(newClient);
            }
			else
			{
				var updatedClient = new Client
                {
                    ClientId = _editClientId,
                    FirstName = firstNameEntryField.Text,
                    LastName = lastNameEntryField.Text,
                    Description = descriptionEntryField.Text
                };
				await _clientService.UpdateClientAsync(updatedClient);
				_editClientId = 0;
            }
            firstNameEntryField.Text = string.Empty;
            lastNameEntryField.Text = string.Empty;
            descriptionEntryField.Text = string.Empty;
			await LoadData();
        }
		catch (Exception ex)
        {
            await DisplayAlert("Error saving data", ex.Message, "OK");
        }
    }
	private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var client = e.Item as Client;
        var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
		switch (action)
		{
			case "Edit":
                _editClientId = client.ClientId;
                firstNameEntryField.Text = client.FirstName;
                lastNameEntryField.Text = client.LastName;
                descriptionEntryField.Text = client.Description;
                break;
            case "Delete":
                try
                {
                    await _clientService.DeleteClientAsync(client.ClientId);
                    await LoadData();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error deleting data", ex.Message, "OK");
                }
                break;
        }
    }
}	