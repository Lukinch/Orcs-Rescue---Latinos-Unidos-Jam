using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCollisionDetector : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        //TODO: Kill Player
        Debug.Log("Collides!!!");
    }
}
