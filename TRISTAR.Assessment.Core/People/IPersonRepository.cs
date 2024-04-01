using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TRISTAR.Assessment.People
{
    /// <summary>
    /// A contract for manipulating and reading <see cref="Person"/> objects.
    /// This is shared by both client and server, and there are different
    /// implementations on each side.
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Reads a single person via the provided <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the person to return.</param>
        /// <returns>The person with the specified id or null if no person is found.</returns>
        Task<Person> GetPerson(Guid id);

        /// <summary>
        /// Retrieves multiple people matching the provided parameters.
        /// </summary>
        /// <param name="parameters">The query parameters used to filter the people</param>
        /// <returns>The people matching the query parameters. If none are matched, an empty sequence is returned.</returns>
        Task<IEnumerable<Person>> GetPeople(QueryPersonParameters parameters);

        /// <summary>
        /// Creates a new <see cref="Person"/>.
        /// </summary>
        /// <param name="parameters">Details for the person to be created</param>
        /// <returns>The person that was created</returns>
        Task<Person> CreatePerson(EditPersonParameters parameters);

        /// <summary>
        /// Updates a person with the provided <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the person to modify</param>
        /// <param name="parameters">The properties of the person to change.</param>
        /// <returns>The modified person.</returns>
        Task<Person> EditPerson(Guid id, EditPersonParameters parameters);

        /// <summary>
        /// Removes a person matching the provided <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the person to remove.</param>
        /// <returns></returns>
        Task DeletePerson(Guid id);
    }
}