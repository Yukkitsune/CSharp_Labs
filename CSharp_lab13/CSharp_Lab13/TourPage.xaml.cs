using CSharp_Lab13.Data;
using CSharp_Lab13.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace CSharp_Lab13
{
    public partial class TourPage : ContentPage
    {
        private readonly TourService _tourService;
        private int _editTourId;

        public TourPage(TourService tourService)
        {
            InitializeComponent();
            _tourService = tourService;
            LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                var tours = await _tourService.GetToursAsync();
                listView.ItemsSource = tours;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error loading data: {ex.Message}", "OK");
            }
        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var price = double.TryParse(priceEntryField.Text, out double parsedPrice) ? parsedPrice : 0;

                if (_editTourId == 0)
                {
                    var newTour = new Tour
                    {
                        Destination = destinationEntryField.Text,
                        Price = price,
                        Description = descriptionEntryField.Text
                    };
                    await _tourService.AddTourAsync(newTour);
                }
                else
                {
                    var updatedTour = new Tour
                    {
                        TourId = _editTourId,
                        Destination = destinationEntryField.Text,
                        Price = price,
                        Description = descriptionEntryField.Text
                    };
                    await _tourService.UpdateTourAsync(updatedTour);
                    _editTourId = 0; 
                }

                destinationEntryField.Text = string.Empty;
                priceEntryField.Text = string.Empty;
                descriptionEntryField.Text = string.Empty;

                await LoadData();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error saving data: {ex.Message}", "OK");
            }
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tour = (Tour)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editTourId = tour.TourId;
                    destinationEntryField.Text = tour.Destination;
                    priceEntryField.Text = tour.Price.ToString();
                    descriptionEntryField.Text = tour.Description;
                    break;
                case "Delete":
                    try
                    {
                        await _tourService.DeleteTourAsync(tour.TourId);
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
