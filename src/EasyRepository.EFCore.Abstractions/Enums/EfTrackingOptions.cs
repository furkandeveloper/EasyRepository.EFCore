namespace EasyRepository.EFCore.Abstractions.Enums;

public enum EfTrackingOptions
{
        
    /// <summary>
    /// Disables EfCore change tracking
    /// </summary>
    AsNoTracking,
            
    /// <summary>
    /// Enables EfCore Change Tracking
    /// </summary>
    WithTracking
}