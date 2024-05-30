using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Store.Models;

namespace Store.ModelViews
{
    public class CreateFile
    {
        public async void FileCreat(string path, string fileName, IFormFile file)
        {
            var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}