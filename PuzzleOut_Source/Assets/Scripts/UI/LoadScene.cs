using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadSyncOp());
    }

    private IEnumerator LoadSyncOp()
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync("Level1");

        yield return new WaitForEndOfFrame();
    }
}
