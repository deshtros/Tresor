using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tresor.Items
{
    public enum itemType
    {
        MOUNTAINE,
        PLAYER,
        TREASURE
    };

    public interface Item
    {
        itemType getType();

        int getLocationX();
        int getLocationY();

        String getLine();
    }
}
