using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RestScript : MonoBehaviour
{
    public CanvasGroup RestUICanvas;
    public Button RestConfirmButton;
    private ConsistentObject ConsistentObj;
    private NodeSpawning ns;
    private bool disable = true;

    // Start is called before the first frame update
    void Start()
    {
        ns = gameObject.GetComponent<NodeSpawning>();
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


        RestConfirmButton.onClick.AddListener(RestConfirmClick);
    }
    
    public void LinkRestNode(Button b)
    {
        b.onClick.AddListener(RestNodeClick);
    }

    private void RestNodeClick()
    {
        ns.nextLevel();
        RestUICanvas.alpha = 0;
        RestUICanvas.gameObject.SetActive(true);
        ConsistentObj.setHealth(10);
        StartCoroutine(RestUIFadeIn());
    }

    private void RestConfirmClick()
    {
        if(!disable)
        {
            disable = true;
            StartCoroutine(RestUIFadeOut());
        }
    }

    IEnumerator RestUIFadeIn()
    {
        while (RestUICanvas.alpha < 1)
        {
            RestUICanvas.alpha += Time.deltaTime;
            yield return null;
        }
        disable = false;
    }

    IEnumerator RestUIFadeOut()
    {
        while (RestUICanvas.alpha > 0)
        {
            RestUICanvas.alpha -= Time.deltaTime;
            yield return null;
        }
        RestUICanvas.gameObject.SetActive(false);
    }
}
