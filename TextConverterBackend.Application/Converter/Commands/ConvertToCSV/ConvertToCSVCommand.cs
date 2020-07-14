using MediatR;

namespace TextConverterBackend.Application.Converter.Commands.ConvertToCSV
{
    public class ConvertToCSVCommand : IRequest
    {
        public string Text { get; set; }

        public string FilePath { get; set; }
    }
}
