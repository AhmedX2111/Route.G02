using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Route.G02.PL.Helpers
{
    public static class DocumentSettings
    {
        public static async Task<string> UploadFile(IFormFile file, string FolderName)
        {
            // 1. Get Located Folder path
            //  string folderPath = $"D:\\C# Sessions\\MVC\\Day3\\Route.G02\\Route.G02.PL\\wwwroot\\files\\images\\";
            // string FolderPath = $"{Directory.GetCurrentDirectory()}wwwroot\\files\\{FolderName}";

            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);

            if(Directory.Exists(FolderPath))
                 Directory.CreateDirectory(FolderPath);

            // 2. GetFile Name and MAke it Unique
            string FileName = $"{Guid.NewGuid()} {Path.GetExtension(file.FileName)}";

            // 3. Get File Path
            string FilePath = Path.Combine(FolderPath, FileName);

            // 4. Save File as Streams[Data per Time]
            using var fileStream = new FileStream(FilePath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return FileName;
        }

        public static void DeleteFile(string FileName, string FolderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName, FileName);
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}
