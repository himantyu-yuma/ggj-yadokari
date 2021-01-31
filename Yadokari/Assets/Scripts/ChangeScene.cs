using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static IEnumerator SceneChange(int sceneNum, int waitSec)
    {
        yield return new WaitForSeconds(waitSec);
        SceneManager.LoadScene(sceneNum);
    }

    public void SceneChangeNoTime(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
