using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.FoodClasses
{
    internal class Calzone : FoodItem
    {
        // Константы

        /// <summary>
        /// Название по умолчанию.
        /// </summary>
        private const string DEFAULT_CALZONE_NAME = "Кальцоне";

        public Calzone(string? name, short weight) : base(name, weight)
        {
            if (name == null || name == string.Empty) this.name = DEFAULT_CALZONE_NAME;
        }

        public override void Deconstruct(out string name, out short weight)
        {
            base.Deconstruct(out name, out weight);
        }

        // выпекать кальцоне
        new public void Bake() => base.Bake();

        // порезать кальцоне
        new public void Cut(byte slices) => base.Cut();

        // съесть кальцоне
        new public void Eat(byte pieces) => base.Eat(pieces);

        public override void PrintFoodShort() => base.PrintFoodShort();

        public override void PrintFoodStatus()
        {
            Console.WriteLine("Тип блюда - кальцоне.");
            base.PrintFoodStatus();
        }

        public override void PrintFoodFull()
        {
            PrintFoodShort();
            PrintFoodStatus();
        }
    }
}
