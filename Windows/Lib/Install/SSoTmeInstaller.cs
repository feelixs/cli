using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SSoTme.OST.Lib.Install
{

    [RunInstaller(true)]
    public partial class SSoTmeInstaller : Installer
    {
        public SSoTmeInstaller()
        {
            InitializeComponent();
        }

        public override void Commit(IDictionary savedState)
        {
            String progFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            String finalPath = Path.Combine(progFiles, @"SSoT.me\SSoTmeBetaCLI");
            AddPathSegments(finalPath);
            base.Commit(savedState);
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }

        /// <summary>
        /// Adds an environment path segments (the PATH varialbe).
        /// </summary>
        /// <param name="pathSegment">The path segment.</param>
        public static void AddPathSegments(string pathSegment)
        {
            string allPaths = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
            if (allPaths != null)
            {
                if (!allPaths.Contains(pathSegment))
                {
                    allPaths = pathSegment + "; " + allPaths;
                }
            }
            else allPaths = pathSegment;
            Environment.SetEnvironmentVariable("PATH", allPaths, EnvironmentVariableTarget.Machine);
        }

    }
}
