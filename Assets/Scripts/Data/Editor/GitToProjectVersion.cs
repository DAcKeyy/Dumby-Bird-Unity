using System;
using System.Diagnostics;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Data.Editor
{
    public class GitException : InvalidOperationException
    {
        public GitException(int exitCode, string errors) : base(errors) =>
            this.ExitCode = exitCode;

        public readonly int ExitCode;
    }

    public class GitToProjectVersion : MonoBehaviour
    {
        public static string BuildVersion
        {
            get
            {
                var version = Run(@"describe --tags --long --match ""v[0-9]*""");
                // Remove initial 'v' and ending git commit hash.
                version = version.Replace('-', '.');
                version = version.Substring(1, version.LastIndexOf('.') - 1);
                return version;
            }
        }
        
        public static string Branch => Run(@"rev-parse --short HEAD");
        
        public static string Status => Run(@"status --porcelain");
        
        public static string Run(string arguments)
        {
            using (var process = new System.Diagnostics.Process())
            {
                var exitCode = process.Run(@"git", arguments, Application.dataPath,
                    out var output, out var errors);
                if (exitCode == 0)
                {
                    return output;
                }
                else
                {
                    throw new GitException(exitCode, errors);
                }
            }
        }
    }
    
    public static class ProcessExtensions
    {
        public static int Run(this Process process, string application,
            string arguments, string workingDirectory, out string output,
            out string errors )
        {
            process.StartInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                FileName = application,
                Arguments = arguments,
                WorkingDirectory = workingDirectory
            };
            
            var outputBuilder = new StringBuilder();
            var errorsBuilder = new StringBuilder();
            process.OutputDataReceived += (_, args) => outputBuilder.AppendLine(args.Data);
            process.ErrorDataReceived += (_, args) => errorsBuilder.AppendLine(args.Data);
            
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            output = outputBuilder.ToString().TrimEnd();
            errors = errorsBuilder.ToString().TrimEnd();
            return process.ExitCode;
        }
    }
    
    [InitializeOnLoad]
    public static class InitializeOnLoad
    {
        static InitializeOnLoad()
        {
            PlayerSettings.bundleVersion = $"{System.DateTime.Now.Year}.{System.DateTime.Now.Month}.{System.DateTime.Now.Day}.{GitToProjectVersion.Branch}";
            
            EditorApplication.quitting += () =>
            {
                PlayerSettings.bundleVersion = $"{System.DateTime.Now.Year}.{System.DateTime.Now.Month}.{System.DateTime.Now.Day}.{GitToProjectVersion.Branch}";
            };
        }
    }
}
