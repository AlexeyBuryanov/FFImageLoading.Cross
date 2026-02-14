using Android.App;
using Android.OS;
using FFImageLoading.Cross;
using FFImageLoading.Droid;

namespace SampleApp.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            ImageService.Instance.Initialize();

            var image1 = FindViewById<MvxCachedImageView>(Resource.Id.imageWithXmlPlaceholder)!;
            var image2 = FindViewById<MvxCachedImageView>(Resource.Id.imageWithXmlErrorPlaceholder)!;
            var image3 = FindViewById<MvxCachedImageView>(Resource.Id.imageWithBothXmlPlaceholders)!;

            // Test 1: Load a real image with an XML vector drawable as loading placeholder.
            // The orange circle placeholder should show briefly, then the actual image loads.
            image1.LoadingPlaceholderImagePath = "placeholder_xml";
            image1.ImagePath = "https://picsum.photos/400/400";

            // Test 2: Load from an invalid URL with an XML vector drawable as error placeholder.
            // The red circle error placeholder should appear after the load fails.
            image2.ErrorPlaceholderImagePath = "error_xml";
            image2.ImagePath = "https://invalid.example.test/does_not_exist.png";

            // Test 3: Both loading and error placeholders as XML vector drawables.
            // The orange circle should show first, then replaced by the red circle on failure.
            image3.LoadingPlaceholderImagePath = "placeholder_xml";
            image3.ErrorPlaceholderImagePath = "error_xml";
            image3.ImagePath = "https://invalid.example.test/does_not_exist_2.png";
        }
    }
}
