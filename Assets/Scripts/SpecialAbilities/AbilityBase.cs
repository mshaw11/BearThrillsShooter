using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour {

    [SerializeField]
    private float cooldown = 0f;
    private float timeToFire = 0;

    protected abstract void ability(Collider2D playerCollider, Vector3 playerPosition, Vector3 crosshairPosition);

    public void useAbility(Collider2D playerCollider, Vector3 playerPosition, Vector3 crosshairPosition)
    {
        timeToFire += Time.deltaTime;

        if (timeToFire > cooldown)
        {
            timeToFire = 0;
            ability(playerCollider, playerPosition, crosshairPosition);
        }
    }
}
