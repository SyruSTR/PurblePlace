using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GlazeBox : Box, IPointerClickHandler
{
    

    private void Awake()
    {
        StartBox();
    }
    private void OnEnable()
    {
        var childrenSImage = GetComponentsInChildren<Image>();

        for (int i = 0; i < childrenSImage.Length; i++)
        {
            childrenSImage[i].sprite = _prefabsBank.GetSprite(PrefabsBank.CakeArrayes.Glazes, _materialNumber);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _cakeController.AddCakeGlaze(_materialNumber);
    }
}
