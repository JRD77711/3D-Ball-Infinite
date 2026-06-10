using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    public float groundLength = 1000f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Move this ground segment forward by ground length
            transform.parent.position += new Vector3(0, 0, groundLength * 2);
        }
    }
}
