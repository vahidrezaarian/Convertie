using PeterO.Cbor;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Media;

namespace Convertie.Utilities;

public static class Extensions
{
    public static string ToBase64UrlString(this byte[] data)
    {
        return Utils.ConvertByteArrayToBase64UrlString(data);
    }

    public static byte[] ToByteArrayFromBase64UrlString(this string data)
    {
        return Utils.ConvertBase64UrlStringToByteArray(data);
    }

    public static string ToBase64String(this byte[] data)
    {
        return Utils.ConvertByteArrayToBase64String(data);
    }

    public static byte[] ToByteArrayFromBase64String(this string data)
    {
        return Utils.ConvertBase64StringToByteArray(data);
    }

    public static string ToHexString(this byte[] data)
    {
        return Utils.ConvertByteArrayToHexString(data);
    }

    public static byte[] ToByteArrayFromHexString(this string data)
    {
        return Utils.ConvertHexStringToByteArray(data);
    }

    public static byte[] ComputeSha256(this byte[] data)
    {
        return Utils.ComputeSha256(data);
    }

    public static string ComputeSha256(this string data)
    {
        return Utils.ComputeSha256(data);
    }

    public static ECParameters ToElipticCurveParameters(this CBORObject key)
    {
        var point = new ECPoint
        {
            X = key[-2].GetByteString(),
            Y = key[-3].GetByteString()
        };

        return new ECParameters
        {
            Q = point,
            Curve = ECCurve.NamedCurves.nistP256
        };
    }

    public static Brush ToBrush(this string data)
    {
        return new SolidColorBrush((Color)ColorConverter.ConvertFromString(data));
    }
}

public static class Utils
{
    public static byte[] GetRandomBytes(int length)
    {
        byte[] bytes = new byte[length];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }
        return bytes;
    }

    public static string ConvertByteArrayToHexString(byte[] byteArray)
    {
        return BitConverter.ToString(byteArray).Replace("-", "");
    }

    public static byte[] ConvertHexStringToByteArray(string hex)
    {
        int length = hex.Length;
        if (length % 2 != 0)
            throw new ArgumentException("Hex string must have an even number of characters.");

        byte[] byteArray = new byte[length / 2];
        for (int i = 0; i < length; i += 2)
        {
            byteArray[i / 2] = System.Convert.ToByte(hex.Substring(i, 2), 16);
        }

        return byteArray;
    }

    public static string ConvertByteArrayToBase64UrlString(byte[] data)
    {
        string base64 = System.Convert.ToBase64String(data);
        string base64Url = base64.Replace('+', '-')
                                 .Replace('/', '_')
                                 .TrimEnd('=');

        return base64Url;
    }

    public static byte[] ConvertBase64UrlStringToByteArray(string data)
    {
        if (string.IsNullOrEmpty(data))
            throw new ArgumentNullException(nameof(data));

        string base64 = data
            .Replace('-', '+')
            .Replace('_', '/');

        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }

        return System.Convert.FromBase64String(base64);
    }

    public static string ConvertByteArrayToBase64String(byte[] data)
    {
        return System.Convert.ToBase64String(data, 0, data.Length);
    }

    public static byte[] ConvertBase64StringToByteArray(string data)
    {
        return System.Convert.FromBase64String(data);
    }

    public static string ConvertHexStringToBase64String(string data)
    {
        var bytes = ConvertHexStringToByteArray(data);
        return System.Convert.ToBase64String(bytes, 0, bytes.Length);
    }

    public static string ConvertBase64StringToHexString(string data)
    {
        return ConvertByteArrayToHexString(System.Convert.FromBase64String(data));
    }

    public static string ComputeSha256(string data)
    {
        var hash = SHA256.Create();
        var dataBytes = Encoding.UTF8.GetBytes(data);
        return hash.ComputeHash(dataBytes).ToHexString();
    }

    public static byte[] ComputeSha256(byte[] data)
    {
        var hash = SHA256.Create();
        return hash.ComputeHash(data);
    }

    public static byte[] Decrypt(byte[] cipherText, byte[] key, byte[] iv, PaddingMode padding = PaddingMode.None)
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Padding = padding;
            ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (MemoryStream output = new MemoryStream())
                    {
                        cs.CopyTo(output);
                        return output.ToArray();
                    }
                }
            }
        }
    }

    public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv, PaddingMode padding = PaddingMode.None)
    {
        byte[] encrypted;
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Padding = padding;
            ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }
                encrypted = ms.ToArray();
            }
        }
        return encrypted;
    }

    public static List<ConvertingTypes> GetInputTypes()
    {
        return [ConvertingTypes.Text, ConvertingTypes.Hex, ConvertingTypes.CBOR, ConvertingTypes.Base64, ConvertingTypes.Base64URL];
    }

    public static List<ConvertingTypes> GetOutputTypes(ConvertingTypes inputType)
    {
        switch (inputType)
        {
            case ConvertingTypes.Text:
                return [ConvertingTypes.Hex, ConvertingTypes.Base64, ConvertingTypes.Base64URL];
            case ConvertingTypes.Hex:
                return [ConvertingTypes.Text, ConvertingTypes.CBOR, ConvertingTypes.Base64, ConvertingTypes.Base64URL];
            case ConvertingTypes.CBOR:
                return [ConvertingTypes.Hex, ConvertingTypes.Base64, ConvertingTypes.Base64URL];
            case ConvertingTypes.Base64:
                return [ConvertingTypes.Text, ConvertingTypes.Hex, ConvertingTypes.CBOR, ConvertingTypes.Base64URL];
            case ConvertingTypes.Base64URL:
                return [ConvertingTypes.Text, ConvertingTypes.Hex, ConvertingTypes.CBOR, ConvertingTypes.Base64];
            default:
                throw new NotSupportedException("Input is not supported!");
        }
    }

    public static List<EncodingTypes> GetEncodingDecodingTypes()
    {
        return [EncodingTypes.UTF8, EncodingTypes.ASCII, EncodingTypes.UTF7, EncodingTypes.UTF32, EncodingTypes.Unicode, EncodingTypes.BigEndianUnicode, EncodingTypes.Latin1 ];
    }
}
