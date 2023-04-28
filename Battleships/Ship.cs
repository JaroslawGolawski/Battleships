namespace Battleships
{
    internal class Ship
    {
        public string Name { get; set; }
        public int Size { get; set; }

        public Ship(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }
}
