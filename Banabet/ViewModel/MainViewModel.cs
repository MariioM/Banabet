using CommunityToolkit.Mvvm.ComponentModel;
using MySqlConnector;
namespace Banabet.ViewModel;

public partial class MainViewModel : ObservableObject
{

    DatabaseManager dbManager = new DatabaseManager();
    MySqlConnection dataBaseCon;

    public List<ImageSource> IconosLogros { get; set; }


    public MainViewModel()
    {
        //Carrusel logros
        IconosLogros = new List<ImageSource>
        {
            ImageSource.FromFile("carrousel_house.svg"),
            ImageSource.FromFile("carrousel_city.svg"),
            ImageSource.FromFile("carrousel_country.svg"),
            ImageSource.FromFile("carrousel_country_disabled.svg"),
            ImageSource.FromFile("carrousel_country_disabled.svg"),
            ImageSource.FromFile("carrousel_country_disabled.svg"),
        };
       dataBaseCon = new MySqlConnection(dbManager.builder.ConnectionString);
    }

    [ObservableProperty]
    float precioKgBanana = 1;
    //Del forms
    [ObservableProperty]
    float apuestaMensual = 500;
    [ObservableProperty]
    float contador;
    [ObservableProperty]
    //Del forms
    DateTime fecha_ini = new DateTime(2024, 03, 14, 15, 15, 15);
    [ObservableProperty]
    TimeSpan diferencia = new TimeSpan();


    public void ReinicioPublico()
    {
        Fecha_ini = DateTime.Now;

        Contador = 0;
    }

    public async Task EmpezarPublico()
    {
        float ahorro = ApuestaMensual / 43200;
        while (true)
        {
            Contador += ahorro;
            DateTime ahora = DateTime.Now;
            Diferencia = ahora - Fecha_ini;
            await Task.Delay(1000);
        }
    }
    //Solo 3 decimales en los kilos de platanos
}