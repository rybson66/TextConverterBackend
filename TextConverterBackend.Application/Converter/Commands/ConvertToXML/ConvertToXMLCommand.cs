using MediatR;

namespace TextConverterBackend.Application.Converter.Commands.ConvertToXML
{
    public class ConvertToXMLCommand : IRequest
    {
        public string Text { get; set; }
        public string FilePath { get; set; }
    }
}
