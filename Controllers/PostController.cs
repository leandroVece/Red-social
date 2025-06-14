
using Microsoft.AspNetCore.Mvc;
using Social.Models;


[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{

    private readonly PostServices _postService;

    public PostController(PostServices postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await _postService.GetPostsAsync();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(string id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null) return NotFound();
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        await _postService.CreatePostAsync(post);
        return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(string id, [FromBody] Post post)
    {
        await _postService.UpdatePostAsync(id, post);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(string id)
    {
        await _postService.DeletePostAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/comentarios")]
    public async Task<IActionResult> AddComment(string id, [FromBody] Comentario comentario)
    {
        await _postService.AddCommentAsync(id, comentario);
        return Ok(comentario);
    }

    [HttpDelete("{id}/comentarios")]
    public async Task<IActionResult> DeleteComment(string id, [FromBody] Comentario comentario)
    {
        await _postService.DeleteCommentAsync(id, comentario.IdUser, comentario.FechaCreacion);
        return NoContent();
    }
    [HttpDelete("reiniciar")]
    public async Task<IActionResult> Reiniciar()
    {
        await _postService.ResetPostsDatabaseAsync();
        return NoContent();
    }

}