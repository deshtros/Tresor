using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tresor.Items;

namespace Tresor
{
    public class Treasure : Item
    {

        private itemType _itemType;

        private int _locationX;
        private int _locationY;

        private int _count;

        public Treasure(int x, int y, int nb)
        {
            _itemType = itemType.TREASURE;
            _locationX = x;
            _locationY = y;
            _count = nb;
        }

        public void takeTreasure(Player player)
        {
            if (_count <= 0)
                return;
            _count--;
            player.addTreasure(1);
            Console.WriteLine("[Treasure] takeTreasure : [" + player.getLocationX() + ", " + player.getLocationY() + "] - " + player.getOrientation() + " Player " + player.getName() + "(" + player.getTreasure() + ") take " + 1 + " tresure");
        }

        public int getTreasure()
        {
            return _count;
        }

        public itemType getType()
        {
            return _itemType;
        }

        public int getLocationX()
        {
            return _locationX;
        }

        public int getLocationY()
        {
            return _locationY;
        }

        public String getLine()
        {
            return ("T - " + _locationX + " - " + _locationY + " - " + _count);
        }
    }
}
