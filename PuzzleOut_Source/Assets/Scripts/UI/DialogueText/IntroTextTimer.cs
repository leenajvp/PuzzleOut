using System.Collections;
using UnityEngine;

public class IntroTextTimer : MonoBehaviour
{
    [SerializeField] 
    protected GameObject[] uiObjects;

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstRoomCompleted") == 0)
        {
            for (int i = 0; i < uiObjects.Length; i++)
            {
                uiObjects[i].SetActive(false);
            }

            StartCoroutine(TimeDialogues());
        }

        else
        {
            for (int i = 0; i < uiObjects.Length; i++)
            {
                uiObjects[i].SetActive(false);
            }
        }
    }

    private IEnumerator TimeDialogues()
    {
        uiObjects[0].SetActive(true);

        yield return new WaitForSeconds(1f);

        uiObjects[1].SetActive(true);

        yield return new WaitForSeconds(1.5f);

        uiObjects[2].SetActive(true);

        yield return new WaitForSeconds(5);

        uiObjects[3].SetActive(true);

        yield return new WaitForSeconds(3.5f);

        uiObjects[4].SetActive(true);
    }
}
