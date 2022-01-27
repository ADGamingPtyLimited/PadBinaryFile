using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/// <summary>
/// Pad binary file. Copyright (c) AD Gaming Pty Limited. Released under the Apache 2.0 Licence, available at https://www.apache.org/licenses/LICENSE-2.0
/// </summary>
namespace PadBinaryFile
{
    class Program
    {
        static void Main(string[] args)
        {
            // Verify arguments
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: PadBinaryFile.exe <inputfilename> <outputfilename> <length in HEX>");
                return;
            }

            if (!File.Exists(args[0])) { Console.WriteLine($"Error: input file {args[0]} does not exist."); return; }
            UInt32 paddedLength = 0;
            try { paddedLength = Convert.ToUInt32(args[2], 16); } catch (Exception) { Console.WriteLine($"I can't convert {args[2]} to a valid binary number."); return; }


            Console.WriteLine($"Reading {args[0]} and outputting {args[1]} padded to {paddedLength} bytes with 0xFF");

            int numBytesToRead = 0;
            int numBytesRead = 0;
            byte[] bytes = null;
            try
            {
                // Read the input file
                using (FileStream inputStream = new FileStream(args[0], FileMode.Open, FileAccess.Read))
                {
                    // Read the source file into a byte array.
                    numBytesToRead = (int)inputStream.Length;
                    numBytesRead = 0;
                    bytes = new byte[inputStream.Length];
                    while (numBytesToRead > 0)
                    {
                        // Read may return anything from 0 to numBytesToRead.
                        int n = inputStream.Read(bytes, numBytesRead, numBytesToRead);

                        // Break when the end of the file is reached.
                        if (n == 0)
                            break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }
                }

                // Write the output file.
                try
                {
                    using (FileStream outputStream = new FileStream(args[1], FileMode.Create, FileAccess.Write))
                    {
                        // First output the original file contents
                        outputStream.Write(bytes, 0, numBytesRead);

                        // Now add FF bytes to pad it to the requested length
                        for (int i = numBytesRead; i < paddedLength; i++) { outputStream.WriteByte(0xFF); }
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Exception {ex.Message} when creating the output file"); }

            }
            catch (Exception ex) { Console.WriteLine($"Exception {ex.Message} when opening input file"); }

        }

    }
}
