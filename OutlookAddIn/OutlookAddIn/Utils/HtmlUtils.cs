using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OutlookAddIn.Utils
{
    public class HtmlUtils
    {
        // This function converts HTML code to plain text
        // Any step is commented to explain it better
        // You can change or remove unnecessary parts to suite your needs
        public string HtmlToText(string p_HtmlCode)
        {
            // Remove new lines since they are not visible in HTML
            p_HtmlCode = p_HtmlCode.Replace("\n", " ");

            // Remove tab spaces
            p_HtmlCode = p_HtmlCode.Replace("\t", " ");

            // Remove multiple white spaces from HTML
            p_HtmlCode = Regex.Replace(p_HtmlCode, "\\s+", " ");

            // Remove HEAD tag
            p_HtmlCode = Regex.Replace(p_HtmlCode, "<head.*?</head>", ""
                                , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Remove any JavaScript
            p_HtmlCode = Regex.Replace(p_HtmlCode, "<script.*?</script>", ""
              , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Replace special characters like &, <, >, " etc.
            StringBuilder v_Html = new StringBuilder(p_HtmlCode);
            // Note: There are many more special characters, these are just
            // most common. You can add new characters in this arrays if needed
            string[] OldWords = {"&nbsp;", "&amp;", "&quot;", "&lt;",
   "&gt;", "&reg;", "&copy;", "&bull;", "&trade;"};
            string[] NewWords = { " ", "&", "\"", "<", ">", "Â®", "Â©", "â€¢", "â„¢" };
            for (int i = 0; i < OldWords.Length; i++)
            {
                v_Html.Replace(OldWords[i], NewWords[i]);
            }

            // Check if there are line breaks (<br>) or paragraph (<p>)
            v_Html.Replace("<br>", "\n<br>");
            v_Html.Replace("<br ", "\n<br ");
            v_Html.Replace("<p ", "\n<p ");

            // Finally, remove all HTML tags and return plain text
            return System.Text.RegularExpressions.Regex.Replace(
              v_Html.ToString(), "<[^>]*>", "");
        }
    }
}
