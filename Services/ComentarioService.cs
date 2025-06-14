// using MySql.Data.MySqlClient;
// using Microsoft.Extensions.Configuration;
// using Social.Models;

// public class ComentarioService
// {
//     private readonly string _connectionString;

//     public ComentarioService()
//     {
//         var config = new ConfigurationBuilder()
//        .AddJsonFile("appsettings.json")
//        .Build();

//         _connectionString = config.GetConnectionString("MySql");
//     }

//     public List<Comentario> GetAll()
//     {
//         var comentarios = new List<Comentario>();
//         using var conn = new MySqlConnection(_connectionString);
//         conn.Open();
//         var cmd = new MySqlCommand("SELECT * FROM comentarios", conn);
//         using var reader = cmd.ExecuteReader();
//         while (reader.Read())
//         {
//             comentarios.Add(new Comentario
//             {
//                 id_comentario = reader.GetInt32("id_comentario"),
//                 id_post = reader.GetInt32("id_post"),
//                 id_user = reader.GetInt32("id_user"),
//                 contenido = reader.GetString("contenido")
//             });
//         }
//         return comentarios;
//     }
//     public void AgregarComentario(Comentario comentario)
//     {
//         using var conn = new MySqlConnection(_connectionString);
//         conn.Open();

//         var query = "INSERT INTO comentarios (id_post, id_user, contenido) VALUES (@id_post, @id_user, @contenido)";
//         using var cmd = new MySqlCommand(query, conn);
//         cmd.Parameters.AddWithValue("@id_post", comentario.id_post);
//         cmd.Parameters.AddWithValue("@id_user", comentario.id_user);
//         cmd.Parameters.AddWithValue("@contenido", comentario.contenido);

//         cmd.ExecuteNonQuery();
//     }

//     public void EliminarComentario(int id_comentario)
//     {
//         using var conn = new MySqlConnection(_connectionString);
//         conn.Open();

//         var query = "DELETE FROM comentarios WHERE id_comentario = @id";
//         using var cmd = new MySqlCommand(query, conn);
//         cmd.Parameters.AddWithValue("@id", id_comentario);

//         cmd.ExecuteNonQuery();
//     }
// }
