using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DebugMenu
{
    public class MergeableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public void Merge(Dictionary<TKey, TValue> other)
        {
            foreach (var item in other)
            {
                if(!this.ContainsKey(item.Key))
                {
                    this.Add(item.Key, item.Value);
                }
            }
        }
    }
}
