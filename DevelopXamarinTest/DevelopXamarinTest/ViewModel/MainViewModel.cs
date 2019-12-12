using System.Windows.Input;
using DevelopXamarinTest.View;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace DevelopXamarinTest.ViewModel
{
    public class MainViewModel
    {

        #region Properties
        public EditProductViewModel EditProduct { get; set; }

        public ProductsViewModel Products { get; set; }

        public AddProductViewModel AddProduct { get; set; }
        #endregion


        #region Constructor
        public MainViewModel() {
            instance = this;
            this.Products = new ProductsViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
                return new MainViewModel();

            return instance;
        }
        #endregion

        #region Command
        public ICommand AddProductCommand
        {
            get
            {
                return new RelayCommand(GoToAddProduct);
            }

        }
        #endregion

        #region Methods
        private async void GoToAddProduct()
        {
            this.AddProduct = new AddProductViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
        }
        #endregion
    }
}
