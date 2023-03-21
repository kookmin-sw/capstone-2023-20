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
		public bool lockon;

        //김원진 interaction - E키 누르면 해당 오브젝트와 상호작용
        public bool interaction;

        //김원진 inventory - I키 누르면 Inventory UI 활성화
        public bool inventory;

		//김원진 minimap - m키 누르면 minimap UI 활성화
		public bool minimap;

		//KKB options - esc key
		public bool option;

		//KKB mouseSensitive
		private float sensitivity = 1f;

		public bool LookLock;
		public bool MoveLock;
		public bool UILock;

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
				LookInput(value.Get<Vector2>() * sensitivity);
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
		//김원진 - InteractionInput 함수에 버튼이 눌렸는지 안눌렸는지 값 넘겨주는 함수
		public void OnInteraction(InputValue value)
		{
			InteractionInput(value.isPressed);
		}

		//김원진 - InventoryInput 함수에 버튼이 눌렸는지 안눌렸는지 값 넘겨주는 함수
		public void OnInventory(InputValue value)
		{
			InventoryInput(value.isPressed);
		}

		//김원진 - MinimapInput 함수에 버튼이 눌렸는지 안눌렸는지 값 넘겨주는 함수
		public void OnMinimap(InputValue value){
			MinimapInput(value.isPressed);
		}

		//KKB - 옵션창 입력 유무

		public void OnOption(InputValue value)
		{
			OptionInput(value.isPressed);
		}
#endif


        // 조사중 일 때 못움직이게 하는게 아니라 조사를 하면 움직임을 아예 멈추게하도록 수정 필요함.
        public void MoveInput(Vector2 newMoveDirection)
		{
			if (!MoveLock)
			{
                move = newMoveDirection;
            }
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			if (!LookLock)
			{
                look = newLookDirection;
            }
			
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
			investigate = newInvestigateState;

        }

        //김원진 - 상호작용 함수 추가;
        public void InteractionInput(bool newInteractionState)
        {
            if (inventory == false)
                interaction = newInteractionState;

        }

        //김원진 - 인벤토리 함수 추가;
        //김원진 - 인벤토리가 열려있을때 누르면 닫히도록 함
        public void InventoryInput(bool newInventoryState)
        {
			Debug.Log("Inventory Pressed");
            if (inventory == false && UILock == false)
            {
				UILock = true;
                move = new Vector2(0, 0);
                inventory = newInventoryState;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (inventory == true && UILock == true)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                inventory = false;
				UILock = false;
            }


        }	
		//KKB - option(esc)
		public void OptionInput(bool newOptionState)
		{
			if(!option && !UILock)
			{
				UILock = true;
                move = new Vector2(0, 0);
                option = newOptionState;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
			else if (option && UILock)
			{
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                UILock = false;
				option = false;
			}
		}

		//김원진 - 미니맵 함수 추가
		//김원진 - 미니맵이 열려있을때 누르면 닫히도록 함.
		public void MinimapInput(bool newMinimapState)
		{
			if (minimap == false && UILock == false) {
				UILock = true;
				minimap = newMinimapState;
			}
			else if (minimap == true && UILock == true) {
				UILock = false;
				minimap = false;
			}
		}

	


        public void PlayerLockOn()
		{
			Debug.Log("Lockon");
            if (lockon == false)
            {
                lockon = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                cursorInputForLook = false;
				look = new Vector2(0, 0);

            }
            else
            {
                lockon = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                cursorInputForLook = true;
            }
        }

		public void PlayerMoveLock()
		{
			move = new Vector2(0, 0);
			look = new Vector2(0, 0);
			LookLock = true;
			MoveLock = true;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
        }

		public void PlayerMoveUnlock()
		{
			LookLock = false;
			MoveLock = false;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}

        private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		//KKB - dpi 조정 함수
		public void SetSensitivity(float newSensitivity)
		{
			sensitivity = newSensitivity;
		}


    }
	
}