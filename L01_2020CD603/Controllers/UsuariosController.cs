using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020CD603.Models;
using Microsoft.EntityFrameworkCore;
using L01_2020CD603.Data;

namespace L01_2020CD603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly SQLServerConsumer _sqlserverconsumer;

        public UsuariosController(SQLServerConsumer sqlserverconsumer)
        {
            _sqlserverconsumer = sqlserverconsumer;
        }


        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            List<Usuarios> listadoUsuarios = (from e in _sqlserverconsumer.usuarios select e).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoUsuarios);
        }


        [HttpGet]
        [Route("getByFirstAndLast")]
        public IActionResult Get(string filtroName, string filtroLast)
        {
            List<Usuarios> listadoUsuarios = (from e in _sqlserverconsumer.usuarios where e.nombre.Contains(filtroName) && e.apellido.Contains(filtroLast) select e).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoUsuarios);
        }

        [HttpGet]
        [Route("getByRole")]
        public IActionResult getRole(int rol)
        {
            List<Usuarios> listadoByRol = (from e in _sqlserverconsumer.usuarios where e.rolId == rol select e).ToList();

            if (listadoByRol.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoByRol);
        }

        //Create

        [HttpPost]
        [Route("addUser")]

        public IActionResult createUser([FromBody] Usuarios newUser)
        {
            try
            {
                _sqlserverconsumer.usuarios.Add(newUser);
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

        public IActionResult actualizarEquipo(int id, [FromBody] Usuarios updateuser)
        {
            Usuarios? userExist = (from e in _sqlserverconsumer.usuarios where e.usuarioId == id select e).FirstOrDefault();

            if (User == null)
                return NotFound();


            userExist.rolId = updateuser.rolId;
            userExist.nombreUsuario = updateuser.nombreUsuario;
            userExist.clave = updateuser.clave;
            userExist.nombre = updateuser.nombre;
            userExist.apellido = updateuser.apellido;

            _sqlserverconsumer.Entry(userExist).State = EntityState.Modified;
            _sqlserverconsumer.SaveChanges();

            return Ok(userExist);
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult deleteEquipo(int id)
        {
            Usuarios? userExist = (from e in _sqlserverconsumer.usuarios where e.usuarioId == id select e).FirstOrDefault();

            if (userExist == null)
                return NotFound();

           
           // Eliminar no es correcto por lo tanto se actualiza los registros
            _sqlserverconsumer.Attach(userExist);
            _sqlserverconsumer.usuarios.Remove(userExist);
            _sqlserverconsumer.SaveChanges();

            return Ok(userExist);
        }
    }
}
