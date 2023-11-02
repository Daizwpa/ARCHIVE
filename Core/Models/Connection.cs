using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.Models
{
    public class Connection
    {
        public ContextDatabase Context { get; private set; }

        public Connection(String path = "warehouse.db")
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("The path does not fit");

            Context = new ContextDatabase(path);

            if (Context.Database.EnsureCreated())
            {
                Seed();
            }

        }

        private void Seed()
        {
            try
            {
                Context.operations.AddRange(
                    new Operation() { Amount = 387440, Name = "Project1", Type = "consultation" },
                    new Operation() { Amount = 20000, Name = "Project2", Type = "Demand" },
                    new Operation() { Amount = 34900, Name = "Project3", Type = "Demand" }
                );

                Context.SaveChanges();

            }
            catch
            {
                throw;
            }
        }
    }
}
