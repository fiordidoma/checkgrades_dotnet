using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OfficeOpenXml;
using OfficeOpenXml.Table;

using System.IO;

namespace checkgrades.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
        public IActionResult OnGetDownloadFile()
    {
        var students = new[]
        {
            new { StudentId = 1, Name = "John Doe", Grade = "A" },
            new { StudentId = 2, Name = "Jane Doe", Grade = "B" }
        };

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("StudentData");

            worksheet.Cells.LoadFromCollection(students, true, TableStyles.None);

            var stream = new MemoryStream(package.GetAsByteArray());

            return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "student_data.xlsx"
            };
        }
    }
    public class DownloadController : Controller
{
    public IActionResult DownloadFile()
    {
        // Your logic to generate or retrieve the file content
        byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes("Hello, this is a sample file content.");

        // Return a FileResult
        return File(fileBytes, "text/plain", "example.txt");
    }
}
}
