using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSR
{
    #region Serializer Class XML
    /// <summary>
    /// The class used for serialization.
    /// </summary>
    #endregion
    [Serializable] public class Serializer
    {
        #region AssignOperator String XML
        /// <summary>
        /// Used to parse the value of properties.
        /// </summary>
        #endregion
        public string AssignOperator { get; private set; }

        #region NewlineOperator String XML
        /// <summary>
        /// Marks a newline in the value of a property.
        /// </summary>
        #endregion
        public string NewlineOperator { get; private set; }


        #region Set Method XML
        /// <summary>
        /// Serializes a property.
        /// </summary>
        /// <param name="Identifier">The name of the property.</param>
        /// <param name="Value">The value of the property.</param>
        /// <param name="SerializedString">The pre-existing serialized string to modify / add the serialized property to.</param>
        /// <returns>All the serialized properties (the pre-existing ones + the one that was just serialized) as a string.</returns>
        #endregion
        public string Set(string Identifier, Object Value, string SerializedString = "")
        {
            List<string> ExistingProperties = SerializedString.Split(Environment.NewLine, StringSplitOptions.None).ToList();
            for (int i = 0; i < ExistingProperties.Count; i++)
            {
                if (ExistingProperties[i].StartsWith($"{Identifier.Replace(Environment.NewLine, NewlineOperator)}{AssignOperator}")) {
                    ExistingProperties[i] = $"{Identifier.Replace(Environment.NewLine, NewlineOperator)}{AssignOperator}{Value.ToString().Replace(Environment.NewLine, NewlineOperator)}";
                    return String.Join(Environment.NewLine, ExistingProperties);
                }
            }

            if (ExistingProperties.Count > 0 && (String.IsNullOrEmpty(ExistingProperties[0]) || String.IsNullOrWhiteSpace(ExistingProperties[0]))) ExistingProperties.RemoveAt(0);
            ExistingProperties.Add($"{Identifier.Replace(Environment.NewLine, NewlineOperator)}{AssignOperator}{Value.ToString().Replace(Environment.NewLine, NewlineOperator)}");
            return String.Join(Environment.NewLine, ExistingProperties);
        }

        #region Get Method XML
        /// <summary>
        /// Gets the value of a serialized property.
        /// </summary>
        /// <param name="Identifier">The name of the property.</param>
        /// <param name="SerializedString">The serialized string that contains the property.</param>
        /// <returns>The value of the property.</returns>
        #endregion
        public Object Get(string Identifier, string SerializedString)
        {
            List<string> Properties = SerializedString.Split(Environment.NewLine, StringSplitOptions.None).ToList();
            for (int i = 0; i < Properties.Count; i++)
            {
                if (Properties[i].StartsWith(Identifier.Replace(Environment.NewLine, NewlineOperator))) {
                    return (Properties[i].Remove(0, ($"{Identifier.Replace(Environment.NewLine, NewlineOperator)}{AssignOperator}").Length)).Replace(NewlineOperator, Environment.NewLine);
                }
            }

            return null;
        }


        #region Delete Method XML
        /// <summary>
        /// Deletes a property from the serialized string.
        /// </summary>
        /// <param name="Identifier">The name of the property.</param>
        /// <param name="SerializedString">The serialized string to remove the property from.</param>
        /// <returns>The serialized string.</returns>
        #endregion
        public string Delete(string Identifier, string SerializedString)
        {
            List<string> Properties = SerializedString.Split(Environment.NewLine, StringSplitOptions.None).ToList();
            for (int i = 0; i < Properties.Count; i++)
            {
                if (Properties[i].StartsWith(Identifier.Replace(Environment.NewLine, NewlineOperator))) {
                    Properties.RemoveAt(i);
                    break;
                }
            }

            return String.Join(Environment.NewLine, Properties);
        }



        #region Serializer Constructor XML
        /// <summary>
        /// Initializes the <c>Serializer</c> class.
        /// </summary>
        /// <param name="AssignOperator">Used to parse the value of properties.</param>
        /// <param name="NewlineOperator">Marks a newline in the value of a property.</param>
        #endregion
        public Serializer(string AssignOperator = " = ", string NewlineOperator = @"\n") {
            this.AssignOperator = AssignOperator;
            this.NewlineOperator = NewlineOperator;
        }
    }
}
