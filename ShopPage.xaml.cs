using PustanRaduLab7.Models;

namespace PustanRaduLab7;

public partial class ShopPage : ContentPage
{
    public ShopPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetShopsAsync();
    }

    async void OnShopAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShopEntryPage
        {
            BindingContext = new Shop()
        });
    }

    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new ShopEntryPage
            {
                BindingContext = e.SelectedItem as Shop
            });
        }
    }

}
