using SingletonPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Singleton pattern or creational pattern
namespace SingletonPattern
{
    public sealed class Singleton
    {
        private static Singleton instance = null;

        private Singleton() { }

        public static Singleton Instance
        {
            get 
            { 
                if (instance == null) 
                    instance = new Singleton();

                return instance;
            }
        }

        public void ShowMessage(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}

class MyClass
{
    static void Main(string[] args)
    {
        //Singleton obj = new Singleton();
        //this will not work as the constructor is private
        Singleton obj = Singleton.Instance;
        obj.ShowMessage("Hello , Singleton pattern");
    }
}
