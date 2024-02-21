using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSR
{
    #region SerializerUtilities Class XML
    /// <summary>
    /// Helper class containing methods used by the <see cref="Serializer">Serializer</see>.
    /// </summary>
    #endregion
    public static class SerializerUtilities
    {
        #region NewlineOperator String XML
        /// <summary>
        /// The raw newline operator, used by <see cref="SerializerUtilities.ComposeNewlineOperator">the compose function</see>. To get an actual newline operator string, compose one with the aforementioned function.
        /// </summary>
        #endregion
        public const string NewlineOperator = "${Newline~Index~}";

        #region ComposeNewlineOperator Method XML
        /// <summary>
        /// Composes a newline operator with the specified index.
        /// </summary>
        /// <param name="Index">The newline operator index.</param>
        /// <returns>The newline operator literal.</returns>
        #endregion
        public static string ComposeNewlineOperator(int Index = 0) {
            return NewlineOperator.Replace("~Index~", ((Index > 0) ? Index.ToString() : String.Empty));
        }
    }
}
