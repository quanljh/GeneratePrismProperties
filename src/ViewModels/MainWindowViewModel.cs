using Ookii.Dialogs.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using static GeneratePrismProperties.IoC;

namespace GeneratePrismProperties
{
    public class MainWindowViewModel : BindableBase
    {
        #region Public Properties

        /// <summary>
        /// The collection of the property list
        /// </summary>
        public ObservableCollection<PropertyModel> PropertyList { get; set; }

        /// <summary>
        /// The type list of the property
        /// </summary>
        public ObservableCollection<string> TypeList { get; set; }

        /// <summary>
        /// The namespace of the cs file
        /// </summary>
        private string _namespace;

        public string Namespace
        {
            get => _namespace;
            set => SetProperty(ref _namespace, value);
        }

        /// <summary>
        /// The class name of the cs file
        /// </summary>
        private string _className;

        public string ClassName
        {
            get => _className;
            set => SetProperty(ref _className, value);
        }


        /// <summary>
        /// The output path of generated file
        /// </summary>
        private string _outputPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        public string OutputPath
        {
            get => _outputPath;
            set => SetProperty(ref _outputPath, value);
        }


        #endregion

        #region Commands

        /// <summary>
        /// Add property command
        /// </summary>
        public ICommand AddCommand { get; set; }

        /// <summary>
        /// Select file path command
        /// </summary>
        public ICommand SelectPathCommand { get; set; }

        /// <summary>
        /// Generate code command
        /// </summary>
        public ICommand GenerateCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindowViewModel()
        {

            PropertyList = new ObservableCollection<PropertyModel>()
            {
                new PropertyModel
                {
                    PropertyType = "string"
                },
                new PropertyModel
                {
                    PropertyType = "string"
                },
                new PropertyModel
                {
                    PropertyType = "string"
                },
                new PropertyModel
                {
                    PropertyType = "string"
                }
            };

            TypeList = new ObservableCollection<string>()
            {
                "string","bool","int","double","DateTime"
            };

            Namespace = Configuration["DefaultNamespace"];
            ClassName = Configuration["DefaultClass"];


            AddCommand = new DelegateCommand(() =>
            {
                PropertyList.Add(new PropertyModel
                {
                    PropertyType = "string"
                });
            });

            SelectPathCommand = new DelegateCommand(() =>
            {
                var folderBrowserDialog = new VistaFolderBrowserDialog()
                {
                    SelectedPath = $@"{OutputPath}\"
                };
                if (folderBrowserDialog.ShowDialog().HasValue)
                {
                    OutputPath = folderBrowserDialog.SelectedPath;
                }
            });

            GenerateCommand = new DelegateCommand(GenerateCode);
        }


        #endregion

        #region Methods

        private void GenerateCode()
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using Prism.Mvvm;");
            sb.AppendLine();
            sb.AppendLine($"namespace {Namespace}");
            sb.AppendLine("{");
            sb.AppendLine($"    public class {ClassName} : BindableBase");
            sb.AppendLine("    {");

            foreach (var propertyModel in PropertyList)
            {
                if (string.IsNullOrWhiteSpace(propertyModel.PropertyType)
                    || string.IsNullOrWhiteSpace(propertyModel.PropertyName))
                {
                    MessageBox.Show("PropertyType or PropertyName cannot be empty.");
                    return;
                }

                var lowercaseName = propertyModel.PropertyName.Substring(0, 1).ToLower() +
                                    propertyModel.PropertyName.Substring(1);

                sb.AppendLine("        /// <summary>");
                sb.AppendLine($"        /// {propertyModel.Discription}");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine($"        private {propertyModel.PropertyType} _{lowercaseName};");
                sb.AppendLine();
                sb.AppendLine($"        public {propertyModel.PropertyType} {propertyModel.PropertyName}");
                sb.AppendLine("        {");
                sb.AppendLine($"            get => _{lowercaseName};");
                sb.AppendLine($"            set => SetProperty(ref _{lowercaseName}, value);");
                sb.AppendLine("        }");
                sb.AppendLine();
            }

            sb.AppendLine("    }");


            var i = 0;
            string filePath = default(string);
            while (true)
            {
                filePath = i == 0 ? $@"{OutputPath}\Property.cs" : $@"{OutputPath}\Property{i}.cs";

                if (File.Exists(filePath))
                    i++;
                else
                    break;
            }

            try
            {
                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //Logger.LogInformation($"Property.cs have been generated successfully at {filePath}");
            MessageBox.Show($"Property.cs have been generated successfully at {filePath}", "Information",
                MessageBoxButton.OK);

            Process.Start(filePath);

            //// Check VS Code is installed
            //var process = Process.Start(new ProcessStartInfo
            //{
            //    UseShellExecute = false,
            //    CreateNoWindow = true,
            //    RedirectStandardOutput = true,
            //    RedirectStandardInput = true,
            //    FileName = "Property.cs",
            //    Arguments = "/C code --version"
            //});

            //if (process == null)
            //    return;

            //// Start and wait for end
            //process.WaitForExit();

            //// If it did not exit...
            //if (!process.HasExited)
            //    // Presume it isn't installed
            //    return;

            //// Get output
            //var codeResponse = process.StandardOutput.ReadToEnd();

            //if (string.IsNullOrWhiteSpace(codeResponse))
            //    return;

            //// Get the output int lines
            //var lines = codeResponse.Split(new[] { "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            //// First line should be version string
            //// If it isn't...
            //if (lines.Length <= 0 || !Version.TryParse(lines[0], out var vsVersion))
            //    // Presume not installed
            //    return;

            //// If we get here, we have got a valid response with a version number
            //// so it is safe to open VS Code with the folder
            //// Open VS Code (on new task otherwise our application doesn't exit unti this does
            //var process2 = Process.Start(new ProcessStartInfo
            //{
            //    // IMPORTANT: If you don't specify UseShellExecute = false
            //    //            and CreateNoWindow = true
            //    //            then our console will never exit until VS Code is closed
            //    UseShellExecute = false,
            //    CreateNoWindow = true,
            //    FileName = "Property.cs",
            //    Arguments = $@"/C code ""{filePath}""",
            //});


        }
    }

    #endregion
}
