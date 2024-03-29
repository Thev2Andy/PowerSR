﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PowerSR
{
    #region Serializer Class XML
    /// <summary>
    /// The class used for serialization.
    /// </summary>
    #endregion
    public static class Serializer
    {
        // Private / Inaccessible variables..
        private static readonly Regex NewlineRegex = new Regex(@"\${Newline(\d+)?}", RegexOptions.Compiled);
        private const string AssignOperator = " = ";


        #region Set Method XML
        /// <summary>
        /// Serializes a property.
        /// </summary>
        /// <param name="SerializedString">The pre-existing serialized string to modify / add the serialized property to.</param>
        /// <param name="Identifier">The name of the property.</param>
        /// <param name="Value">The value of the property.</param>
        /// <returns>All the serialized properties (the pre-existing ones + the one that was just serialized) as a string.</returns>
        #endregion
        public static string Set(this string SerializedString, string Identifier, Object Value)
        {
            SerializedString = ((SerializedString != null) ? SerializedString : String.Empty);
            Identifier = ((Identifier != null) ? Identifier : String.Empty);
            Value = ((Value != null) ? Value : String.Empty);

            string NewlineOperator = SerializerUtilities.ComposeNewlineOperator();

            Value = NewlineRegex.Replace(Value.ToString(), new MatchEvaluator((Match) => {
                int CurrentNumber = ((Match.Groups[1].Success) ? int.Parse(Match.Groups[1].Value) : 0);
                int NewNumber = CurrentNumber + 1;
                return SerializerUtilities.ComposeNewlineOperator(NewNumber);
            }));


            List<string> Properties = SerializedString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            string SingleLineIdentifier = Identifier.Replace("\r\n", NewlineOperator).Replace("\n", NewlineOperator);
            string SingleLineValue = Value.ToString().Replace("\r\n", NewlineOperator).Replace("\n", NewlineOperator);
            for (int I = 0; I < Properties.Count; I++)
            {
                if (Properties[I].StartsWith($"{SingleLineIdentifier}{AssignOperator}"))
                {
                    Properties[I] = $"{SingleLineIdentifier}{AssignOperator}{SingleLineValue}";
                    return String.Join(Environment.NewLine, Properties);
                }
            }

            if (Properties.Count > 0 && (String.IsNullOrEmpty(Properties[0]) || String.IsNullOrWhiteSpace(Properties[0]))) Properties.RemoveAt(0);
            Properties.Add($"{SingleLineIdentifier}{AssignOperator}{SingleLineValue}");
            return String.Join(Environment.NewLine, Properties);
        }

        #region Get Method XML
        /// <summary>
        /// Gets the value of a serialized property.
        /// </summary>
        /// <param name="SerializedString">The serialized string that contains the property.</param>
        /// <param name="Identifier">The name of the property.</param>
        /// <returns>The value of the property.</returns>
        #endregion
        public static Object Get(this string SerializedString, string Identifier)
        {
            SerializedString = ((SerializedString != null) ? SerializedString : String.Empty);
            Identifier = ((Identifier != null) ? Identifier : String.Empty);

            string NewlineOperator = SerializerUtilities.ComposeNewlineOperator();

            List<string> Properties = SerializedString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            string SingleLineIdentifier = Identifier.Replace("\r\n", NewlineOperator).Replace("\n", NewlineOperator);
            for (int I = 0; I < Properties.Count; I++)
            {
                if (Properties[I].StartsWith(Identifier.Replace(Environment.NewLine, NewlineOperator)))
                {
                    string Return = Properties[I].Remove(0, ($"{SingleLineIdentifier}{AssignOperator}").Length).Replace(NewlineOperator, Environment.NewLine);
                    Return = NewlineRegex.Replace(Return.ToString(), new MatchEvaluator((Match) => {
                        int CurrentNumber = ((Match.Groups[1].Success) ? int.Parse(Match.Groups[1].Value) : 0);
                        int NewNumber = CurrentNumber - 1;
                        return ((CurrentNumber > 0) ? SerializerUtilities.ComposeNewlineOperator(NewNumber) : Match.Value);
                    }));

                    return Return;
                }
            }

            return null;
        }

        #region Length Method XML
        /// <summary>
        /// Gets the property count of the serialized string.
        /// </summary>
        /// <param name="SerializedString">The serialized string whose properties to count.</param>
        /// <returns>The property count of the serialized string.</returns>
        #endregion
        public static int Length(this string SerializedString) {
            return SerializedString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).Length;
        }


        #region Delete Method XML
        /// <summary>
        /// Deletes a property from the serialized string.
        /// </summary>
        /// <param name="Identifier">The name of the property.</param>
        /// <param name="SerializedString">The serialized string to remove the property from.</param>
        /// <returns>The serialized string.</returns>
        #endregion
        public static string Delete(this string SerializedString, string Identifier)
        {
            SerializedString = ((SerializedString != null) ? SerializedString : String.Empty);
            Identifier = ((Identifier != null) ? Identifier : String.Empty);

            string NewlineOperator = SerializerUtilities.ComposeNewlineOperator();

            List<string> Properties = SerializedString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            string SingleLineIdentifier = Identifier.Replace("\r\n", NewlineOperator).Replace("\n", NewlineOperator);
            for (int I = 0; I < Properties.Count; I++)
            {
                if (Properties[I].StartsWith(SingleLineIdentifier))
                {
                    Properties.RemoveAt(I);
                    break;
                }
            }

            return String.Join(Environment.NewLine, Properties);
        }
    }
}