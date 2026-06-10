using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public TextMeshProUGUI TxtCoin;

    private int totalCoins;

    private const string CoinKey = "TotalCoins";

    public int TotalCoins
    {
        get { return totalCoins; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadCoins();
    }

    private void LoadCoins()
    {
        totalCoins = PlayerPrefs.GetInt(CoinKey, 0);
        UpdateCoinText();
    }

    public void AddCoin(int amount)
    {
        totalCoins += amount;

        PlayerPrefs.SetInt(CoinKey, totalCoins);
        PlayerPrefs.Save();

        Debug.Log("Coin: " + totalCoins);

        UpdateCoinText();
    }

    public bool SpendCoin(int amount)
    {
        if (totalCoins >= amount)
        {
            totalCoins -= amount;

            PlayerPrefs.SetInt(CoinKey, totalCoins);
            PlayerPrefs.Save();

            UpdateCoinText();

            return true;
        }

        Debug.Log("Coin tidak cukup");
        return false;
    }

    public void UpdateCoinText()
    {
        if (TxtCoin != null)
        {
            TxtCoin.text = totalCoins.ToString();
        }
    }
}