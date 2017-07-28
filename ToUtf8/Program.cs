using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToUtf8
{
    class Program
    {
        private enum argKey
        {
            exeName = 0,
            utf8 = 1,
            path = 2
        };


        static void Main(string[] args)
        {
            var argDic = GetArgs();
            var by = File.ReadAllBytes(argDic[argKey.path]);
            var encoding = DetectEncodingFromBOM(by);

            if (encoding == null)
            {
                if (argDic.ContainsKey(argKey.utf8))
                {
                    encoding = Encoding.UTF8;
                }
                else
                {
                    encoding = Encoding.GetEncoding(932);       //Encoding.GetEncoding("shift_jis");
                }
            }

            Console.OutputEncoding = new UTF8Encoding();
            Console.WriteLine("ToUtf8.exe convert " + encoding.EncodingName + " -> UTF-8");
            Console.WriteLine(encoding.GetString(by));
        }


        /// <summary>
        /// コマンドラインオプションの処理
        /// </summary>
        /// <returns>整理したコマンドラインオプション</returns>
        private static Dictionary<argKey, string> GetArgs()
        {
            var argsDic = new Dictionary<argKey, string>();
            string[] args = Environment.GetCommandLineArgs();

            for (int i = 0; i < args.Length; i++)
            {
                if (i == 0)
                {
                    argsDic.Add(argKey.exeName, args[i]);
                }
                else if (args[i].StartsWith("-"))
                {
                    if (args[i].StartsWith("-utf8", StringComparison.CurrentCultureIgnoreCase))
                    {
                        argsDic.Add(argKey.utf8, String.Empty);
                    }
                }
                else
                {
                    argsDic.Add(argKey.path, args[i]);
                }
            }

            //必須のコマンドラインオプションの確認
            if (!argsDic.ContainsKey(argKey.path))
            {
                throw new Exception("コンバートするファイルのパスがコマンドラインオプションに指定されていません。");
            }

            return argsDic;
        }

        /// <summary>
        /// BOMを調べて、文字コードを判別する。
        /// </summary>
        /// <param name="bytes">文字コードを調べるデータ。</param>
        /// <returns>BOMが見つかった時は、対応するEncodingオブジェクト。
        /// 見つからなかった時は、null。</returns>
        public static Encoding DetectEncodingFromBOM(byte[] bytes)
        {
            if (bytes.Length < 2)
            {
                return null;
            }

            if ((bytes[0] == 0xfe) && (bytes[1] == 0xff))
            {
                //UTF-16 BE
                return new UnicodeEncoding(true, true);
            }

            if ((bytes[0] == 0xff) && (bytes[1] == 0xfe))
            {
                if ((4 <= bytes.Length) && (bytes[2] == 0x00) && (bytes[3] == 0x00))
                {
                    //UTF-32 LE
                    return new UTF32Encoding(false, true);
                }

                //UTF-16 LE
                return new UnicodeEncoding(false, true);
            }

            if (bytes.Length < 3)
            {
                return null;
            }

            if ((bytes[0] == 0xef) && (bytes[1] == 0xbb) && (bytes[2] == 0xbf))
            {
                //UTF-8
                return new UTF8Encoding(true, true);
            }

            if (bytes.Length < 4)
            {
                return null;
            }

            if ((bytes[0] == 0x00) && (bytes[1] == 0x00) && (bytes[2] == 0xfe) && (bytes[3] == 0xff))
            {
                //UTF-32 BE
                return new UTF32Encoding(true, true);
            }

            return null;
        }
    }
}
