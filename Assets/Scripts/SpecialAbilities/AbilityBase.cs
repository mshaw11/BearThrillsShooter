using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour {

    public abstract void useAbility(Collider2D playerCollider, Vector3 playerPosition, Vector3 crosshairPosition);
}
