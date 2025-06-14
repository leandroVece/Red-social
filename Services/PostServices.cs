// using MySql.Data.MySqlClient;
// using Microsoft.Extensions.Configuration;
// using Social.Models;
// using Social.Dto;



using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Social.Models;

public class PostServices
{
    private readonly IMongoCollection<Post> _postsCollection;

    public PostServices(IOptions<MongoDbSettings> mongoSettings, IMongoClient client)
    {
        var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        _postsCollection = database.GetCollection<Post>("Posts"); // Nombre de la colección
    }

    public async Task<List<Post>> GetPostsAsync()
    {
        return await _postsCollection.Find(_ => true)
        .SortByDescending(p => p.FechaCreacion)
        .ToListAsync();
    }

    public async Task CreatePostAsync(Post post)
    {
        await _postsCollection.InsertOneAsync(post);
    }

    public async Task<Post> GetPostByIdAsync(string id)
    {
        return await _postsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdatePostAsync(string id, Post post)
    {
        var filter = Builders<Post>.Filter.Eq(p => p.Id, id);
        await _postsCollection.ReplaceOneAsync(filter, post);
    }

    public async Task DeletePostAsync(string id)
    {
        var filter = Builders<Post>.Filter.Eq(p => p.Id, id);
        await _postsCollection.DeleteOneAsync(filter);
    }

    public async Task AddCommentAsync(string postId, Comentario comentario)
    {
        var filter = Builders<Post>.Filter.Eq(p => p.Id, postId);
        var update = Builders<Post>.Update.Push(p => p.Comentarios, comentario);
        await _postsCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteCommentAsync(string postId, string userId, DateTime fechaComentario)
    {
        var filter = Builders<Post>.Filter.Eq(p => p.Id, postId);
        var update = Builders<Post>.Update.PullFilter(p => p.Comentarios, c => c.IdUser == userId && c.FechaCreacion == fechaComentario);
        await _postsCollection.UpdateOneAsync(filter, update);
    }

    public async Task ResetPostsDatabaseAsync()
    {
        await _postsCollection.Database.DropCollectionAsync("Posts");
    }

}

// public class PostService
// {
//     private readonly string _connectionString;

//     public PostService()
//     {
//         var config = new ConfigurationBuilder()
//             .AddJsonFile("appsettings.json")
//             .Build();

//         _connectionString = config.GetConnectionString("MySql");
//     }

//     public List<PostDto> GetAllConComentarios()
//     {
//         var postsDict = new Dictionary<int, PostDto>();

//         using var conn = new MySqlConnection(_connectionString);
//         conn.Open();

//         var sql = @"
//         SELECT 
//         p.id_post,
//         p.id_user,
//         p.contenido AS post_contenido,
//         p.fecha AS post_fecha,

//         u1.nombre AS UserPost,

//         c.id_comentario,
//         c.id_user AS comentario_user_id,
//         c.contenido AS comentario_contenido,
//         c.fecha AS comentario_fecha,

//         u2.nombre AS UserComentario
//         FROM posts p
//         LEFT JOIN usuarios u1 ON u1.id_user = p.id_user
//         LEFT JOIN comentarios c ON c.id_post = p.id_post
//         LEFT JOIN usuarios u2 ON u2.id_user = c.id_user
//         ORDER BY p.fecha DESC, c.fecha ASC;";

//         using var cmd = new MySqlCommand(sql, conn);
//         using var reader = cmd.ExecuteReader();

//         while (reader.Read())
//         {
//             int postId = reader.GetInt32("id_post");

//             if (!postsDict.ContainsKey(postId))
//             {
//                 postsDict[postId] = new PostDto
//                 {
//                     id_post = postId,
//                     id_user = reader.GetInt32("id_user"),
//                     UserPost = reader.GetString("userPost"),
//                     contenido = reader.GetString("post_contenido"),
//                     FechaCreacion = reader.GetDateTime("post_fecha"),
//                     comentarios = new List<ComentarioDto>()
//                 };
//             }

//             if (!reader.IsDBNull(reader.GetOrdinal("id_comentario")))
//             {
//                 var comentario = new ComentarioDto
//                 {
//                     id_comentario = reader.GetInt32("id_comentario"),
//                     id_user = reader.GetInt32("comentario_user_id"),
//                     contenido = reader.GetString("comentario_contenido"),
//                     UserComentario = reader.GetString("UserComentario"),
//                     FechaCreacion = reader.GetDateTime("comentario_fecha")
//                 };
//                 postsDict[postId].comentarios.Add(comentario);
//             }
//         }

//         return postsDict.Values.ToList();
//     }

//     public void AgregarPost(Post post)
//     {
//         using var conn = new MySqlConnection(_connectionString);
//         conn.Open();

//         var query = "INSERT INTO posts (id_user, contenido) VALUES (@id_user, @contenido)";
//         using var cmd = new MySqlCommand(query, conn);
//         cmd.Parameters.AddWithValue("@id_user", post.id_user);
//         cmd.Parameters.AddWithValue("@contenido", post.contenido);

//         cmd.ExecuteNonQuery();
//     }

//     public void EliminarPost(int id_post)
//     {
//         using var conn = new MySqlConnection(_connectionString);
//         conn.Open();

//         var query = "DELETE FROM posts WHERE id_post = @id";
//         using var cmd = new MySqlCommand(query, conn);
//         cmd.Parameters.AddWithValue("@id", id_post);

//         cmd.ExecuteNonQuery();
//     }
// }