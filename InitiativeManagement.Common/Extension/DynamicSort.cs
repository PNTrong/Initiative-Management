using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeManagement.Common.Extension
{
    public class DynamicSort
    {
        /// <summary>
        /// Used to get method information using refection
        /// </summary>
        private static MethodInfo GetCompareToMethod<T>(T genericInstance, string sortExpression)
        {
            Type genericType = genericInstance.GetType();
            object sortExpressionValue = genericType.GetProperty(sortExpression).GetValue(genericInstance, null);
            Type sortExpressionType = sortExpressionValue.GetType();
            MethodInfo compareToMethodOfSortExpressionType = sortExpressionType.GetMethod("CompareTo", new Type[] { sortExpressionType });

            return compareToMethodOfSortExpressionType;
        }

        public static List<T> Sort<T>(List<T> genericList, string sortExpression, int sortReverser)
        {
            //int sortReverser = sortDirection.ToLower().StartsWith("asc") ? 1 : -1;

            Comparison<T> comparisonDelegate = new Comparison<T>(delegate (T x, T y)
            {
                //Just to get the compare method info to compare the values.
                MethodInfo compareToMethod = GetCompareToMethod<T>(x, sortExpression);

                //Getting current object value.
                object xSortExpressionValue = x.GetType().GetProperty(sortExpression).GetValue(x, null);

                //Getting the previous value.
                object ySortExpressionValue = y.GetType().GetProperty(sortExpression).GetValue(y, null);

                //Comparing the current and next object value of collection.
                object result = compareToMethod.Invoke(xSortExpressionValue, new object[] { ySortExpressionValue });

                // result tells whether the compared object is equal,greater,lesser.
                return sortReverser * Convert.ToInt16(result);
            });

            //here we using the comparison delegate to sort the object by its property
            genericList.Sort(comparisonDelegate);

            return genericList;
        }
    }
}