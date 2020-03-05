using UnityEngine;
using UnityEngine.UI;

public class NodeSpawning : MonoBehaviour
{
    public GameObject BattleNode;
    public GameObject EventNode;
    public GameObject RestNode;
    public GameObject ShopNode;
    public GameObject EmptyNode;
    public GameObject BossNode;

    public GameObject NodeContainer;

    private BattleScript bs;
    private RestScript rs;
    private EventScript es;
    private ShopScript ss;
    private BossScript bss;

    //0: Blank, 1:Battle, 2:Event, 3:Rest, 4:Shop, 5:Boss
    private int[,] NodeTypes;
    private GameObject[,] Levels;

    private int stageHeight;
    private int stageLength;
    private int currentLevel;

    void Start()
    {
        bs = gameObject.GetComponent<BattleScript>();
        rs = gameObject.GetComponent<RestScript>();
        es = gameObject.GetComponent<EventScript>();
        ss = gameObject.GetComponent<ShopScript>();
        bss = gameObject.GetComponent<BossScript>();
        currentLevel = 0;
        stageHeight = 8;
        stageLength = 3;
        Levels = new GameObject[stageHeight+1, stageLength];
        NodeTypes = new int[stageHeight-1, stageLength];

        //Decide what node type is on each point
        for (int y = 0; y < stageHeight-1; y++)
        {
            bool empty = false;
            for (int x = 0; x < stageLength; x++)
            {
                //maximum one empty per row
                if(!empty)
                {
                    NodeTypes[y, x] = Random.Range(0, 101);
                }
                else
                {
                    NodeTypes[y, x] = Random.Range(21, 101);
                }
                if(NodeTypes[y, x] < 11)
                {
                    empty = true;
                }
            }
        }

        //Generate the nodes
        for (int y = 0; y < stageHeight-1; y++)
        {
            for(int x = 0; x < stageLength; x++)
            {
                switch(NodeTypes[y,x])
                {
                    case int n when n <=70 && n>=21:
                        Levels[y, x] = Instantiate(BattleNode, new Vector3(0, 0, 0), Quaternion.identity);
                        bs.LinkBattleNode(Levels[y, x].GetComponent<Button>());
                        break;
                    case int n when n <= 80 && n >= 71 :
                        Levels[y, x] = Instantiate(EventNode, new Vector3(0, 0, 0), Quaternion.identity);
                        es.LinkEventNode(Levels[y, x].GetComponent<Button>());
                        break;
                    case int n when n <= 90 && n >= 81 :
                        Levels[y, x] = Instantiate(RestNode, new Vector3(0, 0, 0), Quaternion.identity);
                        rs.LinkRestNode(Levels[y, x].GetComponent<Button>());
                        break;
                    case int n when n <= 100 && n >=91 :
                        Levels[y, x] = Instantiate(ShopNode, new Vector3(0, 0, 0), Quaternion.identity);
                        ss.LinkShopNode(Levels[y, x].GetComponent<Button>());
                        break;
                    default:
                        Levels[y, x] = Instantiate(EmptyNode, new Vector3(0,0,0), Quaternion.identity);
                        break;
                }
                Levels[y,x].gameObject.name = "Node" + y + x;
                Levels[y,x].transform.SetParent(NodeContainer.transform);
                Levels[y,x].transform.SetPositionAndRotation((Levels[y, x].transform.parent.position + new Vector3((x * 420) - 420, (y * 600) - 2400, 0)), Quaternion.identity);
            }            
        }

        //Rest 
        for (int x = 0; x < stageLength; x++)
        {
            Levels[stageHeight - 1, x] = Instantiate(RestNode, new Vector3(0, 0, 0), Quaternion.identity);
            Levels[stageHeight - 1, x].gameObject.name = "Node" + (stageHeight - 1) + x;
            Levels[stageHeight - 1, x].transform.SetParent(NodeContainer.transform);
            Levels[stageHeight - 1, x].transform.SetPositionAndRotation((Levels[(stageHeight - 1), x].transform.parent.position + new Vector3((x * 420) - 420, ((stageHeight-1) * 600) - 2400, 0)), Quaternion.identity);
            rs.LinkRestNode(Levels[stageHeight - 1, x].GetComponent<Button>());
        }

        //Boss
        Levels[stageHeight, 1] = Instantiate(BossNode, new Vector3(0, 0, 0), Quaternion.identity);
        Levels[stageHeight, 1].gameObject.name = "Node" + stageHeight + "1";
        Levels[stageHeight, 1].transform.SetParent(NodeContainer.transform);
        Levels[stageHeight, 1].transform.SetPositionAndRotation((Levels[stageHeight, 1].transform.parent.position + new Vector3((1 * 420) - 420, (stageHeight * 600) - 2400, 0)), Quaternion.identity);
        Levels[stageHeight, 1].GetComponent<Button>().interactable = false;
        bss.LinkBossNode(Levels[stageHeight, 1].GetComponent<Button>());
        //Disable nodes above first row
        for (int y = 1; y < stageHeight; y++)
        {
            for(int x = 0; x < stageLength; x++)
            {
                Levels[y, x].GetComponent<Button>().interactable = false;
            }
        }
        //connecting lines? Not sure yet how to do
        //for (int y = 0; y < 2; y++)
        //{
        //    for (int x = 0; x < 3; x++)
        //    {
        //    }
        //}
    }

    public void nextLevel()
    {
        if (currentLevel == stageHeight-1)
        {
            Levels[currentLevel, 0].GetComponent<Button>().interactable = false;
            Levels[currentLevel, 1].GetComponent<Button>().interactable = false;
            Levels[currentLevel, 2].GetComponent<Button>().interactable = false;
            Levels[currentLevel + 1, 1].GetComponent<Button>().interactable = true;
        }
        else
        {
            for (int i = 0; i < stageLength; i++)
            {
                Levels[currentLevel, i].GetComponent<Button>().interactable = false;
                Levels[currentLevel + 1, i].GetComponent<Button>().interactable = true;

            }
        }
        currentLevel++;
    }
}
