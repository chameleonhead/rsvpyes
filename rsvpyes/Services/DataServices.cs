using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using rsvpyes.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace rsvpyes.Services
{
    public interface IRsvpDataService
    {
        Task<T> Update<T>(T obj) where T : class;
        Task<T> Insert<T>(T obj) where T : class;
        Task<T> Remove<T>(T obj) where T : class;
        Task<IEnumerable<T>> Where<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> Find<T>(params object[] keyValues) where T : class;
    }

    public interface IDataService<T>
         where T : class
    {
        Task<T> Update(T obj);
        Task<T> Insert(T obj);
        Task<T> Remove(T obj);
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate);
        Task<T> Find(params object[] id);
    }

    sealed class RsvpDataService : IRsvpDataService
    {
        private BlockingCollection<Action<RsvpContext>> taskQueue;

        public RsvpDataService(IConfiguration configuration)
        {
            var option = new DbContextOptionsBuilder<RsvpContext>()
                        .UseSqlite(configuration.GetConnectionString("RsvpDatabase"))
                        .Options;

            using (var db = new RsvpContext(option))
            {
                db.Database.EnsureCreated();
            }

            taskQueue = new BlockingCollection<Action<RsvpContext>>();
            Task.Run(() =>
            {
                while (true)
                {
                    using (var db = new RsvpContext(option))
                    {
                        var action = taskQueue.Take();
                        action(db);
                    }
                }
            });
        }

        private Task<T> DbWork<T>(Func<RsvpContext, T> func) where T : class
        {
            var tcs = new TaskCompletionSource<T>();
            Task.Run(() => taskQueue.Add(db =>
            {
                try
                {
                    tcs.SetResult(func(db));
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }));
            return tcs.Task;
        }

        public Task<T> Insert<T>(T obj) where T : class
        {
            return DbWork(db =>
            {
                db.Set<T>().Add(obj);
                db.SaveChanges();
                return obj;
            });
        }

        public Task<T> Remove<T>(T obj) where T : class
        {
            return DbWork(db =>
            {
                db.Set<T>().Remove(obj);
                db.SaveChanges();
                return obj;
            });
        }

        public Task<T> Update<T>(T obj) where T : class
        {
            return DbWork(db =>
            {
                try
                {
                    db.Set<T>().Update(obj);
                }
                catch (InvalidOperationException)
                {
                    db.Entry(obj).CurrentValues.SetValues(obj);
                    db.Set<T>().Update(obj);
                }

                db.SaveChanges();
                return obj;
            });
        }

        public Task<IEnumerable<T>> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return DbWork<IEnumerable<T>>(db =>
            {
                var e = db.Set<T>().Where(predicate);
                return e.ToList();
            });
        }

        public Task<T> Find<T>(params object[] keyValues) where T : class
        {
            return DbWork(db =>
            {
                return db.Set<T>().Find(keyValues);
            });
        }
    }

    sealed class DataService<T> : IDataService<T>
        where T : class
    {
        private IRsvpDataService service;
        public DataService(IRsvpDataService service)
        {
            this.service = service;
        }

        public Task<T> Insert(T obj)
        {
            return service.Insert(obj);
        }

        public Task<T> Remove(T obj)
        {
            return service.Remove(obj);
        }

        public Task<T> Update(T obj)
        {
            return service.Update(obj);
        }

        public Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate)
        {
            return service.Where(predicate);
        }

        public Task<T> Find(params object[] keyValues)
        {
            return service.Find<T>(keyValues);
        }
    }
}
