using System.Collections.Generic;

namespace TextConverterBackend.Domain.Models
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
