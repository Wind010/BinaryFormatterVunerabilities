
using CommandLine;

namespace BinaryFormatterVunerabilities
{
    public class Options
    {
        [Option("serialize", HelpText = "Serialize")]
        public string Serialize { get; set; }

        [Option("deserialize", HelpText = "Deserialize")]
        public string Deserialize { get; set; }

        [Option("exploit", HelpText = "Exploit")]
        public string Exploit { get; set; }

        [Option("test", HelpText = "Test")]
        public bool Test { get; set; }
    }
}
