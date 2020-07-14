using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using TextConverterBackend.Application.Converter.DTO;
using TextConverterBackend.Application.Converter.Commands.ConvertToCSV;
using TextConverterBackend.Application.Converter.Commands.ConvertToXML;

namespace TextConverterBackend.Controllers
{
    public class ConverterController : BaseController
    {
        private IWebHostEnvironment _hostingEnvironment;

        public ConverterController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        [HttpPost]
        public async Task<IActionResult> DownloadCSV([FromBody] TextToConvertDTO dto)
        {
            ConvertToCSVCommand command = new ConvertToCSVCommand
            {
                Text = dto.Text,
                FilePath = Path.Combine(_hostingEnvironment.ContentRootPath, $"text.csv")
            };

            await Mediator.Send(command);

            MemoryStream memoryStream = await CreateMemoryStreamFromFile(command.FilePath);

            DeleteFile(command.FilePath);

            return File(memoryStream, "application/octet-stream");
        }

        [HttpPost]
        public async Task<IActionResult> DownloadXML([FromBody] TextToConvertDTO dto)
        {
            ConvertToXMLCommand command = new ConvertToXMLCommand
            {
                Text = dto.Text,
                FilePath = Path.Combine(_hostingEnvironment.ContentRootPath, $"text.xml")
            };

            await Mediator.Send(command);

            MemoryStream memoryStream = await CreateMemoryStreamFromFile(command.FilePath);

            DeleteFile(command.FilePath);

            return File(memoryStream, "application/octet-stream");
        }

        private async Task<MemoryStream> CreateMemoryStreamFromFile(string filePath)
        {
            var memoryStream = new MemoryStream();

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }

            memoryStream.Position = 0;

            return memoryStream;
        }

        private void DeleteFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
