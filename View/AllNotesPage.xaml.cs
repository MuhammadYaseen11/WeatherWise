using Service;


namespace WeatherWise.View;

public partial class AllNotesPage : ContentPage
{
    public AllNotesPage()
    {
        InitializeComponent();
        BindingContext = new Model.AllNotes();
    }

    protected override void OnAppearing()
    {
        ((Model.AllNotes)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        // Navigate to the NotePage to create a new note
        await Shell.Current.GoToAsync(nameof(SavedPage));
    }
    private void OnHowToUseClicked(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        DisplayAlert("How to Use", "This page lists all your notes. Swipe right on any note and tap Bin icon to delete the note. Tap the '+' icon to add new notes.", "OK");
    }

    private void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        var swipeItem = sender as SwipeItem;
    var noteToDelete = swipeItem?.BindingContext as Model.Note;
    if (noteToDelete != null)
    {
        // Remove from the ObservableCollection
        ((Model.AllNotes)BindingContext).Notes.Remove(noteToDelete);

        // If you're directly interacting with files or other resources:
        if (File.Exists(noteToDelete.Filename))
        {
            File.Delete(noteToDelete.Filename);
        }

    }
}
}