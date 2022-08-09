using System.Collections;
using UnityEngine;

public class TurnToast : MonoBehaviour
{
    int lifetime = 2;
    private void Start() => StartCoroutine(WaitThenDie());

    IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }
}