using Microsoft.AspNetCore.Mvc;
using BirthDayListRepositoryLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BirthdaysREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonsRepository _repo;

        public PersonsController(PersonsRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<PersonsController>
        [HttpGet]
        public IEnumerable<Person> Get(string? user_id = null, string? sort_by = null, string? name_fragment = null, int? age_below=null, int? age_above=null)
        {
            return _repo.Get(user_id, sort_by, name_fragment, age_below, age_above);
        }

        // GET api/<PersonsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Person> Get(int id)
        {
            Person? p = _repo.Get(id);
            if (p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }

        // POST api/<PersonsController>
        /// <summary>
        /// Add a new person to the list
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        /// p.userId cannot be null
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Person> Post([FromBody] Person p)
        {
            try
            {
                _repo.Add(p);
                return Created("api/persons/" + p.Id, p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PersonsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Person> Put(int id, [FromBody] Person data)
        {
            Person? existingPerson = _repo.Update(id, data);
            if (existingPerson == null)
            {
                return NotFound("No such person, id: " + id);
            }
            return Ok(existingPerson);
        }

        // DELETE api/<PersonsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Person> Delete(int id)
        {
            Person? existingPerson = _repo.Remove(id);
            if (existingPerson == null)
            {
                return NotFound("No such person, id: " + id);
            }
            return Ok(existingPerson);
        }
    }
}
