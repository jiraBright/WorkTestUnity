[System.Serializable]
public class InventoryData<T>
{
    public T Data;
    public int Amount;

    public InventoryData(T data, int amount = 0)
    {
        Data = data;
        Amount = amount;
    }
}
