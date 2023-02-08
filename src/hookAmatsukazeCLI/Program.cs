//
// AmatsukazeCLIをhook、環境変数設定後に名前からhookを取った本来のAmatsukazeCLIを起動
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace hookAmatsukazeCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] argv = Environment.GetCommandLineArgs();   // コマンド自体も[0]で取得

            int num_i = 0;      // 必要項目の引数位置（-i）
            int num_s = 0;      // 必要項目の引数位置（-s）
            int num_o = 0;      // 必要項目の引数位置（-o）
            string str = "";
            for(int i=1; i < argv.Length; i++)
            {
                //--- 引数はダブルクォートで囲んで順番に取得 ---
                if (i > 1) str += " ";
                str += string.Concat("\"", argv[i], "\"");
                //--- 環境変数に設定する項目位置を取得 ---
                if (argv[i] == "-i" || argv[i] == "--input")
                    num_i = i + 1;
                if (argv[i] == "-s" || argv[i] == "--serviceid")
                    num_s = i + 1;
                if (argv[i] == "-o" || argv[i] == "--output")
                    num_o = i + 1;
            }
            //--- 環境変数に設定 ---
            if (num_i > 0){
                Environment.SetEnvironmentVariable("CLI_IN_PATH", argv[num_i]);
            }
            if (num_s > 0){
                Environment.SetEnvironmentVariable("SERVICE_ID", argv[num_s]);
            }
            if (num_o > 0){
                Environment.SetEnvironmentVariable("CLI_OUT_PATH", argv[num_o]);
            }
            //--- 実行コマンド名取得および設定 ---
            int n = argv[0].LastIndexOf("hook");
            string cmd = argv[0].Remove(n, 4);

            //--- プロセス起動して終了までリダイレクト ---
            var p = new Process();
            p.StartInfo.FileName = cmd;
            p.StartInfo.Arguments = str;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.Start();
            p.OutputDataReceived += (sender, e) => {
                Console.WriteLine(e.Data);
            };
            p.ErrorDataReceived += (sender, e) => {
                Console.Error.WriteLine(e.Data);
            };
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();
        }
    }
}
