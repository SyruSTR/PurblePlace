using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Завершение задания
public class FinishTask : MonoBehaviour
{
    public delegate void TaskFinishedHandler();
    public event TaskFinishedHandler TaskFinished;
    [SerializeField] private Image _finishImage;
    [SerializeField] private Sprite[] _finishSprites;
    [SerializeField] private Transform _finishPanel;
    [SerializeField] private Transform _gamePanel;
    [SerializeField] private SceneController _sceneController;
    public enum FinishState
    {
        CakeFinishedCorrect,
        CakeFinishedIncorrect,
        TimeOut
    }

    private FinishState _statePlayer = (FinishState)(-1);
    private bool secondShanceOn = false;

    public void SetState(FinishState state)
    {
        if ((int)_statePlayer < 0)
        {
            _statePlayer = state;
            TaskFinished?.Invoke();

            float animationTime = _finishImage.GetComponent<Animation>().clip.averageDuration;
            _gamePanel.gameObject.SetActive(false);
            switch (_statePlayer)
            {

                case FinishState.CakeFinishedCorrect:
                    int currentLVL = PlayerPrefs.GetInt("LevelNumber");
                    PlayerPrefs.SetInt("LevelNumber", currentLVL + 1);
                    _finishImage.sprite = _finishSprites[0];
                    _sceneController.RestartScene(animationTime);
                    break;
                case FinishState.CakeFinishedIncorrect:
                    _finishImage.sprite = _finishSprites[1];
                    _sceneController.RestartScene(animationTime);
                    break;
                case FinishState.TimeOut:
                    _finishImage.sprite = _finishSprites[2];
                    if (!secondShanceOn)
                        StartCoroutine(InvokePanel(animationTime));
                    else
                        _sceneController.RestartScene(animationTime);
                    break;
            }

            _finishImage.gameObject.SetActive(true);
        }
    }

    private IEnumerator InvokePanel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _finishPanel.gameObject.SetActive(true);
        _finishImage.gameObject.SetActive(false);
        _statePlayer = (FinishState)(-1);
        secondShanceOn = true;
        
    }
}
