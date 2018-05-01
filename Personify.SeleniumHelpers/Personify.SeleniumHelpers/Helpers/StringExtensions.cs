using System;

namespace Personify.Helpers
{
    public static class StringExtensions
    {
        public static string NormalizeForNovus(this string str)
        {
            str = str.Replace(Convert.ToChar(10).ToString(), Convert.ToChar(160).ToString());
            return str.Replace(Convert.ToChar(32).ToString(), Convert.ToChar(160).ToString());
        } 
    }
}