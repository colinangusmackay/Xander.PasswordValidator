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
  [ConfigurationCollection(typeof(CustomWordListItem), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
  public class CustomWordListCollection : ConfigurationElementCollection, ICollection<string>
  {
    public override ConfigurationElementCollectionType CollectionType
    {
      get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((CustomWordListItem) element).File;
    }

    protected override ConfigurationElement CreateNewElement()
    {
      return new CustomWordListItem();
    }

    IEnumerator<string> IEnumerable<string>.GetEnumerator()
    {
      foreach (var o in this)
      {
        var item = (CustomWordListItem)o;
        yield return item.File;
      }
    }

    public void Add(string item)
    {
      var element = new CustomWordListItem();
      element.File = item;
      base.BaseAdd(element);
    }

    public void Clear()
    {
      BaseClear();
    }

    public bool Contains(string item)
    {
      return this.Any(file => item == file);
    }

    public void CopyTo(string[] array, int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (arrayIndex < 0)
        throw new ArgumentOutOfRangeException("arrayIndex", "Cannot be a negative number.");
      if (array.Length < arrayIndex + this.Count)
        throw new ArgumentException("arrayIndex",
                                    "The number of elements in this collection (" + Count + ") is greater than the available space from index (" + arrayIndex + ") to the end of the destination array (Length=" + array.Length + ").");
      CopyToImpl(array, arrayIndex);
    }

    private void CopyToImpl(string[] array, int arrayIndex)
    {
      int i = arrayIndex;
      foreach (var item in (IEnumerable<string>)this)
      {
        array[i] = item;
        i++;
      }
    }

    public bool Remove(string item)
    {
      int startCount = Count;
      BaseRemove(item);
      return Count < startCount;
    }

    bool ICollection<string>.IsReadOnly
    {
      get { return base.IsReadOnly(); }
    }
  }
}