using FileCleaner.Interfaces;
using FileCleaner.Utils;
using System.Text.RegularExpressions;
using System.Text;

namespace FileCleaner.Implementation
{
    public class EFGFormatHandler : IFileFormatHandler<EFGFormatHandler>
    {
        public bool IsFormatValid(byte[] fileBytes)
        {
            if (fileBytes.Length < 6)
                return false;

            string firstThreeBytesDecoded = GeneralUtils.DecodeBytesToString(fileBytes.Take(3).ToArray());
            string lastThreeBytesDecoded = GeneralUtils.DecodeBytesToString(fileBytes.Skip(fileBytes.Length - 3).ToArray());

            return firstThreeBytesDecoded == "567" && lastThreeBytesDecoded == "123";
        }

        public byte[] Sanitize(IFormFile formFile)
        {
            // Regular expression for sanitization check
            var sanitizeRegex = new Regex(@"(A[0-9]C)");

            using (var memoryStream = new MemoryStream())
            {
                // Copy form file to memory stream
                formFile.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();

                // Extract text content from fileBytes
                var txt = Encoding.UTF8.GetString(fileBytes, 3, fileBytes.Length - 3).Trim().Split('\n');

                // StringBuilder to store sanitized bytes
                var sanitizedBytes = new StringBuilder();

                // Copy the first 3 bytes as they are
                sanitizedBytes.Append(Encoding.UTF8.GetString(fileBytes.Take(3).ToArray()));
                sanitizedBytes.AppendLine();

                // Get the last line from txt
                var lastLine = txt[txt.Length - 1];

                // Exclude the last line from further processing
                txt = txt.Take(txt.Length - 1).ToArray();

                // Process each line
                foreach (var line in txt)
                {
                    // Calculate the number of 3-letter blocks in the line
                    var result = new string[line.Trim().Length / 3];

                    // Split the line into 3-letter blocks
                    for (int i = 0; i < result.Length; i++)
                    {
                        result[i] = line.Trim().Substring(i * 3, 3);
                    }

                    // Process each 3-letter block
                    foreach (var item in result)
                    {
                        if (sanitizeRegex.IsMatch(item))
                        {
                            // Add the block to sanitizedBytes if it matches the sanitization pattern
                            sanitizedBytes.Append(item);
                        }
                        else
                        {
                            // Add the default sanitized block (A255C) if it doesn't match
                            sanitizedBytes.Append("A255C");
                        }
                    }

                    sanitizedBytes.AppendLine();
                }

                // Append the last line
                sanitizedBytes.Append(lastLine);

                // Convert the StringBuilder to byte array and return
                return Encoding.UTF8.GetBytes(sanitizedBytes.ToString());
            }
        }
    }
}
