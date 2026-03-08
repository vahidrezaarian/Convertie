using Newtonsoft.Json.Linq;
using PeterO.Cbor;
using System.Text;

namespace Convertie.Utilities
{
    public static class Converter
    {
        public static string Convert(string input, ConvertingTypes inputType, ConvertingTypes outputType, EncodingTypes encodingType)
        {
            byte[]? bytes = GetBytesByEncoding(input, encodingType);
            if (inputType == ConvertingTypes.Hex)
            {
                bytes = input.ToByteArrayFromHexString();
            }
            else if (inputType == ConvertingTypes.Base64)
            {
                bytes = input.ToByteArrayFromBase64String();
            }
            else if (inputType == ConvertingTypes.Base64URL)
            {
                bytes = input.ToByteArrayFromBase64UrlString();
            }

            if (outputType == ConvertingTypes.Hex)
            {
                return bytes.ToHexString();
            }
            else if (outputType == ConvertingTypes.Base64)
            {
                return bytes.ToBase64String();
            }
            else if (outputType == ConvertingTypes.Base64URL)
            {
                return bytes.ToBase64UrlString();
            }
            else if (outputType == ConvertingTypes.JSON)
            {
                return JObject.Parse(GetStringByEncoding(bytes, encodingType)).ToString();
            }
            else if (outputType == ConvertingTypes.CBOR)
            {
                return CBORObject.DecodeFromBytes(bytes).ToString();
            }
            else
            {
                return GetStringByEncoding(bytes, encodingType);
            }
        }

        public static byte[] GetBytesByEncoding(string value, EncodingTypes encodingType)
        {
            if (encodingType == EncodingTypes.ASCII)
            {
                return Encoding.ASCII.GetBytes(value);
            }
            else if (encodingType == EncodingTypes.UTF7)
            {
                return Encoding.UTF7.GetBytes(value);
            }
            else if (encodingType == EncodingTypes.UTF32)
            {
                return Encoding.UTF32.GetBytes(value);
            }
            else if (encodingType == EncodingTypes.Unicode)
            {
                return Encoding.Unicode.GetBytes(value);
            }
            else if (encodingType == EncodingTypes.BigEndianUnicode)
            {
                return Encoding.BigEndianUnicode.GetBytes(value);
            }
            else if (encodingType == EncodingTypes.Latin1)
            {
                return Encoding.Latin1.GetBytes(value);
            }
            else
            {
                return Encoding.UTF8.GetBytes(value);
            }
        }

        public static string GetStringByEncoding(byte[] value, EncodingTypes encodingType)
        {
            if (encodingType == EncodingTypes.ASCII)
            {
                return Encoding.ASCII.GetString(value);
            }
            else if (encodingType == EncodingTypes.UTF7)
            {
                return Encoding.UTF7.GetString(value);
            }
            else if (encodingType == EncodingTypes.UTF32)
            {
                return Encoding.UTF32.GetString(value);
            }
            else if (encodingType == EncodingTypes.Unicode)
            {
                return Encoding.Unicode.GetString(value);
            }
            else if (encodingType == EncodingTypes.BigEndianUnicode)
            {
                return Encoding.BigEndianUnicode.GetString(value);
            }
            else if (encodingType == EncodingTypes.Latin1)
            {
                return Encoding.Latin1.GetString(value);
            }
            else
            {
                return Encoding.UTF8.GetString(value);
            }
        }
    }
}
