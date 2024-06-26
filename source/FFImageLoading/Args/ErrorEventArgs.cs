﻿namespace FFImageLoading.Args
{
    [Helpers.Preserve(AllMembers = true)]
    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(Exception exception)
        {
        	Exception = exception;
        }

        public Exception Exception { get; private set; }
    }
}
