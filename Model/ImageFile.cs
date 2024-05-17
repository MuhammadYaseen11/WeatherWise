namespace WeatherWise.Model
{
    // Define a public class named ImageFile
    public class ImageFile
    {
        // Define a public property named byteBase64 of type string to store the base64 representation of the image data
        public string byteBase64 { get; set; }

        // Define a public property named ContentType of type string to store the content type of the image file (e.g., image/jpeg, image/png)
        public string ContentType { get; set; }

        // Define a public property named FileName of type string to store the name of the image file
        public string FileName { get; set; }
    }


}
