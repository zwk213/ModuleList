using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FileHelper.Model;
using File = FileHelper.Model.File;

namespace FileHelper
{
    public static class ExplorerHelper
    {

        public static Folder VisitFolder(string path, string search = null)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            Folder folder = new Folder
            {
                Name = info.Name,
                Path = path,
                Files = info.GetFileArray(search),
                Folders = info.GetFolderArray(search)
            };
            return folder;
        }

        /// <summary>
        /// 搜索指定路径下的全部信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static string[] GetFileSystemEntries(string path, string search)
        {
            string[] result = Directory.GetFileSystemEntries(path, search, SearchOption.AllDirectories);
            return result;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <param name="path"></param>
        public static void CreateFile(byte[] fileBytes, string path)
        {
            var temp = new FileStream(path, FileMode.Create);
            temp.Write(fileBytes, 0, fileBytes.Length);
            temp.Dispose();
            temp.Close();
        }

        /// <summary>
        ///将文件或目录及其内容移到新位置
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public static void Move(string source, string dest)
        {
            Directory.Move(source, dest);
        }

        #region 私有方法

        private static File[] GetFileArray(this DirectoryInfo info, string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return info.GetFiles().Select(p => new File()
                {
                    Name = p.Name,
                    Extension = p.Extension,
                    Size = p.Length / 1024,
                    Path = p.FullName
                }).ToArray();
            }
            return info.GetFiles(search, SearchOption.AllDirectories).Select(p => new File()
            {
                Name = p.Name,
                Extension = p.Extension,
                Size = p.Length / 1024,
                Path = p.FullName
            }).ToArray();
        }

        private static Folder[] GetFolderArray(this DirectoryInfo info, string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return info.GetDirectories().Select(p => new Folder()
                {
                    Name = p.Name,
                    Path = p.FullName
                }).ToArray();
            }
            return info.GetDirectories(search, SearchOption.AllDirectories).Select(p => new Folder()
            {
                Name = p.Name,
                Path = p.FullName
            }).ToArray();
        }

        #endregion

    }
}
