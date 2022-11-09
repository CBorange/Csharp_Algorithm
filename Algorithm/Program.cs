using System;
using Algorithm.FoodFight;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();

            int[] food = new int[] { 1, 3, 4, 6 };
            string ret = solution.solution(food);
            Console.WriteLine(ret);
        }
    }
}
