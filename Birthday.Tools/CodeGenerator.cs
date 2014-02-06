using System.Security.Cryptography;

namespace Birthday.Tools
{
    public class CodeGenerator
    {
        private const string CharacterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public static string GetCode(int size)
        {
            var result = new char[9];

            var characterArray = CharacterSet.ToCharArray();
            var arr = new byte[1];

            var rng = RNGCryptoServiceProvider.Create();

            for (int i = 0; i < result.Length; i++)
            {
                do
                {
                    rng.GetNonZeroBytes(arr);
                } while (arr[0] >= characterArray.Length);

                result[i] = characterArray[arr[0]];
            }

            return new string(result);
        }
    }
}
