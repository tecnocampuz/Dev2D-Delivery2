using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    public float movementHorizontal { get; private set; }
    public float movementVertical { get; private set; }
    public bool sneak { get; private set; } = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("Gameplay");
        movementHorizontal = Input.GetAxis("Horizontal");
        movementVertical = Input.GetAxis("Vertical");
        sneak = Input.GetKey(KeyCode.LeftShift);
    }
}
