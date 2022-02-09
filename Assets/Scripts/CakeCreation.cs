using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeCreation : MonoBehaviour
{
    Cake _cake;
    private GameObject _cakeGameObject;
    //уменьшение последующих частей торта на N%, стандартно 80%
    [SerializeField] private float _reducingCake = 0.8f;
    private float _cakeHeight = 0f;
    public delegate void ChangeHeightHandler(float newHeight);
    public event ChangeHeightHandler HeightChange;
    [SerializeField] private bool _animateCake = false;
    public void CakeCreate(Cake cake)
    {
        _cake = cake;
        _cake.CakeChange += CakeUpdate;
        _cakeHeight = 0f;
    }

    public void CakeUpdate(Cake.WhatChangeInCake whatChangeInCake, int baseNumber)
    {
        CakePrefabInfo prefabInfo = null;
        switch (whatChangeInCake)
        {
            case Cake.WhatChangeInCake.Cake:
                prefabInfo = _cake.GetCakeBase(baseNumber);
                break;
            case Cake.WhatChangeInCake.Glaze:
                prefabInfo = _cake.GetGlazePrefab(baseNumber);
                break;
            case Cake.WhatChangeInCake.Material:
                if (_cakeGameObject != null)
                    _cakeGameObject.GetComponentInChildren<Renderer>().material = _cake.GetCakeMaterial(baseNumber);
                break;
            case Cake.WhatChangeInCake.Decoration:
                prefabInfo = _cake.GetDecoration();
                break;
        }
        //Если было изменение не для Материалы базы, то мы создаём префаб Основания\ Глазури или Декорации
        if (prefabInfo != null)
        {
            
            float reducing = 1f;
            if (baseNumber > 0)
            {
                //находим нужную степень уменьшения части торта
                reducing = Mathf.Pow(_reducingCake, baseNumber);

            }

            _cakeGameObject = Instantiate(prefabInfo.prefab, new Vector3(transform.position.x, transform.position.y + _cakeHeight, transform.position.z), new Quaternion(0f, 0f, 0f, 0f), transform);

            if (baseNumber > 0)
                _cakeGameObject.transform.localScale *= reducing;
            _cakeHeight += _cakeGameObject.transform.localScale.y;
            if (!_animateCake)
                _cakeGameObject.GetComponent<FallingAnimate>().enabled = false;
            HeightChange?.Invoke(_cakeHeight);
            if (whatChangeInCake == Cake.WhatChangeInCake.Glaze || whatChangeInCake == Cake.WhatChangeInCake.Decoration)
                _cakeGameObject = null;
        }
    }
    
}
