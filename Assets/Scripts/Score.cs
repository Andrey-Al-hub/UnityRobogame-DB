using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int SCORE = -500;
    void Start()
    {
        SCORE = -500;
    }

    void Update()
    {
        if (SCORE == -500) GetComponent<UnityEngine.UI.Text>().text = 0.ToString();
        else GetComponent<UnityEngine.UI.Text>().text = SCORE.ToString();
    }
}
