using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Calzone : Food
    {
        private byte slices; // количество кусочков кальцоне

        public Calzone(string name, short weight) : base(name, weight) => slices = 1;

        // выпекать кальцоне
        new public void Bake()
        {
            if (base.foodIsBaked == true)
            {
                Console.WriteLine($"Кальцоне уже готов.");
                return;
            }
            base.Bake();
        }

        // порезать кальцоне
        new public void Cut()
        {
            if (base.foodIsCut == true)
            {
                Console.WriteLine($"Кальцоне уже разрезан пополам.");
                return;
            }
            this.slices = 2;
            base.Cut();
            Console.WriteLine($"Кол-во кусочков - {slices}.");
        }

        // съесть кальцоне
        new public void Eat()
        {
            if (base.foodIsEaten == true)
            {
                Console.WriteLine($"Кальцоне уже съеден.");
                return;
            }
            base.Eat();
        }
    }
}
