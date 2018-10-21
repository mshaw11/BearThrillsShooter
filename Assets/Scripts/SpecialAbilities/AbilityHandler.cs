using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class AbilityHandler : MonoBehaviour {

    [SerializeField]
    private GameObject grenadeAbility;

    [SerializeField]
    private GameObject flameRingAbility;

    [SerializeField]
    private GameObject knockBackAbility;

    private List<GameObject> abilities = new List<GameObject>();
    private int currentAbility = 0;
    private void Start()
    {
        flameRingAbility = Instantiate(flameRingAbility);
        grenadeAbility = Instantiate(grenadeAbility);
        knockBackAbility = Instantiate(knockBackAbility);

        abilities.Add(grenadeAbility);
        abilities.Add(flameRingAbility); 
        abilities.Add(knockBackAbility);
    }

    public void UseAbility(Collider2D playerCollider, Vector3 playerPosition, Vector3 crosshairPosition)
    {
        Debug.Log(abilities.Count);
        Debug.Log(currentAbility);
        abilities[currentAbility].GetComponent<AbilityBase>().useAbility(playerCollider, playerPosition, crosshairPosition);

    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && currentAbility < (abilities.Count -1))
        {
            currentAbility = (currentAbility + 1);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && currentAbility != 0)
        {
            currentAbility = (currentAbility - 1);
        }
    }
}