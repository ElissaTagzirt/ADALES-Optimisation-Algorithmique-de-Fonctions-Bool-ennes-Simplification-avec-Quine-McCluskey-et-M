using Svg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace APPLICATION.Service
{
    internal class ExpressionDrawer
    {
        private string _exeFilePath;

        public ExpressionDrawer()
        {
            var temp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            temp = Path.Combine(temp, "Temp");
            _exeFilePath = Path.Combine(temp, "draw.exe");
        }

        public async Task<string> DrawExpression(string expression)
        {
            await ExtractIfDoesNotExist();

            expression = PrepareExpressoin(expression);

            var psi = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = _exeFilePath,
                Arguments = $"\"{expression}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            var p = new Process
            {
                StartInfo = psi
            };

            p.Start();

            var output = await p.StandardOutput.ReadToEndAsync();
            p.WaitForExit();

            var svgFileName = output.Replace(Environment.NewLine, string.Empty);
            if (!File.Exists(svgFileName))
            {
                MessageBox.Show("Could not find the output of draw.py", "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            var drawingFileName = ConvertSvgToPng(svgFileName);
            File.Delete(svgFileName);
            return drawingFileName;
        }

        private async Task ExtractIfDoesNotExist()
        {
            if(File.Exists(_exeFilePath))
            {
                return;
            }

            var assembly = Assembly.GetExecutingAssembly();
            var resources = assembly.GetManifestResourceNames();
            var exeResourceName = resources.SingleOrDefault(r => r.EndsWith("draw.exe"));
            if(exeResourceName != null)
            {
                using (var stream = assembly.GetManifestResourceStream(exeResourceName))
                {
                    using (var fileStream = File.OpenWrite(_exeFilePath))
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                }
            }
        }

        private string PrepareExpressoin(string expression)
        {
            return expression.Replace("!", "not ")
                             .Replace(".", " & ");
        }

        string ConvertSvgToPng(string filePath)
        {
            var svgDocument = SvgDocument.Open(filePath);
            var bitmap = svgDocument.Draw();
            string outFileName = "drawing.png";
            bitmap.Save(outFileName);
            bitmap.Dispose();
            return outFileName;
        }
    }
}
