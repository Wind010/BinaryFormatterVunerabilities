using System;

namespace BinaryFormatterVunerabilities
{
    [Serializable]
    public class ExpectedRequest
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
    }
}
