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
        public decimal OperationID;
        public string Name { get; set; } = string.Empty;
        public string  FilePath { get; set; } = string.Empty;
        public decimal Amound { get; set; } 
        public DateTime date { get; set; }

        public Operation operation { get; set; }
    }
}
