using Banabet;
using Banabet.Services;
using Banabet.ViewModel;
using MySqlConnector;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanaBet
{
    public partial class App : Application
    {
        static MainViewModel _mainViewModel;
        public App()
        {
                InitializeComponent();
                MainPage = new AppShell();
                _mainViewModel = new MainViewModel();
        }

        protected override void OnStart()
        {
                // Llamar a EmpezarPublico para continuar actualizando los contadores
                DatabaseManager.mvm = _mainViewModel;
                DatabaseManager.Connection = new MySqlConnection(DatabaseManager.builder.ConnectionString);
        }
    }
}
