using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Core.Files
{
    public class MoveFile
    {
        string filename { get; set; } = string.Empty;
        string newpath { get; set; }
        string oldpath { get; set; }
        


        private void Move(string filename, string newpath)
        {
            try
            {
                FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

                
                

            }catch(Exception ex)
            {

            }
        }



        
    }
}
