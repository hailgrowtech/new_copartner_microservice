using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Ocelot.Multiplexer;
using Microsoft.AspNetCore.Http;
using Ocelot.Middleware;
using Ocelot.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CommonLibrary.CommonResponseModels;

public class OcelotAggregatorResponse : IDefinedAggregator
{
    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
    {
        List<Header> header = new List<Header>();
        try
        {
            List<string> results = new List<string>();

            var headers = responses.SelectMany(x => x.Items.DownstreamResponse().Headers).ToList();
            var contentBuilder = new StringBuilder();
            // contentBuilder.Append("{");
            foreach (var response in responses)
            {
                string downStreamRouteKey = ((DownstreamRoute)response.Items["DownstreamRoute"]).Key;
                var byteArray = await response.Items.DownstreamResponse().Content.ReadAsByteArrayAsync();
                var obj = ConvertToJson(byteArray);
                results.Add($"\"{downStreamRouteKey}\":{obj}");

            }
            contentBuilder.Append(string.Join(",", results));
            //Note Check the response of below 
            // var content = new StringContent(JsonConvert.SerializeObject(contentBuilder), Encoding.UTF8, "application/json");

            var stringContent = new StringContent(contentBuilder.ToString());
            //instead of stringContent write content after checking
            return new DownstreamResponse(stringContent, HttpStatusCode.OK, headers, "OK");
        }
        catch (Exception ex)
        {
            return new DownstreamResponse(null, System.Net.HttpStatusCode.InternalServerError, header, null);
        }
    }
    private static JObject ConvertToJson(byte[] data)
    {
        JObject jObj;
        using (var ms = new MemoryStream(data))
        using (var streamReader = new StreamReader(ms))
        using (var jsonReader = new JsonTextReader(streamReader))
        {
            jObj = (JObject)JToken.ReadFrom(jsonReader);
        }
        return jObj;
    }
}
