using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class AbilityHandler : MonoBehaviour {

    [SerializeField]
    private GameObject grenadeAbility;

    [SerializeField]
    public GameObject flameRingAbility;

    private List<GameObject> abilities = new List<GameObject>();
    private int currentAbility = 0;
    private void Start()
    {
    
        Instantiate(flameRingAbility);
        grenadeAbility = Instantiate(grenadeAbility);


        abilities.Add(grenadeAbility);
        abilities.Add(flameRingAbility);
    }


    public void UseAbility(Collider2D playerCollider, Vector3 playerPosition, Vector3 crosshairPosition)
    {
    
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