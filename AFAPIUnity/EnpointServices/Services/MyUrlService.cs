using AFAPIUnity.EnpointServices.Contract;

namespace AFAPIUnity.EnpointServices.Services
{
    public class MyUrlService: IMyUrlService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyUrlService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetBaseDownloadUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;

            if (request != null)
            {
                string scheme = request.Scheme;
                string host = request.Host.Value;

                // Create the base download URL
                return $"{scheme}://{host}/";
            }

            // Handle the case where HttpContext is not available (e.g., background service)
            throw new InvalidOperationException("HttpContext is not available.");
        }
    }
}
