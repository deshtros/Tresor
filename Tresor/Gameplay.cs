using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tresor.Items;

namespace Tresor
{
    public class Gameplay
    {
        private int _sizeX;
        private int _sizeY;
        private List<Item> _map;
        private List<Player> _players;

        public Gameplay()
        {
            _sizeX = 0;
            _sizeY = 0;
            _map = new List<Item>();
            _players = new List<Player>();
        }
        
        public bool newMap(String[] ligne)
        {
            if (ligne.Count() != 3)
            {
                Console.WriteLine("[TreasureMap] setMap : ERROR Wrong parameter -> \"C - SIZE_X - SIZE_Y");
                return false;
            }
            int x = int.Parse(ligne[1]);
            int y = int.Parse(ligne[2]);
            if (x <= 0 || y <= 0)
            {
                Console.WriteLine("[TreasureMap] setMap : ERROR Wrong size, should be more than 0");
                return false;
            }
            _sizeX = x;
            _sizeY = y;
            _map = new List<Item>();
            Console.WriteLine("[TreasureMap] newMap : New map of size -> [" + x + "," + y + "]");
            return true;
        }
        
        public bool addTreasure(String[] ligne)
        {
            if (ligne.Count() != 4)
            {
                Console.WriteLine("[TreasureMap] addTreasure : ERROR Wrong parameter -> \"T - SIZE_X - SIZE_Y - NB_TREASURE");
                return false;
            }
            int x = int.Parse(ligne[1]);
            int y = int.Parse(ligne[2]);
            int nb = int.Parse(ligne[3]);
            if (x < 0 || x >= _sizeX || y < 0 || y >= _sizeY)
            {
                Console.WriteLine("[TreasureMap] addTreasure : ERROR Wrong location, it shoult be in the map");
                return false;
            }
            if (nb <= 0)
            {
                Console.WriteLine("[TreasureMap] addTreasure : ERROR Wrong nomber of treasure, at least 1 treasure is require");
                return false;
            }
            _map.Add(new Treasure(int.Parse(ligne[1]), int.Parse(ligne[2]), int.Parse(ligne[3])));
            Console.WriteLine("[TreasureMap] addTreasure : ["+ x + "," + y +"] - " + nb + " treasure added");
            return true;
        }

        public bool addPlayer(String[] ligne)
        {
            if (ligne.Count() != 6)
            {
                Console.WriteLine("[TreasureMap] addTreasure : ERROR Wrong parameter -> \"A - NAME - POS_X - POS_Y - [N-S-E-O] - [A-G-D]*");
                return false;
            }

            String name = ligne[1];
            int x = int.Parse(ligne[2]);
            int y = int.Parse(ligne[3]);
            String orientation = ligne[4];
            String moveInstruction = ligne[5];
            
            if (x < 0 || x >= _sizeX || y < 0 || y >= _sizeY)
            {
                Console.WriteLine("[TreasureMap] addPlayer : ERROR Wrong location, it shoult be in the map");
                return false;
            }
            String[] direction = {"N", "S", "E", "O" }; 
            if (!direction.Contains(orientation))
            {
                Console.WriteLine("[TreasureMap] addPlayer : ERROR Wrong orientation, only : N, S, E, O");
                return false;
            }
            String[] move = { "A", "G", "D" };
            foreach (char instruc in moveInstruction)
            {
                if (!move.Contains(instruc.ToString()))
                {
                    Console.WriteLine("[TreasureMap] addPlayer : ERROR Wrong movement instruction, only : A, G, D");
                    return false;
                }
            }
            Player player = new Player(name, x, y, orientation, moveInstruction);
            _map.Add(player);
            _players.Add(player);
            Console.WriteLine("[TreasureMap] addPlayer : [" + x + "," + y + "] - " + direction + " player " + name + " added with action : " + move);
            return true;
        }

        public bool addMontain(String[] ligne)
        {

            if (ligne.Count() != 3)
            {
                Console.WriteLine("[TreasureMap] addMontain : ERROR Wrong parameter -> \"M - SIZE_X - SIZE_Y");
                return false;
            }
            int x = int.Parse(ligne[1]);
            int y = int.Parse(ligne[2]);
            if (x < 0 || x >= _sizeX || y < 0 || y >= _sizeY)
            {
                Console.WriteLine("[TreasureMap] addMontain : ERROR Wrong location, it shoult be in the map");
                return false;
            }
            _map.Add(new Mountain(int.Parse(ligne[1]), int.Parse(ligne[2])));
            Console.WriteLine("[TreasureMap] addTreasure : [" + x + "," + y + "] mountain added");
            return true;
        }
        
        public void playerTurnRight(Player player)
        {
            switch(player.getOrientation())
            {
                case "N":
                    player.setOrientation("E");
                    break;
                case "S":
                    player.setOrientation("O");
                    break;
                case "E":
                    player.setOrientation("S");
                    break;
                case "O":
                    player.setOrientation("N");
                    break;
            }
        }


        public void playerTurnLeft(Player player)
        {
            switch (player.getOrientation())
            {
                case "N":
                    player.setOrientation("O");
                    break;
                case "S":
                    player.setOrientation("E");
                    break;
                case "E":
                    player.setOrientation("N");
                    break;
                case "O":
                    player.setOrientation("S");
                    break;
            }
        }


        public void playerMoveForward(Player player)
        {
            List<Item> itemInFront = new List<Item>();
            int x = player.getLocationX();
            int y = player.getLocationY();

            switch (player.getOrientation())
            {
                case "N":
                    y--;
                    break;
                case "S":
                    y++;
                    break;
                case "E":
                    x++;
                    break;
                case "O":
                    x--;
                    break;
            }
            itemInFront = getItemAt(x, y);
            if (isWalkable(itemInFront))
            {
                player.setLocationX(x);
                player.setLocationY(y);
            }
            else
                return;

            foreach (Item item in itemInFront)
            {
                switch(item.getType())
                {
                    case itemType.TREASURE:
                        Treasure treasure = ((Treasure)item);
                        treasure.takeTreasure(player);
                        if (treasure.getTreasure() <= 0)
                            _map.Remove(item);
                        break;
                }
            }
        }

        public bool isWalkable(List<Item> items)
        {
            foreach (Item item in items)
            {
                switch (item.getType())
                {
                    case itemType.PLAYER:
                        Console.WriteLine("[TreasureMap] isWalkable : [" + item.getLocationX() + ", " + item.getLocationY() + "] " + item.getType() + " can't go there");
                        return false;

                    case itemType.MOUNTAINE:
                        Console.WriteLine("[TreasureMap] isWalkable : [" + item.getLocationX() + ", " + item.getLocationY() + "] " + item.getType() + " can't go there");
                        return false;
                }
            }
            return true;
        }

        public List<Item> getItemAt(int x, int y)
        {
            List<Item> items = new List<Item>();

            foreach(Item item in _map)
            {
                if (item.getLocationX() == x && item.getLocationY() == y)
                {
                    items.Add(item);
                    Console.WriteLine("[TreasureMap] getItemAt : [" + item.getLocationX() + ", " + item.getLocationY() + "] " + item.getType() + " is in front of you");
                }
            }
            return items;
        }

        public List<Player> getPlayerList()
        {
            return _players;
        }
        
        public List<Item> getMap()
        {
            return _map;
        }

        public int getSizeX()
        {
            return _sizeX;
        }

        public int getSizeY()
        {
            return _sizeY;
        }
    }
}
