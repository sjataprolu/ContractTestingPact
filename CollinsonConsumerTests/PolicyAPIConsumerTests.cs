using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PactNet.Output.Xunit;
using PactNet;
using Xunit.Abstractions;
using System.Net;
using System.Net.WebSockets;
using System.Dynamic;
using PactNet.Verifier;

namespace CollinsonConsumerTests
{
    public class PolicyAPIConsumerTests
    {

        private IPactBuilderV3 pact;

      

        public PolicyAPIConsumerTests(ITestOutputHelper output)
        {


            var Config = new PactConfig
            {
                PactDir = Path.Join("..", "..", "..", "..", "pacts"),

                Outputters = new[] { new XunitOutput(output) },
                LogLevel = PactLogLevel.Debug

            };

            pact = Pact.V3("Consumer_Chase", "Provider_Collinson", Config).WithHttpInteractions();

        }

        [Fact]
        public async Task OnTerminated()
        {
            
            string json = "{\"value\":{\"status\":\"Terminated\"},\"statusCode\":200}";
            string jsonbody = "{\"PolicyReference\":\"CH0199827795\",\"DateOfBirth\":\"1970-01-11\"}";
            pact
                 .UponReceiving("A Post request to find the Status of the policy")
                 .Given("an Policy Reference exists")
                  .WithRequest(HttpMethod.Post, "/v1/policy/search")
                  .WithBody(jsonbody, "application/json")
                  .WillRespond()
                  .WithStatus(System.Net.HttpStatusCode.OK)
                  //.WithBody(json , "application/json")
                  .WithJsonBody(new
                  {
                      status = "Terminated"
                  }
                 );

            await pact.VerifyAsync(async ctx =>
            {
                var consumer = new PolicyAPIClient(ctx.MockServerUri.ToString());
                var result = consumer.GetPolicyStatus("CH0199827795", "1970-01-11");
                Assert.Equal("Terminated", result.status);
            });

        }


        [Fact]
        public async Task OnPolicyIssue()
        {
            string jsonbody = "{\"PolicyReference\":\"CH0123817795\",\"StartDate\":\"2023-09-15\",\"Insured\":[{\"FirstName\":\"John\",\"LastName\":\"Dpes\",\"Email\":\"test@test.com\",\"Phone\":\"12345678\",\"DateOfBirth\":\"20-08-1987\",\"IsPolicyHolder\":true,\"HomeAddress\":{\"AddressLine1\":\"eretw\",\"AddressLine2\":\"erwerewrew\",\"PostCode\":\"qw2qw\",\"City\":\"aadsas\",\"CountryCode\":\"GB\"},\"BankAccount\":{\"AccountNumber\":\"1213121\",\"SortCode\":\"202020\"}}]}";

            pact
                 .UponReceiving("A Post request to find the Status of the policy")
                 .Given("an Policy Reference exists")
                  .WithRequest(HttpMethod.Post, "/v1/policy")
                  .WithBody(jsonbody, "application/json")
                  .WillRespond()
                  .WithStatus(System.Net.HttpStatusCode.Created);
                   
  

            await pact.VerifyAsync(async ctx =>
            {
                var consumer = new PolicyAPIClient(ctx.MockServerUri.ToString());
                var result = consumer.Createpolicy(jsonbody);
                Assert.Equal("Created",result.ToString());
            });

        }
    }
}
