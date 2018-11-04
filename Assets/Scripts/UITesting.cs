using UnityEngine;
using UnityEngine.UI;

public class UITesting : MonoBehaviour
{

    int enemyCount;

    [SerializeField]
    private Text numberEnemiesText;


    [SerializeField]
    private Text currentAbility;

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("SpawnedEnemy").Length;
        numberEnemiesText.text = ("Enemies Remaining: " + enemyCount);
    }
}
