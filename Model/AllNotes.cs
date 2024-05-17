using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.Maui.Storage;

namespace WeatherWise.Model;

public class AllNotes
{
    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

    public AllNotes()
    {
        LoadNotes();
    }
    public void LoadNotes()
    {
        // Empty the Notes collection to prepare for reloading
        Notes.Clear();

        // Define the directory where the app stores its data
        string dataDirectory = FileSystem.AppDataDirectory;

        // Collect all note files ending with '.notes.txt' from the app's data directory
        IEnumerable<Note> loadedNotes = Directory.EnumerateFiles(dataDirectory, "*.notes.txt").Select(path => new Note()
        {
            Filename = path, // Set the file path as the note's filename
            Text = File.ReadAllText(path), // Read the content of the note file
            Date = File.GetCreationTime(path) // Get the timestamp of the file's creation
        }).OrderBy(n => n.Date); // Sort the notes by their creation dates

        // Add each note from the sorted collection into the Notes observable collection
        foreach (Note note in loadedNotes)
        {
            Notes.Add(note);
        }
    }

}
