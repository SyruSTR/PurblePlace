using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseBox : Box, IPointerClickHandler
{


    private void Awake()
    {
        StartBox();
    }

    public void Start()
    {
        var childrenImage = GetComponentInChildren<Image>();

        
        //Из коллекции вытаскиваем нужный нам спрайт
        childrenImage.sprite = _prefabsBank.GetSprite(PrefabsBank.CakeArrayes.Bases, _materialNumber);



    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _cakeController.AddCakeBase(_materialNumber);
    }
}
