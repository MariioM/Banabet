using CommunityToolkit.Mvvm.ComponentModel;
using MySqlConnector;
using System.Timers;
using Timer = System.Timers.Timer;
namespace Banabet.ViewModel;

public partial class MainViewModel : ObservableObject
{

    DatabaseManager dbManager = new DatabaseManager();
    MySqlConnection dataBaseCon;

    public List<ImageSource> IconosLogros { get; set; }

    [ObservableProperty]
    float precioKgBanana = 1;
    //Del forms
    [ObservableProperty]
    float apuestaMensual;
    [ObservableProperty]
    float contador;
    [ObservableProperty]
    //Del forms
    DateTime fecha_ini;
    [ObservableProperty]
    TimeSpan diferencia;

    private string timeQuery = "SELECT fecha_inicio FROM usuarios WHERE id = @currentsession";
    private string moneyQuery = "SELECT estimacion_dinero_mensual FROM usuarios WHERE id = @currentsession";

    static object initDateFetched;
    static object moneyEstFetched;


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
        try { 
            
            dataBaseCon.Open();

            MySqlCommand timeCommand = new MySqlCommand(timeQuery, dataBaseCon);
            MySqlCommand moneyCommand = new MySqlCommand(moneyQuery, dataBaseCon);
            timeCommand.Parameters.AddWithValue("@currentsession", dbManager.CurrentSession);
            moneyCommand.Parameters.AddWithValue("@currentsession", dbManager.CurrentSession);
            initDateFetched = timeCommand.ExecuteScalar();
            moneyEstFetched = moneyCommand.ExecuteScalar();
            ApuestaMensual = Convert.ToSingle(moneyEstFetched);
            Fecha_ini = Convert.ToDateTime(initDateFetched);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al intentar abrir la conexión o ejecutar el comando: " + ex.Message);
        }
        finally
        {
            // Cerramos la conexión con la base de datos
            if (dataBaseCon.State == System.Data.ConnectionState.Open)
            {
                dataBaseCon.Close();
            }
        }


    }

 

    public void ReinicioPublico()
    {
        DateTime newDate = DateTime.Now;
        try
        {
            using (MySqlConnection dataBaseCon = new MySqlConnection(dbManager.builder.ConnectionString))
            {
                dataBaseCon.Open();
                string formattedDate = newDate.ToString("yyyy-MM-dd HH:mm:ss"); // Formatear la fecha correctamente
                MySqlCommand resetDateCommand = new MySqlCommand("UPDATE usuarios SET fecha_inicio = @newDate WHERE id = @currentsession", dataBaseCon);
                resetDateCommand.Parameters.AddWithValue("@newDate", formattedDate);
                resetDateCommand.Parameters.AddWithValue("@currentsession", dbManager.CurrentSession);
                resetDateCommand.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al intentar abrir la conexión o ejecutar el comando: " + ex.Message);
        }finally
        {
            dataBaseCon.Close();
        }
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