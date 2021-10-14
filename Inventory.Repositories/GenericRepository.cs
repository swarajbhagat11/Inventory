using Inventory.Core;
using Inventory.Infrastructure;
using Inventory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private InventoryContext _context = null;
        private DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new InventoryContext();
            table = _context.Set<T>();
        }
        public GenericRepository(InventoryContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table;
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public IEnumerable<T> Find(Func<T, bool> filter = null)
        {
            if (filter == null) { return table; }
            return table.Where(filter);
        }
        public void Insert(T obj)
        {
            Utility.updateProp(obj, "updatedOn", DateTime.Now);
            Utility.updateProp(obj, "createdOn", DateTime.Now);
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            Utility.updateProp(obj, "updatedOn", DateTime.Now);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
