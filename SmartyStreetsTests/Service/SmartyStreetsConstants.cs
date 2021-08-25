using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsTests.Service
{
    public static class SmartyStreetsConstants
    {
        // Values correspond to each field name in the UI
        public const string Street = "PropertyAddress";
        public const string Street2 = "PropertyAddressLine2";
        public const string City = "City";
        public const string State = "State";
        public const string ZipCode = "Zip";

        public static readonly Dictionary<string, List<string>> InvalidAddressResult =
            new Dictionary<string, List<string>>()
            {
                { Street, new List<string>() { "Address is invalid." } }
            };

        public static readonly Dictionary<string, Tuple<string, string>[]> DpvFootnotes =
            new Dictionary<string, Tuple<string, string>[]>()
            {
                // Street name, city, state, and ZIP are all valid.
                {
                    "AA", null
                },

                // Address is invalid.
                {
                    "A1", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street, "Address is invalid."),
                        Tuple.Create(City, "Address is invalid."),
                        Tuple.Create(State, "Address is invalid."),
                        Tuple.Create(ZipCode, "Address is invalid.")
                    }
                },

                // Entire address is valid.
                {
                    "BB", null
                },

                // The submitted secondary information (apartment, suite, etc.) was not recognized.
                {
                    "CC", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street2, "The submitted secondary information (apartment, suite, etc.) was not recognized.")
                    }
                },

                // Military or diplomatic address
                {
                    "F1", null
                },

                // General delivery address
                {
                    "G1", null
                },

                // Primary number (e.g., house number) is missing.
                {
                    "M1", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street, "Primary number (e.g., house number) is missing.")
                    }
                },

                // Primary number (e.g., house number) is invalid.
                {
                    "M3", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street, "Primary number (e.g., house number) is invalid.")
                    }
                },

                // Address is missing secondary information (apartment, suite, etc.).
                {
                    "N1", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street2, "Address is missing secondary information (apartment, suite, etc.).")
                    }
                },

                // PO Box street style address.
                {
                    "PB", null
                },

                // PO, RR, or HC box number is missing.
                {
                    "P1", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street, "PO, RR, or HC box number is missing.")
                    }
                },

                // PO, RR, or HC box number is invalid.
                {
                    "P3", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street, "PO, RR, or HC box number is invalid.")
                    }
                },

                // Confirmed address with private mailbox (PMB) info.
                {
                    "RR", null
                },

                // Confirmed address without private mailbox (PMB) info.
                {
                    "R1", null
                },

                // Confirmed as a valid address that doesn't currently receive US Postal Service street delivery.
                {
                    "R7", null
                },

                // Address has a "unique" ZIP Code.
                {
                    "U1", null
                },
            };

        public static readonly Dictionary<string, Tuple<string, string>[]> Footnotes =
            new Dictionary<string, Tuple<string, string>[]>()
            {
                // Corrected ZIP Code
                {
                    "A#", null
                },

                // Corrected city/state spelling
                {
                    "B#", null
                },

                // Invalid city/state/ZIP
                {
                    "C#", new Tuple<string, string>[]
                    {
                        Tuple.Create(City, "Invalid city/state/ZIP."),
                        Tuple.Create(State, "Invalid city/state/ZIP."),
                        Tuple.Create(ZipCode, "Invalid city/state/ZIP.")
                    }
                },

                // No ZIP+4 assigned
                {
                    "D#", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street, "No ZIP+4 assigned."),
                        Tuple.Create(City, "No ZIP+4 assigned."),
                        Tuple.Create(State, "No ZIP+4 assigned."),
                        Tuple.Create(ZipCode, "No ZIP+4 assigned.")
                    }
                },

                // Same ZIP for multiple
                {
                    "E#", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street, "Same ZIP for multiple."),
                        Tuple.Create(City, "Same ZIP for multiple."),
                        Tuple.Create(State, "Same ZIP for multiple."),
                        Tuple.Create(ZipCode, "Same ZIP for multiple.")
                    }
                },

                // Address not found
                {
                    "F#", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street, "Address not found.")
                    }
                },

                // Used addressee data
                {
                    "G#", null
                },

                // Missing secondary number
                {
                    "H#", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street2, "Missing secondary number.")
                    }
                },

                // Insufficient/ incorrect address data
                {
                    "I#", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street, "Insufficient/ incorrect address data.")
                    }
                },

                // Dual address
                {
                    "J#", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street, "Dual address.")
                    }
                },

                // Cardinal rule match
                {
                    "K#", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street, "Cardinal rule match.")
                    }
                },

                // Changed address component
                {
                    "L#", null
                },

                // Flagged address for LACSLink
                {
                    "LL#", null
                },

                // Flagged address for LACSLink
                {
                    "LI#", null
                },

                // Corrected street spelling
                {
                    "M#", null
                },

                // Fixed abbreviations
                {
                    "N#", null
                },

                // Multiple ZIP+4; lowest used
                {
                    "O#", null
                },

                // Better address exists
                {
                    "P#", null
                },

                // Unique ZIP match
                {
                    "Q#", null
                },

                // No match; EWS: Match soon
                {
                    "R#", null
                },

                // Unrecognized secondary address
                {
                    "S#", new Tuple<string, string>[]
                    {
                        Tuple.Create(Street2, "Unrecognized secondary address.")
                    }
                },

                // Multiple response due to magnet street syndrome
                {
                    "T#", null
                },

                // Unofficial city name
                {
                    "U#", null
                },

                // Unverifiable city/state
                {
                    "V#", new Tuple<string, string>[]
                    {
                        Tuple.Create(City, "Unverifiable city/state."),
                        Tuple.Create(State, "Unverifiable city/state."),
                    }
                },

                // Invalid delivery address
                {
                    "W#", null
                },

                // Unique ZIP Code
                {
                    "X#", null
                },

                // Military match
                {
                    "Y#", null
                },

                // Matched with ZIPMOVE
                {
                    "Z#", null
                },
            };
    }
}
