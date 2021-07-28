using UnityEngine;

public class DifficultySettings : MonoBehaviour
{
    [SerializeField] private GameObject[] easyLevel;
    [SerializeField] private GameObject[] mediumLevel;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Difficulty") == 0)
        {
            for (int i = 0; i < easyLevel.Length; i++)
            {
                easyLevel[i].SetActive(true);
            }

            for (int i = 0; i < mediumLevel.Length; i++)
            {
                mediumLevel[i].SetActive(true);
            }
        }

        if (PlayerPrefs.GetInt("Difficulty") == 1)
        {
            for (int i = 0; i < mediumLevel.Length; i++)
            {
                mediumLevel[i].SetActive(true);
            }

            for (int i = 0; i < easyLevel.Length; i++)
            {
                easyLevel[i].SetActive(false);
            }
        }

        if (PlayerPrefs.GetInt("Difficulty") == 2)
        {
            for (int i = 0; i < mediumLevel.Length; i++)
            {
                mediumLevel[i].SetActive(false);
            }

            for (int i = 0; i < easyLevel.Length; i++)
            {
                easyLevel[i].SetActive(false);
            }
        }

    }
}
