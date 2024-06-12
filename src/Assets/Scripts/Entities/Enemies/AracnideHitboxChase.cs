using UnityEngine;

public class AracnideHitboxChase : MonoBehaviour
{
    public bool onChaseRadius = false;

    private void OnTriggerEnter(Collider other) => onChaseRadius = true;

    private void OnTriggerExit(Collider other) => onChaseRadius = false;
}
