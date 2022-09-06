using System;
using System.Collections;

namespace SpitefulPatron
{
    class Program
    {
        static void Main(string[] args)
        {
            IPv4 ip = new IPv4();
            byte[] bytes = ip.Content();
            foreach (var item in bytes)
            {
                Console.Write($"{item:X2}");
            }
        }
    }
}
