using System;
using System.Collections.Generic;
using System.Text;

namespace State.Concrete._1
{
    class Program
    {
        static void _Main(string[] args)
        {
            Water water = new Water(WaterState.LIQUID);
            water.Heat();
            water.Frost();
            water.Frost();

            Console.Read();
        }
    }
    enum WaterState
    {
        SOLID,
        LIQUID,
        GAS
    }
    class Water
    {
        public WaterState State { get; set; }

        public Water(WaterState ws)
        {
            State = ws;
        }

        public void Heat()
        {
            if (State == WaterState.SOLID)
            {
                Console.WriteLine("Превращаем лед в жидкость");
                State = WaterState.LIQUID;
            }
            else if (State == WaterState.LIQUID)
            {
                Console.WriteLine("Превращаем жидкость в пар");
                State = WaterState.GAS;
            }
            else if (State == WaterState.GAS)
            {
                Console.WriteLine("Повышаем температуру водяного пара");
            }
        }
        public void Frost()
        {
            if (State == WaterState.LIQUID)
            {
                Console.WriteLine("Превращаем жидкость в лед");
                State = WaterState.SOLID;
            }
            else if (State == WaterState.GAS)
            {
                Console.WriteLine("Превращаем водяной пар в жидкость");
                State = WaterState.LIQUID;
            }
        }
    }
}
