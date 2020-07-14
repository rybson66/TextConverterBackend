using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextConverterBackend.Application.Converter.Models;
using TextConverterBackend.Application.Converter.Services;

namespace TextConverterBackend.Application.Converter.Commands.ConvertToCSV
{
    public class ConvertToCSVCommandHandler : IRequestHandler<ConvertToCSVCommand>
    {
        private static char wordSeparator = ',';

        public Task<Unit> Handle(ConvertToCSVCommand request, CancellationToken cancellationToken)
        {
            using (var writer = new StreamWriter(request.FilePath))
            {
                Text text = TextModelBuilder.Build(request.Text);

                var maxWordsCount = text.Sentences
                    .Select(s => s.Words.Count)
                    .Max();

                string header = CreateHeader(maxWordsCount);
                string records = CreateRecords(text.Sentences);

                writer.WriteLine(header);
                writer.Write(records);
            }

            return Task.FromResult(Unit.Value);
        }

        private static string CreateHeader(int wordsCount)
        {
            var headerBuilder = new StringBuilder();

            headerBuilder.Append(wordSeparator);

            for (int wordNo = 1; wordNo <= wordsCount; ++wordNo)
            {
                headerBuilder.Append($"Word {wordNo}");

                if (wordNo != wordsCount)
                {
                    headerBuilder.Append(wordSeparator);
                }
            }

            return headerBuilder.ToString();
        }

        private static string CreateRecords(List<Sentence> sentences)
        {
            var recordBuilder = new StringBuilder();

            sentences.ForEach(sentence =>
            {
                recordBuilder.Append($"Sentence {sentences.IndexOf(sentence) + 1}{wordSeparator}");

                recordBuilder.AppendLine(string.Join(wordSeparator, sentence.Words.Select(s => s.Value)));
            });

            return recordBuilder.ToString();
        }
    }
}
