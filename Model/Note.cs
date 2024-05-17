using System.Collections.ObjectModel;

namespace WeatherWise.Model
{
    // Define a public class named Note
    public class Note
    {
        // Define a public property named Filename of type string to store the name of the note file
        public string Filename { get; set; }

        // Define a public property named Date of type DateTime to store the date when the note was created or modified
        public DateTime Date { get; set; }

        // Define a public property named Text of type string to store the textual content of the note
        public string Text { get; set; }

        // Define a public property named ImageFiles of type ObservableCollection<ImageFile> to store a collection of ImageFile objects associated with the note, with initial value as an empty ObservableCollection
        public ObservableCollection<ImageFile> ImageFiles { get; set; } = new ObservableCollection<ImageFile>();
    }

}


