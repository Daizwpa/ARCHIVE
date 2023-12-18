// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.IO;

namespace Arch.Models
{
    public  class AppConfig
    {
        private static AppConfig obj;

        public  string ConfigurationsFolder { get; set; }

        public  string AppPropertiesFileName { get; set; }

        public string DatabaseFilePath { 
            get {
                
                
                string CommonDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string patharchive = Path.Combine(CommonDir, @"archive");
                if (!Directory.Exists(patharchive)) { Directory.CreateDirectory(patharchive); }

                return patharchive;
            } 
            private set{
            
            }
        }

        private AppConfig() { 
        
        
        }

        public static AppConfig GetInstanse()
        {
            return (obj == null) ? new AppConfig() : obj;
        }


        
    }
}
