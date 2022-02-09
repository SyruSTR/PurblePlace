using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private CakeController _cakeController;
    private Slider _slider;
    private float _startTime;
    private int _timeForCreateCake;
    
    private bool _isStart = false;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
    public void AddTime()
    {
        SetTime(_timeForCreateCake + Mathf.RoundToInt(_timeForCreateCake * 0.25f));

    }
    public void SetTime(int timeForCreateCake)
    {
        _startTime = Time.time;
        _timeForCreateCake = timeForCreateCake;
        _slider.maxValue = _timeForCreateCake;
        _slider.value = _timeForCreateCake;

    }
    public void StartTimer()
    {
        _isStart = true;
        _startTime = Time.time;
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PlayerPrefs.SetInt("LevelNumber", 0);
        }
#endif
        
        if (_isStart)
            if (_slider.minValue < _slider.value)
            {
                _slider.value = _timeForCreateCake - (Time.time - _startTime);
                
            }
            else
            {

                _cakeController.GetComponent<FinishTask>().SetState(FinishTask.FinishState.TimeOut);
                _isStart = false;
                //Destroy(GetComponent<TimeManager>());
            }
    }
}
