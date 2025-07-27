using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Casual
{
	public class PlayerController : MonoBehaviour
	{
		PlayerControllerLogic m_logic = null;		

		Transform m_trfThis;	// 플레이어 transform
		Animator m_animThis; // 플레이어 animator

		bool m_isMovable = true; // 이동 가능한지 여부
		public bool IsMovable
		{
			get { return m_isMovable; }
			set { m_isMovable = value; }
		}		

		// 애니메이션 해쉬 값
		readonly int r_iGrounded = Animator.StringToHash("Grounded");
		readonly int r_iMoveSpeed = Animator.StringToHash("MoveSpeed");

		bool m_isCollidingNpc = false; // npc tag와 충돌여부 체크
		ZombieController m_zombie = null; // 충돌된 npc 저장

		// 이동 방향
		enum eMoveDir
		{
			None = 0,
			Front = 1,
			Back,
			Left,
			Right,
		}		

		[SerializeField]
		float m_fMoveSpeed = 2f; // 캐릭터 이동 속도
		[SerializeField]
		float m_fCameraRoateSpeed = 60f; // 카메라 회전 속도

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
		/// 컴포넌트 초기화
		/// </summary>
		void _InitComponent()
		{
			m_trfThis = this.GetComponent<Transform>();
			m_animThis = this.GetComponentInChildren<Animator>();
		}
		
		/// <summary>
		/// 변수 초기화 
		/// </summary>
		void _InitValue()
		{
			m_animThis.SetBool(r_iGrounded, true);
			_MoveSide(eMoveDir.None);
		}


		/// <summary>
		/// 키 처리
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
		/// 포지션과 로테이션 변경
		/// </summary>
		/// <param name="eDir"> 원하는 이동 Direction </param>
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