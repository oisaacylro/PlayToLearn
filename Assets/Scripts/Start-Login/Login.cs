using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Login : MonoBehaviour
{
    public Button LoginButton;
    public CanvasGroup LoginUICanvas;
    public CanvasGroup UICanvas;
    public TMP_InputField Username;
    public TMP_InputField Password;
    public GameObject Error;
    
    void Start()
    {
        LoginButton.onClick.AddListener(LogIn);
    }

    private void LogIn()
    {
        
        if(CheckCredentials(Username.text, Password.text))
        {
            Error.gameObject.SetActive(false);
            StartCoroutine(LogInFade());
        }
        else
            Error.SetActive(true);

    }

    IEnumerator LogInFade()
    {
        while (LoginUICanvas.alpha > 0 )
        {
            LoginUICanvas.alpha = UICanvas.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        SceneManager.LoadScene("Home");
    }

    private bool CheckCredentials(string U, string P)
    {
        if (U == "test" && P == "test")
            return true;
        return false;
    }
}
