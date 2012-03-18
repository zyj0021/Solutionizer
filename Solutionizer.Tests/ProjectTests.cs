﻿using System.IO;
using NUnit.Framework;
using Solutionizer.ViewModels;

namespace Solutionizer.Tests {
    [TestFixture]
    public class ProjectTests : ProjectTestBase {
        [Test]
        public void CanReadProjectFileWithoutProjectReferences() {
            CopyTestDataToPath("CsTestProject1.csproj", _testDataPath);

            var project = Project.Load(Path.Combine(_testDataPath, "CsTestProject1.csproj"));

            Assert.AreEqual("CsTestProject1", project.Name);
            Assert.AreEqual("CsTestProject1", project.AssemblyName);
            Assert.IsEmpty(project.ProjectReferences);
        }

        [Test]
        public void CanReadProjectFileWithProjectReferences() {
            CopyTestDataToPath("CsTestProject2.csproj", Path.Combine(_testDataPath, "p2"));

            var project = Project.Load(Path.Combine(_testDataPath, "p2", "CsTestProject2.csproj"));

            Assert.AreEqual("CsTestProject2", project.Name);
            Assert.AreEqual("CsTestProject2", project.AssemblyName);
            Assert.AreEqual(1, project.ProjectReferences.Count);
            Assert.AreEqual(1, project.ProjectReferences.Count);
            Assert.AreEqual(Path.Combine(_testDataPath, "p1", "CsTestProject1.csproj"), project.ProjectReferences[0]);
        }
    }
}