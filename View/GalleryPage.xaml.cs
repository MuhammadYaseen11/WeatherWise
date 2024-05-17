using System.Collections.ObjectModel;
using System.Windows.Input;
using WeatherWise.Model;
using WeatherWise.Service;
using Service;

namespace WeatherWise.View;

public partial class GalleryPage : ContentPage
{
    public ObservableCollection<ImageFile> ImageFiles { get; set; } = new ObservableCollection<ImageFile>();
    private UploadImage uploadImage = new UploadImage();
    public ICommand DeleteCommand { get; set; }

    public GalleryPage()
    {
        InitializeComponent();
        BindingContext = this;
        DeleteCommand = new Command<ImageFile>(OnDeleteImage);
    }
    private void OnHowToUseClicked(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        DisplayAlert("How to Use", "Use the 'Upload Image' button to add images from your device. Use the 'Open Camera' button to take new photos. Swipe left on an image and tap the bin icon to delete it from the gallery.", "OK");
    }
    private async void UploadImage_Clicked(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        var selectedImage = await uploadImage.OpenMediaPickerAsync();
        if (selectedImage != null)
        {
            var uploadedImageFile = await uploadImage.Upload(selectedImage);
            if (uploadedImageFile != null)
            {
                ImageFiles.Add(uploadedImageFile);
            }
        }

    }
    private async void OpenCamera_Clicked(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        try
        {
            // Check if the device supports opening the camera
            if (MediaPicker.IsCaptureSupported)
            {
                var snapshot = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Capture a photo"
                });

                if (snapshot != null)
                {
                    string savedFilePath = await SavePhotoAsync(snapshot);
                    await DisplayAlert("Save Location", $"File saved at: {savedFilePath}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "This device does not support camera functionality.", "OK");
            }
            }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to open camera: {ex.Message}", "OK");
        }
    }

    private async Task<string> SavePhotoAsync(FileResult image)
    {
        if (image == null)
            throw new ArgumentNullException(nameof(image));

        // Generate a unique filename using the current timestamp
        string filename = $"Image_{DateTime.UtcNow:yyMMddHHmmss}.jpg";
        string locationPath;

        // Check the device platform and set the file save location accordingly
        if (Device.RuntimePlatform == Device.Android)
        {
            locationPath = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath, filename);
        }
        else
        {
            locationPath = Path.Combine(FileSystem.AppDataDirectory, filename); // Use the default path for non-Android devices
        }

        // Open the image file stream and save it to the location
        using (var imageStream = await image.OpenReadAsync())
        using (var destinationStream = new FileStream(locationPath, FileMode.Create, FileAccess.Write))
        {
            await imageStream.CopyToAsync(destinationStream);
        }

        // If the device is Android, refresh the gallery to display the new image
        if (Device.RuntimePlatform == Device.Android)
        {
            Android.Media.MediaScannerConnection.ScanFile(Android.App.Application.Context, new[] { locationPath }, null, null);
        }

        return locationPath;
    }


    private void OnDeleteImage(ImageFile image)
    {
        if (ImageFiles.Contains(image))
        {
            ImageFiles.Remove(image);
        }
    }
}

