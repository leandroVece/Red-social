// using MySql.Data.MySqlClient;
// using Social.Models;

using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Social.Models;
using Microsoft.Extensions.Options;
using Social.Dto;

public class UserService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UserService(IOptions<MongoDbSettings> mongoSettings, IMongoClient client)
    {
        var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        _usersCollection = database.GetCollection<User>("Usuarios"); // Nombre de la colección
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _usersCollection.InsertOneAsync(user);
        return user;
    }
    public async Task<User> GetUserByIdAsync(string id)
    {
        return await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
    }
    public async Task<User> GetUserLogin(UserLoginDto data)
    {
        var res = await _usersCollection.Find(u => u.Nombre == data.Nombre && u.Password == data.Password)
        .FirstOrDefaultAsync();
        if (res != null)
        {
            res.Activo = true;
            await _usersCollection.ReplaceOneAsync(u => u.Id == res.Id, res);
        }

        return res;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _usersCollection.Find(_ => true).ToListAsync();
    }
    public async Task UpdateUserAsync(string id, User user)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, id);
        await _usersCollection.ReplaceOneAsync(filter, user);
    }
    public async Task DeleteUserAsync(string id)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, id);
        await _usersCollection.DeleteOneAsync(filter);
    }
}
// public class UserService
// {
//     private readonly string _connectionString;

//     public UserService()
//     {
//         var config = new ConfigurationBuilder()
//             .AddJsonFile("appsettings.json")
//             .Build();

//         _connectionString = config.GetConnectionString("MySql");
//     }

//     public List<User> GetAll()
//     {
//         var users = new List<User>();
//         using var conn = new MySqlConnection(_connectionString);
//         conn.Open();
//         var cmd = new MySqlCommand("SELECT * FROM usuarios", conn);
//         using var reader = cmd.ExecuteReader();
//         while (reader.Read())
//         {
//             users.Add(new User
//             {
//                 id_user = reader.GetInt32("id_user"),
//                 nombre = reader.GetString("nombre"),
//                 profesion = reader.GetString("profesion"),
//                 password = reader.GetString("password"),
//                 img = reader.GetString("img"),
//                 activo = reader.GetBoolean("activo")
//             });
//         }
//         return users;
//     }


//     public bool LoginOrCreate(string nombre, string password)
//     {
//         using var conn = new MySqlConnection(_connectionString);
//         conn.Open();

//         // Verificar si el usuario ya existe
//         var selectCmd = new MySqlCommand("SELECT COUNT(*) FROM usuarios WHERE nombre = @nombre AND password = @password", conn);
//         selectCmd.Parameters.AddWithValue("@nombre", nombre);
//         selectCmd.Parameters.AddWithValue("@password", password);

//         // var count = Convert.ToInt32(selectCmd.ExecuteScalar());

//         // if (count > 0)
//         // {
//         // Si existe, lo marcamos como activo
//         var updateCmd = new MySqlCommand("UPDATE usuarios SET activo = true WHERE nombre = @nombre AND password = @password", conn);
//         updateCmd.Parameters.AddWithValue("@nombre", nombre);
//         updateCmd.Parameters.AddWithValue("@password", password);
//         updateCmd.ExecuteNonQuery();
//         return true;
//         // }
//         // else
//         // {
//         //     // Si no existe, lo creamos
//         //     var insertCmd = new MySqlCommand("INSERT INTO usuarios (nombre, password, activo) VALUES (@nombre, @password, true)", conn);
//         //     insertCmd.Parameters.AddWithValue("@nombre", nombre);
//         //     insertCmd.Parameters.AddWithValue("@password", password);
//         //     insertCmd.ExecuteNonQuery();
//         //     return true;
//         // }
//     }

//     public bool Logout(int id)
//     {
//         using var conn = new MySqlConnection(_connectionString);
//         conn.Open();

//         var cmd = new MySqlCommand("UPDATE usuarios SET activo = false WHERE nombre = @Id", conn);
//         cmd.Parameters.AddWithValue("@Id", id);
//         var result = cmd.ExecuteNonQuery();

//         return result > 0;
//     }
// }
