using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /// <summary>
    /// 프로그래머스 코테 LV.1 [햄버거 만들기] 솔루션
    /// </summary>
    public class MakeHamburger_Solution
    {
        int _hamburgerCnt = 0;
        public int solution(int[] ingredient)
        {
            LinkedList<int> ingredientList = new LinkedList<int>(ingredient);
            LinkedListNode<int> cursor = ingredientList.First;


            // 빵(1), 야채(2), 고기(3), 빵(1) 순서로 데이터 들어오면 Cnt++;
            // 재료 순회하면서 조건검사
            while (cursor != null)
            {
                LinkedListNode<int> ingredientStart = null;
                LinkedListNode<int> ingredientEnd = null;
                if (Verify_CanMakeBurgerNearBy(cursor, out ingredientStart, out ingredientEnd))
                {

                }
                cursor = cursor.Next;
            }

            return _hamburgerCnt;
        }

        /// <summary>
        /// 현재 노드 위치에서 앞/뒤로 3칸에 있는 재료를 검사한다.<br></br>
        /// </summary>
        /// <param name="curNode"></param>
        /// <returns></returns>
        private bool Verify_CanMakeBurgerNearBy(LinkedListNode<int> curNode, out LinkedListNode<int> ingredientStart, out LinkedListNode<int> ingredientEnd)
        {
            ingredientStart = null;
            ingredientEnd = null;

            LinkedListNode<int> cursor = curNode;
            List<LinkedListNode<int>> cachedIngredients = new List<LinkedListNode<int>>();

            // 1. 뒤로 최대 3칸의 재료 캐싱(Previous 방향)
            // 1-1. 최대 3칸 커서를 뒤로 이동시킨다.
            for (int i = 0; i < 3; ++i) 
            {
                cursor = cursor.Previous;
                if (cursor == null)
                    break;
            }

            // 1-2. 앞으로 커서를 이동시키면서 curNode(기준 노드) 까지 재료를 캐싱한다.
            while (cursor != curNode)
                cachedIngredients.Add(cursor);
            cachedIngredients.Add(cursor);    // curNode 재료 저장(이 시점에서는 cursor와 curNode가 동일하다. while 조건 참조)

            // 2. 앞으로 3칸의 재료 캐싱(Next 방향)
            // 2-1. 최대 3칸 커서를 앞으로 이동시키면서 재료를 캐싱한다.
            for (int i = 0; i < 3; ++i)
            {
                cursor = cursor.Next;

                if (cursor == null)
                    break;
                cachedIngredients.Add(cursor);
            }

            // 캐싱된 재료가 4개 미만이라면 조건 부합하지 않음, 종료
            if (cachedIngredients.Count < 4)
                return false;

            // 캐싱된 재료 순회하면서 햄버거 생성 조건에 부합하는지 검사
            int[] answers = new int[4] { 1, 2, 3, 1 };  // 빵(1) -> 야채(2) -> 고기(3) -> 빵(1) 정답배열
            int matchIndex = 0; // 현재 보고있는 정답배열에서의 Index

            int matchCnt = 0;
            ingredientStart = cachedIngredients.First();
            ingredientEnd = ingredientStart;
            foreach (LinkedListNode<int> target in cachedIngredients)
            {
                if (answers[matchCnt] == target.Value)
                {
                    matchCnt++;
                    matchIndex++;
                    if (matchCnt == 4)
                    {
                        // 부합한다면 재료 시작->끝 Node 반환하고 종료
                    }
                }
                else
                {
                    matchIndex = 0;
                    matchCnt = 0;
                }
            }

            // 햄버거 생성 조건 검사에서 모든 캐싱재료리스트를 순회했음에도
            // 생성할 수 있는 조건이 충족되지 않았다면 생성불가상태로 return
        }
    }
}
