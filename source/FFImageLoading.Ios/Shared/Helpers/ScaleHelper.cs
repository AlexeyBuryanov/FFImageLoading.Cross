using System.Runtime.InteropServices;

namespace FFImageLoading.Ios.Shared.Helpers
{
    public static class ScaleHelper
    {
        static NFloat? _scale;
        public static NFloat Scale
        {
            get
            {
                if (!_scale.HasValue)
                {
                    InitAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                }

                return _scale.Value;
            }
        }

        public static async Task InitAsync()
        {
            if (_scale.HasValue)
                return;

            var dispatcher = ImageService.Instance.Config.MainThreadDispatcher;
            await dispatcher.PostAsync(() =>
            {
#if IOS
                _scale = UIKit.UIScreen.MainScreen.Scale;
#elif MACOS
                _scale = AppKit.NSScreen.MainScreen.BackingScaleFactor;
#endif
            }).ConfigureAwait(false);
        }
    }
}

