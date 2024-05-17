//the following code logic has been developed from c-sharpcorner.com/getbase64stringfromimage

using System.Globalization;

namespace WeatherWise.View
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object input, Type target, object param, CultureInfo info)
        {
            string encodedImage = input as string;
            if (string.IsNullOrWhiteSpace(encodedImage))
                return null;

            byte[] bytes = System.Convert.FromBase64String(encodedImage);
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }

        public object ConvertBack(object input, Type target, object param, CultureInfo info)
        {
            throw new NotSupportedException();
        }

    }

}
