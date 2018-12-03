﻿namespace TeamLegend.Web.Infrastructure.Extensions
{
    using System;

    public static class StringExtensions
    {
        /// <summary>
        /// Retrieves a substring from this instance. The substring starts from the beginning of the word and ends at the last occurence of the given character.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="stopAt"></param>
        /// <returns></returns>
        public static string SubstringBefore(this string text, string stopAt = ".")
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return string.Empty;
        }
    }
}
