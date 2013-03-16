#region Copyright notice
/******************************************************************************
 * Copyright (C) 2013 Colin Angus Mackay
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 * IN THE SOFTWARE.
 * 
 ******************************************************************************
 *
 * For more information visit: 
 * https://github.com/colinangusmackay/Xander.PasswordValidator
 * 
 *****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Xander.PasswordValidator.Config
{
  /// <summary>
  /// Represents a collection of standard list elements in the configuration file.
  /// </summary>
  [ConfigurationCollection(typeof(StandardWordListItem), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
  public class StandardWordListCollection : ConfigurationElementCollection, ICollection<StandardWordList>
  {
    /// <summary>
    /// Specifies the type of the collection.
    /// </summary>
    public override ConfigurationElementCollectionType CollectionType
    {
      get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
    }

    /// <summary>
    /// Gets the element key, in this case the value from the <see cref="StandardWordList"/> enum.
    /// </summary>
    /// <param name="element">The element for which to get the key/standard word list.</param>
    /// <returns>The standard word list.</returns>
    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((StandardWordListItem) element).Value;
    }

    /// <summary>
    /// Creates a new <see cref="StandardWordListItem"/>
    /// </summary>
    /// <returns>A newly created <see cref="StandardWordListItem"/>.</returns>
    protected override ConfigurationElement CreateNewElement()
    {
      return new StandardWordListItem();
    }

    IEnumerator<StandardWordList> IEnumerable<StandardWordList>.GetEnumerator()
    {
      foreach (var o in this)
      {
        var item = (StandardWordListItem)o;
        yield return item.Value;
      }
    }

    /// <summary>
    /// Adds a <see cref="StandardWordListItem"/> to the collection.
    /// </summary>
    /// <param name="item">The <see cref="StandardWordList"/> enum representing the item to 
    /// add</param>
    public void Add(StandardWordList item)
    {
      var element = new StandardWordListItem();
      element.Value = item;
      BaseAdd(element);
    }

    /// <summary>
    /// Removes all configuration elements from the collection.
    /// </summary>
    public void Clear()
    {
      BaseClear();
    }

    /// <summary>
    /// Determines whther an element is in the collection.
    /// </summary>
    /// <param name="item">The object to locate in this collection.</param>
    /// <returns>True if the item is found; otherwise false.</returns>
    public bool Contains(StandardWordList item)
    {
      return this.Any(swl => item == swl);
    }

    /// <summary>
    /// Copies the collection, or a portion of it, to an array.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination
    /// of the elements copies from this collection.</param>
    /// <param name="arrayIndex">The zero based index in the array at which copying begins.</param>
    /// <exception cref="ArgumentNullException">array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less thatn zero.</exception>
    /// <exception cref="ArgumentException">The number of elements in this collection is greater
    /// thatn the available space from arrayIndex to the end of the array.</exception>
    public void CopyTo(StandardWordList[] array, int arrayIndex)
    {
      if (array == null) 
        throw new ArgumentNullException("array");
      if (arrayIndex < 0) 
        throw new ArgumentOutOfRangeException("arrayIndex", "Cannot be a negative number.");
      if (array.Length < arrayIndex + Count)
        throw new ArgumentException("arrayIndex",
                                    "The number of elements in this collection ("+Count+") is greater than the available space from index ("+arrayIndex+") to the end of the destination array (Length="+array.Length+").");
      CopyToImpl(array, arrayIndex);
    }

    private void CopyToImpl(StandardWordList[] array, int arrayIndex)
    {
      int i = arrayIndex;
      foreach (var item in (IEnumerable<StandardWordList>) this)
      {
        array[i] = item;
        i++;
      }
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from this collection
    /// </summary>
    /// <param name="item">The item to remove.</param>
    /// <returns>true is the item is successfully removed; otherwise false. This method
    /// also returns false if the item was not found in the collection.</returns>
    public bool Remove(StandardWordList item)
    {
      int startCount = Count;
      BaseRemove(item);
      return Count < startCount;
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
    /// </summary>
    /// <returns>
    /// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
    /// </returns>
    bool ICollection<StandardWordList>.IsReadOnly
    {
      get { return base.IsReadOnly(); }
    }
  }
}