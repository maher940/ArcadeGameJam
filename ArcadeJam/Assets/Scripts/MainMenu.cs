using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Text pressEnterText;
    [SerializeField]
    private float flickerSpeed;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(TextFlicker());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator TextFlicker()
    {
        Color normalColor = pressEnterText.color;

        while (true)
        {
            pressEnterText.color = Color.black;
            yield return new WaitForSeconds(flickerSpeed);
            pressEnterText.color = normalColor;
            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}
