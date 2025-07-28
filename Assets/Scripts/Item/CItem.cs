using UnityEngine;
using System;

public class CItem : Item
{    
    public override GameObject GetCombinedItem(GameObject itemObject)
    {
        throw new NotImplementedException("DItem does not support combination.");
    }
}
