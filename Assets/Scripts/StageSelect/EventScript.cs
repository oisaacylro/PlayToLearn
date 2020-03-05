using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EventScript : MonoBehaviour
{
    public CanvasGroup EventUICanvas;
    public Button EventConfirmButton;
    private NodeSpawning ns;
    private bool disable = true;


    // Start is called before the first frame update
    void Start()
    {
        ns = gameObject.GetComponent<NodeSpawning>();

        EventConfirmButton.onClick.AddListener(EventConfirmClick);
    }

    public void LinkEventNode(Button b)
    {
        b.onClick.AddListener(EventNodeClick);
    }

    private void EventNodeClick()
    {
        ns.nextLevel();
        EventUICanvas.alpha = 0;
        EventUICanvas.gameObject.SetActive(true);
        StartCoroutine(EventUIFadeIn());
    }
    private void EventConfirmClick()
    {
        if (!disable)
        {
            disable = true;
            StartCoroutine(EventUIFadeOut());
        }
    }
    IEnumerator EventUIFadeIn()
    {
        while (EventUICanvas.alpha < 1)
        {
            EventUICanvas.alpha += Time.deltaTime;
            yield return null;
        }
        disable = false;
    }

    IEnumerator EventUIFadeOut()
    {
        while (EventUICanvas.alpha > 0)
        {
            EventUICanvas.alpha -= Time.deltaTime;
            yield return null;
        }
        EventUICanvas.gameObject.SetActive(false);
    }
}
