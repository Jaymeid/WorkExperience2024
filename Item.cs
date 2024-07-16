using System.Runtime.InteropServices.Marshalling;

abstract class Item
{
    public string name { get; }
    public string description { get; }

    protected Item(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public abstract void Use(Player player);
}