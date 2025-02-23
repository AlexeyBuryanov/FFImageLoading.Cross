# FFImageLoading - Fast & Furious Image Loading 

Library to load images quickly & easily on .NET iOS, .NET Android, MvvmCross.

[![License](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Core](https://img.shields.io/nuget/v/Cross.FFImageLoading.Core)](https://www.nuget.org/packages/Cross.FFImageLoading.Core)
[![Android](https://img.shields.io/nuget/v/Cross.FFImageLoading.Android)](https://www.nuget.org/packages/Cross.FFImageLoading.Android)
[![iOS](https://img.shields.io/nuget/v/Cross.FFImageLoading.Ios)](https://www.nuget.org/packages/Cross.FFImageLoading.Ios)
## Features

- .NET iOS, .NET  Android, MvvmCross support
- Configurable disk and memory caching
- Multiple image views using the same image source (url, path, resource) will use only one bitmap which is cached in memory (less memory usage)
- Deduplication of similar download/load requests. *(If 100 similar requests arrive at same time then one real loading will be performed while 99 others will wait).*
- Error and loading placeholders support
- Images can be automatically downsampled to specified size (less memory usage)
- Fluent API which is inspired by Picasso naming
- SVG / WebP / GIF support
- Image loading Fade-In animations support
- Can retry image downloads (RetryCount, RetryDelay)
- Android bitmap optimization. Saves 50% of memory by trying not to use transparency channel when possible.
- Transformations support
  - BlurredTransformation
  - CircleTransformation, RoundedTransformation, CornersTransformation, CropTransformation
  - ColorSpaceTransformation, GrayscaleTransformation, SepiaTransformation, TintTransformation
  - FlipTransformation, RotateTransformation
  - Supports custom transformations (native platform `ITransformation` implementations)
