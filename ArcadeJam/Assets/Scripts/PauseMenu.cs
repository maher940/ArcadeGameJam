using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject screenOverlay;

    private bool paused;

    // Use this for initialization
    void Start()
    {
        screenOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
            screenOverlay.SetActive(paused);
        }

        Time.timeScale = paused ? 0.0f : 1.0f;
    }
}
