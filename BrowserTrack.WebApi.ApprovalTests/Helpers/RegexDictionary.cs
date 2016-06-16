using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSpecApprovalTester.Helpers
{
    public static class RegexDictionary
    {
        public const string REGEX_ISO_DATETIME = @"(\d{4})-0?(\d+)-0?(\d+)[T ]0?(\d+):0?(\d+):0?(\d+)([+-][0-2]\d:[0-5]\d|Z)";
        public const string REGEX_PRICE_TO_6_DECMINAL = @"(([1-9][0-9]{0,8})|0)?\.[0-9]{1,6}";
        public const string REGEX_PRICE_CHANGE_TO_8_DECMINAL = @"-?(([1-9][0-9]{0,8})|0)?\.[0-9]{1,8}";

        public const string REGEX_NOTE_REF_NUM = @"[A-Z_]* - \d* - \d*";

        public const string REGEX_TXN_REF_NUM = @"\d*:\d*(:~){3}";


        static Dictionary<string, string> _dict = new Dictionary<string, string>
        {
            { "REGEX_ISO_DATETIME",REGEX_ISO_DATETIME},
            { "REGEX_PRICE_TO_6_DECMINAL",REGEX_PRICE_TO_6_DECMINAL},
            { "REGEX_PRICE_CHANGE_TO_8_DECMINAL",REGEX_PRICE_CHANGE_TO_8_DECMINAL},
            { "REGEX_NOTE_REF_NUM" , REGEX_NOTE_REF_NUM },
            { "REGEX_TXN_REF_NUM" , REGEX_TXN_REF_NUM}

        };

        public static string GetRegexString(string key) {
            string result;

            if (_dict.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                return null;
            }


        }
    }
}
