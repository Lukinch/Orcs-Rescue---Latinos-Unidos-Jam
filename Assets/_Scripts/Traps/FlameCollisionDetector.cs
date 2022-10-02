using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCollisionDetector : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            PlayerManager.Instance.OnPlayerDeath();
        }
    }
}
