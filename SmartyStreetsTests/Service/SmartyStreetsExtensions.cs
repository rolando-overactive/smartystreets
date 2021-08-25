using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartyStreets.USStreetApi;

namespace SmartyStreetsTests.Service
{
    public static class SmartyStreetsExtensions
    {
        /// <summary>
        /// Translates SmartyStreets Status of the Delivery Point Validation (DPV)
        /// Code and Footnotes to messages usable by Redibs.
        /// Reference Documentation: https://www.smartystreets.com/docs/cloud/us-street-api#analysis
        /// </summary>
        /// <param name="analysis">SmartyStreets object containing "Analysis" data from an address.</param>
        /// <returns>Validation result and messages based on the Analysis.</returns>
        public static (bool, Dictionary<string, List<string>>) ToValidationResult(this Analysis analysis)
        {
            var isValid = analysis.DpvMatchCode switch
            {
                // Confirmed; entire address is present in the USPS data.
                "Y" => true,

                // Not confirmed; address is not present in the USPS data.
                "N" => false,

                // Confirmed by ignoring secondary info; the main address is present in the USPS data, but the submitted secondary information (apartment, suite, etc.) was not recognized.
                "S" => true,

                // Confirmed but missing secondary info; the main address is present in the USPS data, but it is missing secondary information (apartment, suite, etc.).
                "D" => false,

                // The address is not present in the USPS database.
                _ => false,
            };

            var messages = new Dictionary<string, List<string>>();

            for (int i = 0; i < analysis.DpvFootnotes.Length; i += 2)
            {
                var dpvFootnoteCode = analysis.DpvFootnotes.Substring(i, 2);
                if (SmartyStreetsConstants.DpvFootnotes.TryGetValue(dpvFootnoteCode, out var dpvFootnoteDescriptions) && dpvFootnoteDescriptions != null)
                {
                    foreach (var dpvFootnoteDescription in dpvFootnoteDescriptions)
                    {
                        if (messages.ContainsKey(dpvFootnoteDescription.Item1))
                            messages[dpvFootnoteDescription.Item1].Add(dpvFootnoteDescription.Item2);
                        else
                            messages.Add(dpvFootnoteDescription.Item1, new List<string> { dpvFootnoteDescription.Item2 });
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(analysis.Footnotes))
            {
                var startFootnoteIndex = 0;
                var finalFootnoteIndex = analysis.Footnotes.IndexOf("#");
                while (finalFootnoteIndex != -1)
                {
                    var footnodeCode = analysis.Footnotes[startFootnoteIndex..(finalFootnoteIndex + 1)];
                    if (SmartyStreetsConstants.Footnotes.TryGetValue(footnodeCode, out var footnoteDescriptions) && footnoteDescriptions != null)
                    {
                        foreach (var footnoteDescription in footnoteDescriptions)
                        {
                            if (messages.ContainsKey(footnoteDescription.Item1))
                                messages[footnoteDescription.Item1].Add(footnoteDescription.Item2);
                            else
                                messages.Add(footnoteDescription.Item1, new List<string> { footnoteDescription.Item2 });
                        }
                    }

                    startFootnoteIndex = finalFootnoteIndex + 1;
                    finalFootnoteIndex = analysis.Footnotes.IndexOf("#", startFootnoteIndex);
                }
            }

            if (isValid && messages.Any())
                isValid = false;

            return (isValid, messages);
        }

    }
}
