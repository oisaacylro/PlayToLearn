using UnityEngine;
using System.Collections;

public class StartTap : MonoBehaviour
{
    public GameObject StartText;
    private Animator StartAnim;
    public CanvasGroup LoginUICanvas;
    bool started = false;
    
    void Start()
    {
        Debug.Log("Start script");
        StartAnim = StartText.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!started)
        {
            int tap = 0;

            foreach (Touch t in Input.touches)
            {
                if (t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled)
                    tap++;
            }

            if (Input.GetKey(KeyCode.Space) || tap > 0)
                Start_Tap();
        }
    }

    private void Start_Tap()
    {
        started = true;
        StartAnim.SetBool("Started", true);
        StartCoroutine(ShowLogin());
    }

    IEnumerator ShowLogin()
    {
        yield return new WaitForSeconds(1);
        FadeInLogin();
    }

    private void FadeInLogin()
    {
        LoginUICanvas.alpha = 0;
        LoginUICanvas.gameObject.SetActive(true);
        StartCoroutine(LoginFade1());
    }

    IEnumerator LoginFade1()
    {
        while (LoginUICanvas.alpha < 1)
        {
            LoginUICanvas.alpha += Time.deltaTime * 1f;
            yield return null;
        }
    }
}
