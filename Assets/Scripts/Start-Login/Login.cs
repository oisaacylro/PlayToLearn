using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public Button LoginButton;
    public CanvasGroup LoginUICanvas;
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
            SceneManager.LoadScene("Home");
        }
        else
            Error.SetActive(true);

    }

    private bool CheckCredentials(string U, string P)
    {
        if (U == "test" && P == "test")
            return true;
        return false;
    }
}
