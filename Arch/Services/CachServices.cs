using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch.Services
{
    public class CachServices
    {
        public Operation? last_operation { get; set; } = null;

        public Record? last_record { get; set; } = null;
    }
}
