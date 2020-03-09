using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditControl : MonoBehaviour
{
    public Text creditText;

    // Start is called before the first frame update
    void Start()
    {
        creditText.text = "Score: " + GameManager.score.ToString();
        creditText.text += "\nHigh Score: " + GameManager.highScore.ToString();
        creditText.text += "\n\nGame By Isaac Torres";
        StartCoroutine("ReturnEvent");
    }

    private IEnumerator ReturnEvent()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // wait half a second
            SceneTraversal.LoadStart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
