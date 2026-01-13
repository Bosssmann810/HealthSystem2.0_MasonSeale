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
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your name");
            string name = Console.ReadLine();
            Player player = new Player(name, 100, 100);
            player.TakeDamage(50);
            Console.WriteLine(player.GetHealth());
            Console.ReadKey();
        }
        public class Player
        {
            public Player(string name,int Maxhealth, int MaxShield)
            {
                _name = name;
                _health.Max = Maxhealth;
                _shield.Max = MaxShield;
                _shield.Restore();
                _health.Restore();
            }
            string _name;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            Health _health = new Health();
            public int GetHealth()
            {
                return _health.CurrentHP;
            }
            Health _shield = new Health();
            public int GetSheild()
            {
                return _shield.CurrentHP;
            }
            
            public void TakeDamage(int amount)
            {
                int PotentialExsess = _shield.TakeDamage2(amount);
                if (_shield.CurrentHP <= 0)
                {
                    _shield.CurrentHP = 0;
                    _health.TakeDamage2(PotentialExsess * -1);
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
            public int TakeDamage2(int amount)
            {
                if (amount < 0)
                {
                    Console.WriteLine("Negative damage is not allowed");
                    return _currenthp;
                }
                else if(_currenthp - amount <= 0)
                {
                    int exsess = _currenthp - amount;
                    Console.WriteLine("went over");
                    _currenthp = exsess;
                    return exsess;
                }
                else
                {
                    _currenthp -= amount;
                    return _currenthp;
                }
            }

        }
    }
}
