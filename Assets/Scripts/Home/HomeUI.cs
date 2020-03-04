using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HomeUI : MonoBehaviour
{
    public CanvasGroup HomeUICanvas;
    public CanvasGroup ConsistentUICanvas;
    public CanvasGroup BGCanvas;
    // Start is called before the first frame update
    void Start()
    {
        ConsistentUICanvas.alpha = BGCanvas.alpha = HomeUICanvas.alpha =  0;
        InitialFade();

    }

    private void InitialFade()
    {
        StartCoroutine(InitialFadeEnum());
    }

    IEnumerator InitialFadeEnum()
    {
        while (HomeUICanvas.alpha < 1)
        {
            ConsistentUICanvas.alpha = BGCanvas.alpha = HomeUICanvas.alpha += Time.deltaTime * 1f;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
