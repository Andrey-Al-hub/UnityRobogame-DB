using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class FinalScore : MonoBehaviour
{
    IEnumerator SaveData()
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

    }

    //[Serializable]
    //class SaveData
    //{
    //    public int saveScore;
    //}

    void Start()
    {
        if (Score.SCORE > DBManager.score)
        {
            DBManager.score = Score.SCORE;
            StartCoroutine(SaveData());
            GetComponent<UnityEngine.UI.Text>().text = "Ваш счёт: " +
                Score.SCORE.ToString() + "\nНовый рекорд!";
        }
        else GetComponent<UnityEngine.UI.Text>().text = "Ваш счёт: " +
                Score.SCORE.ToString() + "\nРекорд: " + DBManager.score.ToString();
    }

    //int LoadGame()
    //{
    //    if (File.Exists(Application.persistentDataPath
    //      + "/MySaveData.dat"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file =
    //          File.Open(Application.persistentDataPath
    //          + "/MySaveData.dat", FileMode.Open);
    //        SaveData data = (SaveData)bf.Deserialize(file);
    //        file.Close();
    //        return data.saveScore;
    //    }
    //    else
    //        return 0;
    //}

    //void SaveGame(int score)
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create(Application.persistentDataPath
    //      + "/MySaveData.dat");
    //    SaveData data = new SaveData();
    //    data.saveScore = score;
    //    bf.Serialize(file, data);
    //    file.Close();
    //}
}
