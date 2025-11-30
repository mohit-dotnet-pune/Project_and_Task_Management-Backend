using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Project___Task_Management_Backend.DTO.CloudinaryDtos;


namespace Project___Task_Management_Backend.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<UploadResponse> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using var stream = file.OpenReadStream();

            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "projectAndTaskManagement"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
                throw new Exception(result.Error.Message);

            return new UploadResponse
            {
                FileName = file.FileName,
                FileUrl = result.SecureUrl?.ToString(),
                PublicId = result.PublicId
            };
        }




        public async Task<DeletionResult> DeleteFileAsync(string publicId, string resourceType = "raw")
        {
            var deleteParams = new DeletionParams(publicId)
            {
                ResourceType = resourceType.ToLower() switch
                {
                    "image" => ResourceType.Image,
                    "video" => ResourceType.Video,
                    "raw" => ResourceType.Raw,
                    "auto" => ResourceType.Auto,
                    _ => ResourceType.Raw
                }
            };

            var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

            return deleteResult;
        }

    }

}
