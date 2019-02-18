using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileHelper.Model
{
    public class Folder
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public File[] Files { get; set; }

        public Folder[] Folders { get; set; }

    }
}
