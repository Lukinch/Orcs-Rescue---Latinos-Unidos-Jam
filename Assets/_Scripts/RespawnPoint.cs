using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] Transform _respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            PlayerManager.Instance.RespawnPoint = _respawnPoint;
        }
    }
}
