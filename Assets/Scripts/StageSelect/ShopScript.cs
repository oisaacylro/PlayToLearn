using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ShopScript : MonoBehaviour
{
    public CanvasGroup ShopUICanvas;
    public Button ShopConfirmButton;
    private NodeSpawning ns;
    private bool disable = true;

    // Start is called before the first frame update
    void Start()
    {
        ns = gameObject.GetComponent<NodeSpawning>();

        ShopConfirmButton.onClick.AddListener(ShopConfirmClick);
    }

    public void LinkShopNode(Button b)
    {
        b.onClick.AddListener(ShopNodeClick);
    }

    private void ShopNodeClick()
    {
        ns.nextLevel();
        ShopUICanvas.alpha = 0;
        ShopUICanvas.gameObject.SetActive(true);
        StartCoroutine(ShopUIFadeIn());
    }
    private void ShopConfirmClick()
    {
        if (!disable)
        {
            disable = true;
            StartCoroutine(ShopUIFadeOut());
        }
    }
    IEnumerator ShopUIFadeIn()
    {
        while (ShopUICanvas.alpha < 1)
        {
            ShopUICanvas.alpha += Time.deltaTime;
            yield return null;
        }
        disable = false;
    }

    IEnumerator ShopUIFadeOut()
    {
        while (ShopUICanvas.alpha > 0)
        {
            ShopUICanvas.alpha -= Time.deltaTime;
            yield return null;
        }
        ShopUICanvas.gameObject.SetActive(false);
    }
}
