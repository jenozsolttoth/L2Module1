using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DAL
{
    public class HttpCaller
    {
        public async Task<HttpResponseMessage> GetResponse(string URL)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(URL);
                return response;
            }
        }

        public async Task<HttpResponseMessage> Post(string URL, string data)
        {
            throw new NotImplementedException();
        }
    }
}
