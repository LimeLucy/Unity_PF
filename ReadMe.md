# Unity_PF

이 프로젝트는 Unity 기반의 간단한 어드벤처 게임 예제입니다. VContainer를 사용하여 의존성 주입을 구성했으며, 대화 및 선택지 시스템을 포함하고 있습니다.

## 실행 방법

1. Unity 에디터로 프로젝트를 열어주세요.
2. `Assets/Scenes/0_StartScene.unity` 씬을 실행하면 게임이 시작됩니다.
3. 기본 조작:
   - **W, A, S, D** : 플레이어 이동
   - **Q, E** : 카메라 회전
   - **Space** : NPC와 상호작용하거나 대화 진행, 메뉴 선택 확인
   - **Esc** : 인게임 메뉴 열기/닫기

## 주요 폴더 구조
- `Assets/Sources/DI` : VContainer를 이용한 의존성 주입 설정 및 인터페이스 정의
- `Assets/Sources/InGame` : 게임 플레이 관련 스크립트와 상태 머신 구현
- `Assets/Sources/InMenu` : 메뉴 씬 로직
- `Assets/Sources/Scriptable` : 대화 스크립트 및 선택지용 ScriptableObject
- `Assets/Sources/InGame/UI` : UI 제어 스크립트

## 핵심 클래스 설명

### DI
- **`RootLifetimeScope`
  **Assets/Sources/DI/RootLifetimeScope.cs
  - 게임 전체에서 사용되는 매니저를 등록합니다. `IMainStateManager`, `ISaveLoadManager`, 그리고 `GameSwitch`를 싱글톤으로 제공합니다.
- **`GameLifetimeScope`
  **Assets/Sources/DI/GameLifetimeScope.cs
  - 인게임 씬에서 필요한 컴포넌트와 UI 컨트롤러를 등록합니다.
- **`UIMediator` 및 관련 컨트롤러
  **Assets/Sources/DI/UIMediators.cs
  - 대화창, 선택지, 인게임 메뉴, 이름 마커 UI를 관리하기 위한 중재자 패턴을 제공합니다.

### 게임 상태 관리
- **`MainStateManager`
  **Assets/Sources/InGame/MainStateManager.cs
  - 메뉴와 게임 등 상위 상태를 전환합니다.
- **`GameStateManager`
  **Assets/Sources/InGame/GameState/GameStateManager.cs
  - 인게임 내부의 상태(`IGameState`)를 전환합니다.
- **`StateMenu` / `StateGame`
  **Assets/Sources/InGame/State/StateMenu.cs, Assets/Sources/InGame/State/StateGame.cs
  - 씬을 로드하며 초기화 과정을 담당합니다.
- **`DefaultState`**, **`DialogueState`**, **`SelectState`**, **`InGameMenuState`
  **Assets/Sources/InGame/GameState/DefaultState.cs, Assets/Sources/InGame/GameState/DialogueState.cs, Assets/Sources/InGame/GameState/SelectState.cs, Assets/Sources/InGame/GameState/InGameMenuState.cs
  - 플레이어 이동, 대화, 선택지, 인게임 메뉴 등 실제 게임 진행을 제어합니다.

### 플레이어와 NPC
- **`PlayerController`
  **Assets/Sources/InGame/PlayerController.cs
  - 이동 및 카메라 조작을 처리하고, NPC와의 충돌 시 대화를 시작합니다.
- **`ZombieController`
  **Assets/Sources/InGame/ZombieController.cs
  - 스크립트를 기반으로 이벤트를 실행하며, 대화 또는 선택지를 표시합니다.
- **`PlayerProvider`
  **Assets/Sources/InGame/PlayerProvider.cs
  - DI를 통해 플레이어 인스턴스를 제공하는 간단한 컴포넌트입니다.

### 기타 유틸리티
- **`GameSwitch`
  **Assets/Sources/InGame/GameSwitch.cs
  - 스토리 진행 등을 저장하기 위한 불린 스위치 배열을 관리합니다.
- **`SaveLoadManager`
  **Assets/Sources/InGame/SaveLoadManager.cs
  - JSON 파일로 게임 데이터를 저장하고 불러옵니다.
- **`ChoiceObjectController`
  **Assets/Sources/InGame/ChoiceObjectController.cs
  - 특정 스위치 값에 따라 오브젝트 그룹을 활성/비활성화합니다.
- **`ScreenMaskController`
  **Assets/Sources/InGame/ScreenMaskController.cs
  - 페이드용 화면 마스크를 표시하거나 숨깁니다.

## ScriptableObject
- **`Scripts`**와 **`Selects`
  **Assets/Sources/Scriptable/Scripts.cs, Assets/Sources/Scriptable/Selects.cs
  - 대화 내용과 선택지를 데이터 자산으로 관리합니다.

이 README는 프로젝트 구조와 주요 클래스를 빠르게 파악하기 위한 간단한 안내서입니다. 세부 구현과 사용 방법은 각 스크립트의 주석을 참고해주세요.