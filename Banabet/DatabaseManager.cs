using System;
using BCrypt.Net;
using MySqlConnector;

namespace Banabet
{
    internal class DatabaseManager
    {

        /// <summary>
        /// Información de la base de datos para conectarnos (server, base de datos, userID y contraseña).
        /// </summary>
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
        {
            Server = "banabet.mysql.database.azure.com",
            Database = "app_maui",
            UserID = "banabet",
            Password = "1234asdfASDF",
        };

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
                    CommandTimeout = 10  // Tiempo máximo de espera para la ejecución
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
