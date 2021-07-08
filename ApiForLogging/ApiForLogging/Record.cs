using System;
using System.Collections.Generic;

#nullable disable

namespace ApiForLogging
{
    public partial class Record
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Text { get; set; }
        public string Loglevel { get; set; }
        public string Source { get; set; }

        public Record() { }

        public Record(DateTime? date, string text, string loglevel, string source)
        {
            Date = date;
            Text = text;
            Loglevel = loglevel;
            Source = source;
        }

        public override string ToString()
        {
            return $"id = {Id}, date = {Date}, text = {Text}, logLevel = {Loglevel}, source = {Source}";
        }
    }
}
