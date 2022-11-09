using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeHamburger_Solution solution = new MakeHamburger_Solution();

            int[] ingredient = new int[] { 2, 1, 1, 2, 3, 1, 2, 3, 1 };
            int ret = solution.solution(ingredient);
            Console.WriteLine(ret);
        }
    }
}
