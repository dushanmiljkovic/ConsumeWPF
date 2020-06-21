using Common;
using Models.DTO;
using Models.RequestModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace EatCodeDesktop.Helper
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient apiClient { get; set; }
        public APIHelper()
        {
            InitClient();
        }

        private void InitClient()
        {
            var url = ConfigurationManager.AppSettings["api"];
            apiClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> CreateRecipe(CreateRecipeRequestModel model)
        {
            var data = ConvertObjToJsonStringContent(model);
            using (HttpResponseMessage responseMessage = await apiClient.PostAsync(RecipeApiEndpoints.RecipeCreate, data))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<string>();
                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }

        }
        public async Task<string> UpdateRecipe(UpdateRecipeRequestModel model)
        { 
            var data = ConvertObjToJsonStringContent(model);
            using (HttpResponseMessage responseMessage = await apiClient.PostAsync(RecipeApiEndpoints.RecipeUpdate, data))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<string>();
                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }
        public async Task<bool> DeleteRecipe(Guid guid)
        {
            using (HttpResponseMessage responseMessage = await apiClient.GetAsync(RecipeApiEndpoints.RecipeDelete(guid.ToString())))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<bool>();
                    return result;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<List<RecipeDTO>> GetAllRecipes()
        {

            using (HttpResponseMessage responseMessage = await apiClient.GetAsync(RecipeApiEndpoints.RecipeAll))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<List<RecipeDTO>>();
                    return result;
                }
                else
                {
                    return null;
                }
            }

        }

        public async Task<string> Auth(string userName, string pass)
        {
            var data = new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string,string>("grant_type","password"),
                    new KeyValuePair<string,string>("username",userName),
                    new KeyValuePair<string,string>("password",pass),
                });

            using (HttpResponseMessage responseMessage = await apiClient.PostAsync("/Token", data))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<string>();
                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }
        public static StringContent ConvertObjToJsonStringContent<T>(T model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return data;
        }

       
    }
}
