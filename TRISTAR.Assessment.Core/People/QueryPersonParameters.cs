using System;
using TRISTAR.Assessment.Infrastructure;

namespace TRISTAR.Assessment.People
{
    /// <summary>
    /// Parameters that are used to search / filter the list of people on the server.
    /// </summary>
    public class QueryPersonParameters
    {
        /// <summary>
        /// One or more first names to filter by. Multiple names will be treated like an OR.
        /// </summary>
        public ParametersList<string> FirstName { get; set; }

        /// <summary>
        /// One or more last names to filter by. Multiple names will be treated like an OR.
        /// </summary>
        public ParametersList<string> LastName { get; set; }

        /// <summary>
        /// One or more unique identifiers to filter by. Multiple ids will be treated like an OR.
        /// </summary>
        public ParametersList<Guid> Id { get; set; }
    }
}