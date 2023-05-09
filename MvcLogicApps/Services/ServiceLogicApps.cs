using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcLogicApps.Services
{
    public class ServiceLogicApps
    {
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceLogicApps()
        {
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }


        public async Task SendMailAsync
            (string email, string asunto, string mensaje)
        {
            string urlEmail = "https://prod-104.westeurope.logic.azure.com:443/workflows/e36c2964bc82413ca70feffb14740b63/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=r4U7NcwLhW4a297jKfBnuB4rCqSwtWcdxATpMK10g6Y";
            var model = new
            {
                email = email,
                asunto = asunto,
                mensaje = mensaje
            };
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(urlEmail, content);


            }



        }
    }
}
