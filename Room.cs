
class Room
{
    public string name { get; }
    public string description { get; }
    public Dictionary<string, Room> adjacentRooms { get; }

    private bool hasMonster = false;

    public List<Item> roomItems { get; }

    public Room(string name, string description, bool hasMonster, List<Item>? roomItems = null)
    {
        this.name = name;
        this.description = description;
        this.adjacentRooms = new Dictionary<string, Room>();

        this.hasMonster = hasMonster;
        this.roomItems = roomItems;
    }

    public void AddAdjacentRoom(string direction, Room room)
    {
        adjacentRooms[direction] = room;
    }

    public void SetHasMonster(bool newHasMonsterValue)
    {
        hasMonster = newHasMonsterValue;
    }

    public bool GetHasMonster()
    {
        return hasMonster;
    }
}