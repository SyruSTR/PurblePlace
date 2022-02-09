using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesController : MonoBehaviour
{
    [SerializeField] private Transform[] _boxes;    

    public void AddMaterialInBox<T>(int[] materialIndexes) where T : Box
    {
        //проходим по всем коробкам и высталяем необходимые материалы внутри коробки с коробками
        foreach (var box in _boxes)
        {
            T[] childrens = box.GetComponentsInChildren<T>(true);            
            if (childrens.Length > 0)
            {
                
                for (int i = 0; i < materialIndexes.Length; i++)
                {
                    childrens[i].SetMaterial(materialIndexes[i]);
                    childrens[i].gameObject.SetActive(true);
                }
            }
        }
    }
}
