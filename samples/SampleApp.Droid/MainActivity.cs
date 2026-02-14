using Android.App;
using Android.OS;
using Android.Widget;
using FFImageLoading.Droid;
using FFImageLoading.Droid.Extensions;
using FFImageLoading.Work;

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

            var image1 = FindViewById<ImageView>(Resource.Id.imageWithXmlPlaceholder)!;
            var image2 = FindViewById<ImageView>(Resource.Id.imageWithXmlErrorPlaceholder)!;
            var image3 = FindViewById<ImageView>(Resource.Id.imageWithBothXmlPlaceholders)!;

            // Test 1: Load a real image with an XML vector drawable as loading placeholder.
            // The orange circle placeholder should show briefly, then the actual image loads.
            ImageService.Instance
                .LoadUrl("https://picsum.photos/400/400")
                .LoadingPlaceholder("placeholder_xml", ImageSource.CompiledResource)
                .Into(image1);

            // Test 2: Load from an invalid URL with an XML vector drawable as error placeholder.
            // The red circle error placeholder should appear after the load fails.
            ImageService.Instance
                .LoadUrl("https://invalid.example.test/does_not_exist.png")
                .ErrorPlaceholder("error_xml", ImageSource.CompiledResource)
                .Into(image2);

            // Test 3: Both loading and error placeholders as XML vector drawables.
            // The orange circle should show first, then replaced by the red circle on failure.
            ImageService.Instance
                .LoadUrl("https://invalid.example.test/does_not_exist_2.png")
                .LoadingPlaceholder("placeholder_xml", ImageSource.CompiledResource)
                .ErrorPlaceholder("error_xml", ImageSource.CompiledResource)
                .Into(image3);
        }
    }
}
