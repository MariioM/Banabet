using System;
using BCrypt.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using MySqlConnector;

namespace Banabet
{
    public class DatabaseManager
    {
        object CurrentSession {  get; set; }

        public DatabaseManager() {
            CurrentSession = 0;
        }

        /// <summary>
        /// Información de la base de datos para conectarnos (server, base de datos, userID y contraseña).
        /// </summary>
        public MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
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
        public bool ComprobarUsuarioExiste(string emailInput, string passwordInput)
        {
            string query = "SELECT contraseña FROM usuarios WHERE correo = @correo";
            string sessionQuery = "SELECT id FROM usuarios WHERE correo = @correo";

            using (var databasecon = new MySqlConnection(builder.ConnectionString))
            {
                // Comando para ejecutar la query de INSERT
                MySqlCommand command = new MySqlCommand(query, databasecon)
                {
                    CommandTimeout = 1  // Tiempo máximo de espera para la ejecución
                };
                // Comando para guardar la sesión actual
                MySqlCommand sessionCommand = new MySqlCommand(sessionQuery, databasecon);

                try
                {
                    // Abrimos la conexión con la base de datos
                    databasecon.Open();

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
                            sessionCommand.Parameters.AddWithValue("@correo", emailInput);
                            CurrentSession = sessionCommand.ExecuteScalar();
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
                    if (databasecon.State == System.Data.ConnectionState.Open)
                    {
                        databasecon.Close();
                    }
                }
            }
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
        public bool EnviarDatosRegistroBase(string email, string password, string emergencyContactName, string emergencyContactNumber, decimal estimatedMoney)
        {
            // Query que inserta la info del usuario en un nuevo registro
            string query = @"
            INSERT INTO usuarios (correo, contraseña, nombre_contacto_emergencia, telefono_emergencia, estimacion_dinero_mensual, fecha_inicio)
            VALUES (@correo, @contraseña, @nombreContactoEmergencia, @telefonoEmergencia, @estimacionDineroMensual, @fechaInicio);
            ";

            // Hasheamos la contraseña antes de almacenarla
            string contrasenaHaseada = BCrypt.Net.BCrypt.HashPassword(password);

            // Fecha actual
            DateTime fechaactual = DateTime.Now;

            // Conexión con la base de datos
            using (var databasecon = new MySqlConnection(builder.ConnectionString))
            {
                // Comando para ejecutar la query de INSERT
                MySqlCommand command = new MySqlCommand(query, databasecon)
                {
                    CommandTimeout = 1  // Tiempo máximo de espera para la ejecución
                };

                try
                {
                    // Abrimos la conexión con la base de datos
                    databasecon.Open();

                    // Añadimos los parametros introducidos por el usuario al commando
                    command.Parameters.AddWithValue("@correo", email);
                    command.Parameters.AddWithValue("@contraseña", contrasenaHaseada);
                    command.Parameters.AddWithValue("@nombreContactoEmergencia ", emergencyContactName);
                    command.Parameters.AddWithValue("@telefonoEmergencia", emergencyContactNumber);
                    command.Parameters.AddWithValue("@estimacionDineroMensual", estimatedMoney);
                    command.Parameters.AddWithValue("@fechaInicio", fechaactual);

                    // Ejecutamos la query
                    int result = command.ExecuteNonQuery();

                    if (result > 0) // Query ejecutada con exito
                    {
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
                    if (databasecon.State == System.Data.ConnectionState.Open)
                    {
                        databasecon.Close();
                    }
                }
            }
        }
    }
}
