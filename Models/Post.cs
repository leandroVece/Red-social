using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Social.Models;

// public class Post
// {
//     public int id_post { get; set; }
//     public int id_user { get; set; }
//     public DateTime FechaCreacion { get; set; } = DateTime.Now;
//     public string contenido { get; set; }
// }

public class Post
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } // MongoDB usa string para ObjectId

    [BsonElement("id_user")]
    public string IdUser { get; set; }
    [BsonElement("UserName")]
    public string UserName { get; set; }

    [BsonElement("FechaCreacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [BsonElement("contenido")]
    public string Contenido { get; set; }

    [BsonElement("comentarios")]
    public List<Comentario>? Comentarios { get; set; } = new List<Comentario>();
}

public class PostConUsuario : Post
{
    [BsonElement("usuario")]
    public UserDto Usuario { get; set; } // Relación con la colección de Usuarios
}
