using ConsumeWebAPIBatch9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;

namespace ConsumeWebAPIBatch9.Controllers
{
    public class OperationsController : Controller
    {

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(MyModel obj)
        {
            using (HttpClient client = new HttpClient())
            {
                //method posting API
                client.BaseAddress = new Uri("https://localhost:7299/api/");
                var res = await client.PostAsJsonAsync<MyModel>("Values/PostingData", obj);
                if (res.IsSuccessStatusCode) // status 200
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Ok(new { message = "Something went Wrong" });
                }
            }

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<MyModel> model = await getAlldata();
            return View(model);
        }

        public async Task<IEnumerable<MyModel>> getAlldata()
        {
            IEnumerable<MyModel> obj = null;

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7299/api/");
                var res = client.GetAsync("Values/Gets");
                res.Wait();
                var data = res.Result;
                if (data.IsSuccessStatusCode) // 200
                {
                    var resdata = data.Content.ReadAsAsync<IEnumerable<MyModel>>();
                    obj = resdata.Result;
                }

                return obj;


            }
        }

    }
}
