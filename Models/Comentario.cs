using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Social.Models;

// public class Comentario
// {
//     public int id_comentario { get; set; }
//     public int id_post { get; set; }
//     public int id_user { get; set; }
//     public DateTime FechaCreacion { get; set; } = DateTime.Now;
//     public string contenido { get; set; }
// }

public class Comentario
{
    [BsonElement("id_user")]
    public string IdUser { get; set; }

    [BsonElement("UserName")]
    public string UserName { get; set; }

    [BsonElement("FechaCreacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [BsonElement("contenido")]
    public string Contenido { get; set; }
}
public class ComentarioConUsuario : Comentario
{
    [BsonElement("usuario")]
    public UserDto Usuario { get; set; } // Relación con la colección de Usuarios
}


