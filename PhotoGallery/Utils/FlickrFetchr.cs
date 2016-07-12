using System.Threading.Tasks;
using System.Net.Http;

namespace PhotoGallery.Utils
{
    public class FlickrFetchr
    {
        public async Task<byte[]> GetBytesAsync(string requestUrl)
        {
            using (var client = new HttpClient())
            {
                return await client.GetByteArrayAsync(requestUrl).ConfigureAwait(false);
            }
        }

        public async Task<string> GetStringAsync(string requestUrl)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(requestUrl).ConfigureAwait(false);
            }
        }
    }
}