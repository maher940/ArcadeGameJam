using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed;
    [SerializeField]
    private float waitDuration;
    public static RoundManager instance;
    private Text roundText;

    public int Round
    {
        get;
        private set;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            roundText = GetComponent<Text>();
            roundText.color = new Color(roundText.color.r, roundText.color.g, roundText.color.b, 0.0f);
            NextRound();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void NextRound()
    {
        Round++;
        roundText.text = "Round " + Round;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        Color startColor = roundText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);
        float time = 0.0f;
        bool done = false;

        while (!done)
        {
            roundText.color = Color.Lerp(startColor, endColor, time);
            time += fadeSpeed;

            if (time >= 1.0f)
            {
                time = 0.0f;
                if (endColor.a >= 1.0f)
                {
                    yield return new WaitForSeconds(waitDuration);
                    startColor = roundText.color;
                    endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
                }
                else
                {
                    done = true;
                }
            }
            yield return new WaitForSeconds(0.0f);
        }
    }
}
