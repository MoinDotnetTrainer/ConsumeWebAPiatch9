using ConsumeWebAPIBatch9.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace ConsumeWebAPIBatch9.Controllers
{
    public class ConsumeAPIController : Controller
    {

        public async Task<IEnumerable<MyModel>> ConsumeWithoutPM()
        {
            IEnumerable<MyModel> ApiModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7219/api/");
                var responseTask = client.GetAsync("curd/Getalldata");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<MyModel>>();
                    readTask.Wait();
                    ApiModel = readTask.Result;
                }
                else
                {
                    ApiModel = Enumerable.Empty<MyModel>();
                    ModelState.AddModelError(string.Empty, "Server error.Please contact administrator.");
                }
            }
            return ApiModel;
        }

        public async Task<IEnumerable<MyModel>> ConsumeWithout1PM(string gender)
        {
            IEnumerable<MyModel> ApiModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7219/api/");
                var responce = client.GetAsync("Curd/getlistbygender?gender=" + gender.ToString());
                responce.Wait();
                var result = responce.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<MyModel>>();
                    readTask.Wait();
                    ApiModel = readTask.Result;
                }
                else
                {
                    ApiModel = Enumerable.Empty<MyModel>();
                    ModelState.AddModelError(string.Empty, "Server error.Please contact administrator.");
                }
            }
            return ApiModel;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<MyModel> obj = await ConsumeWithoutPM();
            return View(obj);
        }



        private static List<MyModel> SearchDataBygender(string gender)
        {
            List<MyModel> customers = new List<MyModel>();
            string apiUrl = "https://localhost:7219/api/curd";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/getlistbygender?gender={0}", gender)).Result;
            if (response.IsSuccessStatusCode)
            {
                customers = JsonConvert.DeserializeObject<List<MyModel>>(response.Content.ReadAsStringAsync().Result);
            }
            return customers;
        }

        public async Task<IActionResult> GenderList(string gender)
        {
            //IEnumerable<MyModel> obj =  SearchDataBygender(gender);

            IEnumerable<MyModel> obj = await ConsumeWithout1PM(gender);
            return View(obj);
        }

        private static MyModel getDataByID(int id)
        {

            MyModel obj = new MyModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7219/api/");
                var responce = client.GetAsync("Curd/databyid?id=" + id.ToString());
                responce.Wait();
                var result = responce.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<MyModel>();
                    readTask.Wait();
                    obj = readTask.Result;
                }
                else
                {
                 
                }
            }
            return obj;
        }

        public IActionResult daabyid(int id)
        {
            //IEnumerable<MyModel> obj =  SearchDataBygender(gender);

            MyModel obj =  getDataByID(id);
            return View(obj);
        }
    }
}
