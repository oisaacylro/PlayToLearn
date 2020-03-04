using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnBtn : MonoBehaviour
{
    public CanvasGroup HomeUICanvas;
    public CanvasGroup ConsistentUICanvas;
    public CanvasGroup BGCanvas;
    public Button ReturnButton;
    // Start is called before the first frame update
    void Start()
    {
        ReturnButton.onClick.AddListener(Return);
    }

    private void Return()
    {
        StartCoroutine(ReturnFadeEnum());
    }

    IEnumerator ReturnFadeEnum()
    {
        while (HomeUICanvas.alpha > 0)
        {
            ConsistentUICanvas.alpha = BGCanvas.alpha = HomeUICanvas.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        SceneManager.LoadScene("LoginScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
