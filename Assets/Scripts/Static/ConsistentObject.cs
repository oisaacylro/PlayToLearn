using UnityEngine;

public class ConsistentObject : MonoBehaviour
{
    private string selectedMode;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }
    
    public string getSelectedMode()
    {
        return selectedMode;
    }

    public void setSelectedMode(string s)
    {
        selectedMode = s;
    }
}
