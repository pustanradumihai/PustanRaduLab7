using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls.Maps;
using PustanRaduLab7.Models;

namespace PustanRaduLab7;

public partial class ShopEntryPage : ContentPage
{
    public ShopEntryPage()
    {
        InitializeComponent();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }

    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        if (shop?.Adress == null) return;

        var locations = await Geocoding.GetLocationsAsync(shop.Adress);
        var location = locations?.FirstOrDefault();

        if (location != null)
        {
            var locationUri = $"geo:{location.Latitude},{location.Longitude}?q={location.Latitude},{location.Longitude}";
            await Launcher.OpenAsync(locationUri);
        }
        else
        {
            await DisplayAlert("Error", "Location not found", "OK");
        }
    }
}
