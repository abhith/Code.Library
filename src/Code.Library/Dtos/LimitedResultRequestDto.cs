using System;

namespace Code.Library.Dtos
{
    /// <summary>
    /// Simply implements <see cref="ILimitedResultRequest"/>.
    /// </summary>
    [Serializable]
    public class LimitedResultRequestDto : ILimitedResultRequest
    {
        //[Range(1, int.MaxValue)]
        public virtual int MaxResultCount { get; set; } = 10;
    }
}