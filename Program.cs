using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode3
{
    class Weapon
    {
        public const int BulletToShoot = 1;

        private int _bullets;

        public bool CanShoot() => _bullets >= BulletToShoot;

        public void Shoot() => _bullets -= BulletToShoot;
    }
}
