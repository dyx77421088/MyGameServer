using Common.Model;
using Common.Util;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 尝试
            MyList<Player> players = new MyList<Player>();

            players.Add(new Player(1, "张三", new Vector3DPosition() { X = 1, Y = 2, Z = 4 }));
            players.Add(new Player(3, "里斯", new Vector3DPosition() { X = 22, Y = 43, Z = 34 }));
            players.Add(new Player(4, "王五", new Vector3DPosition() { X = 33, Y = 56, Z = 6 }));
            players.Add(new Player(5, "李六", new Vector3DPosition() { X = 53, Y = 23, Z = 63 }));
            Console.WriteLine(ToGson.Info(200, "我是效果i", players));
            Console.ReadKey();
        }
    }
}
