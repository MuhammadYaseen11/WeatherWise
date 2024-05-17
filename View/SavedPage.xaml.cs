using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.IO;
using WeatherWise.Model;
using WeatherWise.Service;
using Service;

namespace WeatherWise.View;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class SavedPage : ContentPage
{

    public string ItemId
    {
        set { LoadNote(value); }
    }

    public SavedPage()
    {
        InitializeComponent();
        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";
        LoadNote(Path.Combine(appDataPath, randomFileName));
    }

    private void LoadNote(string fileName)
    {
        Model.Note noteModel = new Model.Note();
        noteModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            var text = File.ReadAllText(fileName);
            // Assume there's an Editor or some UI element to display the loaded text
            TextEditor.Text = text;  // Ensure there's an Editor defined in your XAML with x:Name="TextEditor"
        }
        BindingContext = noteModel;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        if (BindingContext is Model.Note note)
            File.WriteAllText(note.Filename, TextEditor.Text);
        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        if (BindingContext is Model.Note note)
        {
            // Delete the file.
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }
        await Shell.Current.GoToAsync("..");
    }
}
