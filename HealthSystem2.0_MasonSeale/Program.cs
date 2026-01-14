using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static HealthSystem2._0_MasonSeale.Program;

namespace HealthSystem2._0_MasonSeale
{
    internal class Program
    {
        static Random rng = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your name");
            string name = Console.ReadLine();
            Player player = new Player(name, 100, 100);

            Console.WriteLine($"Name: {player.Name} Shield: {player.GetShield()} Health: {player.GetHealth()} Status: {player.GetStatusString()}");
            Console.ReadKey(true);
            while (true)
            {
                
                Console.WriteLine("D takes damage H heals, please press a key");
                ConsoleKeyInfo holder;
                while (true)
                {
                    holder = Console.ReadKey(true);
  
                    if (holder.Key == ConsoleKey.H)
                    {
                        Console.Clear();
                        player.Heal(rng.Next(1, 21));
                        break;
                    }
                    if (holder.Key == ConsoleKey.D)
                    {
                        Console.Clear();
                        player.TakeDamage(rng.Next(1, 21));
                        break;

                    }
                    else
                    {
                    
                        Console.WriteLine("D takes damage H heals, please press a key");
                    }
                }
                if(player.GetHealth() <= 0)
                {
                    player.dead();
                    Console.WriteLine($"Name: {player.Name} Shield: {player.GetShield()} Health: {player.GetHealth()} Status: {player.GetStatusString()}");
                    Console.WriteLine("You Died, Press any key to close");
                    Console.ReadKey();
                    break;
                }
                Console.WriteLine($"Name: {player.Name} Shield: {player.GetShield()} Health: {player.GetHealth()} Status: {player.GetStatusString()}");

            }
 
        }
        public class Player
        {
            public Player(string name,int maxHealth, int maxShield)
            {
                _name = name;
                _health.Max = maxHealth;
                _shield.Max = maxShield;
                _shield.Restore();
                _health.Restore();
            }
            string _name;
            public void Heal(int amount)
            {
                _health.Heal(amount);
            }
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
            public void dead()
            {
                _health.CurrentHP = 0;
            }
            public string GetStatusString()
            {
                if(_health.CurrentHP > 90)
                {
                    return "All good";
                }
                if (_health.CurrentHP > 70)
                {
                    return "A little hurt";
                }
                if (_health.CurrentHP > 40)
                {
                    return "Hurting";
                }
                if (_health.CurrentHP > 10)
                {
                    return "Really Hurting";
                }
                if(_health.CurrentHP > 0)
                {
                    return "Critical";
                }
                else
                {
                    _health.CurrentHP = 0;
                    return "Dead";

                }
            }

            Health _health = new Health();
            public int GetHealth()
            {
                return _health.CurrentHP;
            }
            Health _shield = new Health();
            public int GetShield()
            {

                return _shield.CurrentHP;
            }
            
            public void TakeDamage(int amount)
            {
                int PotentialExsess = _shield.TakeDamagePart2(amount);
                if (_shield.CurrentHP <= 0)
                {
                    _shield.CurrentHP = 0;
                    _health.TakeDamagePart2(PotentialExsess * -1);
                }
                else
                {
                    _shield.CurrentHP = PotentialExsess;
                }

            }
        }
        public class Health
        {
            int _maxhp;
            public int Max
            {
                get { return _maxhp; }
                set { _maxhp = value; }
            }
            int _currenthp;
            public int CurrentHP
            {
                get { return _currenthp; }
                set { _currenthp = value; }
            }
            public void Restore()
            {
                _currenthp = _maxhp;
            }
            public int TakeDamagePart2(int amount)
            {
                if (amount < 0)
                {
                    Console.WriteLine("Negative damage is not allowed");
                    return _currenthp;
                }
                else if(_currenthp - amount <= 0)
                {
                    int exsess = _currenthp - amount;
                    _currenthp = exsess;
                    return exsess;
                }
                else
                {
                    _currenthp -= amount;
                    return _currenthp;
                }
            }
            public int Heal(int amount)
            {
                if(amount <= 0)
                {
                    return _currenthp;
                }
                if (amount + _currenthp > _maxhp)
                {
                    _currenthp = _maxhp;
                    return _currenthp;
                }
                else
                {
                    _currenthp += amount;
                    return _currenthp;
                }

            }


        }
    }
}
