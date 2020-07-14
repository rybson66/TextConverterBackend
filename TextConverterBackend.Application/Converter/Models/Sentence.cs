using System.Collections.Generic;

namespace TextConverterBackend.Application.Converter.Models
{
    public class Sentence
    {
        public Sentence()
        {
            Words = new List<Word>();
        }

        public List<Word> Words { get; set; }
    }
}
