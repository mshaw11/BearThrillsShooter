using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

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

    public void SetAbility(AbilityName ab)
    {

        switch (ab)
        {
            case AbilityName.FLAME:
                setActive(0);
                break;
            case AbilityName.GRENADE:
                setActive(1);
                break;
            case AbilityName.KB:
                setActive(2);
                break;
            case AbilityName.RG:
                setActive(3);
                break;
        }
    }

    void setActive(int id)
    {
        foreach (GameObject g in abilityUIObjs)
        {
            if (abilityUIObjs.IndexOf(g) == id)
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
