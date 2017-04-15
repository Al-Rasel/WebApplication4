using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebApplication4.CacheControl
{
    public class CachedOkResult<T> : OkNegotiatedContentResult<T>
    {
        private TimeSpan? MaxAgeValue { get; }

        private CachedOkResult(T content, ApiController controller, TimeSpan? maxAge) 
            : base(content, controller)
        {
            MaxAgeValue = maxAge;
        }

        public CachedOkResult(T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, 
            IEnumerable<MediaTypeFormatter> formatters, TimeSpan? maxAge) 
            : base(content, contentNegotiator, request, formatters)
        {
            MaxAgeValue = maxAge;
        }

        public override async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            if (MaxAgeValue == null)
            {
                return await base.ExecuteAsync(cancellationToken);
            }

            var httpResponseMessage = await base.ExecuteAsync(cancellationToken);
            httpResponseMessage.Headers.CacheControl = new CacheControlHeaderValue()
            {               
                MaxAge = MaxAgeValue,
                Public = true,                
            };
            return httpResponseMessage;
        }

        public static CachedOkResult<T> CachedOk(ApiController controller, T content, 
            TimeSpan? cachingTimeSpan = null)
        {
            return new CachedOkResult<T>(content, controller, cachingTimeSpan);
        }
    }
}