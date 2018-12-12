using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dream.Business.Extensions
{
    /// <summary>
    /// This extension rewrite response body within error and 400 status.
    /// </summary>
    public static class HttpExtension
    {
        public static T CheckError<T>(this T result, HttpResponse response, string error) where T : class
        {
            if (error == null)
                return result;
            var json = new {Error = error}.SerializeToJson();
            response.OverrideResponse(StatusCodes.Status400BadRequest, json);
            return null;
        }

        public static void OverrideResponse(this HttpResponse response, int status, string message)
        {
            response.OnStarting(() =>
            {
                response.StatusCode = status;
                response.ContentLength = message.Length;
                response.WriteAsync(message);
                return Task.CompletedTask;
            });
        }

        public static string SerializeToJson<T>(this T content)
        {
            string json =
                JsonConvert.SerializeObject(content,
                    new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});

            return json;
        }
    }
}