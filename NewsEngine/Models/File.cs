using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsEngine.Models
{
    public class File
    {
        public int Id { get; set; }
        public String Ext { get; set; }
        public String DiskName
        {
            get
            {
                return String.Concat(Id, ".", Ext);
            }
        }
        public static String GetExt(String fileName)
        {
            return fileName.Substring(fileName.Length - 3, 3);
        }

    }
}