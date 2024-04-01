using System;

namespace TRISTAR.Assessment.People
{
    /// <summary>
    /// A person instance.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// The unique identifier for this person.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The given name for this person.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The family name for this person.
        /// </summary>
        public string LastName { get; set; }
    }
}