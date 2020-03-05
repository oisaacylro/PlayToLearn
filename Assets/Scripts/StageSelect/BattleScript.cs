using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleScript : MonoBehaviour
{
    public CanvasGroup BattleUICanvas;
    public TMP_Text Health;
    public TMP_Text EnemyHealth;
    public TMP_Text QuestionLabel;
    public GameObject GameOverLabel;
    public GameObject VictoryLabel;
    public Button Answer1;
    public Button Answer2;
    public Button Answer3;
    public Button Answer4;
    private Button[] Answers = new Button[4];
    private int enemyHealth;
    private int correctAnswer;
    private ConsistentObject ConsistentObj;
    private NodeSpawning ns;
    // Start is called before the first frame update
    void Start()
    {
        ns = gameObject.GetComponent<NodeSpawning>();
        Answer1.onClick.AddListener(Ans1);
        Answer2.onClick.AddListener(Ans2);
        Answer3.onClick.AddListener(Ans3);
        Answer4.onClick.AddListener(Ans4);
        Answers[0] = Answer1;
        Answers[1] = Answer2;
        Answers[2] = Answer3;
        Answers[3] = Answer4;
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
    }

    public void LinkBattleNode(Button b)
    {
        b.onClick.AddListener(BattleNodeClicked);
    }

    private void BattleNodeClicked()
    {
        enemyHealth = 10;
        EnemyHealth.text = "Enemy:\n" + enemyHealth.ToString();
        Health.text = "Health:\n" + ConsistentObj.getHealth().ToString();
        GenerateQuestion();
        BattleUICanvas.alpha = 1;
        BattleUICanvas.gameObject.SetActive(true);
    }

    private void GenerateQuestion()
    {
        QuestionLabel.text = "Random question " + Random.Range(1, 11);

        correctAnswer = Random.Range(0, 3);

        for(int i = 0; i <4; i++)
        {
            if(i!= correctAnswer)
                Answers[i].transform.GetChild(0).GetComponent<TMP_Text>().text = "Wrong";
            else
                Answers[i].transform.GetChild(0).GetComponent<TMP_Text>().text = "Correct";
        }
    }

    private void Ans1()
    {
        CheckAnswer(0);
    }
    private void Ans2()
    {
        CheckAnswer(1);
    }
    private void Ans3()
    {
        CheckAnswer(2);
    }
    private void Ans4()
    {
        CheckAnswer(3);
    }

    private void CheckAnswer(int i)
    {
        if(i == correctAnswer)
        {
            //correct
            enemyHealth -= 5;
            EnemyHealth.text = "Enemy:\n" + enemyHealth.ToString();
        }

        else
        {
            //wrong
            ConsistentObj.setHealth(ConsistentObj.getHealth() - 5);
            Health.text = "Health:\n" + ConsistentObj.getHealth().ToString();

        }

        if(ConsistentObj.getHealth() <= 0)
        {
            //gameover
            GameOverLabel.SetActive(true);
            StartCoroutine(GameOverFade());
        }
        else if(enemyHealth <= 0)
        {
            //win
            VictoryLabel.SetActive(true);
            ns.nextLevel();
            StartCoroutine(VictoryFade());
        }
        else
        {
            GenerateQuestion();
        }
    }

    IEnumerator VictoryFade()
    {
        yield return new WaitForSeconds(1);

        while(BattleUICanvas.alpha > 0)
        {
            BattleUICanvas.alpha -= Time.deltaTime;
            yield return null;
        }
        BattleUICanvas.gameObject.SetActive(false);
        VictoryLabel.gameObject.SetActive(false);

    }


    IEnumerator GameOverFade()
    {
        yield return new WaitForSeconds(1);

        while (BattleUICanvas.alpha > 0)
        {
            BattleUICanvas.alpha -= Time.deltaTime;
            yield return null;
        }
        BattleUICanvas.gameObject.SetActive(false);
        GameOverLabel.gameObject.SetActive(false);
        gameObject.GetComponent<Return>().yesBtn();
    }
}
