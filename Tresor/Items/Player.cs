using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tresor.Items;

namespace Tresor
{
    public class Player : Item
    {
        private itemType _itemType;

        private String _name;
        private int _treasure;

        private String _orientation;
        private int _locationX;
        private int _locationY;

        private String _moveInstruction;
        private int _nextInstruction;

        public Player(String name, int x, int y, String orientation, String moveInstruction)
        {
            _itemType = itemType.PLAYER;
            _name = name;
            _locationX = x;
            _locationY = y;
            _orientation = orientation;
            _moveInstruction = moveInstruction;
            _nextInstruction = 0;
        }

        public itemType getType()
        {
            return _itemType;
        }

        public int getLocationX()
        {
            return _locationX;
        }

        public void setLocationX(int x)
        {
            _locationX = x;
        }

        public int getLocationY()
        {
            return _locationY;
        }

        public void setLocationY(int y)
        {
            _locationY = y;
        }

        public String getOrientation()
        {
            return _orientation;
        }

        public void setOrientation(String orientation)
        {
            _orientation = orientation;
        }

        public string getNextInstruction()
        {
            if (_moveInstruction.Length <= _nextInstruction)
                return "";
            String instruction = _moveInstruction[_nextInstruction].ToString();
            _nextInstruction++;
            return instruction;
        }

        public String getName()
        {
            return _name;
        }

        public void addTreasure(int nb)
        {
            _treasure += nb;
        }

        public int getTreasure()
        {
            return _treasure;
        }

        public String getLine()
        {
            return ("C - " + _name + " - " + _locationX + " - " + _locationY + " - " + _orientation + " - " + _treasure);
        }
    }
}
