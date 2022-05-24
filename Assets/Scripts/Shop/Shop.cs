using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using Project0;

public class Shop : MonoBehaviour
{
    [SerializeField] List<ShopItem> shopInventory = new List<ShopItem>();

    CSS_MoneyManager moneyManager;
    Inventory inventory;

    public delegate void ShopChanged();
    public static event ShopChanged OnShopChanged;

    void Start()
    {
        moneyManager = FindObjectOfType<CSS_MoneyManager>();
        inventory = Inventory.instance;
    }

    public List<ShopItem> GetAllShotItem()
    {
        return shopInventory;
    }

    public ShopItem GetShopItem(int slotNumber)
    {
        return shopInventory[slotNumber];
    }

    public void PurchaseItem(int slotNumber)
    {

        if (shopInventory[slotNumber].GetAmountInStock() > 0)
        {
            if (moneyManager.PayCoins(shopInventory[slotNumber].GetPurchaseValue()))
            {
                inventory.Add(shopInventory[slotNumber].GetItem());
                shopInventory[slotNumber].RemoveStock(1); //TODO: variable purchase amount, allow player to purchase multiple for bulk items (if bulk items are added, eg bombs/nukes).
                OnShopChanged.Invoke();
            }
        }
        else
        {
            Debug.Log("Not enough in stock"); //TODO: Notify the player in ui.
        }
    }

    public void AddItemIntoShop(Item item, int amount, int price)
    {
        shopInventory.Add(new ShopItem(item, amount, price));
        OnShopChanged.Invoke();
    }

    public void DeleteItemInInventory(int index)
    {
        shopInventory.RemoveAt(index);
        OnShopChanged.Invoke();
    }

    public int GetItemCount() { return shopInventory.Count; }

    public ShopItem GetItemAtIndex(int index) { return shopInventory[index]; }//TODO: Change to ShopItem
}
