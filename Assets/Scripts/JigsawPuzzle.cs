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
                //퍼즐위치의 자식이 없으면 모든 퍼즐조각이 놓여지지 않은것입니다.
                if (puzzlePosSet.transform.GetChild(i).childCount == 0)
                {
                    return false;
                }
                //퍼즐조각의 번호와 퍼즐 위치 번호가 일치하지 않으면 퍼즐은 완성되지 않은것입니다.
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