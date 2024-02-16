using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour {

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("Gameplay");
    }

    public void OnStartClick() {
        SceneManager.LoadScene("Gameplay");
    }
}
