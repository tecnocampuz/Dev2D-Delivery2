using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour {

    [SerializeField]
    private GameObject text;

    void OnEnable() {
        float bestTime = PlayerPrefs.GetFloat("bestTime");
        float currentTime = PlayerPrefs.GetFloat("currentTime");
        text.GetComponent<Text>().text = "Time: " + currentTime.ToString() + "\nBest time: " + bestTime.ToString();
    }

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
