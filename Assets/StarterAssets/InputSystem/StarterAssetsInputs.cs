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

        //����� interaction - EŰ ������ �ش� ������Ʈ�� ��ȣ�ۿ�
        public bool interaction;

        //����� inventory - IŰ ������ Inventory UI Ȱ��ȭ
        public bool inventory;

		//����� minimap - mŰ ������ minimap UI Ȱ��ȭ
		public bool minimap;

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
		//����� - InteractionInput �Լ��� ��ư�� ���ȴ��� �ȴ��ȴ��� �� �Ѱ��ִ� �Լ�
		public void OnInteraction(InputValue value)
		{
			InteractionInput(value.isPressed);
		}

		//����� - InventoryInput �Լ��� ��ư�� ���ȴ��� �ȴ��ȴ��� �� �Ѱ��ִ� �Լ�
		public void OnInventory(InputValue value)
		{
			InventoryInput(value.isPressed);
		}

		//����� - MinimapInput �Լ��� ��ư�� ���ȴ��� �ȴ��ȴ��� �� �Ѱ��ִ� �Լ�
		public void OnMinimap(InputValue value){
			MinimapInput(value.isPressed);
		}
#endif


        // ������ �� �� �������̰� �ϴ°� �ƴ϶� ���縦 �ϸ� �������� �ƿ� ���߰��ϵ��� ���� �ʿ���.
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

		// �����ư �Է½� ���콺 ���̱�, ���콺 ȭ�� ���󰡱�, ���콺 ���� ����
		public void InvestigateInput(bool newInvestigateState)
		{
			investigate = newInvestigateState;

        }

        //����� - ��ȣ�ۿ� �Լ� �߰�;
        public void InteractionInput(bool newInteractionState)
        {
            if (inventory == false)
                interaction = newInteractionState;

        }

        //����� - �κ��丮 �Լ� �߰�;
        //����� - �κ��丮�� ���������� ������ �������� ��
        public void InventoryInput(bool newInventoryState)
        {
			Debug.Log("Inventory Pressed");
            if (inventory == false)
            {
                move = new Vector2(0, 0);
                inventory = newInventoryState;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                inventory = false;
            }


        }

		//����� - �̴ϸ� �Լ� �߰�
		//����� - �̴ϸ��� ���������� ������ �������� ��.
		public void MinimapInput(bool newMinimapState)
		{
			if (minimap == false)
				minimap = newMinimapState;
			else
				minimap = false;

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

            }
            else
            {
                lockon = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                cursorInputForLook = true;
            }
        }

        private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

	}
	
}