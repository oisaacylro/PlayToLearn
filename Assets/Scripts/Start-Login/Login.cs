using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Login : MonoBehaviour
{
    //Login button
    public Button LoginButton;

    //Canvasgroups for both of the UIs 
    public CanvasGroup LoginUICanvas;
    public CanvasGroup UICanvas;

    //Text field for user to enter info
    public TMP_InputField Username;
    public TMP_InputField Password;

    //Error message for wrong input
    public GameObject Error;
    
    void Start()
    {
        //Link login button to the function
        LoginButton.onClick.AddListener(LogIn);
    }

    private void LogIn()
    {
        //check the credentials
        if(CheckCredentials(Username.text, Password.text))
        {
            //correct input. Make sure error is inactive
            Error.gameObject.SetActive(false);
            //log the user in
            StartCoroutine(LogInFade());
        }
        //wrong input
        else
            Error.SetActive(true);

    }

    IEnumerator LogInFade()
    {
        //fade the login ui out
        while (LoginUICanvas.alpha > 0 )
        {
            LoginUICanvas.alpha = UICanvas.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        //load the home screen
        SceneManager.LoadScene("Home");
    }

    //function for checking credentials
    //eventually use this to connect to db and check
    private bool CheckCredentials(string U, string P)
    {
        if (U == "test" && P == "test")
            return true;
        return false;
    }
}
