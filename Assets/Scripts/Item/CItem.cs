using UnityEngine;
using System;
using Unity.VisualScripting;

public class CItem : Item
{
    public override GameObject GetCombinedItem(GameObject itemObject)
    {
        // throw new NotImplementedException("DItem does not support combination.");
        if (itemObject.GetComponent<Item>().itemName ==
             canInteractItemList[0].GetComponent<Item>().itemName) return combinedItemPrefabList[0];
        return null;
    }
    public override void ItemUniqueEffect()
    {
        Debug.Log("C 아이템 자체 발광 시작");

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && renderer.material.HasProperty("_EmissionColor"))
        {
            Material mat = renderer.material;
            mat.EnableKeyword("_EMISSION"); // Emission 키워드 활성화
            mat.SetColor("_EmissionColor", new Color(1f, 0.5f, 0f) * 1.5f); // 주황빛 + 강도
        }

        gameObject.AddComponent<FlickeringLights>();  // FlickeringLights는 별도 구현 필요
    }
}

    
    
