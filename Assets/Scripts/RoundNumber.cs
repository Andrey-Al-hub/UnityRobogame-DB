using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RoundNumber : MonoBehaviour
{
    void Start()
    {
        GameManager.round = 1;
    }
    void Update()
    {
        if (GameManager.round != 1) GetComponent<UnityEngine.UI.Text>().text = "Раунд " + (GameManager.round - 1).ToString();
    }
}
