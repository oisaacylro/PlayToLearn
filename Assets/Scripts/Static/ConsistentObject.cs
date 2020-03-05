using UnityEngine;

public class ConsistentObject : MonoBehaviour
{
    //This is the consistent object that will be alive throughout the entire playthrough
    //It will contain all the necessary information that other scenes may need as well
    //Static object thus only one can exist

    //Variables to track
    private int SelectedMode;//Adventure = 0; Freeplay = 1;
    private int SelectedWorld; //1 , 2 , 3 or 4
    private int Health;
    private int Score;
    void Start()
    {
        //Do not destroy this object on changing scenes.
        DontDestroyOnLoad(this);
    }
    
    //getters and setters

    public int getSelectedMode()
    {
        return SelectedMode;
    }

    public void setSelectedMode(int i)
    {
        SelectedMode = i;
    }

    public int getSelectedWorld()
    {
        return SelectedWorld;
    }

    public void setSelectedWorld(int i)
    {
        SelectedWorld = i;
    }

    public int getHealth()
    {
        return Health;
    }

    public void setHealth(int i)
    {
        Health = i;
    }

    public int getScore()
    {
        return Score;
    }

    public void setScore(int i)
    {
        Score = i;
    }
}
