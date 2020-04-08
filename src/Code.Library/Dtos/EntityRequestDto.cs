namespace Code.Library.Dtos
{
    public class EntityRequestDto<TKey>
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        public TKey Id { get; set; }
    }
}