using Microsoft.AspNetCore.Mvc;
using HealthTrack.Web.Models;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace HealthTrack.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid) return View(user);

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/User/register", user);
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "User registered successfully!";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Failed to register user.");
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (!ModelState.IsValid) return View(user);

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/User/login", user);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                // Store the JWT token in a cookie/session or temporary data
                TempData["Token"] = token;
                return RedirectToAction("Index", "Home"); // Redirect to home page after login
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(user);
        }
    }
}
