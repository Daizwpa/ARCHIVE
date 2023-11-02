using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Arch.Extensions
{
    public static class OperationExtension
    {
        public static Operation CopyObject(this Operation origial)
        {
            try
            {
                return new Operation() { Id= origial.Id, Amount = origial.Amount, Name = origial.Name, Type = origial.Type };
            }
            catch
            {
                return new Operation() { Amount = 0.00M, Name = "Empty", Type = "Empty" };
            }
        }
    }
}
