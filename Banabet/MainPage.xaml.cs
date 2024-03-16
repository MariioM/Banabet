using Banabet.ViewModel;

namespace Banabet
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
        }

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}