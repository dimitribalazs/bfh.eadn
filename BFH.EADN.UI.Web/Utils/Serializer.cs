using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace BFH.EADN.UI.Web.Utils
{
    public static class Serializer
    {
        public static string SerializeToBase64String<T>(T obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, obj);
            long length = stream.Length;
            byte[] bytes = stream.GetBuffer();

            string infoData = Convert.ToBase64String(bytes, 0, bytes.Length, Base64FormattingOptions.None);

            string encodedData = infoData;
            return encodedData;
        }

        public static T DeserializeFromBase64String<T>(string content)
        {
            byte[] memData = Convert.FromBase64String(content);
            int length = memData.Length;

            MemoryStream stream = new MemoryStream(memData, 0, length);
            BinaryFormatter bf = new BinaryFormatter();
            T resultOb = (T)bf.Deserialize(stream);

            return resultOb;
        }
    }
}