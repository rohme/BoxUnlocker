using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoxUnlocker
{
    class Tools
    {
        public static void DebugMessage(string iMsg)
        {
            DateTime dtNow = DateTime.Now;
            string strNow = dtNow.ToString("yyyy/MM/dd HH:mm:ss");
            Console.WriteLine(/*"[" + strNow + "]" +*/ iMsg);
        }

        public static ArrayList LoadCsvFile(string iCsvFileName)
        {
            ArrayList retCsv = new ArrayList();
            StreamReader file = new StreamReader(iCsvFileName);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                if (line.IndexOf("#") >= 0)
                {
                    line = line.Substring(0, line.IndexOf("#")).Trim();
                }
                if (line.Length > 0)
                {
                    string[] ss = line.Split(',');
                    for (int i = 0; i < ss.Length; i++)
                    {
                        ss[i] = ss[i].Trim();
                    }
                    retCsv.Add(ss);
                }
            }
            file.Close();
            return retCsv;
        }


        /// <summary>
        /// 正規表現で文字列が含まれているか
        /// </summary>
        /// <param name="iString">検索対象文字列</param>
        /// <param name="iMatchString">正規表現文字列</param>
        /// <returns>True:含まれている False:含まれていない</returns>
        public static Boolean IsRegexString(string iString, string iMatchString)
        {
            Regex reg = new Regex(iMatchString, RegexOptions.None);
            Match ma = reg.Match(iString);
            return ma.Success;
        }
        /// <summary>
        /// 正規表現で文字列が含まれているか
        /// </summary>
        /// <param name="iArrString">検索対象文字列</param>
        /// <param name="iMatchString">正規表現文字列</param>
        /// <returns>True:含まれている False:含まれていない</returns>
        public static Boolean IsRegexString(ArrayList iString, string iMatchString)
        {
            for (int i = 0; i < iString.Count; i++)
            {
                if (IsRegexString(iString[i].ToString(),iMatchString)) return true;
            }
            return false;
        }
        /// <summary>
        /// 正規表現で文字列が含まれているか
        /// </summary>
        /// <param name="iArrString">検索対象文字列</param>
        /// <param name="iMatchString">正規表現文字列</param>
        /// <returns>True:含まれている False:含まれていない</returns>
        public static Boolean IsRegexString(string iString, ArrayList iMatchString)
        {
            for (int i = 0; i < iMatchString.Count; i++)
            {
                if (IsRegexString(iString, iMatchString[i].ToString())) return true;
            }
            return false;
        }
        /// <summary>
        /// 正規表現で文字列を検索する
        /// </summary>
        /// <param name="iString">検索対象文字列</param>
        /// <param name="iMatchString">正規表現文字列</param>
        /// <returns>正規表現で取得された文字列のArrayList</returns>
        public static ArrayList GetRegexString(string iString, string iMatchString)
        {
            ArrayList retStr = new ArrayList();
            Regex reg = new Regex(iMatchString, RegexOptions.None);
            Match ma = reg.Match(iString);
            if (ma.Success)
            {
                if (ma.Groups.Count > 1)
                {
                    for (int i = 1; i < ma.Groups.Count; i++)
                    {
                        retStr.Add(ma.Groups[i].Value);
                    }
                }
            }
            return retStr;
        }

    }
}
