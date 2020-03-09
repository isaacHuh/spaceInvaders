using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTraversal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject sceneManager = GameObject.Find("SceneManager");
        if (sceneManager != this.gameObject) {
            Destroy(sceneManager);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            LoadMain();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneTraversal.LoadStart();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneTraversal.LoadCredits();
        }
    }

    public void LoadMain() {
        SceneManager.LoadScene("MainScreen");
    }

    public static void LoadStart()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public static void LoadCredits()
    {
        SceneManager.LoadScene("CreditScreen");
    }
}
