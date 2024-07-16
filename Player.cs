using System.Security.Cryptography;
using System.Collections.Generic;

class Player : Character{

    private List<Item> inventory;
    private int inventorySize;
    public Player(){
        health = 100;
        attackDamage = 10;

        inventorySize = 5;
        inventory = new List<Item>();
    }

    public bool BlockAttack(){
        Random rand = new Random();
        int randNumber = rand.Next(0, 9);

        if(randNumber >= 2){
            return true;
        }

        return false;
    }

    public override void Die(){
        Console.WriteLine("You have succumbed to the monsters.");
        Environment.Exit(0);
    }

    public void AddHealth(int healthToAdd){
        health += healthToAdd;
    }

    public List<Item> GetInventory(){
        return inventory;
    }

    public void PickUpItem(Item newItem){
        if(inventory.Count < inventorySize){
            Console.WriteLine($"{newItem.name} added to inventory");
            inventory.Add(newItem);
        }
        else{
            Console.WriteLine($"Can't pick up the {newItem.name}. Not enough inventory space!");
        }
    }

}