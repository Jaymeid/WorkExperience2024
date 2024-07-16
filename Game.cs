using System;
using System.Collections.Generic;

class Game
{
    private Room? currentRoom;
    private Dictionary<string, Room>? rooms;

    private Player player;

    public Game()
    {
        player = new Player();
        InitializeRooms();
    }

    private void InitializeRooms()
    {
        rooms = [];

        Room livingRoom = new("Living Room", "You are in a cozy living room. There is a sofa and a TV here.", false);
        Room kitchen = new("Kitchen", "You are in a kitchen. There is a fridge and a stove here.", true);
        Room bedroom = new("Bedroom", "You are in a bedroom. There is a bed and a wardrobe here.", false);
        Room shop = new("Shop", "You have entered the shop. You can buy new items here to help you along the way!", false);

        livingRoom.AddAdjacentRoom("north", kitchen);
        kitchen.AddAdjacentRoom("south", livingRoom);
        kitchen.AddAdjacentRoom("east", bedroom);
        bedroom.AddAdjacentRoom("west", kitchen);
        shop.AddAdjacentRoom("north", bedroom);
        shop.AddAdjacentRoom("west", livingRoom);
        livingRoom.AddAdjacentRoom("east", shop);
        bedroom.AddAdjacentRoom("south", shop);

        rooms.Add("Living Room", livingRoom);
        rooms.Add("Kitchen", kitchen);
        rooms.Add("Bedroom", bedroom);
        rooms.Add("Shop", shop);

        foreach (KeyValuePair<string, Room> kvp in rooms)
        {
            Console.WriteLine(kvp);
        }
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
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string input = Console.ReadLine().ToLower();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            string[] inputParts = input.Split(' ');

            switch (inputParts[0])
            {
                case "go":
                    if (inputParts.Length > 1)
                    {
                        MoveToRoom(inputParts[1]);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        if (currentRoom.GetHasMonster())
                        {
                            CombatLoop();
                        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }
                    else
                    {
                        Console.WriteLine("Go where?");
                    }
                    break;
                case "look":
                    DisplayCurrentRoom();
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

    private void CombatLoop()
    {
        bool continueBattle = true;

        Troll troll = new();

        Console.WriteLine($"Oh no! You have encountered a {troll.GetCharacterName()}!\nYou must now battle to the death!");

        while (continueBattle)
        {
            Console.WriteLine("What is your move?");


#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string input = Console.ReadLine().ToLower();
#pragma warning restore CS8602 // Dereference of a possibly null reference.


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
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        currentRoom.SetHasMonster(false);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private void MoveToRoom(string direction)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        if (currentRoom.AdjacentRooms.ContainsKey(direction))
        {
            currentRoom = currentRoom.AdjacentRooms[direction];
            DisplayCurrentRoom();
        }
        else
        {
            Console.WriteLine("You can't go that way.");
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private void DisplayCurrentRoom()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Console.WriteLine(currentRoom.Description);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private void DisplayHelp()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("  go [direction] - Move to another room (north, south, east, west)");
        Console.WriteLine("  look - Look around the current room");
        Console.WriteLine("  help - Display this help message");
        Console.WriteLine("  attack - Takes 10HP away from enemy");
        Console.WriteLine("  block - Gives an 3/8 chance to block the enemy's attacks");
        Console.WriteLine("  shop - Hint (go east from the living room or south from the bedroom to enter the shop)");
        Console.WriteLine("  quit - Exit the game");
    }
}