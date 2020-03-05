using UnityEngine;
using System.Collections;

public class StartTap : MonoBehaviour
{
    //Blinking start text
    public GameObject StartText;

    //Animator of above text
    private Animator StartAnim;

    //Canvasgroup for the login UI
    public CanvasGroup LoginUICanvas;
    bool started = false;
    
    void Start()
    {
        //retrieve the animator
        StartAnim = StartText.GetComponent<Animator>();
    }

    private void Update()
    {
        //check for any taps
        if (!started)
        {
            int tap = 0;

            foreach (Touch t in Input.touches)
            {
                if (t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled)
                    tap++; //Tap detected
            }

            //if spacebar or tap detected, show login UI
            if (Input.GetKey(KeyCode.Space) || tap > 0)
                Start_Tap();
        }
    }

    private void Start_Tap()
    {
        //started set to stop checking for taps on screen
        started = true;

        //stop the animation for the text
        StartAnim.SetBool("Started", true);

        //fade in the login ui
        StartCoroutine(ShowLogin());
    }

    IEnumerator ShowLogin()
    {
        //wait for a second for the start text to disappear
        yield return new WaitForSeconds(1);
        FadeInLogin();
    }

    private void FadeInLogin()
    {
        //make sure the UI is transaparent
        LoginUICanvas.alpha = 0;

        //Set it active
        LoginUICanvas.gameObject.SetActive(true);

        //fade it in
        StartCoroutine(LoginFade());
    }

    IEnumerator LoginFade()
    {
        while (LoginUICanvas.alpha < 1)
        {
            LoginUICanvas.alpha += Time.deltaTime * 1f;
            yield return null;
        }
    }
}
