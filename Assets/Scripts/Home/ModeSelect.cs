using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelect : MonoBehaviour
{
    public ConsistentObject ConsistentObj;
    public Button AdventureBtn;
    public Button FreePlayBtn;
    public CanvasGroup PlayUICanvas;
    public CanvasGroup BGCanvas;
    public CanvasGroup ConsistentUICanvas;
    private ReturnBtn ReturnBtn;
    private bool Disable;
    // Start is called before the first frame update
    void Start()
    {
        Disable = true;
        ReturnBtn = gameObject.GetComponent<ReturnBtn>();

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("GameController"))
        {
            switch (g.name)
            {
                case "ConsistentObject":
                    ConsistentObj = g.GetComponent<ConsistentObject>();
                    break;
                default:
                    break;
            }
        }

        AdventureBtn.onClick.AddListener(Adventure);
        FreePlayBtn.onClick.AddListener(FreePlay);
    }
    
    private void Adventure()
    {
        if(!Disable)
        {
            ConsistentObj.setSelectedMode("Adventure");
            StartCoroutine(FadeOutHome());
        }
    }

    private void FreePlay()
    {
        if(!Disable)
        {
            ConsistentObj.setSelectedMode("FreePlay");
            StartCoroutine(FadeOutHome());
        }
    }

    public void BtnDisable()
    {
        Disable = true;
    }
    public void BtnEnable()
    {
        Disable = false;
    }

    IEnumerator FadeOutHome()
    {
        ReturnBtn.BtnDisable();
        while (PlayUICanvas.alpha > 0)
        {
            ConsistentUICanvas.alpha = BGCanvas.alpha = PlayUICanvas.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        SceneManager.LoadScene("WorldSelect");
    }
}
