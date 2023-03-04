using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020CD603.Models;
using Microsoft.EntityFrameworkCore;
using L01_2020CD603.Data;

namespace L01_2020CD603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionsController : ControllerBase
    {

        private readonly SQLServerConsumer _sqlserverconsumer;

        public CalificacionsController(SQLServerConsumer sqlserverconsumer)
        {
            _sqlserverconsumer = sqlserverconsumer;
        }

        [HttpGet]
        [Route("getAll")]

        public IActionResult get()
        {
            List<Calificaciones> listadoCalificaciones = (from e in _sqlserverconsumer.calificaciones select e).ToList();

            if (listadoCalificaciones.Count == 0)
            {
                return NotFound();
            }
            else
                return Ok(listadoCalificaciones);
        }

        [HttpGet]
        [Route("getByPublicacion")]

        public IActionResult getByPublicacion(int id)
        {
            List<Calificaciones> listado = (from e in _sqlserverconsumer.calificaciones where e.publicacionId == id select e).ToList();

            if (listado.Count == 0)
            {
                return NotFound();
            }

            return Ok(listado);
        }


        //Create calificacion
        [HttpPost]
        [Route("addCalificacion")]

        public IActionResult createCalificacion([FromBody] Calificaciones newCalificacion)
        {
            try
            {
                _sqlserverconsumer.calificaciones.Add(newCalificacion);
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

        public IActionResult updateCalificacion(int id, [FromBody] Calificaciones updateCalificacion)
        {
            Calificaciones? calificacionExist = (from e in _sqlserverconsumer.calificaciones where e.calificacionId == id select e).FirstOrDefault();

            if (User == null)
                return NotFound();


            calificacionExist.publicacionId = updateCalificacion.publicacionId;
            calificacionExist.usuarioId = updateCalificacion.usuarioId;
            calificacionExist.calificacion = updateCalificacion.calificacion;
            

            _sqlserverconsumer.Entry(calificacionExist).State = EntityState.Modified;
            _sqlserverconsumer.SaveChanges();

            return Ok(calificacionExist);
        }


        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult deleteCalificacion(int id)
        {
            Calificaciones? calificacionExist = (from e in _sqlserverconsumer.calificaciones where e.calificacionId == id select e).FirstOrDefault();

            if (calificacionExist == null)
                return NotFound();


            // Eliminar no es correcto por lo tanto se actualiza los registros
            _sqlserverconsumer.Attach(calificacionExist);
            _sqlserverconsumer.calificaciones.Remove(calificacionExist);
            _sqlserverconsumer.SaveChanges();

            return Ok(calificacionExist);
        }
    }
}
