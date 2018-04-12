using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tresor.Items;

namespace Tresor
{
    public class TresorCore
    {
        Gameplay             _gameplay;
        List<Player>            _players;

        public TresorCore()
        {
            _gameplay = new Gameplay();
        }

        public void init(string[] args)
        {
            string line;
            
            System.IO.StreamReader file = new System.IO.StreamReader(@"D:\Projet\carbonit\Tresor\Tresor\Ressource\test.txt");
            while ((line = file.ReadLine()) != null)
            {
               String[] param = line.Replace(" ", "").Split('-');
                if (param == null || param.Count() == 0)
                    continue;
                switch (param[0])
                {
                    case "#":
                        Console.WriteLine("[TresorCore] init : Comment -> " + param.ToString());
                        break;
                    case "C":
                        _gameplay.newMap(param);
                        break;
                    case "M":
                        _gameplay.addMontain(param);
                        break;
                    case "T":
                        _gameplay.addTreasure(param);
                        break;
                    case "A":
                        _gameplay.addPlayer(param);
                        break;
                    default:
                        break;
                }
            }
            file.Close();
            _players = _gameplay.getPlayerList();
        }
        
        public void start()
        {
            List<Player> playerRemove = new List<Player>();
            Console.WriteLine("[TresorCore] start : Start playing");
            while (_players.Count() != 0)
            {
                foreach (Player player in _players)
                {
                    String instruction = player.getNextInstruction();
                    switch (instruction)
                    {
                        case "A":
                            Console.WriteLine("[TresorCore] start : [" + player.getLocationX() + ", " + player.getLocationY() + "] - " + player.getOrientation() + " Player " + player.getName() + "(" + player.getTreasure() + ") move forward");
                            _gameplay.playerMoveForward(player);
                            break;
                        case "G":
                            Console.WriteLine("[TresorCore] start : [" + player.getLocationX() + ", " + player.getLocationY() + "] - " + player.getOrientation() + " Player " + player.getName() + "(" + player.getTreasure() + ") turn left");
                            _gameplay.playerTurnLeft(player);
                            break;
                        case "D":
                            Console.WriteLine("[TresorCore] start : [" + player.getLocationX() + ", " + player.getLocationY() + "] - " + player.getOrientation() + " Player " + player.getName() + "(" + player.getTreasure() + ") move right");
                            _gameplay.playerTurnRight(player);
                            break;
                        default:
                            Console.WriteLine("[TresorCore] start : [" + player.getLocationX() + ", " + player.getLocationY() + "] - " + player.getOrientation() + " Player " + player.getName() + "(" + player.getTreasure() + ") end instruction ...");
                            playerRemove.Add(player);
                            break;
                    }
                }
                foreach (Player player in playerRemove)
                {
                    _players.Remove(player);
                }
                playerRemove.Clear();
            }
            WriteOutputFile();
        }

        public void WriteOutputFile()
        {
            List<Item> map = _gameplay.getMap();
            int x = _gameplay.getSizeX();
            int y = _gameplay.getSizeY();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\Projet\carbonit\Tresor\Tresor\Ressource\answer.txt"))
            {
                file.WriteLine("C - " + x + " - " + y);
                foreach (Item item in map)
                {
                      file.WriteLine(item.getLine());
                }
            }
        }
    }
}
