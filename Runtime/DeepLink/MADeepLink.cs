using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace RealbizGames.Platform
{
    public class MADeepLink
    {
        private string deeplinkURL;

        private string linkAction;
        private Dictionary<string, string> deeplinkParams = new Dictionary<string, string>();

        public string DeeplinkURL { get => deeplinkURL; }
        public string LinkAction { get => linkAction; }
        public Dictionary<string, string> DeeplinkParams { get => deeplinkParams; }

        public MADeepLink(string deeplinkURL)
        {
            this.deeplinkURL = deeplinkURL;
            this.linkAction = parseLinkAction(deeplinkURL: deeplinkURL);
            this.deeplinkParams = GetParams(deeplinkURL);
        }

        public int getIntValueFromParams(string key, int defaultValue)
        {
            if (deeplinkParams != null && deeplinkParams.ContainsKey(key))
            {
                return int.Parse(deeplinkParams[key]);
            }
            return defaultValue;
        }

        public string getStringValueFromParams(string key, string defaultValue)
        {
            if (deeplinkParams != null && deeplinkParams.ContainsKey(key))
            {
                return deeplinkParams[key];
            }
            return defaultValue;
        }

        private string parseLinkAction(string deeplinkURL) {
            if (deeplinkURL.Contains("?"))
            {
                int indexStart = deeplinkURL.IndexOf("://", 0, StringComparison.Ordinal) + 3;
                int indexEnd = deeplinkURL.IndexOf("?", 0, StringComparison.Ordinal);
                int length = indexEnd - indexStart;
                
                return deeplinkURL.Substring(indexStart, length);
            }
            else
            {
                int indexStart = deeplinkURL.IndexOf("://", 0, StringComparison.Ordinal) + 3;
                int indexEnd = deeplinkURL.Length;
                int length = indexEnd - indexStart;
                return deeplinkURL.Substring(indexStart, length);
            }
        }

        static public Dictionary<string, string> GetParams(string uri)
        {
            var matches = Regex.Matches(uri, @"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);
            var keyValues = new Dictionary<string, string>(matches.Count);
            foreach (Match m in matches)
                keyValues.Add(Uri.UnescapeDataString(m.Groups[2].Value), Uri.UnescapeDataString(m.Groups[3].Value));

            return keyValues;
        }

        public static string parseDictionaryToString(Dictionary<string, string> keyValuePairs)
        {
            string returnedString = "{";
            foreach (var keyValuePair in keyValuePairs)
            {
                returnedString += keyValuePair.Key + ":" + keyValuePair.Value + ",";
            }
            returnedString += "}";
            return returnedString;
        }

        public override string ToString()
        {
            return string.Format("[MADeepLink deeplinkURL={0} LinkAction={1}]", deeplinkURL, LinkAction);
        }


    }
}