using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		public bool investigate;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		private ThirdPlayerController PlayerController;

        public void Awake()
		{
			PlayerController = GetComponent<ThirdPlayerController>();
		}

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnInvestigate(InputValue value) 
		{
			InvestigateInput(value.isPressed);
		}
#endif


		// 조사중 일 때 못움직이게 하는게 아니라 조사를 하면 움직임을 아예 멈추게하도록 수정 필요함.
		public void MoveInput(Vector2 newMoveDirection)
		{

			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		// 조사버튼 입력시 마우스 보이기, 마우스 화면 따라가기, 마우스 고정 제어
		public void InvestigateInput(bool newInvestigateState)
		{
			if (PlayerController.InvestigateValue == true)
			{
				PlayerLockOn();
            }
        }


		public void PlayerLockOn()
		{
            if (investigate == false)
            {
                investigate = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                cursorInputForLook = false;

            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                cursorInputForLook = true;
                investigate = false;
            }
        }

        private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

	}
	
}