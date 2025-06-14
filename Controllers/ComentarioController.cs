// using Microsoft.AspNetCore.Mvc;
// using Social.Models;


// [ApiController]
// [Route("api/[controller]")]
// public class ComentariosController : ControllerBase
// {
//     private readonly ComentarioService _service;

//     public ComentariosController()
//     {
//         _service = new ComentarioService();
//     }

//     [HttpGet]
//     public IActionResult GetAll()
//     {
//         var comentarios = _service.GetAll();
//         return Ok(comentarios);
//     }


//     [HttpPost]
//     public IActionResult AgregarComentario([FromBody] Comentario comentario)
//     {
//         _service.AgregarComentario(comentario);
//         return Ok("Comentario agregado.");
//     }

//     [HttpDelete("{id}")]
//     public IActionResult EliminarComentario(int id)
//     {
//         _service.EliminarComentario(id);
//         return Ok("Comentario eliminado.");
//     }
// }