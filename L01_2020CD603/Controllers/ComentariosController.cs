using L01_2020CD603.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020CD603.Models;
using Microsoft.EntityFrameworkCore;
using L01_2020CD603.Data;

namespace L01_2020CD603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {

        private readonly SQLServerConsumer _sqlserverconsumer;

        public ComentariosController(SQLServerConsumer sqlserverconsumer)
        {
            _sqlserverconsumer = sqlserverconsumer;
        }

        [HttpGet]
        [Route("getAll")]

        public IActionResult get()
        {
            List<Comentarios> listadoComentarios = (from e in _sqlserverconsumer.comentarios select e).ToList();

            if (listadoComentarios.Count == 0)
            {
                return NotFound();
            }
            else
                return Ok(listadoComentarios);
        }

        [HttpGet]
        [Route("getbyuser")]

        public IActionResult getByUser(int id)
        {
            List<Comentarios> listadoComentarios = (from e in _sqlserverconsumer.comentarios where e.usuarioId == id select e).ToList();

            if (listadoComentarios.Count == 0)
            {
                return NotFound();
            }
            else
                return Ok(listadoComentarios);
        }

        //Create comment
        [HttpPost]
        [Route("addComment")]

        public IActionResult createComment([FromBody] Comentarios newComment)
        {
            try
            {
                _sqlserverconsumer.comentarios.Add(newComment);
                _sqlserverconsumer.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            
        }

        //Update

        [HttpPut]
        [Route("update/{id}")]

        public IActionResult updateComments(int id, [FromBody] Comentarios updateComment)
        {
            Comentarios? comentarioExist = (from e in _sqlserverconsumer.comentarios where e.cometarioId == id select e).FirstOrDefault();

            if (User == null)
                return NotFound();


            comentarioExist.publicacionId = updateComment.publicacionId;
            comentarioExist.comentario = updateComment.comentario;
            comentarioExist.usuarioId = updateComment.usuarioId;


            _sqlserverconsumer.Entry(comentarioExist).State = EntityState.Modified;
            _sqlserverconsumer.SaveChanges();

            return Ok(comentarioExist);
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult deleteComment(int id)
        {
            Comentarios? comentarioExist = (from e in _sqlserverconsumer.comentarios where e.cometarioId == id select e).FirstOrDefault();

            if (comentarioExist == null)
                return NotFound();


            // Eliminar no es correcto por lo tanto se actualiza los registros
            _sqlserverconsumer.Attach(comentarioExist);
            _sqlserverconsumer.comentarios.Remove(comentarioExist);
            _sqlserverconsumer.SaveChanges();

            return Ok(comentarioExist);
        }
    }
}
