using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WorldSelectUI : MonoBehaviour
{
    public CanvasGroup WorldSelectUICanvas;
    public Button World1Button;
    public Button World2Button;
    public Button World3Button;
    public Button World4Button;
    public Button ReturnButton;
    private ConsistentObject ConsistentObj;
    public TMP_Text ModeLabel;
    private bool Disable = true;
    // Start is called before the first frame update
    void Start()
    {
        ReturnButton.onClick.AddListener(Return);
        WorldSelectUICanvas.alpha = 0;
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

        switch(ConsistentObj.getSelectedMode())
        {
            case 0:
                ModeLabel.SetText("Adventure");
                break;
            case 1:
                ModeLabel.SetText("Free Play");
                break;
            default:
                break;
        }

        World1Button.onClick.AddListener(WorldSelected1);
        World2Button.onClick.AddListener(WorldSelected2);
        World3Button.onClick.AddListener(WorldSelected3);
        World4Button.onClick.AddListener(WorldSelected4);
        StartCoroutine(InitialFadeEnum());
    }
    private void WorldSelected1()
    {
        WorldSelected(1);
    }
    private void WorldSelected2()
    {
        WorldSelected(2);
    }
    private void WorldSelected3()
    {
        WorldSelected(3);
    }
    private void WorldSelected4()
    {
        WorldSelected(4);
    }

    private void WorldSelected(int i)
    {
        if (!Disable)
        {
            setDisable(true);
            ConsistentObj.setSelectedWorld(i);
            ConsistentObj.setHealth(10);
            StartCoroutine(FadeOutWordSelect());
        }

    }

    IEnumerator FadeOutWordSelect()
    {
        while (WorldSelectUICanvas.alpha > 0)
        {
            WorldSelectUICanvas.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        SceneManager.LoadScene("StageSelect");
    }


    private void Return()
    {
        if (!Disable)
        {
            StartCoroutine(ReturnToHomeFadeEnum());
        }
    }

    IEnumerator ReturnToHomeFadeEnum()
    {
        setDisable(true);
        while (WorldSelectUICanvas.alpha > 0)
        {
           WorldSelectUICanvas.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        SceneManager.LoadScene("Home");
    }

    IEnumerator InitialFadeEnum()
    {
        while (WorldSelectUICanvas.alpha < 1)
        {
            WorldSelectUICanvas.alpha += Time.deltaTime * 1f;
            yield return null;
        }
        setDisable(false);
    }
    
    public void setDisable(bool b)
    {
        Disable = b;
    }
}
