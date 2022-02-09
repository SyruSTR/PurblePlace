using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Создание торта для начального экрана, где создаются случайные тортики
public class CreateCakeForStartScene : MonoBehaviour
{
    private CakeCreation _cakeCreation;
    private PrefabsBank _prefabsBank;
    private Cake _cake;
    private System.Random _random;
    private void Awake()
    {
        _cakeCreation = GetComponent<CakeCreation>();
        _prefabsBank = GetComponent<PrefabsBank>();
    }
    // Start is called before the first frame update
    void Start()
    {


        _random = new System.Random();
        StartCoroutine(ChangeCake());
    }

    private IEnumerator ChangeCake()
    {

        int randomCakeLeght = _random.Next(1, 6);
        _cake = new Cake(_prefabsBank, randomCakeLeght);
        _cakeCreation.CakeCreate(_cake);

        for (int i = 0; i < randomCakeLeght; i++)
        {
            if (i == 0)
                _cake.SetCakeBase(0);
            else
                _cake.SetCakeBase(_random.Next(_prefabsBank.GetArrayLenght(PrefabsBank.CakeArrayes.Bases)));
            _cake.PaintCakeBase(_random.Next(_prefabsBank.GetArrayLenght(PrefabsBank.CakeArrayes.Materials)));
            _cake.AddGlaze(_random.Next(_prefabsBank.GetArrayLenght(PrefabsBank.CakeArrayes.Glazes)));
        }
        _cake.AddDecoration(_random.Next(_prefabsBank.GetArrayLenght(PrefabsBank.CakeArrayes.Decorations)));
        yield return new WaitForSeconds(3f);

        var children = GetComponentsInChildren<FallingAnimate>();

        foreach (var child in children)
        {
            Destroy(child.gameObject);
        }

        StartCoroutine(ChangeCake());

    }
    private void Update()
    {
        transform.Rotate(0f, 10f * Time.deltaTime, 0f);
    }
}
