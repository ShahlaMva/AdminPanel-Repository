namespace AdminPanel.Helpers.Extensions
{
    public static class PhotoExtension
    {
        public static bool IsPhoto(this IFormFile formFile)
        {
            return formFile.ContentType.StartsWith("image/");
        }

        public static bool PhotoSize(this IFormFile formFile,int maxSize)
        {
            return formFile.Length / 1024 / 1024 < maxSize;
        }

        public async static Task<string>CopyPhoto(this IFormFile formFile,string root,string folder)
        {
            if (formFile == null || formFile.Length == 0)
            {
                throw new ArgumentException("It is null");
            }

            string extension = Path.GetExtension(formFile.FileName);
            string newFileName = $"{Guid.NewGuid()}{extension}";

            string path = Path.Combine(root, folder);
            if (!Directory.Exists(path)) 
            {
                Directory.CreateDirectory(path);
            }
            
            
            string resultPath=Path.Combine(path,newFileName);

            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            
            return $"{folder}/{newFileName}";

        }
    }
}
