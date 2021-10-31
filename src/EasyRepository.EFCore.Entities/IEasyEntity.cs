namespace EasyRepository.EFCore.Abstractions
{
    /// <summary>
    /// This interface implemented primary key entity
    /// </summary>
    /// <typeparam name="TPrimaryKey">
    /// Primary Key type of the entity
    /// </typeparam>
    internal interface IEasyEntity<TPrimaryKey>
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}
