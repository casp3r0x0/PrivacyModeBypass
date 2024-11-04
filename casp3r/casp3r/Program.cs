using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        // Get the current directory and specify the Python script path
        string currentDirectory = Environment.CurrentDirectory;
        string pythonScriptPath = Path.Combine(currentDirectory, "script.py");

        // Initialize the process to start the Python script
        ProcessStartInfo processInfo = new ProcessStartInfo
        {
            FileName = "python", // or specify full path to python.exe if not in PATH
            Arguments = pythonScriptPath,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true // Run in the background without a visible window
        };

        try
        {
            // Start the process
            using (Process process = Process.Start(processInfo))
            {
                // Optionally read output or errors (for debugging)
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                // Wait for the process to exit if needed
                process.WaitForExit();

                // Check output or errors (optional)
                if (!string.IsNullOrEmpty(output)) Console.WriteLine("Output: " + output);
                if (!string.IsNullOrEmpty(error)) Console.WriteLine("Error: " + error);
            }

            Console.WriteLine("Python script started in the background.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}
