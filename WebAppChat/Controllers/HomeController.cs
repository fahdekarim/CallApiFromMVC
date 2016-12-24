using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Web.Mvc;
using WebAppChat.Models;

namespace WebAppChat.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        HttpClient client;

        string urlAnExternalApi = "https://backend.sigfox.com/api/devices/17AA23/messages";
        //The URL of the WEB API Service
        string urlAnInternalApi = "http://localhost:59897/api/infoperso";
        List<IOTSigfox> tmpdb;
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public HomeController()
        {

            client = new HttpClient();
            client.BaseAddress = new Uri(urlAnExternalApi);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var byteArray = Encoding.ASCII.GetBytes("5842fff59058c2525ddd85c2:8e8d25dc19fcc43bcc0b0432301fd5cc");
            var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            client.DefaultRequestHeaders.Authorization = header;

            //si tmpdb est null ->initialisation  / sinon reaffecter l'ancienne






        }
        public async Task<ActionResult> index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlAnInternalApi);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employees = JsonConvert.DeserializeObject<List<infoperso>>(responseData);
                TempData["id"] = Employees.Count;

                return View(Employees);
            }
            return View("Error");
        }
        // GET: EmployeeInfo
        /* [HttpPost]
         public void Sigfox(String device , string time , string data ,string snr, string lingQuality)
         {
             TempData["IOT"] = new IOTSigfox(device,time,data,snr,lingQuality);
             RedirectToAction("Sigfox");
         }
         */
        [HttpPost]
        public void Sigfox(IOTSigfox iot)
        {
            Session["IOT"] = iot;
            RedirectToAction("Sigfox");
        }
        //http://d9afee76.ngrok.io/sigfox?device=device&time=time&data=data&snr=snr&linkQuality=lng
        public async Task<ActionResult> Sigfox(String device, string time, string data, string snr, string linkQuality)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlAnExternalApi);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employees = JsonConvert.DeserializeObject<IOTSigfoxData>(responseData);
                //  TempData["id"]= Employees.Count;
                //  if(!Employees.data.Contains((IOTSigfox)TempData["IOT"]))
                //  {
                //     RedirectToAction("index");
                //}
                tmpdb = (List<IOTSigfox>)TempData["list"] == null ? new List<IOTSigfox>() : (List<IOTSigfox>)TempData["list"];
                if (tmpdb.Count == 0)
                {
                    tmpdb = Employees.data;
                    TempData["list"] = tmpdb;
                }
                else
                {
                    tmpdb.Insert(0, new IOTSigfox(device, time, data, snr, linkQuality));
                    TempData["list"] = tmpdb;
                    //Clients.All.PostFromTheCloud(device, time, data, snr, linkQuality);
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                    if (hubContext != null)
                    {
                       
                        hubContext.Clients.All.updateSigfox(device, time, data, snr, linkQuality);
                    }

                }

                /*  if (TempData["IOT"] != null)
                  {
                  tmpdb.Insert(0, new IOTSigfox(device, time, data, snr, lingQuality));
                      //tmpdb.Insert(0,(IOTSigfox)TempData["IOT"]); //!tmpdb.Contains((IOTSigfox)TempData["IOT"]) &&
                      TempData["IOT"] = null;
                  }
                  */
                return View(tmpdb);

                //return View(Employees.data);
            }
            return View("Error");
        }
        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult Create()
        {
            infoperso p = new infoperso();
            p.Id = (int)TempData["id"];
            return View(p);
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(infoperso Emp)
        {

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(urlAnInternalApi, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction(responseMessage.IsSuccessStatusCode.ToString());
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlAnInternalApi + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employee = JsonConvert.DeserializeObject<infoperso>(responseData);

                return View(Employee);
            }
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, infoperso Emp)
        {

            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlAnInternalApi + "/" + id, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlAnInternalApi + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employee = JsonConvert.DeserializeObject<infoperso>(responseData);

                return View(Employee);
            }
            return View("Error");
        }

        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, infoperso Emp)
        {

            HttpResponseMessage responseMessage = await client.DeleteAsync(urlAnInternalApi + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

    }
}