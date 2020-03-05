using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelect : MonoBehaviour
{
    //Controller for mode selection

    //ConsistentObj will be used to store mode information
    public ConsistentObject ConsistentObj;

    //buttons for selection
    public Button AdventureBtn;
    public Button FreePlayBtn;

    //UI for transitioning
    public CanvasGroup PlayUICanvas;
    public CanvasGroup BGCanvas;
    public CanvasGroup ConsistentUICanvas;

    //Return button
    private ReturnBtn ReturnBtn;

    //Disable to prevent glitchy UI
    private bool Disable;



    void Start()
    {
        //Default disable until all objects are transitioned
        Disable = true;
        ReturnBtn = gameObject.GetComponent<ReturnBtn>();

        //Find the consistentobj
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

        //Link buttons to functions
        AdventureBtn.onClick.AddListener(Adventure);
        FreePlayBtn.onClick.AddListener(FreePlay);
    }
    
    //Adventure/Freeplay functions

    private void Adventure()
    {
        if(!Disable)
        {
            ConsistentObj.setSelectedMode(0);
            StartCoroutine(FadeOutHome());
        }
    }

    private void FreePlay()
    {
        if(!Disable)
        {
            ConsistentObj.setSelectedMode(1);
            StartCoroutine(FadeOutHome());
        }
    }

    //Public functions to allow other UI to disable these buttons. Mainly return
    public void BtnDisable()
    {
        Disable = true;
    }
    public void BtnEnable()
    {
        Disable = false;
    }

    //Enumerator to fade out the home screen once a mode is selected
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
