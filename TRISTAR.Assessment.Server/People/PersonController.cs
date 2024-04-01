using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TRISTAR.Assessment.People
{
    /// <summary>
    /// The http controller that defines the API endpoints.
    /// This class essentially just hands off to the <see cref="IPersonRepository"/>.
    /// </summary>
    [Route("api/people")]
    public class PersonController : ControllerBase, IPersonRepository
    {
        private readonly IPersonRepository _repository;

        public PersonController(IPersonRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        public Task<Person> CreatePerson([FromBody] EditPersonParameters parameters)
        {
            return _repository.CreatePerson(parameters);
        }

        [HttpDelete("{id}")]
        public Task DeletePerson(Guid id)
        {
            return _repository.DeletePerson(id);
        }

        [HttpPatch("{id}")]
        public Task<Person> EditPerson(Guid id, [FromBody] EditPersonParameters parameters)
        {
            return _repository.EditPerson(id, parameters);
        }

        [HttpGet]
        public Task<IEnumerable<Person>> GetPeople([FromQuery] QueryPersonParameters parameters)
        {
            return _repository.GetPeople(parameters);
        }

        [HttpGet("{id}")]
        public Task<Person> GetPerson(Guid id)
        {
            return _repository.GetPerson(id);
        }
    }
}
