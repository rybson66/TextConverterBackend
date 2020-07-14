using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using TextConverterBackend.Application.Converter.Models;
using TextConverterBackend.Application.Converter.Services;

namespace TextConverterBackend.Application.Converter.Commands.ConvertToXML
{
    public class ConvertToXMLCommandHandler : IRequestHandler<ConvertToXMLCommand>
    {
        public Task<Unit> Handle(ConvertToXMLCommand request, CancellationToken cancellationToken)
        {
            using (XmlWriter writer = XmlWriter.Create(request.FilePath))
            {
                Text text = TextModelBuilder.Build(request.Text);

                writer.WriteStartDocument(true);
                writer.WriteStartElement("text");

                foreach (Sentence sentence in text.Sentences)
                {
                    writer.WriteStartElement("sentence");

                    foreach (Word word in sentence.Words)
                    {
                        writer.WriteElementString("word", word.Value);
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
