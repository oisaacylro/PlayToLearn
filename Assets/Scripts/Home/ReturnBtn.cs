using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnBtn : MonoBehaviour
{
    //The main controller for the return button for the home screen
    //Seperate from main as there are many possible UIs on the home screen

    //All canvas groups linked to ensure proper transitions
    public CanvasGroup HomeUICanvas;
    public CanvasGroup ConsistentUICanvas;
    public CanvasGroup BGCanvas;
    public CanvasGroup PlayUICanvas;

    //The button itself
    public Button ReturnButton;

    //buttons in other UIs
    private ModeSelect ModeSelect;

    //Disable to prevent glitchy UI
    private bool Disable = false;

    //Current state to know which state to transition to
    private string CurrentState;


    void Start()
    {
        //Default state is home
        CurrentState = "Home";

        //Get the buttons of the other UI
        ModeSelect = gameObject.GetComponent<ModeSelect>();

        //Link button to function
        ReturnButton.onClick.AddListener(Return);
    }

    //Return function
    private void Return()
    {
        //Ensure button is not disabled. IE Already running
        if (!Disable)
        {
            //Detects current state and transitions to respective state
            switch (CurrentState)
            {
                case "Home":
                    StartCoroutine(ReturnToStartFadeEnum());
                    break;
                case "Play":
                    StartCoroutine(ReturnToHomeFadeEnum());
                    ChangeState("Home");
                    break;
                default:
                    break;
            }
        }
    }

    //Enumerators to control fading from one state to another



    IEnumerator ReturnToStartFadeEnum()
    {
        BtnDisable();
        while (HomeUICanvas.alpha > 0)
        {
            ConsistentUICanvas.alpha = BGCanvas.alpha = HomeUICanvas.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        SceneManager.LoadScene("Login");
    }

    IEnumerator ReturnToHomeFadeEnum()
    {
        BtnDisable();
        ModeSelect.BtnDisable();
        while (PlayUICanvas.alpha > 0)
        {
            PlayUICanvas.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        PlayUICanvas.gameObject.SetActive(false);
        HomeUICanvas.alpha = 0;
        HomeUICanvas.gameObject.SetActive(true);
        while (HomeUICanvas.alpha < 1)
        {
            HomeUICanvas.alpha += Time.deltaTime * 1f;
            yield return null;
        }
        BtnEnable();
    }

    //Simple functions to allow other Home screen UI to disable the return button

    public void BtnDisable()
    {
       Disable = true;
    }
    public void BtnEnable()
    {
       Disable = false;
    }

    //Allow other UI functions to change the state
    public void ChangeState(string s)
    {
        CurrentState = s;
    }
}
