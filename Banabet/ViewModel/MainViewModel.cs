using CommunityToolkit.Mvvm.ComponentModel;
using MySqlConnector;
namespace Banabet.ViewModel;

public partial class MainViewModel : ObservableObject
{

    DatabaseManager dbManager = new DatabaseManager();
    MySqlConnection dataBaseCon;

    public List<ImageSource> IconosLogros { get; set; }

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

    [ObservableProperty]
    float precioKgBanana = 1;
    //Del forms
    [ObservableProperty]
    float apuestaMensual = Convert.ToSingle(moneyEstFetched);
    [ObservableProperty]
    float contador;
    [ObservableProperty]
    //Del forms
    DateTime fecha_ini = Convert.ToDateTime(initDateFetched);
    [ObservableProperty]
    TimeSpan diferencia = new TimeSpan();


    public void ReinicioPublico()
    {
        DateTime newDate = DateTime.Now;
        try
        {
            dataBaseCon.Open();
            MySqlCommand resetDateCommand = new MySqlCommand("UPDATE usuarios SET fecha_inicio " + newDate + " WHERE id = @currentsession");
            resetDateCommand.Parameters.AddWithValue("@currentsession", dbManager.CurrentSession);
            resetDateCommand.ExecuteNonQuery();
        } catch (Exception ex)
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