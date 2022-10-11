using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebGUI.Models;

namespace WebGUI.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string adminUsername = "admin";
        private static readonly string adminPassword = "adminpass";
        private static bool isAdmin = false;

        public IActionResult Index()
        {
            ViewBag.Title = "Home";

            return View();
        }

        [HttpGet]
        public IActionResult AdminLogin(string username, string password)
        {
            if (username.Equals(adminUsername) && password.Equals(adminPassword))
            {
                isAdmin = true;
                return Ok(isAdmin);
            }
            else
            {
                isAdmin = false;
                return Unauthorized();
            }
        }

        [HttpPost]
        public IActionResult InsertCentre([FromBody] Centre centre)
        {
            if(!isAdmin) // Only admins can insert a centre
            {
                return Unauthorized();
            }

            RestClient client = new RestClient("http://localhost:49961/");
            RestRequest request = new RestRequest("api/Centres", Method.Post);
            request.AddJsonBody(JsonConvert.SerializeObject(centre));
            RestResponse response = client.Execute(request);

            Centre returnCentre = JsonConvert.DeserializeObject<Centre>(response.Content);

            if(returnCentre != null)
            {
                return Ok(returnCentre);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult InsertBooking([FromBody] Booking newBooking)
        {
            if(isAdmin) // Only users can insert a booking
            {
                return Unauthorized();
            }
            else if(newBooking.StartDate < DateTime.Now.Date) // New booking start date cannot be before current date
            {
                return BadRequest();
            }
            else if(newBooking.EndDate < newBooking.StartDate) // New booking start date is after end date
            {
                return BadRequest();
            }

            RestClient client = new RestClient("http://localhost:49961/");
            RestRequest getRequest = new RestRequest("api/Bookings", Method.Get);
            RestResponse getResponse = client.Execute(getRequest);

            List<Booking> bookings = JsonConvert.DeserializeObject<List<Booking>>(getResponse.Content);

            if (bookings != null)
            {
                foreach (Booking booking in bookings)
                {
                    if(booking.CentreName.Equals(newBooking.CentreName))
                    {
                        if((newBooking.StartDate >= booking.StartDate && newBooking.StartDate <= booking.EndDate) ||
                           (newBooking.EndDate >= booking.StartDate && newBooking.EndDate <= booking.EndDate))
                        {
                            return BadRequest();
                        }
                    }
                }
            }

            RestRequest postRequest = new RestRequest("api/Bookings", Method.Post);
            postRequest.AddJsonBody(JsonConvert.SerializeObject(newBooking));
            RestResponse postResponse = client.Execute(postRequest);

            Booking returnBooking = JsonConvert.DeserializeObject<Booking>(postResponse.Content);

            if(returnBooking != null)
            {
                return Ok(returnBooking);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult SearchCentres(string centreName)
        {
            if (isAdmin) // Only users can search centres
            {
                return Unauthorized();
            }

            RestClient client = new RestClient("http://localhost:49961/");
            RestRequest request = new RestRequest("api/Centres", Method.Get);
            RestResponse response = client.Execute(request);

            List<Centre> centres = JsonConvert.DeserializeObject<List<Centre>>(response.Content);

            if(centres == null)
            {
                return BadRequest();
            }
            else
            {
                List<Centre> matchingCentres = new List<Centre>();

                foreach (Centre centre in centres)
                {
                    if (centre.CentreName.Contains(centreName))
                    {
                        matchingCentres.Add(centre);
                    }
                }

                return View(matchingCentres);
            }
        }

        public IActionResult AllCentresView()
        {
            RestClient client = new RestClient("http://localhost:49961/");
            RestRequest request = new RestRequest("api/Centres", Method.Get);
            RestResponse response = client.Execute(request);

            List<Centre> centres = JsonConvert.DeserializeObject<List<Centre>>(response.Content);

            return View(centres);
        }

        public IActionResult InsertCentreView()
        {
            return View();
        }

        public IActionResult AdminLoginView()
        {
            return View();
        }

        public IActionResult InsertBookingView()
        {
            return View();
        }

        public IActionResult SearchCentresView()
        {
            return View();
        }
    }
}
