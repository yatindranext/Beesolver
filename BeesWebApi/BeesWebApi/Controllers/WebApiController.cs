using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BeesWebApi.Controllers
{

    public class WebApiController : ApiController
    {
        public async Task<IHttpActionResult> GetAllCard(string Name, string Colors,string Type)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.magicthegathering.io/v1/cards");
                var jsondata = new ApiRequestModel();
                jsondata.Name = Name;
                jsondata.Type = Type;
                jsondata.Color = Colors;
                var data = JsonConvert.SerializeObject(jsondata);

                var content = new StringContent(data, null, "application/json");

                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return Ok(new ApiLoginResponseModel
                {
                    Error = true,
                    Message = await response.Content.ReadAsStringAsync(),
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiLoginResponseModel
                {
                    Error = true,
                    Message = ex.Message,
                });
            }

           
        }

        public class ApiLoginResponseModel
        {
            public bool Error { get; set; }
            public string Message { get; set; }
        }
        public class ApiRequestModel
        {
            public string Name { get; set; }
            public string Color { get; set; }
            public string Type { get; set; }
        }
    }
}
