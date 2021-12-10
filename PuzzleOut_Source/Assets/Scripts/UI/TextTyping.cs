using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextTyping : MonoBehaviour
{
    [SerializeField] 
    private float timer = 0.1f;

    [SerializeField] 
    private string partOne;
    [SerializeField] 
    private string ptOneCurrent = "";

    private void Start() => StartCoroutine(IntroTextEntry());

    private IEnumerator IntroTextEntry()
    {
        for (int i = 0; i < partOne.Length; i++)
        {
            ptOneCurrent = partOne.Substring(0, i);
            this.GetComponent<Text>().text = ptOneCurrent;

            yield return new WaitForSeconds(timer);
        }
    }
}
