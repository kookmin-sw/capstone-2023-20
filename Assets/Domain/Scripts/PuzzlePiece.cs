using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Puzzles
{
    public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        public int snapOffset = 30;
        public JigsawPuzzle puzzle;
        public int piece_no;

        bool CheckSnapPuzzle()
        {
            for (int i = 0; i < puzzle.puzzlePosSet.transform.childCount; i++)
            {
                //��ġ�� �ڽĿ�����Ʈ�� ������ �̹� ���������� ������ ���Դϴ�.
                if (puzzle.puzzlePosSet.transform.GetChild(i).childCount != 0)
                {
                    continue;
                }
                else if (Vector2.Distance(puzzle.puzzlePosSet.transform.GetChild(i).position, transform.position) < snapOffset)
                {
                    transform.SetParent(puzzle.puzzlePosSet.transform.GetChild(i).transform);
                    transform.localPosition = Vector3.zero;
                    return true;
                }
            }
            return false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //��ġ�ϴ� ��ġ�� ���� ��� �θ��ڽ� ���踦 �����մϴ�.
            if (!CheckSnapPuzzle())
            {
                transform.SetParent(puzzle.puzzlePieceSet.transform);
            }

            if (puzzle.IsClear())
            {
                Debug.Log("Clear");
                GameObject.Find("Puzzle2").GetComponent<Activate1>().Activate();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            piece_no = gameObject.name[gameObject.name.Length - 1] - '0';
        }
    }

}
