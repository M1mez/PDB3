using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BIF.SWE2.UnitTests
{
    public abstract class AbstractTestFixture<T>
    {
        private Type _typeToTest;

        #region Setup
        [SetUp]
        public virtual void Setup()
        {
            // I don't know why TestSetup is not called on Jenkins and on some other machines.
            // Call it - ensure typeToTest
            TestSetup();
        }

        private string _workingDirectory;
        public string WorkingDirectory
        {
            get
            {
                if (_workingDirectory == null)
                {
                    _workingDirectory = TestContext.Parameters["targetpath"] ?? System.Environment.CurrentDirectory;
                }
                return _workingDirectory;
            }
        }

        [OneTimeSetUp]
        public virtual void TestSetup()
        {
            if (_typeToTest != null) return;

            Log("Starting tests in {0}", WorkingDirectory);
            Log("Seaching for a class that implements this interface: {0}", typeof(T).FullName);
            foreach (var file in System.IO.Directory.GetFiles(WorkingDirectory, "*.exe").Concat(System.IO.Directory.GetFiles(WorkingDirectory, "*.dll")))
            {
                var fileName = System.IO.Path.GetFileName(file).ToLower();
                // TODO: Bad hack!
                if (fileName == "bif-swe2.unittests.dll" || fileName == "bif-swe2.interfaces.dll") continue;
                Log("Inspecting file {0}", file);
                var assembly = System.Reflection.Assembly.LoadFrom(file);
                Type candidate = null;
                try
                {
                    candidate = assembly.GetTypes().SingleOrDefault(t => t.GetInterfaces().Any(i => i.FullName == typeof(T).FullName));
                }
                catch (System.Reflection.ReflectionTypeLoadException rtlex)
                {
                    Log("ReflectionTypeLoadException while inspecting file: {0}", rtlex.Message);
                    foreach (var le in rtlex.LoaderExceptions)
                    {
                        Log("  {0}", le.Message);
                    }
                }
                catch (Exception ex)
                {
                    Log("WARNING while inspecting file: {0}", ex.Message);
                }
                if (candidate != null)
                {
                    this._typeToTest = candidate;
                    Log("Found a type to test: {0}", candidate);
                    break;
                }
            }

            if (_typeToTest == null)
            {
                throw new InvalidOperationException(string.Format("Unable to find a type that implements {0}", typeof(T).FullName));
            }
        }
        #endregion

        #region Support
        protected T CreateInstance(params object[] parameter)
        {
            Log("Creating instance of type {0} with {1} parameter", _typeToTest.FullName, parameter != null ? parameter.Length : 0);
            return (T)Activator.CreateInstance(_typeToTest, parameter);
        }

        protected void Log(string format, params object[] parameter)
        {
            System.Diagnostics.Trace.WriteLine(string.Format(format, parameter));
            if (System.Console.Out != null)
            {
                System.Console.WriteLine(format, parameter);
            }
        }
        #endregion

        #region SWE2 specific
        protected void EnsureTestImages()
        {
            var path = Path.Combine(WorkingDirectory, Constants.PicturePath);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            Directory.CreateDirectory(path);

            Properties.Resources.Img1.Save(Path.Combine(path, "Img1.jpg"));
            Properties.Resources.Img2.Save(Path.Combine(path, "Img2.jpg"));
            Properties.Resources.Img3.Save(Path.Combine(path, "Img3.jpg"));
            Properties.Resources.Img4.Save(Path.Combine(path, "Img4.jpg"));
            Properties.Resources.Img5.Save(Path.Combine(path, "Img5.jpg"));
        }
        #endregion

        #region Java/C# unification
        [DebuggerHidden]
        protected void AssertNotNull(string msg, object obj)
        {
            Assert.That(obj, Is.Not.Null, msg);
        }

        [DebuggerHidden]
        protected void AssertEquals(object expected, object actual)
        {
            Assert.That(actual, Is.EqualTo(expected));
        }

        [DebuggerHidden]
        protected void AssertEquals(string msg, object expected, object actual)
        {
            Assert.That(actual, Is.EqualTo(expected), msg);
        }

        [DebuggerHidden]
        protected void AssertEquals(string expected, string actual)
        {
            expected = (expected ?? "").ToLower().Replace(" ", "");
            actual = (actual ?? "").ToLower().Replace(" ", "");
            Assert.That(actual, Is.EqualTo(expected));
        }

        [DebuggerHidden]
        protected void AssertEquals(string msg, string expected, string actual)
        {
            expected = (expected ?? "").ToLower().Replace(" ", "");
            actual = (actual ?? "").ToLower().Replace(" ", "");
            Assert.That(actual, Is.EqualTo(expected), msg);
        }

        [DebuggerHidden]
        protected void AssertEmptyOrNull(String str)
        {
            AssertTrue("string is not empty or null", string.IsNullOrWhiteSpace(str));
        }

        [DebuggerHidden]
        protected void AssertNotEmptyOrNull(String str)
        {
            AssertTrue("string is empty or null", !string.IsNullOrWhiteSpace(str));
        }

        [DebuggerHidden]
        protected void AssertTrue(string msg, bool condition)
        {
            Assert.That(condition, Is.True, msg);
        }
        [DebuggerHidden]
        protected void AssertFalse(string msg, bool condition)
        {
            Assert.That(condition, Is.False, msg);
        }
        #endregion
    }
}
