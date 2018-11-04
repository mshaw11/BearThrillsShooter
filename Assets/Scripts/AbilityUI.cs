using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour {

    [SerializeField]
    private List<GameObject> abilityUIObjs;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    void enableColor(GameObject abilityImage)
    {
        abilityImage.GetComponent<Image>().color = new Color(1,1,1,1);
    }

    void disableColor(GameObject abilityImage)
    {
        abilityImage.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
    }

    public void SetAbility(int num)
    {
        Debug.Log("Ability: " + num);
        foreach (GameObject g in abilityUIObjs)
        {
            if (abilityUIObjs.IndexOf(g) == num)
            {
                enableColor(g);
            }
            else
            {
                disableColor(g);
            }
        }
    }
}
