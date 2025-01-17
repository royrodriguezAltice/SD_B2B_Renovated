using Microsoft.AspNetCore.Http;
using SD.Domain.Enums.Common.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Interfaces.Helpers.Files
{
    public interface IFilesManager
    {
        //Task<string> FilesNaming(string fileName, string id, string contentType, FilesTypes fileType);
        Task<bool> FileSaving(List<IFormFile> files, string id,  FilesTypes fileType);
    }
}
