
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Return : MonoBehaviour
{
    public CanvasGroup ReturnConfirmationCanvas;
    public CanvasGroup StageSelectUI;
    public CanvasGroup StageSelectNodes;

    public Button returnButton;
    public Button yesButton;
    public Button noButton;

    // Start is called before the first frame update
    void Start()
    {
        yesButton.onClick.AddListener(yesBtn);
        noButton.onClick.AddListener(noBtn);
        returnButton.onClick.AddListener(returnBtn);
    }
    
    private void returnBtn()
    {
        ReturnConfirmationCanvas.gameObject.SetActive(true);
    }

    private void noBtn()
    {
        ReturnConfirmationCanvas.gameObject.SetActive(false);
    }

    public void yesBtn()
    {
        ReturnConfirmationCanvas.gameObject.SetActive(false);
        StartCoroutine(FadeOutStageSelect());
    }

    IEnumerator FadeOutStageSelect()
    {
        while (StageSelectUI.alpha > 0)
        {
            StageSelectUI.alpha = StageSelectNodes.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        SceneManager.LoadScene("WorldSelect");
    }

}
