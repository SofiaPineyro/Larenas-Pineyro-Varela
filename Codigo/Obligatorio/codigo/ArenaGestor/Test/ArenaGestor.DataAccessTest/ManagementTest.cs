using ArenaGestor.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;

namespace ArenaGestor.DataAccessTest
{
    public abstract class ManagementTest
    {
        public DbContext CreateDbContext()
        {
            var dbName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<ArenaGestorContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new ArenaGestorContext(options);
        }
    }
}
