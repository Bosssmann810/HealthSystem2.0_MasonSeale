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
            Player player = new Player(name: name, maxHealth: 100, maxShield: 100);

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
                    player.Dead();
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
                _health = new Health(maxHealth);
                _shield = new Health(maxHealth);
            }
            string _name;
            public void Heal(int amount)
            {
                _health.Heal(amount);
            }
            public string Name
            {
                get { return _name; }
                private set { _name = value; }
            }
            public void Dead()
            {
                _health.HpChanger(0);
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
                    _health.HpChanger(0);
                    return "Dead";

                }
            }

            Health _health;
            public int GetHealth()
            {
                return _health.CurrentHP;
            }
            Health _shield;
            public int GetShield()
            {

                return _shield.CurrentHP;
            }
            //takes an amount of damage
            public void TakeDamage(int amount)
            {
                //this is for later
                int potentialExsess = _shield.TakeDamagePart2(amount);
                //if the current hp hits or goes below 0
                if (_shield.CurrentHP <= 0)
                {
                    //set shield to 0
                    _shield.HpChanger(0);
                    //and preform the takedamage() to _health
                    _health.TakeDamagePart2(potentialExsess * -1);
                }
                else
                {
                    //otherwise just take damage to shield
                    _shield.HpChanger(potentialExsess);
                }

            }
        }
        public class Health
        {
            int _maxHp;
            //get and set for _maxHp
            public int MaxHP
            {
                get { return _maxHp; }

                private set { _maxHp = value; }
            }
            int _currentHp;
            //get and set for _currenthp
            public int CurrentHP
            {
                get { return _currentHp; }
                private set { _currentHp = value; }
            }
            //sets _currenthp to _maxhp
            public void Restore()
            {
                _currentHp = _maxHp;
            }
            //takes an amount, if its not negative, subtract that amount from current hp
            public int TakeDamagePart2(int amount)
            {
                if (amount < 0)
                {
                    Console.WriteLine("Negative damage is not allowed");
                    return _currentHp;
                }
                //if current gos past 0, return the leftover amount
                else if(_currentHp - amount <= 0)
                {
                    int exsess = _currentHp - amount;
                    _currentHp = exsess;
                    return exsess;
                }
                //otherwise just return the current hp
                else
                {
                    _currentHp -= amount;
                    return _currentHp;
                }
            }
            //takes an amount, if the amount isn't negative adds that to the health, only up to the max.
            public int Heal(int amount)
            {
                if(amount <= 0)
                {
                    Console.WriteLine("Cannot heal for negative numbers");
                    return _currentHp;
                    
                }
                if (amount + _currentHp > _maxHp)
                {
                    _currentHp = _maxHp;
                    return _currentHp;
                }
                else
                {
                    _currentHp += amount;
                    return _currentHp;
                }

            }
            //this lets you change CurrentHP outside of the class
            public void HpChanger(int amount)
            {
                CurrentHP = amount;
            }
            //health constructor
            public Health(int maxHealth)
            {
                CurrentHP = maxHealth;
                MaxHP = maxHealth;
            }


        }
    }
}
