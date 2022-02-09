using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CakeController : MonoBehaviour
{

    private PrefabsBank _prefabsBank;
    private CakeCreation _cakeCreation;
    private FinishingCake _finishingCake;
    private FinishTask _finishTask;

    public UnityEvent Chacked;

    private Cake _cake = null;


    private void Awake()
    {
        _prefabsBank = GetComponent<PrefabsBank>();
        _cakeCreation = GetComponent<CakeCreation>();
        _finishingCake = GetComponent<FinishingCake>();
        _finishTask = GetComponent<FinishTask>();
    }
    private void Start()
    {
        _cake = new Cake(_prefabsBank, 5);
        _cakeCreation.CakeCreate(_cake);

        _cake.CakeFinished += CheckingCake;
        _finishTask.TaskFinished += _cake.ChangeState;
    }
    private void CheckingCake()
    {
        Chacked.Invoke();
    }
    public void ChangeCakeState()
    {
        _cake.ChangeState();
    }

    public void AddCakeBase(int numberCakeBase)
    {
        _cake.SetCakeBase(numberCakeBase);
    }

    public void AddCakeMaterial(int materialNumber)
    {
        _cake.PaintCakeBase(materialNumber);
    }
    public void AddCakeGlaze(int glazeNumber)
    {
        _cake.AddGlaze(glazeNumber);
    }
    public void AddCakeDecoration(int decorationNumber)
    {
        _cake.AddDecoration(decorationNumber);
    }

    public void CheckCakes(Cake taskCake)
    {
        _finishTask.SetState(_finishingCake.CheckCakes(_cake, taskCake));
    }
}
