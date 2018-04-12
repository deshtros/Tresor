using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tresor.Items;

namespace Tresor.Items
{
    public class Mountain : Item
    {
        private itemType _itemType;

        private int _locationX;
        private int _locationY;

        public Mountain(int x, int y)
        {
            _itemType = itemType.MOUNTAINE;
            _locationX = x;
            _locationY = y;
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
            return ("M - " + _locationX + " - " + _locationY);
        }
    }
}
