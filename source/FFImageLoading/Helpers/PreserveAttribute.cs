using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FFImageLoading.Transformations")]
namespace FFImageLoading.Helpers
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
	public sealed class PreserveAttribute : Attribute
    {
        public bool AllMembers;
        public bool Conditional;
    }
}
