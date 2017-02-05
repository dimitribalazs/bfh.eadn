using System.Runtime.Serialization;

namespace BFH.EADN.Common.Types.Enums
{
    [DataContract]
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
