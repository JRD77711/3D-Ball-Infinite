using TMPro;
using UnityEngine;

public class BallColorShop : MonoBehaviour
{
    [Header("Player Ball")]
    public Renderer ballRenderer;

    [Header("Shop UI")]
    public TextMeshProUGUI shopCoinText;

    public TextMeshProUGUI defaultButtonText;
    public TextMeshProUGUI redButtonText;
    public TextMeshProUGUI blueButtonText;
    public TextMeshProUGUI greenButtonText;
    public TextMeshProUGUI yellowButtonText;

    private const string CoinKey = "TotalCoins";
    private const string SelectedColorKey = "SelectedBallColor";

    private void Start()
    {
        PlayerPrefs.SetInt("Owned_Default", 1);

        ApplySavedColor();
        UpdateShopUI();
    }

    public void SelectDefaultColor()
    {
        SelectColor("Default", Color.white);
        UpdateShopUI();
    }

    public void BuyOrSelectRed()
    {
        BuyOrSelectColor("Red", 25, Color.red);
    }

    public void BuyOrSelectBlue()
    {
        BuyOrSelectColor("Blue", 50, Color.blue);
    }

    public void BuyOrSelectGreen()
    {
        BuyOrSelectColor("Green", 75, Color.green);
    }

    public void BuyOrSelectYellow()
    {
        BuyOrSelectColor("Yellow", 100, Color.yellow);
    }

    private void BuyOrSelectColor(string colorName, int price, Color color)
    {
        bool owned = IsOwned(colorName);

        if (owned)
        {
            SelectColor(colorName, color);
        }
        else
        {
            bool success = SpendCoins(price);

            if (success)
            {
                PlayerPrefs.SetInt("Owned_" + colorName, 1);
                SelectColor(colorName, color);
            }
            else
            {
                Debug.Log("Coin tidak cukup untuk membeli warna " + colorName);
            }
        }

        PlayerPrefs.Save();
        UpdateShopUI();
    }

    private bool IsOwned(string colorName)
    {
        if (colorName == "Default")
        {
            return true;
        }

        return PlayerPrefs.GetInt("Owned_" + colorName, 0) == 1;
    }

    private bool SpendCoins(int amount)
    {
        if (CoinManager.instance != null)
        {
            return CoinManager.instance.SpendCoin(amount);
        }

        int coins = PlayerPrefs.GetInt(CoinKey, 0);

        if (coins >= amount)
        {
            coins -= amount;
            PlayerPrefs.SetInt(CoinKey, coins);
            PlayerPrefs.Save();
            return true;
        }

        return false;
    }

    private int GetCoins()
    {
        if (CoinManager.instance != null)
        {
            return CoinManager.instance.TotalCoins;
        }

        return PlayerPrefs.GetInt(CoinKey, 0);
    }

    private void SelectColor(string colorName, Color color)
    {
        PlayerPrefs.SetString(SelectedColorKey, colorName);
        PlayerPrefs.Save();

        ApplyColorToBall(color);
    }

    private void ApplySavedColor()
    {
        string selectedColor = PlayerPrefs.GetString(SelectedColorKey, "Default");

        if (selectedColor == "Red")
        {
            ApplyColorToBall(Color.red);
        }
        else if (selectedColor == "Blue")
        {
            ApplyColorToBall(Color.blue);
        }
        else if (selectedColor == "Green")
        {
            ApplyColorToBall(Color.green);
        }
        else if (selectedColor == "Yellow")
        {
            ApplyColorToBall(Color.yellow);
        }
        else
        {
            ApplyColorToBall(Color.white);
        }
    }

    private void ApplyColorToBall(Color color)
    {
        if (ballRenderer == null)
        {
            Debug.LogWarning("Ball Renderer belum diisi di Inspector");
            return;
        }

        ballRenderer.material.color = color;

        if (ballRenderer.material.HasProperty("_BaseColor"))
        {
            ballRenderer.material.SetColor("_BaseColor", color);
        }
    }

    public void UpdateShopUI()
    {
        if (shopCoinText != null)
        {
            shopCoinText.text = "Coins: " + GetCoins();
        }

        UpdateButtonText("Default", defaultButtonText, 0);
        UpdateButtonText("Red", redButtonText, 25);
        UpdateButtonText("Blue", blueButtonText, 50);
        UpdateButtonText("Green", greenButtonText, 75);
        UpdateButtonText("Yellow", yellowButtonText, 100);
    }

    private void UpdateButtonText(string colorName, TextMeshProUGUI buttonText, int price)
    {
        if (buttonText == null)
        {
            return;
        }

        string selectedColor = PlayerPrefs.GetString(SelectedColorKey, "Default");
        bool owned = IsOwned(colorName);

        if (selectedColor == colorName)
        {
            buttonText.text = "Selected";
        }
        else if (owned)
        {
            buttonText.text = "Select";
        }
        else
        {
            buttonText.text = "Buy " + price;
        }
    }
}