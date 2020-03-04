using System.Collections;
using UnityEngine.UI;
using UnityEngine;
public class PlayBtn : MonoBehaviour
{
    public CanvasGroup HomeUICanvas;
    public CanvasGroup PlayUICanvas;
    public Button PlayButton;
    private ReturnBtn ReturnBtn;
    private ModeSelect ModeSelect;
    // Start is called before the first frame update
    void Start()
    {
        ReturnBtn = gameObject.GetComponent<ReturnBtn>();
        ModeSelect = gameObject.GetComponent<ModeSelect>();
        PlayButton.onClick.AddListener(Play);
    }

    private void Play()
    {
        ReturnBtn.ChangeState("Play");
        ReturnBtn.BtnDisable();
        StartCoroutine(PlayFadeEnum());
    }

    IEnumerator PlayFadeEnum()
    {

        while (HomeUICanvas.alpha > 0)
        {
            HomeUICanvas.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        HomeUICanvas.gameObject.SetActive(false);
        PlayUICanvas.alpha = 0;
        PlayUICanvas.gameObject.SetActive(true);
        while (PlayUICanvas.alpha < 1)
        {
            PlayUICanvas.alpha += Time.deltaTime * 1f;
            yield return null;
        }
        ModeSelect.BtnEnable();
        ReturnBtn.BtnEnable();
    }
}
