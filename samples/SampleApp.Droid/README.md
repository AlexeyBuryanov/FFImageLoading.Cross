# FFImageLoading XML Placeholder Sample

A .NET 10 Android sample app that demonstrates using XML vector drawables as placeholders with FFImageLoading.

## What it tests

1. **XML loading placeholder** — An orange circle vector drawable (`placeholder_xml.xml`) is used as a loading placeholder while an image is fetched from a URL.
2. **XML error placeholder** — A red circle vector drawable (`error_xml.xml`) is shown when image loading fails (invalid URL).
3. **Both XML placeholders** — Both loading and error placeholders are XML vector drawables. The loading placeholder shows first, then is replaced by the error placeholder on failure.

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
