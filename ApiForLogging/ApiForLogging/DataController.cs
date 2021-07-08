using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ApiForLogging
{
    public class DataController
    {
        private apiforloggingdbContext dbContext;

        public int RecordsCount => dbContext.Records.Count();

        public DataController(apiforloggingdbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int RecordsByLogCount(string logLevel)
        {
            return dbContext.Records.Count(x => x.Loglevel.Equals(logLevel));
        }
        
        public int RecordsBySourceCount(string source)
        {
            return dbContext.Records.Count(x => x.Source.Equals(source));
        }

        public int AddRecord(DateTime? date, string text, string loglevel, string source)
        {
            Record rec = new Record(date, text, loglevel, source);
            dbContext.Records.Add(rec);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
            }
            finally
            {
                dbContext.Entry(rec).State = EntityState.Detached;
            }

            Console.WriteLine("New entry has been created: " + rec);
            return rec.Id;
        }

        public List<Record> FindByData(DateTime dataFrom, DateTime dataTo)
        {
            var recs = dbContext.Records.ToList().Where(x => x.Date >= dataFrom & x.Date <= dataTo).ToList();
            foreach (var rec in recs)
            {
                // Console.WriteLine(rec);
                dbContext.Entry(rec).State = EntityState.Detached;
            }

            return recs;
        }

        public List<Record> FindByLog(string logLevel)
        {
            var recs = dbContext.Records.ToList().Where(x => x.Loglevel.Equals(logLevel)).ToList();
            foreach (var rec in recs)
            {
                // Console.WriteLine(rec);
                dbContext.Entry(rec).State = EntityState.Detached;
            }

            return recs;
        }

        public List<Record> FindByTextOrSource(string textOrSourceMayContains)
        {
            var recs = dbContext.Records.ToList().Where(x => x.Text.Contains(textOrSourceMayContains) | x.Source.Contains(textOrSourceMayContains)).ToList();
            foreach (var rec in recs)
            {
                // Console.WriteLine(rec);
                dbContext.Entry(rec).State = EntityState.Detached;
            }

            return recs;
        }

        public int DeleteRecords(params int[] IDs)
        {
            int count = 0;
            
            foreach (var id in IDs)
            {
                var rec = dbContext.Records.FirstOrDefault(x => x.Id == id);
                try
                {
                    dbContext.Records.Remove(rec);
                    count += dbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Выбранный индекс [{id}] не существует");
                }
                finally
                {
                    dbContext.Entry(rec).State = EntityState.Detached;
                }
            }
            
            return count;
        }
    }
}