using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<PlayerMovement>();
        if (player) {
            ShowWin();
        }
    }

    private void ShowWin() {
        TimeController.instance.CheckHighScore();
        PlayerPrefs.SetFloat("distance", DistanceController.instance.currentDistance);
        PlayerPrefs.SetFloat("bestTime", TimeController.bestTime);
        PlayerPrefs.SetFloat("currentTime", TimeController.instance.currentTime);
        SceneManager.LoadScene("Ending");
    }
}