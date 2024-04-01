using System;
using TRISTAR.Assessment.People;

namespace TRISTAR.Assessment
{
    /// <summary>
    /// Holds testing constants and creates test instances of data.
    /// </summary>
    internal static class TestData
    {
        internal static class JaneSmith
        {
            public static readonly Guid Id = Guid.Parse("b548a703-f2ee-45b5-9ea3-514dcd502cca");
            public const string FirstName = "Jane";
            public const string LastName = "Smith";
        }

        internal static class JohnDoe
        {
            public static readonly Guid Id = Guid.Parse("cd6b4fbf-69bc-4f88-974f-a5e308d32185");
            public const string FirstName = "John";
            public const string LastName = "Doe";
        }

        internal static class BobJones
        {
            public static readonly Guid Id = Guid.Parse("561832d0-364c-4b19-ba8a-0d730467d806");
            public const string FirstName = "Bob";
            public const string LastName = "Jones";
        }

        /// <summary>
        /// Creates a new instance of the Jane Smith person for testing.
        /// </summary>
        /// <returns></returns>
        internal static Person CreateJaneSmith()
        {
            return new Person
            {
                FirstName = JaneSmith.FirstName,
                Id = JaneSmith.Id,
                LastName = JaneSmith.LastName
            };
        }

        /// <summary>
        /// Creates a new instance of the John Doe person for testing.
        /// </summary>
        /// <returns></returns>
        internal static Person CreateJohnDoe()
        {
            return new Person
            {
                FirstName = JohnDoe.FirstName,
                Id = JohnDoe.Id,
                LastName = JohnDoe.LastName
            };
        }

        /// <summary>
        /// Creates a new instance of the Bob Jones person for testing.
        /// </summary>
        /// <returns></returns>
        internal static Person CreateBobJones()
        {
            return new Person
            {
                FirstName = BobJones.FirstName,
                Id = BobJones.Id,
                LastName = BobJones.LastName
            };
        }

        /// <summary>
        /// Creates an array of two people: John Doe and Jane Smith.
        /// </summary>
        /// <returns></returns>
        internal static Person[] CreateTestPeople()
        {
            return new[]
            {
                CreateJohnDoe(),
                CreateJaneSmith(),
                CreateBobJones()
            };
        }
    }
}
