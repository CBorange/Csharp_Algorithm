using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            KAKAO_ReportMail reportMail = new KAKAO_ReportMail();

            // param
            string[] id_list = { "muzi", "frodo", "apeach", "neo"};
            string[] report = { "muzi frodo", "apeach frodo", "frodo neo", "muzi neo", "apeach muzi" };
            int k = 2;

            // ret
            int[] ret = reportMail.solution(id_list, report, k);

            Console.WriteLine(ret);
        }
    }
}
