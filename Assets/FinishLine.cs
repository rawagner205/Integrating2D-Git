using UnityEngine;

public class FinishLine : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Destroying Player");
        Destroy(collision.gameObject.transform.parent.gameObject);
    }
}
