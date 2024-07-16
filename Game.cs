using System;
using System.Collections.Generic;
using System.ComponentModel;

class Game
{
    private Room currentRoom;
    private Dictionary<string, Room> rooms;

    private Player player;

    public Game()
    {
        player = new Player();
        InitializeRooms();
    }

    private void InitializeRooms()
    {
        rooms = new Dictionary<string, Room>();

        List<Item> items = new List<Item>{
            new HealthPotion("health_potion", "drink to restore 20 HP", 20),
        };

        Room livingRoom = new Room("Living Room", "You are in a cozy living room. There is a sofa and a TV here.", false, items);
        Room kitchen = new Room("Kitchen", "You are in a kitchen. There is a fridge and a stove here.", true);
        Room bedroom = new Room("Bedroom", "You are in a bedroom. There is a bed and a wardrobe here.", false);

        livingRoom.AddAdjacentRoom("north", kitchen);
        kitchen.AddAdjacentRoom("south", livingRoom);
        kitchen.AddAdjacentRoom("east", bedroom);
        bedroom.AddAdjacentRoom("west", kitchen);

        rooms.Add("Living Room", livingRoom);
        rooms.Add("Kitchen", kitchen);
        rooms.Add("Bedroom", bedroom);

        currentRoom = livingRoom;
    }

    public void Start()
    {
        Console.WriteLine("Welcome to the Text-Based Room Explorer!");
        Console.WriteLine("Type 'help' to see the list of commands.");
        Console.WriteLine();
        DisplayCurrentRoom();

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine().ToLower();
            string[] inputParts = input.Split(' ');

            switch (inputParts[0])
            {
                case "go":
                    if (inputParts.Length > 1)
                    {
                        MoveToRoom(inputParts[1]);
                        if (currentRoom.GetHasMonster())
                        {
                            CombatLoop();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Go where?");
                    }
                    break;
                case "look":
                    DisplayCurrentRoom();
                    break;
                case "take":
                    TakeItem(inputParts[1]);
                    break;
                case "use":
                    UseItem(inputParts[1]);
                    break;
                case "help":
                    DisplayHelp();
                    break;
                case "quit":
                    Console.WriteLine("Thanks for playing!");
                    return;
                default:
                    Console.WriteLine("Unknown command. Type 'help' to see the list of commands.");
                    break;
            }
        }
    }

    private void TakeItem(string itemToTake)
    {

        foreach (Item item in currentRoom.roomItems)
        {
            if (item.name == itemToTake)
            {
                player.PickUpItem(item);
                return;
            }
        }

        Console.WriteLine($"There were no items that matched description {itemToTake}");
    }

    private void UseItem(string itemToUse)
    {
        foreach (Item item in currentRoom.roomItems)
        {
            if (item.name == itemToUse)
            {
                item.Use(player);
                return;
            }
        }
        Console.WriteLine($"There were no items that matched description {itemToUse}");
    }
    private void CombatLoop()
    {
        bool continueBattle = true;

        Troll troll = new Troll();

        Console.WriteLine($"Oh no! You have encountered a {troll.GetCharacterName()}!\nYou must now battle to the death!");

        while (continueBattle)
        {
            Console.WriteLine("What is your move?");

            string input = Console.ReadLine().ToLower();
            string[] inputParts = input.Split(' ');

            bool playerHasBlocked = false;
            bool blockResult = false;

            //Players turn
            switch (inputParts[0])
            {
                case "attack":
                    Console.WriteLine($"You attack the troll for {player.GetAttackDamage()} damage");
                    troll.TakeDamage(player.GetAttackDamage());

                    if (troll.GetIsDead())
                    {
                        continueBattle = false;
                    }
                    break;
                case "block":
                    playerHasBlocked = true;
                    if (player.BlockAttack())
                    {
                        blockResult = true;
                        Console.WriteLine("You will block the next attack");
                    }
                    else
                    {
                        blockResult = false;
                        Console.WriteLine("Block failed, you will take double damage next turn");
                    }
                    break;
                default:
                    Console.WriteLine("Unknown command. Type 'help' to see the list of commands.");
                    break;

            }

            //Monsters turn
            if (playerHasBlocked)
            {
                if (blockResult)
                {
                    Console.WriteLine($"You blocked the {troll.GetCharacterName()}s attack!");
                }
                else
                {
                    Console.WriteLine($"Your block failed, you take double damage!");
                    player.TakeDamage(troll.GetAttackDamage() * 2);
                }
            }
            else
            {
                player.TakeDamage(troll.GetAttackDamage());
            }

            Console.WriteLine($"\nThe {troll.GetCharacterName()} has {troll.GetHealth()} HP");
            Console.WriteLine($"You have {player.GetHealth()} HP\n");
        }

        Console.WriteLine($"The {troll.GetCharacterName()} has been vanquished!\n");
        currentRoom.SetHasMonster(false);
    }

    private void MoveToRoom(string direction)
    {
        if (currentRoom.adjacentRooms.ContainsKey(direction))
        {
            currentRoom = currentRoom.adjacentRooms[direction];
            DisplayCurrentRoom();
        }
        else
        {
            Console.WriteLine("You can't go that way.");
        }
    }

    private void DisplayCurrentRoom()
    {
        Console.WriteLine(currentRoom.description);
        if (!IsNullOrEmpty(currentRoom.roomItems))
        {
            Console.WriteLine("There are items in the room: ");
            foreach (Item item in currentRoom.roomItems)
            {
                Console.WriteLine(item.name);
            }
        }
    }

    private void DisplayHelp()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("  go [direction] - Move to another room (north, south, east, west)");
        Console.WriteLine("  look - Look around the current room");
        Console.WriteLine("  help - Display this help message");
        Console.WriteLine("  quit - Exit the game");
    }

    static bool IsNullOrEmpty<T>(List<T> list)
    {
        return list == null || list.Count == 0;
    }
}