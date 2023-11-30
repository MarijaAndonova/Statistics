using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatisticsWebApp.Models;

namespace StatisticsWebApp.Controllers
{
    public class FileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0 && file.ContentType == "text/plain")
            {

                var fileModel = new FileModel
                {
                    FileName = file.FileName,
                    LinesCount = 0,
                    UploadDateTime = DateTime.Now
                };

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        string[] data = line.Split(',');
                        if (data.Length == 3)
                        {
                            var statistic = new Statistic
                            {
                                Color = data[0].Trim(),
                                Number = int.Parse(data[1].Trim()),
                                Label = data[2].Trim(),
                                File = fileModel
                            };

                            _context.Statistics.Add(statistic);
                            fileModel.LinesCount++; 
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Invalid format in the file.";
                            ModelState.AddModelError("", "Invalid format in the file.");
                            return View("UploadFile");
                        }
                    }
                }
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    var innerException = ex.InnerException;
                    throw; 
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Please select a valid .txt file.";
                ModelState.AddModelError("", "Please select a valid .txt file.");
                return View("UploadFile");
            }

            return RedirectToAction("Statistics");
        }

        public IActionResult File()
        {
            var filesInfo = _context.Statistics
                .Select(s => new FileModel
                {
                    FileName = s.File.FileName,
                    LinesCount = s.File.LinesCount,
                    UploadDateTime = s.File.UploadDateTime
                })
                .Distinct()
                .OrderByDescending(s => s.UploadDateTime)
                .ToList();

            return View(filesInfo);
        }

        public IActionResult Statistics()
        {
            var statistics = _context.Statistics
                .Include(stat => stat.File)
                .ToList(); 

            return View(statistics);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var statistic = await _context.Statistics.FindAsync(id);

            if (statistic != null)
            {
                _context.Statistics.Remove(statistic);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Statistics");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string color, int number, string label)
        {
            var statisticToUpdate = await _context.Statistics.FindAsync(id);

            if (statisticToUpdate != null)
            {
                statisticToUpdate.Color = color;
                statisticToUpdate.Number = number;
                statisticToUpdate.Label = label;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Statistics");
        }
    }
}
