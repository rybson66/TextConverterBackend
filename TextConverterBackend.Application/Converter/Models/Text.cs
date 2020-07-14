using System.Collections.Generic;

namespace TextConverterBackend.Application.Converter.Models
{
    public class Text
    {
        public Text()
        {
            Sentences = new List<Sentence>();
        }

        public List<Sentence> Sentences { get; set; }
    }
}
