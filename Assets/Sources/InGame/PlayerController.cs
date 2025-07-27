using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Casual
{
	public class PlayerController : MonoBehaviour
	{
		PlayerControllerLogic m_logic = null;		

		Transform m_trfThis;	// �÷��̾� transform
		Animator m_animThis; // �÷��̾� animator

		bool m_isMovable = true; // �̵� �������� ����
		public bool IsMovable
		{
			get { return m_isMovable; }
			set { m_isMovable = value; }
		}		

		// �ִϸ��̼� �ؽ� ��
		readonly int r_iGrounded = Animator.StringToHash("Grounded");
		readonly int r_iMoveSpeed = Animator.StringToHash("MoveSpeed");

		bool m_isCollidingNpc = false; // npc tag�� �浹���� üũ
		ZombieController m_zombie = null; // �浹�� npc ����

		// �̵� ����
		enum eMoveDir
		{
			None = 0,
			Front = 1,
			Back,
			Left,
			Right,
		}		

		[SerializeField]
		float m_fMoveSpeed = 2f; // ĳ���� �̵� �ӵ�
		[SerializeField]
		float m_fCameraRoateSpeed = 60f; // ī�޶� ȸ�� �ӵ�

		void Start()
		{
			if(m_logic == null)
			{
				var container = LifetimeScope.Find<GameLifetimeScope>().Container;
				var gameStateManager = container.Resolve<IGameStateManager>();
				var uiMediator = container.Resolve<UIMediator>();
				m_logic = new PlayerControllerLogic(gameStateManager, uiMediator);
			}			
			_InitComponent();
			_InitValue();
		}

		private void OnDestroy()
		{
			m_logic = null;
		}

		void Update()
		{
			_ProcessKey();
		}

		private void OnCollisionStay(Collision collision)
		{
			m_isCollidingNpc = collision.gameObject.tag == "NPC";
			if(m_isCollidingNpc)
				m_zombie = collision.gameObject.GetComponent<ZombieController>();
		}

		/// <summary>
		/// ������Ʈ �ʱ�ȭ
		/// </summary>
		void _InitComponent()
		{
			m_trfThis = this.GetComponent<Transform>();
			m_animThis = this.GetComponentInChildren<Animator>();
		}
		
		/// <summary>
		/// ���� �ʱ�ȭ 
		/// </summary>
		void _InitValue()
		{
			m_animThis.SetBool(r_iGrounded, true);
			_MoveSide(eMoveDir.None);
		}


		/// <summary>
		/// Ű ó��
		/// </summary>
		void _ProcessKey()
		{
			if(!m_isMovable) return;

			if(Input.GetKeyDown(KeyCode.Space) && m_isCollidingNpc)
				m_zombie?.RunEvent();

			if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
			{
				_MoveSide(eMoveDir.None);
			}
			else
			{
				if(Input.GetKey(KeyCode.W))
				{
					_MoveSide(eMoveDir.Front);
				}
				else if(Input.GetKey(KeyCode.S))
				{
					_MoveSide(eMoveDir.Back);
				}

				if(Input.GetKey(KeyCode.A))
				{
					_MoveSide(eMoveDir.Left);
				}
				else if(Input.GetKey(KeyCode.D))
				{
					_MoveSide(eMoveDir.Right);
				}
			}

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				m_logic.RunInGameMenu();
			}
	}

		/// <summary>
		/// �����ǰ� �����̼� ����
		/// </summary>
		/// <param name="eDir"> ���ϴ� �̵� Direction </param>
		void _MoveSide(eMoveDir eDir)
		{
			switch(eDir)
			{
				case eMoveDir.None :
					m_animThis.SetFloat(r_iMoveSpeed, 0f);
					break;
				case eMoveDir.Left :
					m_trfThis.Rotate(new Vector3(0, -m_fCameraRoateSpeed * Time.deltaTime, 0));
					break;
				case eMoveDir.Right :
					m_trfThis.Rotate(new Vector3(0, m_fCameraRoateSpeed * Time.deltaTime, 0));
					break;
				case eMoveDir.Front :
					m_animThis.SetFloat(r_iMoveSpeed, 0.38f);
					m_trfThis.Translate(new Vector3(0f, 0f, m_fMoveSpeed * Time.deltaTime));
					break;
				case eMoveDir.Back:
					m_animThis.SetFloat(r_iMoveSpeed, -0.3f);
					m_trfThis.Translate(new Vector3(0f, 0f, -m_fMoveSpeed * Time.deltaTime));
					break;
			}			
		}
	}

	public class PlayerControllerLogic
	{
		IGameStateManager m_gameStateManager;
		UIMediator m_ui;

		public PlayerControllerLogic(IGameStateManager gameStateManager, UIMediator ui)
		{
			m_gameStateManager = gameStateManager;
			m_ui = ui;
		}

		public void RunInGameMenu()
		{
			m_gameStateManager.ChangeState(new InGameMenuState(m_ui));
		}
	}
}