using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccess.Managements
{
    public class SnacksManagement : ISnacksManagement
    {
        private readonly DbSet<Snack> snacks;
        private readonly DbContext context;

        public SnacksManagement(DbContext context)
        {
            this.snacks = context.Set<Snack>();
            this.context = context;
        }

        public void DeleteSnack(Snack snack)
        {
            snacks.Remove(snack);
        }

        public bool ExistsSnack(Func<Snack, bool> filter)
        {
            return snacks.Where(filter).Any();
        }

        public IEnumerable<Snack> GetSnacks(Func<Snack, bool> filter)
        {
            return snacks.Where(filter).OrderBy(x => x.Id);
        }

        public void InsertSnack(Snack snack)
        {
            snacks.Add(snack);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
