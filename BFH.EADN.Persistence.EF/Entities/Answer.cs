namespace BFH.EADN.Persistence.EF.Entities
{
    /// <summary>
    /// Answer entity
    /// </summary>
    public class Answer : BaseEntity
    {
        /// <summary>
        /// Question to which the answer belong
        /// </summary>
        public virtual Question Question { get; set; }

        /// <summary>
        /// Text of the answer
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Answer is solutions
        /// </summary>
        public bool IsSolution { get; set; }
    }


}
