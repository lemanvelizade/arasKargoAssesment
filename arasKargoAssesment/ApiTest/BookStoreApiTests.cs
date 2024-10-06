using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using System.IO;

namespace BookStoreApiTests
{
    public class BookStoreApiTests
    {
        private RestClient client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("http://bookstore.com/api");
        }

        [Test]
        public void ValidateSearchBooksApiResponse()
        {
            // Load the JSON schema
            var schema = JSchema.Parse(File.ReadAllText("Schemas/search-books-schema.json"));

            // Make the API call
            var request = new RestRequest("books", RestSharp.Method.Get);
            request.AddQueryParameter("title", "The Great Gatsby");
            var response = client.Execute(request);
            var jsonResponse = JObject.Parse(response.Content);

            // Validate the response against the schema
            Assert.IsTrue(jsonResponse.IsValid(schema));
        }

        [Test]
        public void ValidateGetBookDetailsApiResponse()
        {
            // Load the JSON schema
            var schema = JSchema.Parse(File.ReadAllText("Schemas/book-details-schema.json"));

            // Make the API call
            var request = new RestRequest("books/{id}", RestSharp.Method.Get);
            request.AddUrlSegment("id", "1");
            var response = client.Execute(request);
            var jsonResponse = JObject.Parse(response.Content);

            // Validate the response against the schema
            Assert.IsTrue(jsonResponse.IsValid(schema));
        }

        [TearDown]
        public void Cleanup()
        {
            client.Dispose();
        }
    }
}
