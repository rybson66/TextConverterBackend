using System;
using System.Collections.Generic;
using System.Linq;
using TextConverterBackend.Domain.Models;

namespace TextConverterBackend.Application.Converter.Services
{
    public static class TextModelBuilder
    {
        static char[] sentenceSeparators = new char[] { '.', '?', '!' };

        public static Text Build(string textToParse)
        {
            Text text = new Text();

            List<string> rawSentences = new List<string>(textToParse.Trim().Split(sentenceSeparators, StringSplitOptions.RemoveEmptyEntries));

            rawSentences.ForEach(rawSentence =>
            {
                Sentence sentence = new Sentence();

                var splitters = rawSentence
                    .Where(Char.IsPunctuation)
                    .Distinct()
                    .ToArray();

                sentence.Words = rawSentence
                    .Trim()
                    .Split()
                    .Select(str => new Word { Value = str.Trim(splitters) })
                    .Where(w => !string.IsNullOrWhiteSpace(w.Value))
                    .OrderBy(w => w.Value)
                    .ToList();

                text.Sentences.Add(sentence);
            });

            return text;
        }
    }
}
