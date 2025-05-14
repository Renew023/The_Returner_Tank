
<div align="center" markdown>
  
  # 탱크로 다시 태어난 나는 미궁을 방랑한다.
<img src="https://github.com/user-attachments/assets/b1a1e2a4-d13a-4e30-b495-d17b885ee701" width="300" />
</div>


<div align="center" markdown>
[내배캠] 궁수의 전설 모작 팀프로젝트<br>
  
__탱크로 다시 태어난 나는 미궁을 방랑한다. 게임의 기술서__

♂️
작업자별 기술서 주소 |
[**이수명**](https://github.com/Renew023/The_Returner_Tank/blob/develop2/README_Leesoo.md) |
[**권우성**](https://github.com/Renew023/The_Returner_Tank/blob/develop2/README_Wooseong.md) |
[**손유민**](https://github.com/Renew023/The_Returner_Tank/blob/develop2/README_Yumin.md) |
[**박진우ReadMePlease**](https://github.com/ryul1206/multilingual-markdown/blob/main/README.ko.md) |
[**박준식ReadMePlease**](https://github.com/ryul1206/multilingual-markdown/blob/main/README.ja.md)
</div>
<br>

<div align="left"; style="border: 1px solid #ccc; border-radius: 8px; padding: 16px; background-color: #f9f9f9;">
  
> **🎉 Table of Contents**
> - 👨🏻 [플레이어](#-플레이어)
>     - [카메라 이동 및 제한](#1-카메라-이동-및-제한-followcameracs)
>     - [플레이어 이동](#2-플레이어-이동)
>     - [플레이어 공격](#3-플레이어-공격)
>     - [플레이어 피격](#4-플레이어-피격)
> - 🎱 [스킬](#-스킬)
>     - [스킬 구조](#1-스킬-구조)
>     - [스킬 패턴](#.-스킬-패턴)
> - 📺 [미니맵](#-미니맵)
>     - [미니맵 구현](#1-미니맵-구현)
> - 🏫 [스테이지 선택지](#-스테이지-선택지)
>     - [스크립트별 기능 설명](#1-스크립트별-기능-설명)
>       - [MapManager.cs](#11-mapmanagercs)
>       - [SceneController](#12-scenecontroller)
>       - [NodeController](#13-nodecontroller)
>       - [HealTrigger/EventTrigger](#14-healtrigger--eventtrigger)
>     - [트러블 슈팅](#2-트러블-슈팅)
>       - [플레이어 인디케이터 위치 초기화 누락](#21-플레이어-인디케이터-위치-초기화-누락)
>       - [맵 데이터 재생성 누락](#22-맵-데이터-재생성-누락)
> - 🧠 [Enemy AI & Pathfinding System](#-enemy-ai--pathfinding-system)
>   - 👾 [EnemyAI](#-enmeyai)
>     - [1. 상태 기반 FSM 구조 (`IEnemyState.cs` 및 하위 클래스)](#1-상태-기반-fsm-구조-ienemystatecs-및-하위-클래스)
>     - [EnemyAI 클래스 핵심 기능 (`EnemyAI.cs`)](#2-enemyai-클래스-핵심-기능-enemyaics)
>   - 🧭 [PathfindingSystem](#-pathfindingsystem)
>     - 📌 [Grid 기반 탐색 시스템 (`GridScanner.cs`)](-grid-기반-탐색-시스템-gridscannercs)
>     - 📌 [A* 경로 탐색 시스템 (AStarPathfinder.cs, Node.cs, PriorityQueue.cs)](#-a-경로-탐색-시스템-astarpathfindercs-nodecs-priorityqueuecs)
>   - 💫 [경험치 오브젝트 흡수 시스템 (ExpObject.cs)](#-경험치-오브젝트-흡수-시스템-expobjectcs)
>     - [핵심 기능](#핵심-기능)
>     - 🗂 [주요 스크립트 요약](#-주요-스크립트-요약)

</div> 
<br>

*** 
## 👨🏻 [플레이어]

### 1. 카메라 이동 및 제한 (FollowCamera.cs)
플레이어를 따라다니는 카메라 추적 기능입니다.
- target을 지정하여 카메라가 추적
- Math.Clamp로 카메라 영역 제한
<br>

### 2. 플레이어 이동
Input.GetAxisRaw를 활용하여 쉽게 이동을 구현하였습니다. 
또한 ~~Mouse좌표~~ 쏠 공격 방향의 좌표와 플레이어의 좌표를 비교하여 플레이어의 방향이 바뀌도록 만들었습니다.
- Input.GetAxisRaw로 좌표 이동
<br>

### 3. 플레이어 공격
플레이어가 주변 적을 탐지하고 그 중 가장 가까운 상대방을 공격하게 만들었습니다.
- Physics2D.OverlapCircle로 주변 Collider(trigger) 탐지
- 탐지한 Collider까지의 거리를 비교 후 가장 짧은 거리 탐지
- 예외처리로 Raycast로 그 좌표까지 이동하는 도중에 벽이 있을 경우 거리계산하지 않음.
- WeaponController에서 일정 시간마다 공격
<br>

### 4. 플레이어 피격
화살을 맞을 시 플레이어가 피가 닳는 판정을 만들었습니다.
- WeaponController에서 발사할 화살에게 공격력과 좌표를 부여.
- 피격받는 대상이 OnTriggerEnter2D로 화살에 맞았는지 판정.
- ~~owner를 지정하여 본인이 피격받지 않게함~~
- Tag를 활용하여 발사한 대상과 같은 Tag일 경우 피해를 입히지 않게함.
- TakeDamage로 맞은 대상의 체력을 감소.
<br>

***
## 🎱 [스킬]

### 1. 스킬 구조
스킬에 내부 트리입니다.
- Skill(부여할 스킬)
- SkillType(증가시킬 능력치)
- WeaponController(무기에 대한 정보 중 참조형)
- Weapon(무기에 대한 정보 중 값형)
- Arrow(발사체)
<br>

### 2. 스킬 패턴
스킬은 Arrow만 날라가는 것이 아닌 랜덤한 위치에 번개를 치게 하는 등에 수정 작업이 가능했으나, 
작업에 대한 시간관계상 WeaponController의 값을 다양하게 수정하는 것으로 스킬 패턴을 구현했습니다.
- ArrowSpeed, ArrowDamage 등 화살에 부여할 능력치 변경.
- 각도를 계산하여 화살을 여러개 쏠 때 단조로움 제거.
<br>

***
## 📺 [미니맵]

### 1. 미니맵 구현
기존에 사용하던 맵의 사이즈를 줄이고 플레이어와 몬스터의 부분은 색칠된 점을 표시해주는 것으로 위치 파악을 쉽게 할 수 있도록 만들었습니다.
- 실제 맵에서의 몬스터와 캐릭터를 추적
- Math.Clamp를 활용하여 미니맵 밖으로 나가는 거 방지
- 추적한 위치를 기반으로 미니맵에 표시
- 몬스터의 존재 여부에 따라 List를 추가하거나 SetActive(false)로 관리
- Mask 기능을 활용하여 기존 맵 구조 중 일부를 화면에 표기되지 않도록 설정
<br>  

# 🏫 [스테이지 선택지]

## 1. 스크립트별 기능 설명

### 1.1 `MapManager.cs`
- MapScene에 진입하여 맵을 랜덤으로 생성하기 위해 노드·점선·플레이어 인디케이터를 생성·관리하는 핵심 클래스

**맵 데이터 생성 (`GenerateMapData`)**
  ```cs
  void GenerateMapData()
  {
      mapData = new List<List<NodeType>>();
      var rnd = new System.Random();
      // Start 고정
      mapData.Add(new List<NodeType>{ NodeType.Start });
      for (int r = 0; r < totalRows - 1; r++)
      {
          var rowList = new List<NodeType>();
          for (int i = 0; i < choicesPerRow; i++)
          {
              int roll = rnd.Next(10);
              if (roll < 6)         rowList.Add(NodeType.Enemy);  // 60%
              else if (roll < 8)    rowList.Add(NodeType.Heal);   // 20%
              else                  rowList.Add(NodeType.Event);  // 20%
          }
          mapData.Add(rowList);
      }
      // Boss 고정
      mapData.Add(new List<NodeType>{ NodeType.Boss });
  }
```

**맵 렌더링 (RenderMap)**

- nodePrefab 인스턴스화 후 정렬

- DrawDots(a, b) 로 점선 연결

- playerIndicatorPrefab 으로 플레이어 표시

- 상태 복원 (RestoreMap)

- currentRow/currentCol 위치로 인디케이터 이동 & 노드 알파 갱신

- 맵 리셋 (ResetMap)
 ```cs
public void ResetMap()
{
    // 기존 자원 삭제
    if (mapNodes != null) mapNodes.ForEach(row => row.ForEach(n => Destroy(n.gameObject)));
    dotLines?.ForEach(d => Destroy(d.gameObject));
    Destroy(playerIndicatorRt?.gameObject);
    connections.Clear();
    // 완전 재생성
    initialized = false;
    GenerateMapData();
    RenderMap();
}
```


### 1.2 'SceneController'
 - 씬 전환 로직 관리, 일반 진입(ToMap) vs 특수 초기화 진입(FirstToMap) 분리
 ```cs
public static void FirstToMap()
{
    needResetMap = true;
    SceneManager.LoadScene("MapScene");
}

private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (scene.name == "MapScene" && needResetMap)
    {
        MapManager.Instance.ResetMap();
        needResetMap = false;
    }
    else
    {
        MapManager.Instance.RestoreMap();
        // 카메라 리셋 등…
    }
}
```

### 1.3 'NodeController'
 - 각 노드의 Image, Button 초기화
 - 클릭 시 MapManager.OnNodeClicked(r,c,type) 호출

### 1.4 'HealTrigger / EventTrigger'
 - EventScene인 Heal 이벤트와 Levelup 이벤트를 작동하게 하는 클래스
 - OnTriggerEnter2D에서 Player 충돌 감지 → Player.HealTrigger(), Player.LevelUpTrigger() 호출


## 2. 트러블 슈팅
### 2.1 '플레이어 인디케이터 위치 초기화 누락'
 - 문제: ResetMap() 후 currentRow/currentCol 값이 이전 상태로 남아 있어 인디케이터가 항상 마지막 위치에 렌더링됨

** 발생 코드 (수정 전)**
 ```cs
public void ResetMap()
{
    GenerateMapData();
    RenderMap();
    // currentRow/currentCol 초기화 누락
}
```

**해결방법**
- ResetMap() 내에 시작점으로 상태 초기화 추가

 ```cs
public void ResetMap()
{
    // 1) 삭제 로직 …
    // 2) 데이터 재생성
    GenerateMapData();
    RenderMap();
    // 3) 위치 초기화
    currentRow = 0;
    currentCol = 0;
    RestoreMap();   
}
```

**결과**
- 매번 “첫 진입”처럼 인디케이터가 스타트 노드로 리셋되어 정상 동작

### 2.2 '맵 데이터 재생성 누락'
 ** 증상 **
 - 첫 맵 진입 시에는 새로운 맵이 잘 생성되지만, FirstToMap() 등으로 다시 맵씬에 들어와도 같은 맵이 반복 표시됨.

 ** 원인 코드 **
- MapManager 내의 initialized 플래그가 true로 설정되면,
InitializeOrRestoreMap()에서 더 이상 GenerateMapData()를 실행하지 않음.
 ```cs
static bool initialized = false;

void InitializeOrRestoreMap()
{
    if (!initialized && GameManager.Instance.FirstMapEntry)
    {
        GenerateMapData();          // 첫 진입에서만 실행
        initialized = true;
        GameManager.Instance.DisableFirstMapEntry();
    }
    RenderMap();
    RestoreMap();
}
```

** 해결 방법 **
 - ResetMap() 호출 시 플래그를 리셋하거나, InitializeOrRestoreMap() 로직을 항상 새로운 맵 데이터를 생성하도록 변경

 ```cs
// MapManager.cs
public void ResetMap()
{
    // 맵 삭제 로직…

+   // 초기화 플래그 리셋
+   initialized = false;

    // 맵 재생성
    GenerateMapData();
    RenderMap();
}

void InitializeOrRestoreMap()
{
+   // ResetMap()으로 리셋된 경우에도 GenerateMapData 호출 보장
    if (!initialized && GameManager.Instance.FirstMapEntry)
    {
        GenerateMapData();
        initialized = true;
        GameManager.Instance.DisableFirstMapEntry();
    }
    RenderMap();
    RestoreMap();
}
```

** 결과 **
- ResetMap()을 거쳐 다시 맵씬에 들어올 때마다 initialized가 false로 바뀌어
GenerateMapData()가 반드시 실행됨 >> 항상 새로운 맵이 랜덤 생성되어 정상 작동

***
# 🧠 Enemy AI & Pathfinding System

---

## 👾 EnemyAI

### 1. 상태 기반 FSM 구조 (`IEnemyState.cs` 및 하위 클래스)

- **`ChaseState.cs`**
  - 플레이어를 A* 경로로 추적하거나 랜덤 이동 수행
  - Raycast를 이용해 시야 확보 후 공격 범위 내 진입 시 `AttackS***tate`로 전환

- **`AttackState.cs`**
  - 상태 진입 시 즉시 공격 실행 (`PerformAttack`)
  - 공격 후 자동으로 `AttackCooldownState`로 전환

- **`AttackCooldownState.cs`**
  - 짧은 쿨타임 동안 대기
  - 일정 확률로 인접한 타일로 랜덤 이동 수행

### 2. EnemyAI 클래스 핵심 기능 (`EnemyAI.cs`)

- 상태 전환 메서드: `ChangeState(IEnemyState newState)`
- 코루틴 기반의 자연스러운 이동: `MoveTo(Vector3 target)`
- 공격 실행: `PerformAttack()`
- 플레이어를 태그 기반으로 자동 탐색하여 추적 시작

---

## 🧭 PathfindingSystem

### 📌 Grid 기반 탐색 시스템 (`GridScanner.cs`)

- Tilemap의 자식 타일맵을 전부 검사하여 이동 가능 타일만 필터링
- 좌표 변환 기능:
  - `WorldToCell(Vector3)`
  - `CellToWorld(Vector2Int)`
- 네 방향(`상, 하, 좌, 우`) 기준 인접 셀 탐색 (`GetNeighbours`)
- 이동 가능 여부 판정: `IsWalkable(Vector2Int cellPos)`

### 📌 A* 경로 탐색 시스템 (`AStarPathfinder.cs`, `Node.cs`, `PriorityQueue.cs`)

- `AStarPathfinder`는 플레이어와 적의 위치를 바탕으로 최적 경로 탐색
- `Node`: G/H/FCost 및 부모 노드를 저장하는 단일 노드 클래스
- `PriorityQueue<T>`: 최소 힙 구조의 우선순위 큐로, FCost 기준 정렬

---

# 💫 경험치 오브젝트 흡수 시스템 (`ExpObject.cs`)

경험치 시스템은 Enemy AI & Pathfinding과 별개로 동작하는 추적형 보상 오브젝트 기능이다.

### 핵심 기능

- `StartAbsorb(Transform player)` 호출 시 흡수 시작
- 방향 변화가 클수록 감속 → 부드러운 곡선형 움직임 유도
- `acceleration`, `maxSpeed` 값을 기반으로 가속도 및 최고 속도 설정
- 플레이어와 충돌 시 `Player.AddExp(int)` 호출 후 오브젝트 파괴

---

## 🗂 주요 스크립트 요약

| 파일명 | 설명 |
|--------|------|
| `EnemyAI.cs` | 적의 상태 전환, 이동, 공격 등 전반 관리 |
| `IEnemyState.cs` | 상태 인터페이스 |
| `ChaseState.cs` | 추적 상태 구현 |
| `AttackState.cs` | 공격 상태 구현 |
| `AttackCooldownState.cs` | 공격 후 대기 상태 구현 |
| `GridScanner.cs` | 타일맵 기반 셀 검사 및 좌표 변환 |
| `AStarPathfinder.cs` | 경로 탐색 로직 (A* 알고리즘) |
| `Node.cs` | A* 알고리즘용 노드 클래스 |
| `PriorityQueue.cs` | 최소 힙 구조 우선순위 큐 |
| `ExpObject.cs` | 추적형 경험치 오브젝트 구현 |


