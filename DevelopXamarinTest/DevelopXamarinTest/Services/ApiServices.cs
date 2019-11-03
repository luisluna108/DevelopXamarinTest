using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DevelopXamarinTest.Common.Models;
using DevelopXamarinTest.Helpers;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace DevelopXamarinTest.Services
{
    public class ApiServices
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.TurnOnInternetLbl
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.NoInternetLbl
                };
            }

            return new Response
            {
                IsSuccess = true
            };
        }

        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer
                    };

                }

                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };

            }
            catch (Exception e)
            {

                return new Response {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
    }
}
