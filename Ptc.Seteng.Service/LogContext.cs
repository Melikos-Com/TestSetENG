using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class LogContext : DbContext
    {
        public LogContext(string Context) : base(Context)
        {

        }

        public override int SaveChanges()
        {
            try
            {

                var trackerData = this.ChangeTracker
                                      .Entries()
                                      .ToLookup(x => x.State);

                RecordStatus(trackerData);

            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return base.SaveChanges();
        }

        public void RecordStatus(ILookup<EntityState, DbEntityEntry> data)
        {
            data.AsParallel().ForEach(x =>
            {
                IEnumerable<Tracks> novals = GetNovalValue(x);
                IEnumerable<Tracks> originals = GetOriginalValue(x);

                switch (x.Key)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Added:
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Modified:
                        break;
                    default:
                        break;
                }

            });
        }

        public IEnumerable<Tracks> GetNovalValue(IGrouping<EntityState, DbEntityEntry> items)
          => items.Select(x => new Tracks(x.CurrentValues, x.Entity.GetType().FullName)) ?? new List<Tracks>();

        public IEnumerable<Tracks> GetOriginalValue(IGrouping<EntityState, DbEntityEntry> items)
          => items.Select(x => new Tracks(x.OriginalValues, x.Entity.GetType().FullName)) ?? new List<Tracks>();

        public class Tracks
        {

            public Tracks(DbPropertyValues data, string tableName)
            {
                this.data = data;
                this.tableName = tableName;

            }

            public DbPropertyValues data { get; set; }

            public string tableName { get; set; }

        }
    }
}
