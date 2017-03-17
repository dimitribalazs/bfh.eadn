using System.Runtime.Serialization;

namespace BFH.EADN.Common.Types.Enums
{
    /// <summary>
    /// Defines the types of the quiz
    /// </summary>
    [DataContract(Namespace = Constants.XMLNamespace, Name = "QuizType")]
    public enum QuizType
    {
        [EnumMember]
        Fix,
        [EnumMember]
        Variable,
        [EnumMember]
        Dynamic
    }
}
