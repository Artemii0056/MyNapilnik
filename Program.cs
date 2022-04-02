using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    class Program
    {
        class Player
        {
            public string Name { get; private set; }
            public int Age { get; private set; }
        }

        class Movement
        {
            public float MovementSpeed { get; private set; }
            public float MovementDirectionX { get; private set; }
            public float MovementDirectionY { get; private set; }

            public void Move()
            {
                //Do move
            }
        }

        class Weapon
        {
            public int WeaponDamage { get; private set; }
            public float WeaponCooldown { get; private set; }

            public void Attack()
            {
                //attack
            }

            public bool IsReloading()
            {
                throw new NotImplementedException();
            }
        }
    }
}
   
