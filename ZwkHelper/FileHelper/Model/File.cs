using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelper.Model
{
    public class File
    {
        public string Name { get; set; }
        public string Extension { get; set; }

        /// <summary>
        /// kb
        /// </summary>
        public long Size { get; set; }

        public string Path { get; set; }
    }
}
