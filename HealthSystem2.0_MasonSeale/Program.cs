using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthSystem2._0_MasonSeale.Program;

namespace HealthSystem2._0_MasonSeale
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
        public class Player
        {
            string _name;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            Health _health = new Health();
            Health _sheild = new Health();
            public Player(string name,int Maxhealth, int MaxSheild)
            {
                _name = name;
                _health.Max = Maxhealth;
                _sheild.Max = MaxSheild;
                _sheild.Restore();
                _health.Restore();
            }
            public void TakeDamage(int amount)
            {
                int Potentialexsess = _sheild.TakeDamage2(amount);
                if (_sheild.CurrentHP <= 0)
                {
                    _sheild.CurrentHP = 0;
                    _health.TakeDamage2(Potentialexsess);
                }
                else
                {
                    _sheild.CurrentHP = Potentialexsess;
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
                    int exsess = _currenthp - amount * -1;
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
