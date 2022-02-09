using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Алгоритм создания задания
public class TaskManager : MonoBehaviour
{
    private Cake _taskCake = null;
    [SerializeField] private PrefabsBank _prefabsBank;
    private CakeCreation _cakeCreation;
    [SerializeField] private CakeController _cakeController;
    [SerializeField] private int _level = 1;
    private int difference;
    private BoxesController _boxesController;
    [SerializeField] TimeManager _timeManagerForSlider;
    [SerializeField] private const int _timeFor1Base = 10;

    Dictionary<PrefabsBank.CakeArrayes, int[]> _arraysForTaskGenerate = new Dictionary<PrefabsBank.CakeArrayes, int[]>();
    private void Awake()
    {
        _cakeCreation = GetComponent<CakeCreation>();
        
        _boxesController = GetComponent<BoxesController>();
        _level = PlayerPrefs.GetInt("LevelNumber", 1);
        Debug.Log($"Level: {_level}");
    }
    private void Start()
    {
        difference = _level / 10 + 1;

        if (difference > 5)
            difference = 5;

        _taskCake = new Cake(_prefabsBank, difference);
        //Создания набора загатовок для уровня
        GenerateSet();
        AddMaterialsToBoard();
        //Создаём шаблон задания
        GenerateCake();
        GenerateTimer();
    }

    private void GenerateSet()
    {
        System.Random random = new System.Random();
        //Генерируем ID Материала и добавляем его, если его нет в коллеции уже добавленных.
        //4 цикла, потому что имеем 4 типа материалов Основание\ Глазурь\ Окраска основания\ Декорация (Вишенка на торте)
        for (int i = 0; i < 4; i++)
        {
            List<int> randomMaterialsForCake = new List<int>();

            int countMaterials;
            if (i < 2)
                countMaterials = _level > 2 ? 5 : 3;
            else
                countMaterials = 4;
            
            int arrayLeght = _prefabsBank.GetArrayLenght((PrefabsBank.CakeArrayes)i);
            if (countMaterials > arrayLeght)
                countMaterials = arrayLeght;
            if(i == 0)
                randomMaterialsForCake.Add(0);
            
            do
            {
                int randomNumber;
                if (i == 0)
                {
                    randomNumber = random.Next(1,arrayLeght);                    
                }
                else
                    randomNumber = random.Next(arrayLeght);
                if (randomMaterialsForCake.Contains(randomNumber))
                    continue;
                randomMaterialsForCake.Add(randomNumber);

            } while (randomMaterialsForCake.Count < countMaterials);
            _arraysForTaskGenerate.Add((PrefabsBank.CakeArrayes)i, randomMaterialsForCake.ToArray());

        }
    }
    private void AddMaterialsToBoard()
    {
        _boxesController.AddMaterialInBox<BaseBox>(_arraysForTaskGenerate[PrefabsBank.CakeArrayes.Bases]);
        _boxesController.AddMaterialInBox<GlazeBox>(_arraysForTaskGenerate[PrefabsBank.CakeArrayes.Glazes]);
        _boxesController.AddMaterialInBox<MaterialBox>(_arraysForTaskGenerate[PrefabsBank.CakeArrayes.Materials]);
        _boxesController.AddMaterialInBox<DecorationBox>(_arraysForTaskGenerate[PrefabsBank.CakeArrayes.Decorations]);
    }
    //Из соззданного сета материалов, генерируем торт
    private void GenerateCake()
    {

        _cakeCreation.CakeCreate(_taskCake);
        System.Random random = new System.Random();
        for (int i = 0; i < difference; i++)
        {

            if (i == 0)
                _taskCake.SetCakeBase(0);
            else
                _taskCake.SetCakeBase(_arraysForTaskGenerate[PrefabsBank.CakeArrayes.Bases][random.Next(_arraysForTaskGenerate[0].Length)]);
            _taskCake.PaintCakeBase(_arraysForTaskGenerate[PrefabsBank.CakeArrayes.Materials][random.Next(_arraysForTaskGenerate[PrefabsBank.CakeArrayes.Materials].Length)]);
            _taskCake.AddGlaze(_arraysForTaskGenerate[PrefabsBank.CakeArrayes.Glazes][random.Next(_arraysForTaskGenerate[PrefabsBank.CakeArrayes.Glazes].Length)]);
        }
        _taskCake.AddDecoration(_arraysForTaskGenerate[PrefabsBank.CakeArrayes.Decorations][random.Next(_arraysForTaskGenerate[PrefabsBank.CakeArrayes.Decorations].Length)]);


    }
    public void SendCakeOnCheking()
    {
        _cakeController.CheckCakes(_taskCake);
    }
    public void GenerateTimer()
    {
        int timeForCake = _timeFor1Base * difference - (difference - 1) * (difference - 1);
        _timeManagerForSlider.SetTime(timeForCake);
    }
}
