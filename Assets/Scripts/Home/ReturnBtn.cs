using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnBtn : MonoBehaviour
{
    public CanvasGroup HomeUICanvas;
    public CanvasGroup ConsistentUICanvas;
    public CanvasGroup BGCanvas;
    public CanvasGroup PlayUICanvas;
    public Button ReturnButton;
    private ModeSelect ModeSelect;
    private bool Disable = false;
    private string CurrentState;


    void Start()
    {
        CurrentState = "Home";
        ModeSelect = gameObject.GetComponent<ModeSelect>();
        ReturnButton.onClick.AddListener(Return);
    }

    private void Return()
    {
        if (!Disable)
        {
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
    public void BtnDisable()
    {
       Disable = true;
    }
    public void BtnEnable()
    {
       Disable = false;
    }
    public void ChangeState(string s)
    {
        CurrentState = s;
    }
}
