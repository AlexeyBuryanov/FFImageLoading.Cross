# Copilot Instructions for FFImageLoading.Cross

## Project Overview

FFImageLoading.Cross is a .NET image loading, caching, and transforming library for .NET Android, .NET iOS, and MvvmCross. It provides configurable disk/memory caching, deduplication of download requests, placeholder support, downsampling, SVG/WebP/GIF support, fade-in animations, and built-in image transformations.

## Repository Structure

- `source/FFImageLoading/` — Cross-platform core library (`Cross.FFImageLoading.Core` NuGet package), targeting `net9.0;net10.0`
- `source/FFImageLoading.Droid/` — Android platform implementation (`Cross.FFImageLoading.Android` NuGet package), targeting `net9.0-android;net10.0-android`
- `source/FFImageLoading.Ios/` — iOS platform implementation (`Cross.FFImageLoading.Ios` NuGet package), targeting `net9.0-ios;net10.0-ios`
- `samples/SampleApp.Droid/` — Sample Android application demonstrating library usage
- `Directory.Build.props` — Shared MSBuild properties, multi-target framework versions (`NetVersion`, `NetVersionAndroid`, `NetVersionIos`), and NuGet package metadata

## Build & Development Setup

### Prerequisites

- .NET 9 and .NET 10 SDKs
- Android and iOS workloads: `dotnet workload install android ios`

### Build Commands

```bash
dotnet restore
dotnet build -c Release
```

### CI/CD

- **CI** (`.github/workflows/ci.yml`): Runs on `macos-latest` on push/PR to `master`. Installs android + ios workloads, restores, and builds.
- **Publish** (`.github/workflows/publish.yml`): Triggers on GitHub release. Uses NuGet Trusted Publishing via `nuget/login@v1` with OIDC to push packages.

## Code Style & Conventions

### General

- Follow the `.editorconfig` at the repo root for all formatting rules.
- C# files use **tabs** for indentation (size 4), with `utf-8-bom` encoding.
- XML/JSON files use **spaces** (indent size 2).
- Nullable reference types are enabled globally (`<Nullable>enable</Nullable>`).
- Implicit usings are enabled globally (`<ImplicitUsings>enable</ImplicitUsings>`).

### Naming Conventions

- **PascalCase** for constants, methods, properties, classes, interfaces, enums, structs, delegates, and events.
- **camelCase** for parameters and local variables.
- **_camelCase** (underscore prefix) for private fields (e.g., `_initialized`, `_config`, `_disposed`).
- Avoid `this.` qualification unless necessary.
- Use language keywords over framework types (e.g., `string` not `String`).

### Code Style Preferences

- Methods, constructors, and operators: prefer **block bodies**.
- Properties, indexers, and accessors: prefer **expression bodies**.
- Use pattern matching over `is`/`as` with casts.
- Use inline variable declarations.
- Use null-coalescing (`??`) and null-propagation (`?.`) operators.
- Allman brace style — opening braces on a new line.

## Architecture & Key Patterns

### Namespaces

- Core types: `FFImageLoading`, `FFImageLoading.Work`, `FFImageLoading.Cache`, `FFImageLoading.Config`, `FFImageLoading.Helpers`
- Android-specific: `FFImageLoading.Droid`, `FFImageLoading.Droid.*`
- iOS-specific: `FFImageLoading.Ios`, `FFImageLoading.Ios.*`
- Shared public view: `FFImageLoading.Cross` (contains `MvxCachedImageView` on both platforms)

### Key Types

- `ImageServiceBase<TImageContainer>` — Abstract base providing the core image service logic. Platform implementations override abstract methods for platform-specific behavior.
- `ImageService` — Platform-specific singleton (in `FFImageLoading.Droid` / `FFImageLoading.Ios`). Access via `ImageService.Instance`. Call `Initialize()` before use.
- `TaskParameter` — Fluent API for configuring image loading tasks (via `FromFile`, `FromUrl`, `FromEmbeddedResource`, etc.).
- `MvxCachedImageView` — The primary image view for both Android (`Android.Widget.ImageView` subclass) and iOS (`UIImageView` subclass), registered as `ffimageloading.cross.MvxCachedImageView` on Android.
- `Configuration` — Centralized configuration (caching, timeouts, logging, animations, etc.).
- `ITransformation` — Interface for image transformations (blur, circle, crop, grayscale, etc.).

### Data Resolver Pattern

- `DataResolverResult` supports dual paths: **Stream** (for standard bitmap decoding) or **Decoded** (pre-decoded image, bypasses decoder). XML/vector drawables on Android must use the Decoded path.
- Platform-specific resolvers: `ResourceDataResolver`, `BundleDataResolver`, `UrlDataResolver`, etc.

## Multi-Targeting

- Target framework versions are centralized in `Directory.Build.props` via properties:
  - `NetVersion` = `net9.0;net10.0`
  - `NetVersionAndroid` = `net9.0-android;net10.0-android`
  - `NetVersionIos` = `net9.0-ios;net10.0-ios`
- Android uses conditional `TargetPlatformVersion`: 35 for net9.0, 36.1 for net10.0.

## NuGet Packages

The repo produces three packages (with `GeneratePackageOnBuild=true`):

| Package | Project |
|---|---|
| `Cross.FFImageLoading.Core` | `source/FFImageLoading/` |
| `Cross.FFImageLoading.Android` | `source/FFImageLoading.Droid/` |
| `Cross.FFImageLoading.Ios` | `source/FFImageLoading.Ios/` |

## Adding Transformations

Transformations follow a consistent pattern:
1. Create a platform-agnostic class in `source/FFImageLoading/Transformations/` inheriting common logic.
2. Create platform-specific implementations in `source/FFImageLoading.Droid/Transformations/` and `source/FFImageLoading.Ios/Transformations/`, typically extending `TransformationBase`.
3. Implement the `ITransformation` interface with a `Key` property and `Transform` method.
