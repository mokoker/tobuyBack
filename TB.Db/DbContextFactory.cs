using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace TB.Db
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ToBuyContext>
    {
        public ToBuyContext CreateDbContext(string[] args)
        {
            Console.WriteLine("ok");
            var options = new DbContextOptionsBuilder<ToBuyContext>()
           .UseNpgsql("Server=localhost;Port=5432;Database=tobuy;User Id=postgres; Password=1234qqqQ;")
           .Options;
            return new ToBuyContext(options);
        }
    }
}
