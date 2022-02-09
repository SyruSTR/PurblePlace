using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake
{
    public Cake(PrefabsBank prefabsBank, int countCakeBases = 1)
    {
        _cakeInfo = new CakeInfo(countCakeBases);
        _cakeOrder = CakeOrder.Base;
        _prefabsBank = prefabsBank;
    }
    private CakeOrder _cakeOrder;
    public delegate void CakeChangeHandler(WhatChangeInCake cakeChanges, int baseNumberWhyChange);
    public event CakeChangeHandler CakeChange;
    public delegate void CakeFinishedHandler();
    public event CakeFinishedHandler CakeFinished;
    private CakeInfo _cakeInfo;
    private PrefabsBank _prefabsBank;
    
        // Торт иммет два состояния очереди, что именно должны быть следующим, Форма или глазурь
    private CakeOrder _CakeOrder
    {
        set
        {
            if ((int)value == 2)
            {
                _cakeOrder = 0;
            }
            else
                _cakeOrder = value;
        }
        get { return _cakeOrder; }
    }

    public int BasesCount { get { return _cakeInfo.cakeBases.Length; } }
    private bool _finish = false;
    public enum CakeOrder
    {
        Base,
        Glaze
    }
    public enum WhatChangeInCake
    {
        Cake,
        Glaze,
        Material,
        Decoration
    }
    public void ChangeState()
    {
        _finish = !_finish;
    }
    public int GetIndexLastCake()
    {
        
        int countNullBase = 0;
        for (int i = 0; i < _cakeInfo.cakeBases.Length; i++)
        {
            if (_cakeInfo.cakeBases[i].cakeBase == null)
            {
                countNullBase++;
            }
        }
        return _cakeInfo.cakeBases.Length - countNullBase - 1;
    }

    public CakePrefabInfo GetCakeBase(int baseNumber = 0)
    {
        return _cakeInfo.cakeBases[baseNumber].cakeBase;
    }
    public Material GetCakeMaterial(int baseNumber = 0)
    {
        return _cakeInfo.cakeBases[baseNumber].cakeMaterial;
    }
    public void PaintCakeBase(int materialNumber)
    {
        if (_finish)
            return;
        if (_cakeOrder == CakeOrder.Glaze)
        {
            int indexLastCake = GetIndexLastCake();
            _cakeInfo.cakeBases[indexLastCake].cakeMaterial = _prefabsBank.GetMaterial(materialNumber);
            CakeChange?.Invoke(WhatChangeInCake.Material, indexLastCake);
        }
    }
    public CakePrefabInfo GetGlazePrefab(int baseNumber = 0)
    {
        return _cakeInfo.cakeBases[baseNumber].glaze;

    }
    public void SetCakeBase(int cakeBaseIndex)
    {
        if (_finish)
            return;
        if (_cakeOrder != CakeOrder.Base)
        {
            Debug.LogWarning("Glaze order");
            return;
        }
        int indexLastCake = GetIndexLastCake() + 1;
        if (indexLastCake >= _cakeInfo.cakeBases.Length)
            return;
        _cakeInfo.cakeBases[indexLastCake].cakeBase = _prefabsBank.GetBasePrefab(cakeBaseIndex);
        CakeChange?.Invoke(WhatChangeInCake.Cake, indexLastCake);
        _CakeOrder++;

    }
    public void AddGlaze(int glazeNumber)
    {
        if (_finish)
            return;
        if (_cakeOrder != CakeOrder.Glaze)
        {
            Debug.LogWarning("Base order");
            return;
        }
        int indexLastCake = GetIndexLastCake();
        _cakeInfo.cakeBases[indexLastCake].glaze = _prefabsBank.GetGlaze(glazeNumber);
        CakeChange?.Invoke(WhatChangeInCake.Glaze, indexLastCake);
        _CakeOrder++;
    }

    public void AddDecoration(int decorationNumber)
    {
        if (_finish)
            return;
        int indexLastCake = GetIndexLastCake();
        _cakeInfo.cakeDecoration = _prefabsBank.GetDecoration(decorationNumber);
        CakeChange?.Invoke(WhatChangeInCake.Decoration, indexLastCake);
        _finish = true;
        CakeFinished?.Invoke();
    }
    public CakePrefabInfo GetDecoration()
    {
        return _cakeInfo.cakeDecoration;
    }
}

public class CakeInfo
{
    public CakeInfo(int countCakeBases)
    {
        cakeBases = new CakeBase[countCakeBases];
    }
    public CakeBase[] cakeBases;
    public CakePrefabInfo cakeDecoration;

}

public struct CakeBase
{
    public CakePrefabInfo cakeBase;
    public CakePrefabInfo glaze;
    public Material cakeMaterial;
}

