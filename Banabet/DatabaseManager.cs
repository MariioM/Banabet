using System;
using System.Diagnostics;
using Banabet.ViewModel;
using BCrypt.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using MySqlConnector;

namespace Banabet
{
    public static class DatabaseManager
    {
        public static object CurrentSession {  get; set; }
        public static MySqlConnection Connection { get; set; }


        public static void GetPreference()
        {
            CurrentSession = Preferences.Get("CurrentSession", null);
        }

        public static void ClearCurrentSession()
        {
            Preferences.Remove("CurrentSession");
            CurrentSession = 0;
        }

        

        /// <summary>
        /// Información de la base de datos para conectarnos (server, base de datos, userID y contraseña).
        /// </summary>
        public static MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
        {
            Server = "banabet.mysql.database.azure.com",
            Database = "app_maui",
            UserID = "banabet",
            Password = "1234asdfASDF",
        };

        /// <summary>
        /// Busca en la base de datos el correo electrónico introducido, recuperando la
        /// contraseña hasheada si existe el usuario y verifica si la contraseña ingresada
        /// coincide con la hasheada almacenada.
        /// </summary>
        /// <param name="emailInput">Correo electrónico ingresado por el usuario para hacer login.</param>
        /// <param name="passwordInput">Contraseña ingresada por el usuario para hacer login.</param>
        /// <returns>
        /// Retorna true si el usuario existe y la contraseña es correcta; de lo contrario, retorna false.
        /// Retorna false también si el usuario no existe en la base de datos, si la contraseña no coincide,
        /// o si ocurre alguna excepción durante la operación de base de datos.
        /// </returns>
        public static bool ComprobarUsuarioExiste(string emailInput, string passwordInput)
        {
            string query = "SELECT contraseña FROM usuarios WHERE correo = @correo";
            

            using (Connection)
            {
                // Comando para ejecutar la query de INSERT
                MySqlCommand command = new MySqlCommand(query, Connection)
                {
                    CommandTimeout = 1  // Tiempo máximo de espera para la ejecución
                };
                

                try
                {
                    // Abrimos la conexión con la base de datos
                    Connection.Open();

                    // Añadimos los parametros introducidos por el usuario al commando
                    command.Parameters.AddWithValue("@correo", emailInput);
                    

                    // Ejecuta la consulta y retorna el primer resultado.
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        string passwordBBDD = result.ToString();

                        // Compara la contraseña proporcionada con la almacenada
                        if (BCrypt.Net.BCrypt.Verify(passwordInput, passwordBBDD))
                        {
                            Console.WriteLine("Autenticación exitosa.");
                            StoreSession(emailInput, Connection);
                            
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Contraseña incorrecta.");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usuario no encontrado.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al intentar abrir la conexión o ejecutar el comando: " + ex.Message);
                    return false;
                }
                finally
                {
                    // Cerramos la conexión con la base de datos
                    if (Connection.State == System.Data.ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                }
            }
        }

        public static void StoreSession(string email, MySqlConnection Connection)
        {
            string sessionQuery = "SELECT id FROM usuarios WHERE correo = @correo";
            // Comando para guardar la sesión actual
            MySqlCommand sessionCommand = new MySqlCommand(sessionQuery, Connection);
            sessionCommand.Parameters.AddWithValue("@correo", email);
            CurrentSession = sessionCommand.ExecuteScalar();
            Preferences.Set("CurrentSession", Convert.ToString(CurrentSession));
        }

        private static string GenerateUserName()
        {
            string userName;
            string animals = "abyssinian\nalbatross\nalligator\nangelfish\nant\nanteater\nantelope\narmadillo\nbaboon\nbadger\nbandicoot\nbat\nbeagle\nbear\nbeaver\nbee\nbeetle\nbird\nbison\nboar\nbobcat\nbombay\nbongo\nbonobo\nbooby\nbuffalo\nbulldog\nbullfrog\nbutterfly\ncamel\ncapybara\ncat\ncaterpillar\ncatfish\ncentipede\nchameleon\ncheetah\nchicken\nchihuahua\nchimpanzee\nchinchilla\nchipmunk\ncivet\ncockroach\ncougar\ncow\ncoyote\ncrab\ncrane\ncuttlefish\ndeer\ndingo\ndodo\ndog\ndolphin\ndonkey\ndragon\ndragonfly\ndrever\nduck\neagle\nearwig\neel\nelephant\nemu\nfalcon\nferret\nfish\nflamingo\nflounder\nfox\nfrog\ngecko\ngerbil\ngibbon\ngiraffe\ngoat\ngoose\ngopher\ngorilla\ngrasshopper\ngrey seal\ngreyhound\ngrizzly bear\ngrouse\nguinea fowl\nguppy\nhamster\nharrier\nhedgehog\nheron\nhippopotamus\nhorse\nhound\nhowler monkey\nhummingbird\nhyena\ni\nibis\niguana\njackal\njaguar\njellyfish\nkangaroo\nking crab\nkingfisher\nkiwi\nkoala\nlemming\nlemur\nleopard\nlion\nlionfish\nlizard\nllama\nlobster\nlynx\nm\nmacaque\nmacaw\nmammoth\nmanatee\nmandrill\nmarkhor\nmarmoset\nmeerkat\nmillipede\nmole\nmongoose\nmongrel\nmonkey\nmoose\nmoth\nmouse\nmule\nnewt\nnightingale\nocelot\noctopus\nopossum\norangutan\noriole\nostrich\notter\nowl\noyster\npanther\nparrot\npeacock\npelican\npenguin\npheasant\npig\npike\npiranha\nplatypus\nporcupine\npossum\nprawn\npuffin\npuma\nquail\nrabbit\nraccoon\nrat\nrattlesnake\nreindeer\nrhinoceros\nrobin\nsalamander\nscorpion\nseahorse\nseal\nshark\nsheep\nshrimp\nskunk\nsloth\nsnail\nsnake\nsparrow\nsponge\nsquid\nsquirrel\nstingray\nstoat\nswan\ntamarin\ntapir\ntarantula\ntermite\ntiger\ntoad\ntortoise\ntoucan\nturkey\nturtle\nvole\nvulture\nwallaby\nwalrus\nwarthog\nwasp\nweasel\nwhale\nwildebeest\nwolf\nwolfhound\nwolverine\nwombat\nwoodpecker\nyak\nzebra";
            string colours = "amber\nash\nasphalt\nauburn\navocado\naquamarine\nazure\nbeige\nbisque\nblack\nblue\nbone\nbordeaux\nbrass\nbronze\nbrown\nburgundy\ncamel\ncaramel\ncanary\nceleste\ncerulean\nchampagne\ncharcoal\nchartreuse\nchestnut\nchocolate\ncitron\nclaret\ncoal\ncobalt\ncoffee\ncoral\ncorn\ncream\ncrimson\ncyan\ndenim\ndesert\nebony\necru\nemerald\nfeldspar\nfuchsia\ngold\ngray\ngreen\nheather\nindigo\nivory\njet\nkhaki\nlime\nmagenta\nmaroon\nmint\nnavy\nolive\norange\npink\nplum\npurple\nred\nrust\nsalmon\nsienna\nsilver\nsnow\nsteel\ntan\nteal\ntomato\nviolet\nwhite\nyellow";
            string[] animalList = animals.Split('\n');
            string[] colourList = colours.Split('\n');
            Random rnd = new Random();
            int animalnum = rnd.Next(0, 211);
            int colornum = rnd.Next(0, 71);
            userName = animalList[animalnum] + colourList[colornum];
            return userName;
        }

        /// <summary>
        /// Inserta un nuevo registro con los datos del usuario en la tabla 'usuarios'
        /// de la base de datos. La contraseña proporcionada se hashea antes de ser almacenada.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <param name="emergencyContactName">Apodo del contacto de emergencia del usuario.</param>
        /// <param name="emergencyContactNumber">Número del contacto de emergencia del usuario.</param>
        /// <param name="estimatedMoney">Estimación del dinero mensual que el usuario apuesta.</param>
        /// <returns>Devuelve un que indica si la query ha sido ejecutada con exito (true) o si falló (false)</returns>
        public static bool EnviarDatosRegistroBase(string email, string password, string emergencyContactName, string emergencyContactNumber, decimal estimatedMoney)
        {
            // Query que inserta la info del usuario en un nuevo registro
            string query = @"
            INSERT INTO usuarios (correo, contraseña, nombre_contacto_emergencia, telefono_emergencia, estimacion_dinero_mensual, fecha_inicio, user_name)
            VALUES (@correo, @contraseña, @nombreContactoEmergencia, @telefonoEmergencia, @estimacionDineroMensual, @fechaInicio, @username);
            ";

            // Hasheamos la contraseña antes de almacenarla
            string contrasenaHaseada = BCrypt.Net.BCrypt.HashPassword(password);

            // Fecha actual
            DateTime fechaactual = DateTime.Now;

            // Conexión con la base de datos
            using (var Connection = new MySqlConnection(builder.ConnectionString))
            {
                // Comando para ejecutar la query de INSERT
                MySqlCommand command = new MySqlCommand(query, Connection)
                {
                    CommandTimeout = 1  // Tiempo máximo de espera para la ejecución
                };

                try
                {
                    // Abrimos la conexión con la base de datos
                    Connection.Open();
                    string nombreUsu = GenerateUserName();
                    // Añadimos los parametros introducidos por el usuario al commando
                    command.Parameters.AddWithValue("@correo", email);
                    command.Parameters.AddWithValue("@contraseña", contrasenaHaseada);
                    command.Parameters.AddWithValue("@nombreContactoEmergencia ", emergencyContactName);
                    command.Parameters.AddWithValue("@telefonoEmergencia", emergencyContactNumber);
                    command.Parameters.AddWithValue("@estimacionDineroMensual", estimatedMoney);
                    command.Parameters.AddWithValue("@fechaInicio", fechaactual);
                    command.Parameters.AddWithValue("@username", nombreUsu);

                    // Ejecutamos la query
                    int result = command.ExecuteNonQuery();

                    if (result > 0) // Query ejecutada con exito
                    {
                        StoreSession(email, Connection);
                        
                        Console.WriteLine("Datos insertados correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("No se insertaron datos.");
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al intentar abrir la conexión o ejecutar el comando: " + ex.Message);
                    return false;
                }
                finally
                {
                    // Cerramos la conexión con la base de datos
                    if (Connection.State == System.Data.ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                }
            }
        }
    }
}
