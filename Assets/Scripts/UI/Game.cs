using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Text playerDisplay;
    public void CallSaveData()
    {
        StartCoroutine(SaveLayerData());
    }

    IEnumerator SaveLayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score", DBManager.score);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("Game saved.");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }

        //DBManager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
