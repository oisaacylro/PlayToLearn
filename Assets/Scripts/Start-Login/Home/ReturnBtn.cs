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
    private string CurrentState;


    void Start()
    {
        CurrentState = "Home";
        ReturnButton.onClick.AddListener(Return);
    }

    private void Return()
    {
        switch(CurrentState)
        {
            case "Home":
                StartCoroutine(ReturnToStartFadeEnum());
                break;
            case "Play":
                StartCoroutine(ReturnToHomeFadeEnum());
                break;
            default:
                break;
        }
    }

    IEnumerator ReturnToStartFadeEnum()
    {
        while (HomeUICanvas.alpha > 0)
        {
            ConsistentUICanvas.alpha = BGCanvas.alpha = HomeUICanvas.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        SceneManager.LoadScene("LoginScene");
    }

    IEnumerator ReturnToHomeFadeEnum()
    {
        while(PlayUICanvas.alpha > 0)
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
    }

    public void ChangeState(string s)
    {
        CurrentState = s;
    }
}
