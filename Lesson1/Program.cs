using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napilnik1
{
    class Weapon
    {
        private Bullet _bullet;
        private int _bulletsCount; 

        public bool CanFire => _bulletsCount > 0;

        public void Fire(Player player)
        {
            if (CanFire == true && player != null)
            {
                player.TakeDamage(_bullet.Damage);
                _bulletsCount--;
            }           
        }
    }

    class Bullet
    {
        private int _damage;

        public Bullet(int damage)
        {
            if (damage <= 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            _damage = damage;
        }           

        public int Damage => _damage;
    }

    class Player
    {
        private int _health;

        public Player(int health)
        {
            if (health <= 0)
                throw new ArgumentOutOfRangeException(nameof(health));

            _health = health;
        }

        public void TakeDamage(int damage)
        {
            if (damage > 0)
                _health -= damage; 
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public Bot(Weapon weapon)
        {
            if (weapon == null)
                throw new ArgumentOutOfRangeException(nameof(weapon));

            _weapon = weapon;
        }

        public void OnSeePlayer(Player player)
        {
            if (player != null)
                _weapon.Fire(player);
        }
    }    
}
