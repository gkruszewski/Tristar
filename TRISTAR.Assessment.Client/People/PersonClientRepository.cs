using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TRISTAR.Assessment.People
{
    /// <summary>
    /// This is the client-side repository for people. 
    /// It makes http requests to the person API endpoints.
    /// </summary>
    public class PersonClientRepository : IPersonRepository
    {
        private readonly HttpClient _httpClient;

        public PersonClientRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Invokes the http api to create a person on the server.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<Person> CreatePerson(EditPersonParameters parameters)
        {
            // Implementation goes here!
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invokes the http api to delete a person from the server.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeletePerson(Guid id)
        {
            // Implementation goes here!
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invokes the http api to modify a person on the server.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<Person> EditPerson(Guid id, EditPersonParameters parameters)
        {
            // Implementation goes here!
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invokes the http api to return one or more people that match the query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<IEnumerable<Person>> GetPeople(QueryPersonParameters parameters)
        {
            // Implementation goes here!
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invokes the http api to return one person who matches the provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Person> GetPerson(Guid id)
        {
            // Implementation goes here!
            throw new NotImplementedException();
        }
    }
}