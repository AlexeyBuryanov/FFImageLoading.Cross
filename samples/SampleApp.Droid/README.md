# FFImageLoading XML Placeholder Sample

A .NET 10 Android sample app that demonstrates using XML vector drawables as placeholders with FFImageLoading, using `ffimageloading.cross.MvxCachedImageView` in the XML layout.

## What it tests

1. **XML loading placeholder** — An orange circle vector drawable (`placeholder_xml.xml`) is used as a loading placeholder while an image is fetched from a URL.
2. **XML error placeholder** — A red circle vector drawable (`error_xml.xml`) is shown when image loading fails (invalid URL).
3. **Both XML placeholders** — Both loading and error placeholders are XML vector drawables. The loading placeholder shows first, then is replaced by the error placeholder on failure.

## Key implementation

The XML layout uses `ffimageloading.cross.MvxCachedImageView` instead of plain `ImageView`:

```xml
<ffimageloading.cross.MvxCachedImageView
    android:id="@+id/imageWithXmlPlaceholder"
    android:layout_width="200dp"
    android:layout_height="200dp" />
```

Image loading is driven by setting properties on the view in code:

```csharp
var image = FindViewById<MvxCachedImageView>(Resource.Id.imageWithXmlPlaceholder)!;
image.LoadingPlaceholderImagePath = "placeholder_xml";
image.ImagePath = "https://picsum.photos/400/400";
```

## How to run

```bash
dotnet workload restore
dotnet build samples/SampleApp.Droid/SampleApp.Droid.csproj
```

Deploy to an Android device or emulator to verify that the XML vector drawable placeholders display correctly.

## Expected behavior

- **Test 1**: Orange circle shows briefly, then a photo from picsum.photos loads.
- **Test 2**: Red circle error placeholder appears after the invalid URL fails.
- **Test 3**: Orange circle shows first, then transitions to the red circle after the invalid URL fails.

Before the fix in `ResourceDataResolver`, all three tests would fail because `OpenRawResource()` returns binary XML that `BitmapFactory.DecodeStream()` cannot decode. The fix detects XML resources and uses `Resources.GetDrawable()` + `Canvas` rendering instead.
