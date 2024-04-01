using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TRISTAR.Assessment.Infrastructure;

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
        public async Task<Person> CreatePerson(EditPersonParameters parameters)
        {
            var response = await _httpClient.PostAsJsonAsync("api/people", parameters);

            return await response.Content.ReadFromJsonAsync<Person>();
        }

        /// <summary>
        /// Invokes the http api to delete a person from the server.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeletePerson(Guid id)
        {
            return _httpClient.DeleteAsync($"api/people/{id}");
        }

        /// <summary>
        /// Invokes the http api to modify a person on the server.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<Person> EditPerson(Guid id, EditPersonParameters parameters)
        {
            var response = await _httpClient.PatchAsJsonAsync($"api/people/{id}", parameters, new JsonSerializerOptions
            {
                Converters = { new PatchParametersFactory() }
            });

            return await response.Content.ReadFromJsonAsync<Person>();
        }

        /// <summary>
        /// Invokes the http api to return one or more people that match the query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Person>> GetPeople(QueryPersonParameters parameters)
        {
            var queryBuilder = new QueryBuilder();

            void AddQueryParameter(string key, IEnumerable<string> parameterValues)
            {
                if (parameterValues?.Any() ?? false)
                {
                    queryBuilder.Add(key, parameterValues);
                }
            }

            AddQueryParameter(nameof(QueryPersonParameters.FirstName), parameters.FirstName);
            AddQueryParameter(nameof(QueryPersonParameters.LastName), parameters.LastName);
            AddQueryParameter(nameof(QueryPersonParameters.Id), parameters.Id?.Select(id => id.ToString()));

            return await _httpClient.GetFromJsonAsync<Person[]>($"api/people/{queryBuilder}");
        }

        /// <summary>
        /// Invokes the http api to return one person who matches the provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Person> GetPerson(Guid id)
        {
            return _httpClient.GetFromJsonAsync<Person>($"api/people/{id}");
        }

        /// <summary>
        /// Converter to ignore writing changes that are not being tracked 
        /// </summary>
        /// <remarks>
        /// Subclass properties that are null from not being changed should be ignored during deserialization
        /// </remarks>
        private class PatchParametersFactory : JsonConverterFactory
        {
            public override bool CanConvert(Type typeToConvert)
            {
                return typeof(PatchParametersBase).IsAssignableFrom(typeToConvert);
            }

            public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            {
                return new PatchParametersBaseConverter();
            }

            private class PatchParametersBaseConverter : JsonConverter<PatchParametersBase>
            {
                public override PatchParametersBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

                public override void Write(Utf8JsonWriter writer, PatchParametersBase value, JsonSerializerOptions options)
                {
                    var type = value.GetType();

                    writer.WriteStartObject();

                    foreach (var changedProperty in value.GetChangedProperties())
                    {
                        writer.WritePropertyName(changedProperty);

                        JsonSerializer.Serialize(writer, type.GetProperty(changedProperty).GetValue(value), options);
                    }

                    writer.WriteEndObject();
                }
            }
        }
    }
}