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


        [HttpGet]
        public IActionResult Edit(int id)
        {
            //get data by id logic

            MyModel obj = getDatabyID(id);
            return View(obj);
        }

        public static MyModel getDatabyID(int id)
        {
            MyModel obj = new MyModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7299/api/");
                var data = client.GetAsync("Values/getDatabyID?id=" + id.ToString());
                data.Wait();
                var res = data.Result;
                if (res.IsSuccessStatusCode)
                {
                    var resultdata = res.Content.ReadAsAsync<MyModel>();
                    obj = resultdata.Result;
                }
            }
            return obj;
        }



        [HttpPost]
        public async Task<IActionResult> Edit(MyModel obj)
        {
            //Update
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7299/api/");
                var data = client.PutAsJsonAsync<MyModel>("Values/updatedata", obj);
                data.Wait();
                var resulteddata = data.Result;
                if (resulteddata.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest();
                }
            }

        }


        public async Task<ActionResult<MyModel>> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7299/api/");
                var result = client.DeleteAsync("Values/DeleteData?id=" + id.ToString());
                result.Wait();
                var resulteddata = result.Result;
                if (resulteddata.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            return View();
        }

    }
}
