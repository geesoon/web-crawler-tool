using System.Text;
using EnsureThat;

namespace BLBConcordance.Core.Model
{
    public sealed class BibleVerse
    {
        public string Reference
        {
            get
            {
                return new StringBuilder()
                    .Append(this.Book.ToString())
                    .Append(' ')
                    .Append(this.Chapter)
                    .Append(':')
                    .Append(this.Verse)
                    .ToString();
            }
        }

        public BibleBooks Book { get; set; }
        public int Chapter { get; set; }
        public int Verse { get; set; }
        public string Text { get; set; }

        public BibleVerse(string reference, string text)
        {
            EnsureArg.IsNotNull(reference, nameof(reference));
            this.Text = EnsureArg.IsNotNullOrEmpty(text, nameof(text));
            try
            {
                var spaceIndex = reference.LastIndexOf(' ');
                if (spaceIndex == -1)
                {
                    throw new ArgumentException("The reference must contain a space separating the book name and the chapter/verse part.");
                }

                var bookPart = reference.Substring(0, spaceIndex);
                var chapterVersePart = reference.Substring(spaceIndex + 1);

                if (!Enum.TryParse(bookPart.Replace(" ", ""), true, out BibleBooks book))
                {
                    throw new ArgumentException("Not able to parse book reference part into proper BibleBooks.");
                }

                try
                {
                    this.Book = BibleBooksExtensions.GetEnumFromAbbreviation(book.ToString());
                }
                catch (ArgumentException)
                {
                    this.Book = book;
                }

                var chapterVerseParts = chapterVersePart.Split(':');
                if (chapterVerseParts.Length != 2 ||
                    !int.TryParse(chapterVerseParts[0], out var chapter) ||
                    !int.TryParse(chapterVerseParts[1], out var verse))
                {
                    throw new ArgumentException("Not able to parse chapter or verse reference part into integers.");
                }

                var maxChapters = BibleStructure.BookStructure[this.Book].chapters;
                if (chapter < 1 || chapter > maxChapters)
                {
                    throw new ArgumentException($"Invalid chapter number. {this.Book} has {maxChapters} chapters.");
                }

                var maxVerses = BibleStructure.BookStructure[this.Book].versesPerChapter[chapter - 1];
                if (verse < 1 || verse > maxVerses)
                {
                    throw new ArgumentException($"Invalid verse number. Chapter {chapter} of {this.Book} has {maxVerses} verses.");
                }

                this.Chapter = chapter;
                this.Verse = verse;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"The bible reference is not properly formatted. Reason: {ex.Message}");
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(this.Reference)
                .Append(" - ")
                .Append(this.Text)
                .ToString();
        }
    }
}