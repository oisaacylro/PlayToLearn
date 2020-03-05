using UnityEngine;

public class NodeSpawning : MonoBehaviour
{
    public GameObject BattleNode;
    public GameObject EventNode;
    public GameObject RestNode;
    public GameObject ShopNode;
    public GameObject EmptyNode;

    public GameObject NodeContainer;

    //0: Blank, 1:Battle, 2:Event, 3:Rest, 4:Shop, 5:Boss
    private int[,] NodeTypes = new int[4, 4];
    private GameObject[,] Levels = new GameObject[4, 4];
    // Bottom most node at pos y -1600
    void Start()
    {
        for (int y = 0; y < 3; y++)
        {
            bool empty = false;
            for (int x = 0; x < 3; x++)
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
        for (int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 3; x++)
            {
                switch(NodeTypes[y,x])
                {
                    case int n when n <70 && n>11:
                        Levels[y, x] = Instantiate(BattleNode, new Vector3((x * 420) - 420, (y * 600) - 1600, 0), Quaternion.identity);
                        break;
                    case int n when n < 80 && n > 71 :
                        Levels[y, x] = Instantiate(EventNode, new Vector3((x * 420) - 420, (y * 600) - 1600, 0), Quaternion.identity);
                        break;
                    case int n when n < 90 && n > 81 :
                        Levels[y, x] = Instantiate(RestNode, new Vector3((x * 420) - 420, (y * 600) - 1600, 0), Quaternion.identity);
                        break;
                    case int n when n < 100 && n > 91 :
                        Levels[y, x] = Instantiate(ShopNode, new Vector3((x * 420) - 420, (y * 600) - 1600, 0), Quaternion.identity);
                        break;
                    default:
                        Levels[y, x] = Instantiate(EmptyNode, new Vector3((x * 420) - 420, (y * 600) - 1600, 0), Quaternion.identity);
                        break;
                }
                Levels[y,x].gameObject.name = "Node" + y + x;
                Levels[y,x].transform.SetParent(NodeContainer.transform);
                Levels[y,x].transform.SetPositionAndRotation((Levels[y, x].transform.parent.position + new Vector3((x * 420) - 420, (y * 600) - 1600, 0)), Quaternion.identity);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
