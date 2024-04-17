using Banabet.ViewModel;
using Mopups.Services;

namespace BanaBet
{
    public partial class MainPage : ContentPage
    {
        MainViewModel VM;
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            VM = vm;
        }
        
        private void Panico(object sender, EventArgs e)
        {
            PanicViewModel pmv = new PanicViewModel();
            MopupService.Instance.PushAsync(new PanicPage(pmv, VM));
        }
    }

}