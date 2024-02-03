using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RemoteConfigHelper
{ 
    internal static class HandleConversions
    {
        /// <summary>
        /// this guy converts things properly, handling JArray
        /// </summary>
        /// <param name="valueToConvert">The value that has the undesirable type</param>
        /// <param name="valueToMatchTypeWith">the value that has the desired type</param>
        /// <returns>the converted value</returns>
        internal static object Convert(object valueToConvert, object valueToMatchTypeWith)
        {

            if (valueToConvert is JArray newCollection)
            {
                if (!typeof(IEnumerable<object>).IsAssignableFrom(valueToMatchTypeWith.GetType()))
                {
                    return ConvertList((IList)valueToMatchTypeWith, newCollection);
                }
                return ConvertArray((Array)valueToMatchTypeWith, newCollection);
            }
            return System.Convert.ChangeType(valueToConvert, valueToMatchTypeWith.GetType());
        }

        private static object ConvertList(IList listToConvert, JArray jarray)
        {
            Type listType = listToConvert.GetType();
            // creating a new list that we're gonna add our original values to and return but with the correct type
            IList newList = (IList)Activator.CreateInstance(listType);
            Type elementType = listToConvert[0].GetType();

            foreach (JValue value in jarray)
            {
                newList.Add(System.Convert.ChangeType(value, elementType));
            }

            for (int j = 0; j < newList.Count; ++j)
            {
                listToConvert[j] = newList[j];
            }

            return listToConvert;
        }

        private static object ConvertArray(Array arrayToConvert, JArray jarray)
        {
            Type arrayType = arrayToConvert.GetType().GetElementType();
            // convert each value from the original array and set it again
            for (int i = 0; i < arrayToConvert.Length; ++i)
            {
                var convertedValue = System.Convert.ChangeType(jarray[i], arrayType);
                arrayToConvert.SetValue(convertedValue, i);
            }

            return arrayToConvert;
        }
    }
}