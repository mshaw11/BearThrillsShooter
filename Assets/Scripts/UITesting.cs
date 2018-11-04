using UnityEngine;
using UnityEngine.UI;

public class UITesting : MonoBehaviour
{
    public int killCount;

    [SerializeField]
    private Text numberEnemiesText;
    
    [SerializeField]
    private Text currentAbility;

    // Update is called once per frame
    void Update()
    {
        numberEnemiesText.text = ("Enemies Killed: " + killCount);
    }

    public void UpdateKillCount()
    {
        killCount++;
    }
}
