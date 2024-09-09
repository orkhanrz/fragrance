using fragrance.Models;
using System;

namespace fragrance.Services
{
	public class FileService
	{
        private static YourDbContext db = new YourDbContext();

        public static async Task<string> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return null;
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/uploads/" + fileName;
        }

        public static async Task UploadFragranceImages(int fragranceId, List<IFormFile> images)
        {
            foreach (var image in images)
            {
                var imageUrl = await UploadImage(image);

                if (imageUrl != null)
                {
                    var fragranceImage = new FragranceImage
                    {
                        FragranceId = fragranceId,
                        FragranceImageUrl = imageUrl
                    };

                    db.FragranceImages.Add(fragranceImage);
                }
            }

            db.SaveChanges();
        }

        public static void DeleteImages(List<FragranceImage> fragranceImages)
        {
            fragranceImages.ForEach(image =>
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.FragranceImageUrl.TrimStart('/'));

                if (System.IO.File.Exists(oldImagePath))
                {
                     System.IO.File.Delete(oldImagePath);
                }
                else
                {
                    Console.WriteLine("File does not exist at path: " + oldImagePath);
                }
            });
        }
    }
}

