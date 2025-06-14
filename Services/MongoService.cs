using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Social.Models;

public class MongoService
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Post> _postsCollection;
    private readonly IMongoCollection<User> _usersCollection;

    public MongoService(IOptions<MongoDbSettings> mongoSettings, IMongoClient client)
    {
        _database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        _postsCollection = _database.GetCollection<Post>(mongoSettings.Value.CollectionPosts);
        _usersCollection = _database.GetCollection<User>(mongoSettings.Value.CollectionUsers);
    }

    public async Task<List<Post>> GetPostsAsync()
    {
        return await _postsCollection.Find(_ => true).ToListAsync();
    }

    public async Task CreateUserAsync(User user)
    {
        await _usersCollection.InsertOneAsync(user);
    }
    public async Task<User> GetUserByIdAsync(string id)
    {
        return await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
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