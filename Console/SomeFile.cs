using System;
using System.IO;
using System.Runtime.Serialization;

namespace BinaryFormatterVunerabilities
{
    /// <summary>
    /// This is some class that can have bad consequences.
    /// </summary>
    [Serializable]
    public class SomeFile : ISerializable, IDisposable
    {
        private string _fileName;
        public const string SomeBadScript = "Some bad script.";

        /// <summary>
        /// Execution Point 1
        /// </summary>
        public SomeFile()
        {
            //_fileName = Path.GetTempFileName();
            _fileName = "C:\\constructor.txt";
            File.WriteAllText(_fileName, SomeBadScript);
        }

        // The finalizer/destructor is called non-deterministically by GC.
        ~SomeFile()
        {
            Dispose();
        }

        public FileStream GetStream()
        {
            return new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        /// <summary>
        /// Execution Point 2
        /// </summary>
        public void Dispose()
        {
            try
            {
                _fileName = "C:\\destructor.txt";
                File.WriteAllText(_fileName, SomeBadScript);
            }
            //catch { }
            finally { }
        }

        protected SomeFile(SerializationInfo info, StreamingContext context)
        {
            _fileName = "C:\\constructor2.txt";
            File.WriteAllText(_fileName, SomeBadScript);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _fileName = "C:\\getObjectData.txt";
            File.WriteAllText(_fileName, SomeBadScript);
        }
    }
}
