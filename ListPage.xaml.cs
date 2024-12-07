using PustanRaduLab7.Models;

namespace PustanRaduLab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
        {
            BindingContext = new Product()
        });
    }

    // Funcția pentru ștergerea unui produs selectat
    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        // Obținem produsul selectat din listView
        var selectedProduct = listView.SelectedItem as Product;

        // Verificăm dacă un produs a fost selectat
        if (selectedProduct != null)
        {
            // Ștergem produsul din baza de date
            await App.Database.DeleteProductAsync(selectedProduct);

            // Actualizăm lista de produse
            var shopList = (ShopList)BindingContext;
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);
        }
        else
        {
            // Afișăm un mesaj de alertă dacă nu este niciun produs selectat
            await DisplayAlert("No Selection", "Please select a product to delete.", "OK");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;
        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
}