using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HomeUI : MonoBehaviour
{
    //Controls the main UI of the home screen

    //canvasgroups that will be used on this screen
    public CanvasGroup HomeUICanvas;
    public CanvasGroup ConsistentUICanvas;
    public CanvasGroup BGCanvas;
    public CanvasGroup PlayUICanvas;

    //Buttons that will be used on this screen
    public Button PlayButton;
    private ReturnBtn ReturnBtn;

    //Mode select is seperate for easier comprehension
    //Focuses on the logic of selecting freeplay or adventure
    private ModeSelect ModeSelect;


    void Start()
    {
        //Link objects
        ReturnBtn = gameObject.GetComponent<ReturnBtn>();
        ModeSelect = gameObject.GetComponent<ModeSelect>();

        //Link the play button
        PlayButton.onClick.AddListener(Play);

        //ensure all transaperent
        ConsistentUICanvas.alpha = BGCanvas.alpha = HomeUICanvas.alpha =  0;

        //fade them in
        StartCoroutine(InitialFadeEnum());


    }

    //Fade in the home screen on load
    IEnumerator InitialFadeEnum()
    {
        while (HomeUICanvas.alpha < 1)
        {
            ConsistentUICanvas.alpha = BGCanvas.alpha = HomeUICanvas.alpha += Time.deltaTime * 1f;
            yield return null;
        }
    }

    //Play function
    private void Play()
    {
        //inform the return btn which UI we are on
        ReturnBtn.ChangeState("Play");

        //disable return to prevent glitchyui
        ReturnBtn.BtnDisable();

        //fade in mode select screen
        StartCoroutine(PlayFadeEnum());
    }

    IEnumerator PlayFadeEnum()
    {
        //fade out the main home ui 
        while (HomeUICanvas.alpha > 0)
        {
            HomeUICanvas.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        //deactivate it to prevent accidental input
        HomeUICanvas.gameObject.SetActive(false);

        //ensure modeselect is transparent
        PlayUICanvas.alpha = 0;

        //activate it
        PlayUICanvas.gameObject.SetActive(true);

        //fade it in
        while (PlayUICanvas.alpha < 1)
        {
            PlayUICanvas.alpha += Time.deltaTime * 1f;
            yield return null;
        }

        //re-enable all the buttons
        ModeSelect.BtnEnable();
        ReturnBtn.BtnEnable();
    }
}
