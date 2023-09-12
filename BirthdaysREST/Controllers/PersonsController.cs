using Microsoft.AspNetCore.Mvc;
using BirthDayListRepositoryLib;

namespace BirthdaysREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonsRepository _repo = new();

        // GET: api/<PersonsController>
        [HttpGet]
        public IEnumerable<Person> Get(string? user_id = null, string? sort_by = null, string? name = null)
        {
            return _repo.Get(user_id, sort_by, name);
        }

        // GET api/<PersonsController>/5
        [HttpGet("{id}")]
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
        [HttpPost]
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
