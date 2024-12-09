using Microsoft.AspNetCore.Mvc;
using HealthTrack.Web.Models;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HealthTrack.Web.Controllers
{
    public class HealthRecordController : Controller
    {
        private readonly HttpClient _httpClient;

        public HealthRecordController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var records = await _httpClient.GetFromJsonAsync<List<HealthRecord>>("http://localhost:5000/api/HealthRecord/user/1");
            return View(records);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var record = await _httpClient.GetFromJsonAsync<HealthRecord>($"http://localhost:5000/api/HealthRecord/{id}");
            if (record == null)
            {
                return NotFound();
            }
            return View(record);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HealthRecord record)
        {
            if (!ModelState.IsValid) return View(record);

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/HealthRecord/add", record);
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Health record added successfully!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to add health record.");
            return View(record);
        }

        // Edit Health Record
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var record = await _httpClient.GetFromJsonAsync<HealthRecord>($"http://localhost:5000/api/HealthRecord/{id}");
            if (record == null)
            {
                return NotFound();
            }
            return View(record);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HealthRecord record)
        {
            if (!ModelState.IsValid) return View(record);

            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5000/api/HealthRecord/{record.Id}", record);
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Health record updated successfully!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to update the health record.");
            return View(record);
        }

        // Delete Health Record
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _httpClient.GetFromJsonAsync<HealthRecord>($"http://localhost:5000/api/HealthRecord/{id}");
            if (record == null)
            {
                return NotFound();
            }
            return View(record);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5000/api/HealthRecord/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Health record deleted successfully!";
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
