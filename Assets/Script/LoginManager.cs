using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    public Text username;
    public APISystem api;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void savePlayerName()
    {
        if (string.IsNullOrEmpty(username.text))
        {
            Debug.Log("Enter Your Name");
        }
        else
        {
            PlayerPrefs.SetString("username", username.text);
            FindObjectOfType<APISystem>().Register(username.text, username.text, username.text, "");
            Debug.Log(username.text);
            Debug.Log("Player Name : " + PlayerPrefs.GetString("username"));
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
