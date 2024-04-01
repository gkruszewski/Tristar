using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using TRISTAR.Assessment.Infrastructure;
using TRISTAR.Assessment.People;

namespace TRISTAR.Assessment
{
    /// <summary>
    /// These tests verify that the API and client libraries correctly work with the people endpoints.
    /// </summary>
    [TestClass]
    public class PersonIntegrationTests
    {
        #region Read

        /// <summary>
        /// Tests that the API can get a single person by their unique identifier.
        /// </summary>
        /// <returns></returns>
        [TestCategory("Difficulty 1")]
        [TestMethod]
        public async Task CanGetPerson()
        {
            var factory = new TestWebApplicationFactory();
            var client = factory.CreateClient();
            factory.AddTestData(TestData.CreateTestPeople());
            var httpRepo = new PersonClientRepository(client);

            var person = await httpRepo.GetPerson(TestData.JohnDoe.Id)
                .ConfigureAwait(false);

            // Ensure that we received something from GetPerson.
            Assert.IsNotNull(person);
            // Ensure that the person we received is the one we asked for based on the identifier.
            Assert.AreEqual(TestData.JohnDoe.Id, person.Id);
        }

        /// <summary>
        /// Tests that the API can get all existing people.
        /// </summary>
        /// <returns></returns>
        [TestCategory("Difficulty 1")]
        [TestMethod]
        public async Task CanGetPeople()
        {
            var factory = new TestWebApplicationFactory();
            var client = factory.CreateClient();
            var testPeople = TestData.CreateTestPeople();
            factory.AddTestData(testPeople);
            var httpRepo = new PersonClientRepository(client);

            var people = await httpRepo.GetPeople(new QueryPersonParameters())
                .ConfigureAwait(false);
            var peopleArray = people?.ToArray();

            // Ensure that we received something from GetPeople.
            Assert.IsNotNull(peopleArray);
            // Ensure that we received the expected number of people.
            Assert.AreEqual(testPeople.Length, peopleArray.Length);
            // Ensure that the people we received were in our test data set.
            Assert.IsTrue(peopleArray.All(p => testPeople.Select(x => x.Id).Contains(p.Id)));
        }

        /// <summary>
        /// Tests that the API can query existing people by last name.
        /// </summary>
        /// <returns></returns>
        [TestCategory("Difficulty 2")]
        [TestMethod]
        public async Task CanGetPeopleByLastName()
        {
            var factory = new TestWebApplicationFactory();
            var client = factory.CreateClient();
            factory.AddTestData(TestData.CreateTestPeople());
            var httpRepo = new PersonClientRepository(client);

            var people = await httpRepo.GetPeople(new QueryPersonParameters
            {
                LastName = TestData.JohnDoe.LastName
            }).ConfigureAwait(false);
            var peopleArray = people?.ToArray();

            // Ensure that we received an instance from GetPeople.
            Assert.IsNotNull(peopleArray);
            // Ensure that we only received one result from our filter.
            Assert.AreEqual(1, peopleArray.Length);
            // Test that the person we received was the one we wanted.
            Assert.AreEqual(TestData.JohnDoe.Id, peopleArray[0].Id);
        }

        /// <summary>
        /// Tests that the API can query existing people by their unique identifier.
        /// </summary>
        /// <returns></returns>
        [TestCategory("Difficulty 2")]
        [TestMethod]
        public async Task CanGetPeopleById()
        {
            var factory = new TestWebApplicationFactory();
            var client = factory.CreateClient();
            factory.AddTestData(TestData.CreateTestPeople());
            var httpRepo = new PersonClientRepository(client);
            var idsToFind = new ParametersList<Guid>
            {
                TestData.JohnDoe.Id,
                TestData.JaneSmith.Id
            };

            var people = await httpRepo.GetPeople(new QueryPersonParameters
            {
                Id = idsToFind
            }).ConfigureAwait(false);
            var peopleArray = people?.ToArray();

            // Ensure that we got something back from GetPeople.
            Assert.IsNotNull(peopleArray);
            // Ensure that we received the count we expected.
            Assert.AreEqual(idsToFind.Count, peopleArray.Length);
        }

        #endregion

        #region Write

        /// <summary>
        /// Tests that the API can add a person.
        /// </summary>
        /// <returns></returns>
        [TestCategory("Difficulty 1")]
        [TestMethod]
        public async Task CanCreatePerson()
        {
            var factory = new TestWebApplicationFactory();
            var client = factory.CreateClient();
            var httpRepo = new PersonClientRepository(client);

            var person = await httpRepo.CreatePerson(new EditPersonParameters
            {
                FirstName = TestData.JohnDoe.FirstName,
                LastName = TestData.JohnDoe.LastName
            }).ConfigureAwait(false);

            // Ensure that we received an instance of Person.
            Assert.IsNotNull(person);
            // Test that the first name is correct.
            Assert.AreEqual(TestData.JohnDoe.FirstName, person.FirstName);
            // Test that the last name is correct.
            Assert.AreEqual(TestData.JohnDoe.LastName, person.LastName);
        }

        /// <summary>
        /// Tests that the API can modify an existing person.
        /// </summary>
        /// <returns></returns>
        [TestCategory("Difficulty 1")]
        [TestMethod]
        public async Task CanEditPerson()
        {
            var factory = new TestWebApplicationFactory();
            var client = factory.CreateClient();
            factory.AddTestData(TestData.CreateTestPeople());
            var httpRepo = new PersonClientRepository(client);

            var person = await httpRepo.EditPerson(TestData.JaneSmith.Id,
                new EditPersonParameters
                {
                    LastName = TestData.JohnDoe.LastName
                }).ConfigureAwait(false);

            // Ensure that we received an instance of Person.
            Assert.IsNotNull(person);
            // Ensure that the last name has changed to the expected value.
            Assert.AreEqual(TestData.JohnDoe.LastName, person.LastName);
        }

        /// <summary>
        /// Tests that the API can remove a person.
        /// </summary>
        /// <returns></returns>
        [TestCategory("Difficulty 1")]
        [TestMethod]
        public async Task CanDeletePerson()
        {
            var factory = new TestWebApplicationFactory();
            var client = factory.CreateClient();
            var testPeople = TestData.CreateTestPeople();
            factory.AddTestData(testPeople);
            var httpRepo = new PersonClientRepository(client);

            await httpRepo.DeletePerson(TestData.JohnDoe.Id)
                .ConfigureAwait(false);

            var people = await httpRepo.GetPeople(new QueryPersonParameters())
                .ConfigureAwait(false);
            var peopleArray = people?.ToArray();

            // Ensure that something was returned from GetPeople.
            Assert.IsNotNull(peopleArray);
            // Since we deleted one person, our people count should reflect that.
            Assert.AreEqual(testPeople.Length - 1, peopleArray.Length);
            // Ensure that none of the remaining identifiers belong to John Doe because he was deleted.
            Assert.IsTrue(peopleArray.All(p => p.Id != TestData.JohnDoe.Id));
        }

        /// <summary>
        /// Tests that the API can modify an existing person without affecting other properties.
        /// </summary>
        /// <returns></returns>
        [TestCategory("Difficulty 2")]
        [TestMethod]
        public async Task CanEditLastNameWithoutAffectingOtherData()
        {
            var factory = new TestWebApplicationFactory();
            var client = factory.CreateClient();
            factory.AddTestData(TestData.CreateTestPeople());
            var httpRepo = new PersonClientRepository(client);

            var person = await httpRepo.EditPerson(TestData.JaneSmith.Id,
                new EditPersonParameters
                {
                    LastName = TestData.JohnDoe.LastName
                }).ConfigureAwait(false);

            // Ensure that we received an instance of Person.
            Assert.IsNotNull(person);
            // Ensure that the last name has changed to the expected value.
            Assert.AreEqual(TestData.JohnDoe.LastName, person.LastName);
            // Ensure that the other field was unmodified.
            Assert.AreEqual(TestData.JaneSmith.FirstName, person.FirstName);
        }

        /// <summary>
        /// Tests that the API can modify an existing person without affecting other properties.
        /// Inverts properties of CanEditPersonWithoutAffectingOtherData to ensure solutions work for multiple 
        /// types of edits.
        /// </summary>
        /// <returns></returns>
        [TestCategory("Difficulty 2")]
        [TestMethod]
        public async Task CanEditFirstNameWithoutAffectingOtherData()
        {
            var factory = new TestWebApplicationFactory();
            var client = factory.CreateClient();
            factory.AddTestData(TestData.CreateTestPeople());
            var httpRepo = new PersonClientRepository(client);

            var person = await httpRepo.EditPerson(TestData.JaneSmith.Id,
                new EditPersonParameters
                {
                    FirstName = TestData.JohnDoe.FirstName
                }).ConfigureAwait(false);

            // Ensure that we received an instance of Person.
            Assert.IsNotNull(person);
            // Ensure that the last name has changed to the expected value.
            Assert.AreEqual(TestData.JohnDoe.FirstName, person.FirstName);
            // Ensure that the other field was unmodified.
            Assert.AreEqual(TestData.JaneSmith.LastName, person.LastName);
        }

        /// <summary>
        /// Tests that the API can modify a person by setting a property to null.
        /// </summary>
        /// <returns></returns>
        [TestCategory("Difficulty 3")]
        [TestMethod]
        public async Task CanEditPersonAndSetNullLastName()
        {
            var factory = new TestWebApplicationFactory();
            var client = factory.CreateClient();
            factory.AddTestData(TestData.CreateTestPeople());
            var httpRepo = new PersonClientRepository(client);

            var person = await httpRepo.EditPerson(TestData.JaneSmith.Id,
                new EditPersonParameters
                {
                    LastName = null
                }).ConfigureAwait(false);

            // Ensure that we received an instance of Person.
            Assert.IsNotNull(person);
            // Ensure that the last name was removed (set to null).
            Assert.IsNull(person.LastName);
            // Ensure that the other field was unmodified.
            Assert.AreEqual(TestData.JaneSmith.FirstName, person.FirstName);
        }

        /// <summary>
        /// Tests that the API can modify a person by setting a property to null.
        /// Inverts properties of CanEditPersonAndSetNullProperty() to ensure solutions work for multiple 
        /// types of edits.
        /// </summary>
        /// <returns></returns>
        [TestCategory("Difficulty 3")]
        [TestMethod]
        public async Task CanEditPersonAndSetNullFirstName()
        {
            var factory = new TestWebApplicationFactory();
            var client = factory.CreateClient();
            factory.AddTestData(TestData.CreateTestPeople());
            var httpRepo = new PersonClientRepository(client);

            var person = await httpRepo.EditPerson(TestData.JaneSmith.Id,
                new EditPersonParameters
                {
                    FirstName = null
                }).ConfigureAwait(false);

            // Ensure that we received an instance of Person.
            Assert.IsNotNull(person);
            // Ensure that the last name was removed (set to null).
            Assert.IsNull(person.FirstName);
            // Ensure that the other field was unmodified.
            Assert.AreEqual(TestData.JaneSmith.LastName, person.LastName);
        }

        #endregion
    }
}
