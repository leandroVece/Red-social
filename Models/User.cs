using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Social.Models;

// public class User
// {
//     public int id_user { get; set; }
//     public string nombre { get; set; }
//     public string profesion { get; set; }
//     public string password { get; set; }
//     public string img { get; set; }
//     public bool activo { get; set; }

// }
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("nombre")]
    public string Nombre { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }


    [BsonElement("activo")]
    public bool Activo { get; set; }
}
public class UserDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("nombre")]
    public string Nombre { get; set; }

}

