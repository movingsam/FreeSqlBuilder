﻿using FreeSql.Generator.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FreeSql.Generator.Helper
{
    /// <summary>
    /// 文件提供助手
    /// </summary>
    public class FileProviderHelper
    {

        private string DefaultRazorTemplate = "RazorTemplate";

        private readonly IWebHostEnvironment webEnv;
        private readonly IFreeSql<FsGen> _freeSql;
        private readonly GenTemplateOptions _options;
        /// <summary>
        /// 文件提供助手
        /// </summary>
        /// <param name="_webEnv"></param>
        public FileProviderHelper(IWebHostEnvironment _webEnv, IFreeSql<FsGen> freeSql, GenTemplateOptions options)
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
        public void ClearTemplate()
        {
            var all = this._freeSql.Select<Template>().ToList(x => x.Id);
            this._freeSql.Delete<Template>().Where(x => all.Contains(x.Id)).ExecuteAffrows();
        }
        /// <summary>
        /// 复制模板文件到项目目录
        /// </summary>
        public void CopyToProjectRoot()
        {
            var binPath = AppContext.BaseDirectory;//bin
            var sourcePath = Path.Combine(binPath, DefaultRazorTemplate);//源
            var outPutPath = Path.Combine(webEnv.ContentRootPath, _options.DefaultTemplatePath);//目标
            if (!Directory.Exists(sourcePath))
            {
                Directory.CreateDirectory(sourcePath);
            }
            CopyFolder(sourcePath, outPutPath);
        }

        public void InitTemplate()
        {
            var outPutPath = Path.Combine(webEnv.ContentRootPath, _options.DefaultTemplatePath);//目标
            ImportTemplate(outPutPath);

        }
        private void ImportTemplate(string outPutPath)
        {
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
                    var template = _freeSql.Select<Template>().Where(x => x.TemplateName == file.TemplateName && x.TemplatePath == file.TemplatePath).ToOne();
                    if (template != null)
                    {
                        if (template.TemplateContent != content)
                        {
                            template.TemplateContent = content;
                            _freeSql.Update<Template>().SetSource(template).ExecuteAffrows();
                        }
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