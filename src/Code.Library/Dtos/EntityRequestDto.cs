namespace Code.Library
{
    public class EntityRequestDto<TKey>
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        public TKey Id { get; set; }
    }
}