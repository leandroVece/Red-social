namespace Social.Dto;

public class PostDto
{
    public int id_user { get; set; }
    public int id_post { get; set; }
    public string UserPost { get; set; }
    public string contenido { get; set; }
    public DateTime FechaCreacion { get; set; }

    public List<ComentarioDto> comentarios { get; set; }

}

public class ComentarioDto
{
    public int id_user { get; set; }

    public string UserComentario { get; set; }

    public int id_comentario { get; set; }
    public DateTime FechaCreacion { get; set; }

    public string contenido { get; set; }

}