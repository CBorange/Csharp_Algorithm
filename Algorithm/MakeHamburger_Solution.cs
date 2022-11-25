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
        private int _hamburgerCnt = 0;
        public int solution(int[] ingredient)
        {
            LinkedList<int> ingredientList = new LinkedList<int>(ingredient);
            LinkedListNode<int> cursor = ingredientList.First;


            // 빵(1), 야채(2), 고기(3), 빵(1) 순서로 데이터 들어오면 Cnt++;
            // 재료 순회하면서 조건검사
            while (cursor != null)
            {
                // 현재 노드를 기준으로 뒤로 3칸 앞으로 3칸을 검사하여 햄버거를 만들 수 있는지 확인하고
                // 햄버거를 만들 수 있으면 햄버거를 만들고 사용한 재료를 리스트에서 제외하고 햄버거 개수를 추가한다.

                LinkedListNode<int> ingredientStart = null;
                LinkedListNode<int> ingredientEnd = null;

                if (Verify_CanMakeBurgerNearBy(cursor, out ingredientStart, out ingredientEnd))
                {
                    LinkedListNode<int> deleteCursor = ingredientStart;
                    while ((deleteCursor != null))
                    {
                        // deleteCursor를 List에서 제거하는 순간 앞/뒤 링크가 깨지므로 다음 Delete Node를 미리 캐싱
                        LinkedListNode<int> nextDel = deleteCursor.Next;
                        ingredientList.Remove(deleteCursor);
                        deleteCursor = nextDel;

                        if (deleteCursor == ingredientEnd)
                        {
                            cursor = deleteCursor.Next;
                            ingredientList.Remove(deleteCursor);    // deleteCursor가 마지막 재료면 삭제하고 break
                            break;
                        }
                    }
                    _hamburgerCnt++;
                }
                else
                    cursor = cursor.Next;
            }

            return _hamburgerCnt;
        }

        /// <summary>
        /// 현재 노드 위치 기준에서 앞/뒤로 3칸에 있는 재료를 검사하여 햄버거를 만들 수 있는지 확인한다.<br></br>
        /// 햄버거를 만들 수 있다면 재료 4개(빵, 야채, 고기, 빵)의 연결리스트 상의 시작 위치와 끝 위치 노드를 반환한다.
        /// </summary>
        /// <param name="curNode"></param>
        /// <returns></returns>
        private bool Verify_CanMakeBurgerNearBy(LinkedListNode<int> curNode, out LinkedListNode<int> ingredientStart, out LinkedListNode<int> ingredientEnd)
        {
            ingredientStart = null;
            ingredientEnd = null;

            LinkedListNode<int> cursor = curNode;
            List<LinkedListNode<int>> cachedIngredients = new List<LinkedListNode<int>>();

            // 앞 : -> 방향, 뒤 : <- 방향 의미
            // 1. 뒤로 최대 3칸의 재료 캐싱(Previous 방향)
            // 1-1. 최대 3칸 커서를 뒤로 이동시킨다.
            for ( int i = 0; i < 3; ++i ) 
            {
                if (cursor.Previous != null)
                    cursor = cursor.Previous;
                else
                    break;
            }

            // 1-2. 뒤로 이동한 커서 위치에서 앞으로 커서를 이동시키면서 curNode(기준 노드) 까지 재료를 캐싱한다.
            while (cursor != curNode)
            {
                cachedIngredients.Add(cursor);
                cursor = cursor.Next;
            }
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
            int matchCnt = 0;   // 재료배열 세트가 맞은 개수 [빵(1) -> 야채(2) -> 고기(3) -> 빵(1)] 순서로 연속으로 맞게 들어온 횟수 ex) 빵 -> 야채 까지 맞았으면 2

            ingredientStart = cachedIngredients.First();
            ingredientEnd = null;
            //LinkedListNode<int> target = cachedIngredients.First();
            foreach (LinkedListNode<int> target in cachedIngredients)
            {
                if (answers[matchIndex] == target.Value)
                {
                    matchCnt++;
                    matchIndex++;
                    if (matchCnt == 1)  // 첫번째 재료라면 Start에 캐싱
                        ingredientStart = target;

                    ingredientEnd = target;
                    if (matchCnt == 4)
                    {
                        // 재료배열과 검사하여 연속으로 4개가 맞았다면(재료 세트 1개를 찾았다면) 재료 시작->끝 Node 반환하고 종료
                        return true;
                    }
                }
                else
                {
                    matchIndex = 0;
                    matchCnt = 0;

                    // 이전 재료와는 연속으로 이어지지 않지만, 현재 target 재료가 재료배열의 첫번째 배열 요소와 동일한경우
                    if (target.Value == answers[0])
                    {
                        matchIndex = 1;
                        matchCnt = 1;
                        ingredientStart = target; // 재료 위치 초기화(지금 target이 재료 배열에 부합하지 않으므로 여기서부터 다시 시작)
                        ingredientEnd = ingredientStart;
                    }
                }
            }

            // 햄버거 생성 조건 검사에서 모든 캐싱재료리스트를 순회했음에도
            // 생성할 수 있는 조건이 충족되지 않았다면 생성불가상태로 return
            return false;
        }
    }
}
