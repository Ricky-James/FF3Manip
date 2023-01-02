using System;
using System.Security.Principal;

namespace FF3Manip
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler (OnProcessExit);
            TimeZone.Instance.SaveTimeZone();

            Console.WriteLine("Press Any Key when ready...");
            Console.ReadKey();

            ManipList.Instance.AltarCave();
            Console.WriteLine("Altar Cave Manip Complete.\nPress Any Key to begin Land Turtle manip");
            Console.ReadKey();

            ManipList.Instance.LandTurtle();
            Console.WriteLine("Land Turtle Manip Complete.\nPress Any Key to exit.");
            Console.ReadKey();
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            TimeZone.Instance.RevertTime();
        }
    }
}