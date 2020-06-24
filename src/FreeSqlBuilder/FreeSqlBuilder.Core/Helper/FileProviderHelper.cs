using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace FreeSqlBuilder.Core.Helper
{
    /// <summary>
    /// 文件提供助手
    /// </summary>
    public class FileProviderHelper
    {

        private string DefaultRazorTemplate = "RazorTemplate";

        private readonly IWebHostEnvironment webEnv;
        private readonly IFreeSql<FsBuilder> _freeSql;
        private readonly TemplateOptions _options;
        /// <summary>
        /// 文件提供助手
        /// </summary>
        /// <param name="_webEnv"></param>
        public FileProviderHelper(IWebHostEnvironment _webEnv, IFreeSql<FsBuilder> freeSql, TemplateOptions options)
        {

            webEnv = _webEnv;
            _freeSql = freeSql;
            _options = options;
        }
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
        /// <summary>
        /// cshtml
        /// </summary>
        /// <param name="path"></param>
        /// <param name="endwith"></param>
        /// <returns></returns>
        public Task<List<Dir>> GetPathExsitCshtml(string path, string endwith)
        {
            var provider = new PhysicalFileProvider(path);
            var ppath = provider.GetDirectoryContents(string.Empty).ToList()
                .Where(x => x.IsDirectory || x.Name.EndsWith($".{endwith}"))
                .Select(x => new Dir(x.PhysicalPath, path)).ToList();
            return Task.FromResult(ppath);
        }
        /// <summary>
        /// 获取cshtml
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Task<ImportData> GetCshtml(string path)
        {
            return Task.FromResult(new ImportData(File.ReadAllText(path)));
        }

        /// <summary>
        /// 复制模板文件到项目目录
        /// </summary>
        public void CopyToProjectRoot(Type type)
        {
            var outPutPath = Path.Combine(webEnv.ContentRootPath, _options.DefaultTemplatePath);//目标
            CopyFolderFromStream(type, outPutPath);
        }

        private void CopyFolderFromStream(Type type, string dest)
        {
            var fileProvider = new EmbeddedFileProvider(Assembly.GetAssembly(type));
            var res = fileProvider.GetDirectoryContents("").ToList();
            foreach (IFileInfo file in res)
            {
                var path = file.Name.Split(".").SkipLast(2).ToList();
                var fileName = file.Name.Replace(string.Join(".", path), "").TrimStart('.');
                string destName = Path.Combine(dest);
                path.ForEach(fp =>
                {
                    if (fp != DefaultRazorTemplate)
                    {
                        destName = Path.Combine(destName, fp);
                    }
                });
                var dirPath = destName;
                destName = Path.Combine(destName, fileName);
                if (!file.IsDirectory)
                {
                    if (!File.Exists(destName))
                    {
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }
                        var fileString = new StreamReader(file.CreateReadStream()).ReadToEnd();
                        File.WriteAllText(destName, fileString, Encoding.UTF8);
                    }
                }
                else
                {
                    Directory.CreateDirectory(destName);
                }

            }
        }



        public void InitTemplate()
        {
            var outPutPath = Path.Combine(webEnv.ContentRootPath, _options.DefaultTemplatePath);//目标
            ImportTemplate(outPutPath);

        }
        private void ImportTemplate(string outPutPath)
        {
            var allTemplate = _freeSql.Select<Template>().ToList();
            DirectoryInfo dinfo = new DirectoryInfo(outPutPath);
            //注，这里面传的是路径，并不是文件，所以不能包含带后缀的文件                
            foreach (FileSystemInfo f in dinfo.GetFileSystemInfos())
            {
                if (f is FileInfo)
                {
                    using var stream = File.OpenText(f.FullName);
                    var content = stream.ReadToEnd();
                    var file = new Template
                    {
                        TemplateContent = content,
                        TemplateName = f.Name,
                        TemplatePath = f.FullName
                    };
                    var template = allTemplate.FirstOrDefault(x => x.TemplateName == file.TemplateName && x.TemplatePath == file.TemplatePath);
                    if (template != null)
                    {
                        if (template.TemplateContent.GetHashCode() == content.GetHashCode()) continue;
                        template.TemplateContent = content;
                        _freeSql.Update<Template>().SetSource(template).ExecuteAffrows();
                    }
                    else
                    {
                        _freeSql.Insert<Template>().AppendData(file).ExecuteInserted();
                    }
                }
                else
                {
                    ImportTemplate(f.FullName);
                }
            }
        }
        /// <summary>
        /// 复制源文件夹下所有文件到目标文件夹
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="dest"></param>
        private void CopyFolder(string sources, string dest)
        {
            DirectoryInfo dinfo = new DirectoryInfo(sources);
            //注，这里面传的是路径，并不是文件，所以不能包含带后缀的文件                
            foreach (FileSystemInfo f in dinfo.GetFileSystemInfos())
            {
                string destName = Path.Combine(dest, f.Name);
                if (f is FileInfo)
                {
                    //如果是文件就复制      
                    if (!File.Exists(destName))
                        File.Copy(f.FullName, destName, true);//true代表可以覆盖同名文件                     
                }
                else
                {
                    //如果是文件夹就创建文件夹，然后递归复制              
                    Directory.CreateDirectory(destName);
                    CopyFolder(f.FullName, destName);
                }
            }
        }

    }
    public class Dir
    {
        public Dir(string key, string oldPath)
        {
            Key = key;
            OldPath = oldPath;
        }
        private string OldPath;
        public string Key { get; set; }
        public string Value => Key;
        public string Title => string.IsNullOrWhiteSpace(OldPath) ? Key : Key.Replace(OldPath, "");
        public List<Dir> Children { get; set; } = new List<Dir>();
    }
    public class ImportData
    {
        public ImportData(string fileContent)
        {
            FileContent = fileContent;
        }
        public string FileContent { get; set; }
    }
}
