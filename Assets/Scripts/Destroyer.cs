using Unity.VisualScripting;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < player.transform.position.z - 30)
        {
            Destroy(gameObject);
        }
    }
}
