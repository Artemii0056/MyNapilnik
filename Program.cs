using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode7
{
    class Program
    {
        public static void CreateNewObject()
        {
            //Создание объекта на карте
        }

        public static void InstallChance()
        {
            _chance = Random.Range(0, 100);
        }

        public static int DefineSalary(int hoursWorked)
        {
            return _hourlyRate * hoursWorked;
        }
    }
}
