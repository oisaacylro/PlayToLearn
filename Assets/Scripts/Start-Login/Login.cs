using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Login : MonoBehaviour
{
    public Button LoginButton;
    public CanvasGroup LoginUICanvas;
    public TMP_InputField Username;
    public TMP_InputField Password;

    // Start is called before the first frame update
    void Start()
    {
        LoginButton.onClick.AddListener(LogIn);
    }

    private void LogIn()
    {
        Debug.Log("Login Clicked!");

        Debug.Log("Username: " + Username.text);

        Debug.Log("Password: " + Password.text);

    }
}
