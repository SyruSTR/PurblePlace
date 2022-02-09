using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    protected CakeController _cakeController;
    protected PrefabsBank _prefabsBank;
    [SerializeField] protected int _materialNumber;
    
    public void SetMaterial(int materialNumber)
    {
        _materialNumber = materialNumber;
    }
    
    protected void StartBox()
    {
        _cakeController = FindObjectOfType<CakeController>();
        _prefabsBank = _cakeController.GetComponent<PrefabsBank>();
    }
}
