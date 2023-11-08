using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static CollinsonConsumerTests.PolicyAPIClient;

namespace CollinsonConsumerTests
{
    public class PolicyAPIClient
    {


        private readonly HttpClient client;

        public PolicyAPIClient(string baseUri = null)
        {
            client = new HttpClient { BaseAddress = new Uri(baseUri ?? "https://localhost:5001") };
        }

        public class PolicyValues
        {
         
            public string PolicyReference { get; set; }

        
            public string StartDate { get; set; }

           public List<Insured> Insured { get; set; }

           
        }

        public class PolicyStatus
        {
            public string status { get; set; }
        }

        public partial class Insured
        {
          
            public string FirstName { get; set; }

            public string LastName { get; set; }

            
            public string Email { get; set; }

          
            public string Phone { get; set; }

        
            public string DateOfBirth { get; set; }

            public bool IsPolicyHolder { get; set; }
           public HomeAddress HomeAddress { get; set; }

     
            public BankAccount BankAccount { get; set; }

          
        }

        
        public partial class HomeAddress
        {
          
            public string AddressLine1 { get; set; }

  
            public string AddressLine2 { get; set; }


            public string PostCode { get; set; }


            public string City { get; set; }

            public string CountryCode { get; set; }
        }

        public partial class BankAccount
        {

            public string AccountNumber { get; set; }


            public string SortCode { get; set; }
        }

        public PolicyStatus? GetPolicyStatus(string policyref, string date)
        {
           string requestbody = "{\"PolicyReference\":\""+policyref+"\",\"DateOfBirth\":\""+date+"\"}";
            // var request = new HttpRequestMessage(HttpMethod.Post, $"/v1/policy/search");
            //    request.Headers.Add("Accept", "application/json");
            //  request.Content = new StringContent("{\"PolicyReference\":\"CH0199827795\",\"DateOfBirth\":\"1970-01-11\"}", Encoding.UTF8, "application/json");
            //  var response = _client.SendAsync(request);

            var response = client.PostAsync($"/v1/policy/search", new StringContent(requestbody, Encoding.UTF8, "application/json"));

            var content = response.Result.Content.ReadAsStringAsync().Result;
            var status = response.Result.StatusCode;

            var reasonPhrase = response.Result
                .ReasonPhrase;

           // request.Dispose();
            response.Dispose();

            if (status == HttpStatusCode.OK || status == HttpStatusCode.NotFound || status == HttpStatusCode.Created)
            {
                return !string.IsNullOrEmpty(content)
                    ? JsonConvert.DeserializeObject<PolicyStatus>(content)
                    : null;
            }

            throw new Exception(reasonPhrase);
        }


        public HttpStatusCode Createpolicy(string payload)
        {
            //var  policyvalues = new PolicyValues
            //{
            //    PolicyReference = "CH0123817795",
            //    StartDate = "2023-09-15",
            //    Insured = new List<Insured>
            //    {
            //        new Insured
            //        {
            //            FirstName = "John",
            //            LastName = "Dpes",
            //            Email = "test@test.com",
            //            Phone = "12345678",
            //            DateOfBirth = "20-08-1987",
            //            IsPolicyHolder = true,
            //            BankAccount = new BankAccount
            //            {
            //                AccountNumber = "1213121",
            //                SortCode = "202020"
            //            },
            //            HomeAddress = new HomeAddress
            //            {
            //                AddressLine1 = "eretw",
            //                AddressLine2 = "erwerewrew",
            //                PostCode = "qw2qw",
            //                City = "aadsas",
            //                CountryCode = "GB",
            //            }
            //       }
            //    }
            //};


            var response = client.PostAsync($"/v1/policy", new StringContent(payload.ToString(), Encoding.UTF8, "application/json"));

           var content = response.Result.Content.ReadAsStringAsync().Result;
            var status = response.Result.StatusCode;

            var reasonPhrase = response.Result
                .ReasonPhrase;

            // request.Dispose();
            response.Dispose();

            if (status == HttpStatusCode.OK || status == HttpStatusCode.NotFound || status == HttpStatusCode.Created)
            {
                return status;
                 //   ? JsonConvert.DeserializeObject<PolicyValues>(content)
                //    : null;
            }

            throw new Exception(reasonPhrase);
        }
    }

}
