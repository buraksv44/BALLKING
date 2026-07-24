using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    [Flags]
    internal enum LogOpts
    {
        kNone                 = 0,     // No logging will occur.

        kLogToFile            = 1,      // Log to the Editor log file.
        kLogToFileAndConsole  = 1 << 2,    // Log to the Editor log file and Console window.
        kLogErrors            = 1 << 3,    // If the exit code is non 0, log to file and console as an Error.

        // The following options have no effect unless one of the Log options above are used.
        // If neither are used, both the command and the output are logged.
        kNoCommand            = 1 << 4,    // Do not log the command, unless there is an error.
        kNoOutput             = 1 << 5,    // Do not log the output, unless there is an error.
        kProcessOutputOnly    = 1 << 6,    // Only log process output, no extra characters or new lines.
        kDefault              = kLogToFile | kLogErrors,
        kVerbose              = kLogToFileAndConsole | kLogErrors,
    }

    internal interface ISystemProcess : IDisposable
    {
        void Start();
        bool WaitForExit(int milliseconds);
        int ExitCode { get; }
        string GetStandardOutputAsString();
        string GetErrorOutputAsString();
    }

    // Launch a tool during the build process.
    // Running a command as admin requires the path to the command and any
    // arguments you pass to it be full paths, not relative paths.  The working
    // directory will be that of the shell, not the current running program.
    internal class ExecuteSystemProcess
    {
        public int exitCode { get; private set; }
        public string output { get; private set; }

        private string command;
        private string args;
        private string workingdir;
        private LogOpts logOpts;
        private ISystemProcess program;

        public ExecuteSystemProcess(
            string command,
            string args = "",
            string workingdir = "",
            LogOpts logOpts = LogOpts.kDefault,
            bool forceAdministrator = false,
            int attempCount = 1)
        {
            // Trying to set exitCode uniquely if we run into a problem running the
            // process. Values around -100 seem to be seldom used return values.
            exitCode = -100;
            output = "";
            this.logOpts = logOpts;
            this.workingdir = workingdir;
            this.command = command;
            this.args = args;

            Execute(forceAdministrator, attempCount);

            if (!HasFlag(logOpts, LogOpts.kProcessOutputOnly))
                output += Environment.NewLine + "Exit code: " + exitCode.ToString();

            if (LogOpts.kNone != logOpts)
                LogOptsOutput();
        }

        private void Execute(bool forceAdministrator, int attemptCount)
        {
            var psi = CreateInfo();
            for (int i = 0; i <= attemptCount; ++i)
            {
                program = (forceAdministrator ? (ISystemProcess) new AdminProgram(psi) : new UserProgram(psi));
                CallProcess();
                if (exitCode == 0)
                    break;

                if (!HasFlag(logOpts, LogOpts.kProcessOutputOnly))
                    output += Environment.NewLine + "Retrying...";
                Thread.Sleep(1000);
            }
        }

        private ProcessStartInfo CreateInfo()
        {
            return new System.Diagnostics.ProcessStartInfo
            {
                FileName = command,
                Arguments = args,
                WorkingDirectory = workingdir,
                CreateNoWindow = true
            };
        }

        private void CallProcess()
        {
            using (program)
            {
                try
                {
                    program.Start();
                    while (!program.WaitForExit(100)) {}

                    if (!HasFlag(logOpts, LogOpts.kProcessOutputOnly))
                    {
                        output +=
                            "Stdout:" + Environment.NewLine + program.GetStandardOutputAsString() + Environment.NewLine +
                            "Stderr:" + Environment.NewLine + program.GetErrorOutputAsString() + Environment.NewLine;
                    }
                    else
                    {
                        output += program.GetStandardOutputAsString();
                    }
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    // Most likely cause: the process could not be found.
                    exitCode = -101;
                }

                try
                {
                    exitCode = program.ExitCode;
                }
                catch (System.InvalidOperationException)
                {
                    // First check to see if the executable even exists!
                    // (Sometimes virus scanners like to quarantine it)
                    if (!File.Exists(command))
                    {
                        exitCode = -103;
                        output += "ERROR: Missing executable: " + command + Environment.NewLine + "Please check your install (also check that your virus scanner hasn't \"quarantined\" this executable)." + Environment.NewLine + Environment.NewLine + output;
                    }
                    else
                    {
                        // According to MSDN docs, We'll get here if the process has not exited (IE: we timed out), which
                        // is the anticipated exception case.  This could also mean that the
                        // process handle was invalid, though I don't know how to distinguish the
                        // two scenarios.  I would expect an invalid process handle would also
                        // cause the statements outside of the try to fail.
                        exitCode = -102;
                    }
                }
            }
        }

        private void LogOptsOutput()
        {
            bool logAsError = (exitCode != 0 && HasFlag(logOpts, LogOpts.kLogErrors) &&
                !HasFlag(logOpts, LogOpts.kProcessOutputOnly));
            string log = "";

            if (logAsError || !HasFlag(logOpts, LogOpts.kNoCommand))
                log += workingdir + "> " + command + " " + args + Environment.NewLine;

            if (logAsError || !HasFlag(logOpts, LogOpts.kNoOutput))
                log += output;

            if (logAsError)
                UnityEngine.Debug.LogError("Failed running command: " + log);
            else if (HasFlag(logOpts, LogOpts.kLogToFileAndConsole))
                UnityEngine.Debug.Log("Ran command: " + log);
            else if (HasFlag(logOpts, LogOpts.kLogToFile))
                Console.WriteLine("Ran command: " + log);     // This will print to the log file, but not the console window.
        }

        public static bool HasFlag(LogOpts optsToTest, LogOpts optsYouWant)
        {
            return optsYouWant == (optsToTest & optsYouWant);
        }

        public static String StringConcat(IEnumerable<string> strings)
        {
            var sb = new System.Text.StringBuilder("");
            foreach (var t in strings)
                sb.Append(t + Environment.NewLine);
            return sb.ToString();
        }
    }

    // Runs a program or command with admin privs.
    // Mimics UnityEditor.Utils.Program with some copy+pasting.
    // Running a command this way requires the path to the command and any
    // arguments you pass to it be full paths, not relative paths. The working
    // directory will be that of the shell, not the current running program.
    internal class AdminProgram : Program, ISystemProcess
    {
        // This will create a file with a .tmp extension, which we can't execute
        // as a batch script.  We use it just to get a unique name.
        private string seedPath;

        // path to a bat file that will run the command.
        private string runnerPath;

        // path to the redirected stdout file.
        private string outPath;

        // path to the redirected stderr file.
        private string errPath;

        // ref to the stream used to write to the runner file.  The stream is created
        // when we create the file.
        StreamWriter runnerWriter;

        public new virtual void Dispose()
        {
            base.Dispose();

            runnerWriter.Dispose();

            File.Delete(seedPath);
            File.Delete(runnerPath);
            File.Delete(outPath);
            File.Delete(errPath);
        }

        public AdminProgram(ProcessStartInfo si)
            : base(si)
        {
            // This will create a file with a .tmp extension, which we can't execute
            // as a batch script.  We use it just to get a unique name.
            seedPath = Path.GetTempFileName();

            runnerPath = seedPath + ".bat";

            // creating the file opens a FileStream to it.  Using will close that stream,
            // and we use a StreamWriter to it later.  We could cache this return here,
            // but StreamWriter.WriteLine is less clunky than FileStream.Write
            runnerWriter = new StreamWriter(File.Create(runnerPath));

            outPath = Path.GetTempFileName();
            errPath = Path.GetTempFileName();
        }

        public new void Start()
        {
            // in order to run as admin, we need to set these flags as follows.
            // This makes it impossible to capture the output directly from the
            // process, either through the Process object or adding output redirect
            // arguments.  Output redirection does work if it's in a batch file,
            // so we create a temporary batch file for our command and then scrape
            // the output after
            var psi = _process.StartInfo;
            psi.UseShellExecute = true;
            psi.Verb = "runas";

            psi.RedirectStandardInput = false;
            psi.RedirectStandardError = false;
            psi.RedirectStandardOutput = false;

            try
            {
                using (runnerWriter)
                {
                    string command =
                        "\"" + psi.FileName  + "\" " + psi.Arguments + " " + "1>" + outPath + " " + "2>" + errPath;
                    runnerWriter.WriteLine(command);
                }
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError(e.ToString());
            }

            psi.FileName = runnerPath;
            psi.Arguments = "";

            base.Start();

            // It makes sense to restore the StartInfo members incase the caller
            // reuses it, but UnityEditor.Utils.Program doesn't do that.  This
            // may be a surprise, but it's a consistent surprise.
        }

        public new string GetStandardOutputAsString()
        {
            return File.ReadAllText(outPath);
        }

        public new string GetErrorOutputAsString()
        {
            return File.ReadAllText(errPath);
        }

        public new bool HasExited
        {
            get
            {
                if (_process == null)
                    throw new InvalidOperationException("You cannot call HasExited before calling Start");
                try
                {
                    return _process.HasExited;
                }
                catch (InvalidOperationException)
                {
                    return true;
                }
            }
        }
    }

    internal class Program : IDisposable
    {
        private ProcessOutputStreamReader _stdout;
        private ProcessOutputStreamReader _stderr;
        private Stream _stdin;
        public Process _process;

        protected Program()
        {
            _process = new Process();
        }

        public Program(ProcessStartInfo si)
            : this()
        {
            _process.StartInfo = si;
        }

        public void Start()
        {
            Start(null);
        }

        public void Start(EventHandler exitCallback)
        {
            if (exitCallback != null)
            {
                _process.EnableRaisingEvents = true;
                _process.Exited += exitCallback;
            }

            _process.StartInfo.RedirectStandardInput = true;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.UseShellExecute = false;

            _process.Start();
            _stdout = new ProcessOutputStreamReader(_process, _process.StandardOutput);
            _stderr = new ProcessOutputStreamReader(_process, _process.StandardError);
            _stdin  = _process.StandardInput.BaseStream;
        }

        public ProcessStartInfo GetProcessStartInfo()
        {
            return _process.StartInfo;
        }

        public void LogProcessStartInfo()
        {
            foreach (string line in RetrieveProcessStartInfo())
            {
                Console.WriteLine(line);
            }
        }

        public List<string> RetrieveProcessStartInfo()
        {
            return _process != null
                ? RetrieveProcessStartInfo(_process.StartInfo)
                : new List<string> {"Failed to retrieve process startInfo"};
        }

        //please dont kill this code.
        private static List<string> RetrieveProcessStartInfo(ProcessStartInfo si)
        {
            List<string> processStartInfo = new List<string> {"Filename: " + si.FileName, "Arguments: " + si.Arguments};

            foreach (DictionaryEntry envVar in si.EnvironmentVariables)
                if (envVar.Key.ToString().StartsWith("MONO"))
                {
                    processStartInfo.Add($"{envVar.Key}: {envVar.Value}");
                }

            int responsefileindex = si.Arguments.IndexOf("Temp/UnityTempFile");
            if (responsefileindex > 0)
            {
                var responsefile = si.Arguments.Substring(responsefileindex);
                processStartInfo.Add($"Responsefile: {responsefile} Contents: ");
                processStartInfo.Add(File.ReadAllText(responsefile));
            }

            return processStartInfo;
        }

        public string GetAllOutput()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("stdout:");
            foreach (var s in GetStandardOutput())
                sb.AppendLine(s);
            sb.AppendLine("stderr:");
            foreach (var s in GetErrorOutput())
                sb.AppendLine(s);
            return sb.ToString();
        }

        public bool HasExited
        {
            get
            {
                if (_process == null)
                    throw new InvalidOperationException("You cannot call HasExited before calling Start");
                try
                {
                    return _process.HasExited;
                }
                catch (InvalidOperationException)
                {
                    return true;
                }
            }
        }

        public int ExitCode
        {
            get { return _process.ExitCode; }
        }

        public int Id
        {
            get { return _process.Id; }
        }

        public void Dispose()
        {
            Kill();
            _process.Dispose();
            _stdin?.Dispose();
            _stdout?.Dispose();
            _stderr?.Dispose();
        }

        public void Kill()
        {
            if (!HasExited)
            {
                _process.Kill();
                _process.WaitForExit();
            }
        }

        public Stream GetStandardInput()
        {
            return _stdin;
        }

        public string[] GetStandardOutput()
        {
            return _stdout.GetOutput();
        }

        public string GetStandardOutputAsString()
        {
            var output = GetStandardOutput();
            return GetOutputAsString(output);
        }

        public string[] GetErrorOutput()
        {
            return _stderr.GetOutput();
        }

        public string GetErrorOutputAsString()
        {
            var output = GetErrorOutput();
            return GetOutputAsString(output);
        }

        private static string GetOutputAsString(string[] output)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var t in output)
                sb.AppendLine(t);
            return sb.ToString();
        }

        private int SleepTimeoutMiliseconds
        {
            get { return 10; }
        }

        public void WaitForExit()
        {
            _process.WaitForExit();
        }

        public bool WaitForExit(int milliseconds)
        {
            return _process.WaitForExit(milliseconds);
        }
    }

    internal class UserProgram : Program, ISystemProcess
    {
        public UserProgram(ProcessStartInfo psi)
            : base(psi)
        {
        }
    }

    internal class ProcessOutputStreamReader : IDisposable
    {
        private readonly Func<bool> hostProcessExited;
        private readonly StreamReader stream;
        internal List<string> lines;
        private Thread thread;

        internal ProcessOutputStreamReader(Process p, StreamReader stream) : this(() => p.HasExited, stream)
        {
        }

        internal ProcessOutputStreamReader(Func<bool> hostProcessExited, StreamReader stream)
        {
            this.hostProcessExited = hostProcessExited;
            this.stream = stream;
            lines = new List<string>();

            thread = new Thread(ThreadFunc);
            thread.Start();
        }

        public void Dispose()
        {
            lock (stream)
            {
                stream.Dispose();
            }
        }

        private void ThreadFunc()
        {
            try
            {
                while (true)
                {
                    string line;
                    lock (stream)
                    {
                        if (stream == null || stream.BaseStream == null)
                            return;
                        line = stream.ReadLine();
                    }

                    if (line == null)
                        return;
                    lock (lines)
                    {
                        lines.Add(line);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // We have had this throw in a run on Katana in what appears to be a case of a very short running
                // process exiting between the check to hostProcessExited() and the call to stream.ReadLine();
                // So catch this case to avoid this from happening again.
                lock (lines)
                {
                    lines.Add("Could not read output because an ObjectDisposedException was thrown.");
                }
            }
        }

        internal string[] GetOutput()
        {
            if (hostProcessExited())
                thread.Join();
            lock (lines)
            {
                return lines.ToArray();
            }
        }
    }
}