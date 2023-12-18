using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string  FilePath { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime date { get; set; } = DateTime.Now;

        public Operation operation { get; set; } = new Operation();
    }
}
