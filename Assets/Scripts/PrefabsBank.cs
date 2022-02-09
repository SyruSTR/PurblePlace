using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Коллекция префабов, не более)
public class PrefabsBank : MonoBehaviour
{
    public enum CakeArrayes
    {
        Bases,
        Glazes,
        Decorations,
        Materials,
        BoxDecoration
    }    

    [SerializeField] private CakeMaterialInfo[] _cakeMaterials;
    [SerializeField] private CakePrefabInfo[] _cakeBasesPrefabs;
    [SerializeField] private CakePrefabInfo[] _cakeGlazesPrefabs;
    [SerializeField] private CakePrefabInfo[] _cakeDecorationPrefabs;
    [SerializeField] private GameObject[] _boxDecoration;

    public Material GetMaterial(int materialNumber)
    {
        return _cakeMaterials[materialNumber].material;
    }
    public CakePrefabInfo GetBasePrefab(int baseNumber)
    {
        return _cakeBasesPrefabs[baseNumber];
    }
    public CakePrefabInfo GetGlaze(int glazeNumber)
    {
        return _cakeGlazesPrefabs[glazeNumber];
    }
    public CakePrefabInfo GetDecoration(int decorationNumver)
    {
        return _cakeDecorationPrefabs[decorationNumver];
    }
    public GameObject GetBoxDecoration(int decorationNumber)
    {
       
        return _boxDecoration[decorationNumber];
    }
    public Sprite GetSprite(CakeArrayes type, int spriteNumber)
    {
        switch (type)
        {
            case CakeArrayes.Bases:
                return _cakeBasesPrefabs[spriteNumber].sprite;                
            case CakeArrayes.Glazes:
                return _cakeGlazesPrefabs[spriteNumber].sprite;
            case CakeArrayes.Decorations:
                return _cakeDecorationPrefabs[spriteNumber].sprite;
            case CakeArrayes.Materials:
                return _cakeMaterials[spriteNumber].sprite;                
        }
        return null;
    }
    public int GetArrayLenght(CakeArrayes array)
    {
        switch (array)
        {
            case CakeArrayes.Bases:
                return _cakeBasesPrefabs.Length;
            case CakeArrayes.Glazes:
                return _cakeGlazesPrefabs.Length;
            case CakeArrayes.Decorations:
                return _cakeDecorationPrefabs.Length;
            case CakeArrayes.Materials:
                return _cakeMaterials.Length;
            case CakeArrayes.BoxDecoration:
                return _boxDecoration.Length;
            default:
                return 0;
        }
    }
}
