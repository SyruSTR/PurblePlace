using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void RestartScene(float timeToRestart)
    {
        StartCoroutine(LoadScene(timeToRestart, 0));
    }
    private IEnumerator LoadScene(float t, int sceneIndex)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(sceneIndex);
    }
}
