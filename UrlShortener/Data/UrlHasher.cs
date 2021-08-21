using System;

namespace UrlShortener.Data
{
    public class UrlHasher
    {
		public int base36toInt(string number)
		{
			char[] baseChars = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();
			char[] target = number.ToCharArray();
			double result = 0;
			for (int i = 0; i < target.Length; i++)
			{
				result += Array.IndexOf(baseChars, target[i]) * Math.Pow(baseChars.Length, target.Length - i - 1);
			}
			return Convert.ToInt32(result);
		}

		public string IntTo36Base(int value)
		{
			char[] baseChars = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();
			string result = string.Empty;
			int targetBase = baseChars.Length;
			do
			{
				result = baseChars[value % targetBase] + result;
				value = value / targetBase;
			}
			while (value > 0);

			return result;
		}
	}
}
