using CommunityToolkit.Mvvm.ComponentModel;
using MySqlConnector;
using System.Timers;
namespace Banabet.ViewModel;

public partial class MainViewModel : ObservableObject
{


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

    private static string timeQuery = "SELECT fecha_inicio FROM usuarios WHERE id = @currentsession";
    private static string moneyQuery = "SELECT estimacion_dinero_mensual FROM usuarios WHERE id = @currentsession";

    static object initDateFetched;
    static object moneyEstFetched;

    static float dineroTemp;
    static DateTime fechaTemp;

    public bool userAlreadyLogged = false;


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

       
    }

    public void FetchData()
    {

        DatabaseManager.Connection = new MySqlConnection(DatabaseManager.builder.ConnectionString);
        try
        {
            DatabaseManager.Connection.Open();
            MySqlCommand timeCommand = new MySqlCommand(timeQuery, DatabaseManager.Connection);
            MySqlCommand moneyCommand = new MySqlCommand(moneyQuery, DatabaseManager.Connection);
            timeCommand.Parameters.AddWithValue("@currentsession", DatabaseManager.CurrentSession);
            moneyCommand.Parameters.AddWithValue("@currentsession", DatabaseManager.CurrentSession);
            initDateFetched = timeCommand.ExecuteScalar();
            moneyEstFetched = moneyCommand.ExecuteScalar();
            dineroTemp = Convert.ToSingle(moneyEstFetched);
            fechaTemp = Convert.ToDateTime(initDateFetched);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al intentar abrir la conexión o ejecutar el comando: " + ex.Message);
        }
        finally
        {
            // Cerramos la conexión con la base de datos
            if (DatabaseManager.Connection.State == System.Data.ConnectionState.Open)
            {
                DatabaseManager.Connection.Close();
            }
        }
    }

    public void updateData() 
    {
        Fecha_ini = fechaTemp;
        ApuestaMensual = dineroTemp;
    }

    public void ReinicioPublico()
    {
        DateTime newDate = DateTime.Now;
        try
        {
            using (MySqlConnection dataBaseCon = new MySqlConnection(DatabaseManager.builder.ConnectionString))
            {
                DatabaseManager.Connection.Open();
                string formattedDate = newDate.ToString("yyyy-MM-dd HH:mm:ss"); // Formatear la fecha correctamente
                MySqlCommand resetDateCommand = new MySqlCommand("UPDATE usuarios SET fecha_inicio = @newDate WHERE id = @currentsession", dataBaseCon);
                resetDateCommand.Parameters.AddWithValue("@newDate", formattedDate);
                resetDateCommand.Parameters.AddWithValue("@currentsession", DatabaseManager.CurrentSession);
                resetDateCommand.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al intentar abrir la conexión o ejecutar el comando: " + ex.Message);
        }finally
        {
            DatabaseManager.Connection.Close();
        }
    }


    public async Task EmpezarPublico()
    {
        updateData();

        DateTime ahoratemp = DateTime.Now;
        Diferencia = ahoratemp - Fecha_ini;

        float ahorro = (ApuestaMensual / 43200);

        if (userAlreadyLogged)
        {
            Contador = (Convert.ToSingle(Math.Truncate((Contador + (ahorro * ((Diferencia.Days * 86400) + (Diferencia.Hours * 3600) + (Diferencia.Minutes * 60) + Diferencia.Seconds))) * 1000) / 1000));
        }

        while (true)
        {
            Contador = Convert.ToSingle(Math.Truncate((Contador + ahorro) * 1000) / 1000);
            DateTime ahora = DateTime.Now;
            Diferencia = ahora - Fecha_ini;
            await Task.Delay(1000);
        }
    }
}