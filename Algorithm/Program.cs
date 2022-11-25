using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeHamburger_Solution solution = new MakeHamburger_Solution();

            //int[] ingredient = new int[] { 2, 1, 1, 2, 3, 1, 2, 3, 1 };
            int[] ingredient = new int[] { 1, 3, 2, 1, 2, 1, 3, 1, 2 };
            int ret = solution.solution(ingredient);
            Console.WriteLine(ret);
        }
    }
}
