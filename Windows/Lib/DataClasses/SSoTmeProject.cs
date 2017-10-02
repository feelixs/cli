using System;
using System.ComponentModel;
using SassyMQ.SSOTME.Lib.RMQActors;
using System.IO;
using Newtonsoft.Json;
using SassyMQ.Lib.RabbitMQ;
using System.Linq;

namespace SSoTme.OST.Lib.DataClasses
{
    public partial class SSoTmeProject
    {
        public SSoTmeProject()
        {
            this.InitPoco();
        }

        public static void Init()
        {
            var currentProject = TryToLoad(new DirectoryInfo(Environment.CurrentDirectory));
            if (!ReferenceEquals(currentProject, null))
            {
                throw new Exception(String.Format("Project has already been initialized in: {0}", currentProject.RootPath));
            }
            else
            {
                var newProject = new SSoTmeProject();
                newProject.RootPath = Environment.CurrentDirectory;
                newProject.Name = Path.GetFileName(Environment.CurrentDirectory);
                newProject.Save();
                Console.WriteLine("SSoTme Project Created and Initialized Successfully.");
            }
        }

        public void Save()
        {
            this.Save(new DirectoryInfo(this.RootPath));
        }

        private void Save(DirectoryInfo rootDI)
        {
            string projectJson = JsonConvert.SerializeObject(this);
            File.WriteAllText(this.GetProjectFileName(), projectJson);
        }

        protected String GetProjectFileName()
        {
            return GetProjectFI().FullName;
        }
        protected FileInfo GetProjectFI()
        {
            return GetProjectFIAt(new DirectoryInfo(this.RootPath));
        }

        protected static FileInfo GetProjectFIAt(DirectoryInfo rootDI)
        {
            return new FileInfo(Path.Combine(rootDI.FullName, "SSoTmeProject.json"));
        }

        private static SSoTmeProject Load(FileInfo projectFI)
        {
            var projectJson = File.ReadAllText(projectFI.FullName);
            var ssotmeProject = JsonConvert.DeserializeObject<SSoTmeProject>(projectJson);
            ssotmeProject.RootPath = projectFI.Directory.FullName;
            return ssotmeProject;
        }

        public static SSoTmeProject LoadOrFail(DirectoryInfo dirToCheck)
        {
            var proj = TryToLoad(dirToCheck);

            if (ReferenceEquals(proj, null))
            {
                throw new Exception(String.Format("\nSSoTmeProject could not be found in {0}.  \n\nPlease run `>ssotme -init` from the root of your project to initialize the SSoTme Project.", dirToCheck.FullName));
            }
            else return proj;
        }
        public static SSoTmeProject TryToLoad(DirectoryInfo dirToCheck)
        {
            FileInfo projectFI = GetProjectFIAt(dirToCheck);

            if (projectFI.Exists) return SSoTmeProject.Load(projectFI);
            else
            {
                // Try parent
                if (ReferenceEquals(dirToCheck.Parent, null)) return default(SSoTmeProject);
                else return TryToLoad(dirToCheck.Parent);
            }
        }

        private static SSoTmeProject Load(DirectoryInfo rootDI)
        {
            return Load(GetProjectFIAt(rootDI));
        }

        public void AddSetting(string setting)
        {
            var partsOfSetting = setting.SafeToString().Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var settingName = partsOfSetting.FirstOrDefault();
            var settingValue = String.Join(String.Empty, partsOfSetting.Skip(1));

            if (string.IsNullOrEmpty(settingName)) throw new Exception("Settings must be in the format of 'name=value'");
            else
            {
                this.ProjectSettings.Add(new ProjectSetting()
                {
                    Name = settingName,
                    Value = settingValue
                });
                Console.WriteLine("Added Setting: {0}: '{1}'", settingName, settingValue);
            }
        }

        internal void ListSettings()
        {
            Console.WriteLine("\nSETTINGS: ");

            if (this.ProjectSettings.Any())
            {
                foreach (var projectSetting in this.ProjectSettings)
                {
                    Console.WriteLine("    - {0} = {1}", projectSetting.Name, projectSetting.Value);
                }
            }
            else Console.WriteLine("NO settings added to the project yet.");
        }

        public void Describe()
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("======  {0}", this.Name);
            Console.WriteLine("======    {0}", this.RootPath);
            Console.WriteLine("==========================================");

            Console.WriteLine();

            this.ListSettings();

            Console.WriteLine();

            this.ListTranspilers();
        }

        private void ListTranspilers()
        {
            Console.WriteLine("\nTRANSPILERS: ");
            foreach (var projectTranspiler in this.ProjectTranspilers) {
                projectTranspiler.Describe(this);
            }
        }

        public void Install(SSOTMEPayload result)
        {
            string relativePath = this.GetProjectRelativePath(Environment.CurrentDirectory);

            var projectTranspiler = new ProjectTranspiler(relativePath, result);

            this.IntegrateNewTranspiler(projectTranspiler);

            this.Save();
        }

        private string GetProjectRelativePath(String fullPath)
        {
            var relativePathDI = new DirectoryInfo(fullPath);
            var rootPathDI = new DirectoryInfo(this.RootPath);
            var relativePath = relativePathDI.FullName.Substring(rootPathDI.FullName.Length);
            return relativePath.Replace("\\","/");
        }

        public void Rebuild(string buildPath)
        {
            var currentDirectory = Environment.CurrentDirectory;
            try
            {
                var relativePath = this.GetProjectRelativePath(buildPath);
                var matchingProjectTranspilers = this.ProjectTranspilers.Where(wherePT => wherePT.IsAtPath(relativePath));
                foreach (var pt in matchingProjectTranspilers)
                {
                    pt.Rebuild(this);
                }
            }
            finally
            {
                Environment.CurrentDirectory = currentDirectory;
            }
        }

        public void Clean(bool preserveZFS)
        {
            this.Clean(this.RootPath, preserveZFS);
        }

        public void Clean(string cleanPath, bool preserveZFS)
        {
            var currentDirectory = Environment.CurrentDirectory;
            try
            {
                var relativePath = this.GetProjectRelativePath(cleanPath);
                var matchingProjectTranspilers = this.ProjectTranspilers.Where(wherePT => wherePT.IsAtPath(relativePath));
                foreach (var pt in matchingProjectTranspilers)
                {
                    pt.Clean(this, preserveZFS);
                }
            }
            finally
            {
                Environment.CurrentDirectory = currentDirectory;
            }
        }

        public void Rebuild()
        {
            this.Rebuild(this.RootPath);
        }

        private void IntegrateNewTranspiler(ProjectTranspiler projectTranspiler)
        {
            var matchingTranspiler = this.ProjectTranspilers.FirstOrDefault(fodPT => (fodPT.Name == projectTranspiler.Name) && (fodPT.RelativePath == projectTranspiler.RelativePath));
            while (!ReferenceEquals(matchingTranspiler, null))
            {
                this.ProjectTranspilers.Remove(matchingTranspiler);
                matchingTranspiler = this.ProjectTranspilers.FirstOrDefault(fodPT => (fodPT.Name == projectTranspiler.Name) && (fodPT.RelativePath == projectTranspiler.RelativePath));
            }
            this.ProjectTranspilers.Add(projectTranspiler);
        }

        public void RemoveSetting(string setting)
        {
            var partsOfSetting = setting.SafeToString().Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var settingName = partsOfSetting.FirstOrDefault();
            var settingValue = String.Join(String.Empty, partsOfSetting.Skip(1));

            if (string.IsNullOrEmpty(settingName)) throw new Exception("Setting name not provided - unable to remove.");
            else
            {
                var matchingSetting = this.ProjectSettings.FirstOrDefault(fodSetting => fodSetting.Name.Equals(settingName, StringComparison.OrdinalIgnoreCase));
                if (ReferenceEquals(matchingSetting, null)) throw new Exception(String.Format("Can't find matching setting: {0}", settingName));
                else
                {
                    this.ProjectSettings.Remove(matchingSetting);
                    Console.WriteLine("Successfully Removed Setting: {0}: '{1}'", matchingSetting.Name, matchingSetting.Value);
                }
            }
        }
    }
}