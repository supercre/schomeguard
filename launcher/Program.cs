// SuperCreative 홈 PC 보안 점검 — 실행기
// 내장된 SCHomeGuard.ps1을 C:\ProgramData\SCHomeGuard 에 풀어 Windows PowerShell(관리자)로 실행한다.
// ProgramData\SCHomeGuard 는 관리자만 쓸 수 있는 위치라, 주간 자동 갱신 작업도 이 사본을 쓴다.
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SCHomeGuard
{
    static class Program
    {
        const string Title = "SuperCreative 홈 PC 보안 점검";

        [STAThread]
        static int Main(string[] args)
        {
            try
            {
                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SCHomeGuard");
                Directory.CreateDirectory(dir);
                string script = Path.Combine(dir, "SCHomeGuard.ps1");
                using (Stream res = Assembly.GetExecutingAssembly().GetManifestResourceStream("SCHomeGuard.ps1"))
                using (FileStream fs = File.Create(script))
                    res.CopyTo(fs);

                string ps = Path.Combine(Environment.SystemDirectory, @"WindowsPowerShell\v1.0\powershell.exe");
                string extra = string.Join(" ", args.Select(a => "\"" + a.Replace("\"", "") + "\""));
                bool cli = args.Any(a => a.Equals("-Cli", StringComparison.OrdinalIgnoreCase));
                var psi = new ProcessStartInfo(ps,
                    "-NoProfile -ExecutionPolicy Bypass -STA " + (cli ? "" : "-WindowStyle Hidden ") + "-File \"" + script + "\" " + extra)
                {
                    UseShellExecute = false,
                    CreateNoWindow = !cli
                };
                using (Process p = Process.Start(psi))
                {
                    if (cli) { p.WaitForExit(); return p.ExitCode; }
                }
                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("실행하지 못했습니다.\n\n" + e.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
        }
    }
}
