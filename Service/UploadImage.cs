using WeatherWise.Model;


namespace WeatherWise.Service
{
    public class UploadImage
    {
        public async Task<FileResult> OpenMediaPickerAsync()
        {
            try
            {
                //source learn.microsoft.com/mediapickerforphotosandvideos
                var selection = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select a photo"
                });

                if (selection != null)
                {
                    var fileType = selection.ContentType.ToLowerInvariant();
                    // Validate image formats
                    if (fileType.StartsWith("image/") &&
                        (fileType.EndsWith("png") || fileType.EndsWith("jpeg") || fileType.EndsWith("jpg")))
                    {
                        return selection;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Unsupported Image", "Select a PNG or JPEG image.", "OK");
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Media selection failed: {ex.Message}");
                return null;
            }
        }

        public async Task<Stream> FileResultToStream(FileResult fileResult)
        {
            if (fileResult == null)
                return null;

            return await fileResult.OpenReadAsync();
        }

        public Stream ByteArrayToStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public string ByteBase64ToString(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
        public byte[] StringToByteBase64(string text)
        {
            return Convert.FromBase64String(text);
        }
        //source learn.microsoft.com/File Picker
        public async Task<ImageFile> Upload(FileResult fileResult)
        {
            byte[] imageData;

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    var fileStream = await FileResultToStream(fileResult);
                    fileStream.CopyTo(memoryStream);
                    imageData = memoryStream.ToArray();
                }

                return new ImageFile
                {
                    byteBase64 = ByteBase64ToString(imageData),
                    ContentType = fileResult.ContentType,
                    FileName = fileResult.FileName
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Upload failed: {ex.Message}");
                return null;
            }
        }
    }
}