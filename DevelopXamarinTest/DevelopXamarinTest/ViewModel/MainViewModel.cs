using System.Windows.Input;
using DevelopXamarinTest.View;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace DevelopXamarinTest.ViewModel
{
    public class MainViewModel
    {
        public ProductsViewModel Products { get; set; }

        public MainViewModel() {
            this.Products = new ProductsViewModel();
        }

        public ICommand AddProductCommand
        {
            get
            {
                return new RelayCommand(GoToAddProduct);
            }

        }

        public AddProductViewModel AddProduct { get; set; }

        private async void GoToAddProduct()
        {
            this.AddProduct = new AddProductViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
        }
    }
}
