// 슈크인 홈 PC 프로텍터(SCHomeGuard) — 실행기
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
        static Version ScriptVersion(string text)
        {
            var m = System.Text.RegularExpressions.Regex.Match(text, @"^\$Version\s*=\s*'(\d+(\.\d+){1,3})", System.Text.RegularExpressions.RegexOptions.Multiline);
            Version v;
            return m.Success && Version.TryParse(m.Groups[1].Value, out v) ? v : new Version(0, 0);
        }

        const string Title = "슈크인 홈 PC 프로텍터";

        [STAThread]
        static int Main(string[] args)
        {
            try
            {
                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SCHomeGuard");
                Directory.CreateDirectory(dir);
                string script = Path.Combine(dir, "SCHomeGuard.ps1");
                // 내장 스크립트는 '설치본이 없거나 더 오래됐을 때만' 푼다.
                // 자동 업데이트(서명 확인)로 받은 더 새 설치본을 옛 내장본으로 덮어쓰지 않기 위해서다.
                byte[] embedded;
                using (Stream res = Assembly.GetExecutingAssembly().GetManifestResourceStream("SCHomeGuard.ps1"))
                using (var ms = new MemoryStream()) { res.CopyTo(ms); embedded = ms.ToArray(); }
                Version embVer = ScriptVersion(System.Text.Encoding.UTF8.GetString(embedded));
                Version instVer = File.Exists(script) ? ScriptVersion(File.ReadAllText(script)) : new Version(0, 0);
                if (embVer >= instVer) File.WriteAllBytes(script, embedded);

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
