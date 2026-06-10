using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.coinClip);
            CoinManager.instance.AddCoin(coinValue);
            Destroy(gameObject);
        }
    }
}
