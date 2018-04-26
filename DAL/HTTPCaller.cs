using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DAL
{
    public class HttpCaller
    {
        public async Task<HttpResponseMessage> GetResponse(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                return response;
            }
        }

        public HttpResponseMessage Post(string url, string data)
        {
            throw new NotImplementedException();
        }
    }
}
