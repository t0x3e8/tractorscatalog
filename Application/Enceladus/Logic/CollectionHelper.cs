using System.Collections.Generic;
using Enceladus.Api;
using System.Reflection;
using System;
using System.Windows.Forms;

namespace Enceladus
{
    static class CollectionHelper
    {
        public static IList<TractorSearchResult> CopyCollection(IList<TractorSearchResult> sourceCollection, int numberItemToCopy)
        {
            IList<TractorSearchResult> tartgetCollection = new List<TractorSearchResult>(numberItemToCopy);
            for (int i = 0; i < numberItemToCopy; i++)
            {
                tartgetCollection.Add(sourceCollection[i]);
            }

            return tartgetCollection;
        }

        public static IList<TractorSearchResult> SortCollectionByColumn(IList<TractorSearchResult> tractors, DataGridViewColumn column, SortOrder sortMode)
        {
            List<TractorSearchResult> returnList = new List<TractorSearchResult>(tractors.Count);
            returnList.AddRange(tractors);
            PropertyInfo propInfo = typeof(TractorSearchResult).GetProperty(column.DataPropertyName);

            Comparison<TractorSearchResult> compare = delegate(TractorSearchResult a, TractorSearchResult b)
            {
                bool asc = sortMode == SortOrder.Ascending;
                object valueA = asc ? propInfo.GetValue(a, null) : propInfo.GetValue(b, null);
                object valueB = asc ? propInfo.GetValue(b, null) : propInfo.GetValue(a, null);

                if (column.ValueType == typeof(int))
                {
                    int valueAint = int.Parse(valueA.ToString());
                    int valueBint = int.Parse(valueB.ToString());

                    return valueAint.CompareTo(valueBint);
                }
                else

                return valueA is IComparable ? ((IComparable)valueA).CompareTo(valueB) : 0;
            };

            returnList.Sort(compare);
            return returnList;
        }
    }
}
