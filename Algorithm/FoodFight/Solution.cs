using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.FoodFight
{
    public class Solution
    {
        private int _foodNo = 1;
        private StringBuilder _sb = new StringBuilder();

        public string solution(int[] food)
        {
            // 좌->우 음식 나열
            for (int i = 1; i < food.Length; ++i)
            {
                int foodCnt = food[i];  // i 번째 음식의 개수
                if (PrintFood(foodCnt))
                {
                    _foodNo++;
                    continue;
                }
                _foodNo++;   // 음식번호 증가
            }

            // 물
            _sb.Append("0");

            _foodNo--;
            // 우->좌 음식 나열
            for (int i = food.Length - 1; i > 0; --i)
            {
                int foodCnt = food[i];  // i 번째 음식의 개수
                if (PrintFood(foodCnt))
                {
                    _foodNo--;
                    continue;
                }
                _foodNo--;   // 음식번호 감소
            }

            string answer = _sb.ToString();
            return answer;
        }

        private bool PrintFood(int foodCnt)
        {
            if (foodCnt % 2 != 0)
            {
                // 짝수가 아니면서 음식을 1개 제외했을 때 2개 이하가 되는 음식은 Pass
                foodCnt -= 1;
                if (foodCnt < 2)
                {
                    return false;
                }
            }

            // 음식 개수의 반 만큼 나열
            int halfFoodCnt = foodCnt / 2;
            for (int j = 0; j < halfFoodCnt; ++j)
                _sb.Append(_foodNo);

            return true;
        }
    }
}
