using Banabet.Services;
using Banabet.ViewModel;

namespace BanaBet
{
    public partial class App : Application
    {
        private MainViewModel _mainViewModel;
        public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
        _mainViewModel = new MainViewModel();
    }

    protected override void OnStart()
    {
        // Llamar a EmpezarPublico para continuar actualizando los contadores
        _mainViewModel.EmpezarPublico();
    }
    }
}
