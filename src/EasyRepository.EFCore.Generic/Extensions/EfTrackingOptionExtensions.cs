namespace EasyRepository.EFCore.Generic.Extensions;

using Abstractions.Enums;

internal static class EfTrackingOptionExtensions
{
    public static bool HasNoTracking(this EfTrackingOptions options)
    {
        return options == EfTrackingOptions.AsNoTracking;
    }
}