using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Xander.PasswordValidator.Config
{
  [ConfigurationCollection(typeof(StandardWordListItem), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
  public class StandardWordListCollection : ConfigurationElementCollection, ICollection<StandardWordList>
  {
    public override ConfigurationElementCollectionType CollectionType
    {
      get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((StandardWordListItem) element).Value;
    }
    
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

    public void Add(StandardWordList item)
    {
      var element = new StandardWordListItem();
      element.Value = item;
      base.BaseAdd(element);
    }

    public void Clear()
    {
      BaseClear();
    }

    public bool Contains(StandardWordList item)
    {
      return this.Any(swl => item == swl);
    }

    public void CopyTo(StandardWordList[] array, int arrayIndex)
    {
      if (array == null) 
        throw new ArgumentNullException("array");
      if (arrayIndex < 0) 
        throw new ArgumentOutOfRangeException("arrayIndex", "Cannot be a negative number.");
      if (array.Length < arrayIndex + this.Count)
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

    public bool Remove(StandardWordList item)
    {
      int startCount = Count;
      BaseRemove(item);
      return Count < startCount;
    }

    bool ICollection<StandardWordList>.IsReadOnly
    {
      get { return base.IsReadOnly(); }
    }
  }
}