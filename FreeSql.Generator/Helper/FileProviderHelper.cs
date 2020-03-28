using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AngularGenerator.Helper
{
    public class FileProviderHelper
    {
        /// <summary>
        /// 获取盘符
        /// </summary>
        /// <returns></returns>
        public async Task<List<Dir>> GetDriveInfos()
        {
            var dirs = DriveInfo.GetDrives().Select(x => new Dir(x.Name, "")).ToList();
            foreach (var d in dirs)
            {
                d.Children = (await GetPathExsitDir(d.Key)).ToList();
            }
            return dirs;
        }
        /// <summary>
        /// 获取路径下所有文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Task<List<Dir>> GetPathExsitDir(string path)
        {
            var provider = new PhysicalFileProvider(path);
            var ppath = provider.GetDirectoryContents(string.Empty).ToList()
                .Where(x => x.IsDirectory)
                .Select(x => new Dir(x.PhysicalPath, path)).ToList();
            return Task.FromResult(ppath);
        }
        public Task<List<Dir>> GetPathExsitCshtml(string path, string endwith)
        {
            var provider = new PhysicalFileProvider(path);
            var ppath = provider.GetDirectoryContents(string.Empty).ToList()
                .Where(x => x.IsDirectory || x.Name.EndsWith($".{endwith}"))
                .Select(x => new Dir(x.PhysicalPath, path)).ToList();
            return Task.FromResult(ppath);
        }

        public Task<ImportData> GetCshtml(string path)
        {
            return Task.FromResult(new ImportData(File.ReadAllText(path)));
        }

    }
    public class Dir
    {
        public Dir(string key, string oldPath)
        {
            this.Key = key;
            this.OldPath = oldPath;
        }
        private string OldPath;
        public string Key { get; set; }
        public string Value => this.Key;
        public string Title => string.IsNullOrWhiteSpace(OldPath) ? this.Key : Key.Replace(OldPath, "");
        public List<Dir> Children { get; set; } = new List<Dir>();
    }
    public class ImportData
    {
        public ImportData(string fileContent)
        {
            this.FileContent = fileContent;
        }
        public string FileContent { get; set; }
    }
}
