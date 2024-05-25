using System.Text;

namespace BLBConcordance.Core.Model
{
    public sealed class BibleReference
    {
        public BibleVerse FromVerse { get; set; }
        public BibleVerse ToVerse { get; set; }
        public string Reference
        {
            get
            {
                if (this.FromVerse == this.ToVerse)
                {
                    return this.FromVerse.Reference;
                }

                var builder = new StringBuilder();

                // if from and to are not within the same book
                if (this.FromVerse.Book != this.ToVerse.Book)
                {
                    return builder
                        .Append(this.FromVerse.Reference)
                        .Append(" - ")
                        .Append(this.ToVerse.Reference)
                        .ToString();
                }

                builder
                    .Append(this.FromVerse.Book.ToString())
                    .Append(' ');

                // if from and to are within the same book, chapter
                if (this.FromVerse.Chapter == this.ToVerse.Chapter)
                {
                    return builder
                        .Append(this.FromVerse.Chapter)
                        .Append(':')
                        .Append(this.FromVerse.Verse)
                        .Append('-')
                        .Append(this.ToVerse.Verse)
                        .ToString();
                }

                // if from and to are within the same book, different chapter
                return builder
                    .Append(this.FromVerse.Chapter)
                    .Append(':')
                    .Append(this.FromVerse.Verse)
                    .Append('-')
                    .Append(this.ToVerse.Chapter)
                    .Append(':')
                    .Append(this.ToVerse.Verse)
                    .ToString();
            }
        }

        public BibleReference(BibleVerse From, BibleVerse? To)
        {
            this.FromVerse = From;

            if (To is null)
            {
                this.ToVerse = this.FromVerse;
            }
            else
            {
                this.ToVerse = To;
            }
        }
    }
}