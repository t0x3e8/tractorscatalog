﻿using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;
using System.ComponentModel;
using System.Collections;
using System.Reflection;

namespace Enceladus
{
    public class SortableSearchableList : BindingList<TractorSearchResult>
    {
        public SortableSearchableList()
        {

        }

        public override void EndNew(int itemIndex)
        {
            if (sortPropertyValue != null && itemIndex > 0
                && itemIndex == this.Count - 1)
                ApplySortCore(this.sortPropertyValue, this.sortDirectionValue);
            base.EndNew(itemIndex);
        }

        ListSortDirection sortDirectionValue;
        PropertyDescriptor sortPropertyValue;

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortPropertyValue; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return sortDirectionValue; }
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        public void RemoveSort()
        {
            RemoveSortCore();
        }

        private ArrayList sortedList;
        ArrayList unsortedItems;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            sortedList = new ArrayList();

            // Check to see if the property type we are sorting by implements the IComparable interface.
            Type interfaceType = prop.PropertyType.GetInterface("IComparable");

            if (interfaceType != null)
            {
                // If so, set the SortPropertyValue and SortDirectionValue.
                sortPropertyValue = prop;
                sortDirectionValue = direction;

                unsortedItems = new ArrayList(this.Count);

                // Loop through each item, adding it the the sortedItems ArrayList.
                foreach (Object item in this.Items)
                {
                    sortedList.Add(prop.GetValue(item));
                    unsortedItems.Add(item);
                }

                // Call Sort on the ArrayList.
                sortedList.Sort();
                TractorSearchResult temp;

                // Check the sort direction and then copy the sorted items
                // back into the list.
                if (direction == ListSortDirection.Descending)
                    sortedList.Reverse();

                for (int i = 0; i < this.Count; i++)
                {
                    int position = Find(prop.Name, sortedList[i]);
                    if (position != i)
                    {
                        temp = this[i];
                        this[i] = this[position];
                        this[position] = temp;
                    }
                }
                // Raise the ListChanged event so bound controls refresh their
                // values.
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
            else
                // If the property type does not implement IComparable, let the user
                // know.
                throw new NotSupportedException("Cannot sort by " + prop.Name + ". This" +
                    prop.PropertyType.ToString() + " does not implement IComparable");
        }

        protected override void RemoveSortCore()
        {
            int position;
            object temp;
            // Ensure the list has been sorted.
            if (unsortedItems != null)
            {
                // Loop through the unsorted items and reorder the
                // list per the unsorted list.
                for (int i = 0; i < unsortedItems.Count; )
                {
                    position = this.Find(SortPropertyCore.Name,
                        unsortedItems[i].GetType().
                        GetProperty(SortPropertyCore.Name).
                        GetValue(unsortedItems[i], null));
                    if (position >= 0 && position != i)
                    {
                        temp = this[i];
                        this[i] = this[position];
                        this[position] = (TractorSearchResult)temp;
                        i++;
                    }
                    else if (position == i)
                        i++;
                    else
                        // If an item in the unsorted list no longer exists, delete it.
                        unsortedItems.RemoveAt(i);
                }
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        protected override bool SupportsSearchingCore
        {
            get
            {
                return true;
            }
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            // Get the property info for the specified property.
            PropertyInfo propInfo = typeof(TractorSearchResult).GetProperty(prop.Name);
            TractorSearchResult item;

            if (key != null)
            {
                // Loop through the the items to see if the key
                // value matches the property value.
                for (int i = 0; i < Count; ++i)
                {
                    item = (TractorSearchResult)Items[i];
                    if (propInfo.GetValue(item, null).Equals(key))
                        return i;
                }
            }
            return -1;
        }

        public int Find(string property, object key)
        {
            // Check the properties for a property with the specified name.
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(TractorSearchResult));
            PropertyDescriptor prop = properties.Find(property, true);

            // If there is not a match, return -1 otherwise pass search to
            // FindCore method.
            if (prop == null)
                return -1;
            else
                return FindCore(prop, key);
        }
    }
}
