using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StatisticsWebApp.Models;

namespace StatisticsWebApp.Controllers
{
	public class ChartController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ChartController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
            List<DataPoint> dataPoints = new List<DataPoint>();

            var latestStatistics = _context.Statistics
                .Include(stat => stat.File)
                .OrderByDescending(stat => stat.File.UploadDateTime)
                .ToList();

            var latestFileFormat = latestStatistics
                .Select(stat => stat.File)
                .OrderByDescending(x => x.Id == latestStatistics.FirstOrDefault().Id)
                .FirstOrDefault();

            var finalResultStatistics = latestStatistics
                .Where(stat => stat.File.Id == latestFileFormat.Id)
                .ToList();

            if (finalResultStatistics != null)
            {
                foreach (var stat in finalResultStatistics)
                {
                    dataPoints.Add(new DataPoint(stat.Color, stat.Number, stat.Label));
                }
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
	}
}
