using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzles
{
    public class JigsawPuzzle : MonoBehaviour
    {
        public GameObject puzzlePosSet;
        public GameObject puzzlePieceSet;

        public bool IsClear()
        {
            for (int i = 0; i < puzzlePosSet.transform.childCount; i++)
            {
                //������ġ�� �ڽ��� ������ ��� ���������� �������� �������Դϴ�.
                if (puzzlePosSet.transform.GetChild(i).childCount == 0)
                {
                    return false;
                }
                //���������� ��ȣ�� ���� ��ġ ��ȣ�� ��ġ���� ������ ������ �ϼ����� �������Դϴ�.
                if (puzzlePosSet.transform.GetChild(i).GetChild(0).GetComponent<PuzzlePiece>().piece_no != i)
                {
                    return false;
                }
            }
            return true;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
    }
}