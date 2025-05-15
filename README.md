
<div align="center" markdown>
  
  # 탱크로 다시 태어난 나는 미궁을 방랑한다.
<img src="https://github.com/user-attachments/assets/b1a1e2a4-d13a-4e30-b495-d17b885ee701" width="600" />
</div>


<div align="center" markdown>
[내배캠] 궁수의 전설 모작 팀프로젝트<br>
  
__탱크로 다시 태어난 나는 미궁을 방랑한다. 게임의 기술서__

♂️
작업자별 기술서 주소 |
[**이수명**](https://github.com/Renew023/The_Returner_Tank/blob/NewMain/README_Leesoo.md) |
[**권우성**](https://github.com/Renew023/The_Returner_Tank/blob/NewMain/README_Wooseong.md) |
[**손유민**](https://github.com/Renew023/The_Returner_Tank/blob/NewMain/README_Yumin.md) |
[**박진우**](https://github.com/Renew023/The_Returner_Tank/blob/NewMain/README_Jinwoo.md) |
[**박준식**](https://github.com/Renew023/The_Returner_Tank/blob/NewMain/README_Junsik.md)
</div>
<br>


> **게임 안내**
> - 🎞 [시놉시스](#-시놉시스)
> - 🖼 [플레이화면](#-플레이화면)
> - 🎮 [플레이방법](#-플레이방법)
> - 🗝 [기술 스택 바로가기](#기술-스택)
<br>

***
## 🎞 [시놉시스]
>> 어느 날, 일어나보니 탱크가 되어있었다. 평하롭던 세상에는 갑자기 미궁이 생겨났고 나는 평화를 위해 미궁에 도전한다.
<br>

***
## 🖼 [플레이화면]
| ![Movie_023](https://github.com/user-attachments/assets/58750829-5e68-43b0-9459-84a73bbfcdc4) | ![Movie_024](https://github.com/user-attachments/assets/dc49e93b-5000-4b90-843b-af1719bb0ce3) |
|---------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------- |
| 게임시작 화면                                                                                  | 맵 선택지                                                                                      | 
| ![Movie_025](https://github.com/user-attachments/assets/4459db3f-641b-4810-a174-b2de3bf9b78b) | ![Movie_026](https://github.com/user-attachments/assets/02d66ecd-33d9-4516-a039-daa1051af3aa) |
| 공격 화면                                                                                      | 스킬 선택창                                                                                    |

<br>

***
## 🎮 [플레이방법]
  조작키 - `W,A,S,D 또는 방향키` <br>
  공격 - 근처의 가장 가까운 적 공격 <br>
  <br>
  1. 게임 시작을 누를 시 3가지 길의 선택지가 생성됩니다.<br>
  2. 맵은 레벨업, 힐, 전투로 구성되어 있으며, 원하는 선택지를 고르시면 됩니다.<br>
  3. 던전에 입장하게 되면 wave마다 몬스터가 생성되고 총 3wave가 존재합니다.<br>
  4. 몬스터를 잡아서 레벨업하여 스킬을 획득하고 미궁에 있는 최종 보스를 잡고 게임을 클리어해보세요!<br>
  
<br>

***
###### 기술 스택
> **🎉 기술 설명**
> - 👨🏻 [플레이어](#-플레이어)
>     - [카메라 이동 및 제한 (`FollowCamera.cs`)](#1-카메라-이동-및-제한-followcameracs)
>     - [플레이어 이동 (`Player.cs`)](#2-플레이어-이동-playercs)
>     - [플레이어 공격 (`WeaponController.cs`)](#3-플레이어-공격-weaponcontrollercs)
>     - [플레이어 피격 (`Player.cs`)](#4-플레이어-피격-playercs)
>     <br><br>
> - 🎱 [스킬](#-스킬)
>     - [스킬 구조 (`Skill.cs`)](#1-스킬-구조-skillcs)
>     - [스킬 패턴 (`WeaponController.cs`)](#2-스킬-패턴-weaponcontrollercs)
>       <br><br>
> - 📺 [미니맵](#-미니맵)
>     - [미니맵 추적 기능 (`FollowMiniMap.cs`)](#1-미니맵-추적-기능-followminimapcs)
>     - [미니맵 엔티티 감지 (`TargetSearch.cs`)](#2-미니맵-엔티티-감지-targetsearchcs)
>       <br><br>
> - 🏫 [스테이지 선택지](#-스테이지-선택지)
>     - [맵 선택지 생성 (`MapManager.cs`)](#1-맵-선택지-생성-mapmanagercs)
>     - [씬 전환 (`SceneController.cs`)](#2-씬-전환-scenecontrollercs)
>     - [소스 초기화 (`NodeController.cs`)](#3-소스-초기화-nodecontrollercs)
>     - [이벤트 맵 (`HealTrigger.cs/EventTrigger.cs`)](#4-이벤트-맵-healtriggercseventtriggercs)
>       <br><br>
> - 🧠 [Enemy AI & Pathfinding System](#-enemy-ai--pathfinding-system)
>   - 👾 [EnemyAI](#-enmeyai)
>     - [상태 기반 FSM 구조 (`IEnemyState.cs` 및 하위 클래스)](#1-상태-기반-fsm-구조-ienemystatecs-및-하위-클래스)
>     - [EnemyAI 클래스 핵심 기능 (`EnemyAI.cs`)](#2-enemyai-클래스-핵심-기능-enemyaics)
>       <br><br>
>   - 🧭 [PathfindingSystem](#-pathfindingsystem)
>     - 📌 [Grid 기반 탐색 시스템 (`GridScanner.cs`)](-grid-기반-탐색-시스템-gridscannercs)
>     - 📌 [A* 경로 탐색 시스템 (`AStarPathfinder.cs, Node.cs, PriorityQueue.cs`)](#-a-경로-탐색-시스템-astarpathfindercs-nodecs-priorityqueuecs)
>       <br><br>
>   - 💫 [경험치 오브젝트 흡수 시스템 (`ExpObject.cs`)](#-경험치-오브젝트-흡수-시스템-expobjectcs)
>     - [핵심 기능](#핵심-기능)
>       <br><br>
>   - 🗂 [주요 스크립트 요약](#-주요-스크립트-요약)
>       <br><br>
> - 🎃 [몬스터 생성](#몬스터-생성)
>   - [몬스터 오브젝트 풀링 (`PoolManager.cs`)](#1-몬스터-오브젝트-풀링-poolmanagercs)
>   - [스폰 포인트 / 웨이브 시스템 (`Spawner.cs`)](#2-스폰-포인트--웨이브-시스템-spawnercs)
>   - [웨이브 설정, 몬스터 풀 기능 구현 (`DungeonManager.cs`)](#3-웨이브-설정-몬스터-풀-기능-구현-dungeonmanagercs)
>   - [작업한 스크립트 / 메서드 요약](#작업한-스크립트--메서드-요약)
>     <br><br>
> - 🎪 [구현한 UI 기능](#-구현한-ui-기능)
>   - [플레이어 UI (`Player.cs, PlayerHP.cs, PlayerEXP.cs, PlayerLevel.cs`)](#-1-플레이어-ui)
>   - [몬스터 UI (`Monster.cs`)](#-2-몬스터-ui)
>   - [일시정지 UI (`PauseUI.cs`)](#%EF%B8%8F-3-일시정지-ui)
>   - [사망 UI (`DeathUI.cs`)](#-4-사망-ui)
>   - [웨이브 UI (`WaveMessageUI.cs`)](#-5-웨이브-ui)

<br>

*** 
## 👨🏻 [플레이어]

### 1. 카메라 이동 및 제한 (`FollowCamera.cs`)
플레이어를 따라다니는 카메라 추적 기능입니다.
- target을 지정하여 카메라가 추적
- Math.Clamp로 카메라 영역 제한
<br>

### 2. 플레이어 이동 (`Player.cs`)
Input.GetAxisRaw를 활용하여 쉽게 이동을 구현하였습니다. 
또한 ~~Mouse좌표~~ 쏠 공격 방향의 좌표와 플레이어의 좌표를 비교하여 플레이어의 방향이 바뀌도록 만들었습니다.
- Input.GetAxisRaw로 좌표 이동
<br>

### 3. 플레이어 공격 (`WeaponController.cs`)
플레이어가 주변 적을 탐지하고 그 중 가장 가까운 상대방을 공격하게 만들었습니다.
- Physics2D.OverlapCircle로 주변 Collider(trigger) 탐지
- 탐지한 Collider까지의 거리를 비교 후 가장 짧은 거리 탐지
- 예외처리로 Raycast로 그 좌표까지 이동하는 도중에 벽이 있을 경우 거리계산하지 않음.
- WeaponController에서 일정 시간마다 공격
<br>

### 4. 플레이어 피격 (`Player.cs`)
화살을 맞을 시 플레이어가 피가 닳는 판정을 만들었습니다.
- WeaponController에서 발사할 화살에게 공격력과 좌표를 부여.
- 피격받는 대상이 OnTriggerEnter2D로 화살에 맞았는지 판정.
- ~~owner를 지정하여 본인이 피격받지 않게함~~
- Tag를 활용하여 발사한 대상과 같은 Tag일 경우 피해를 입히지 않게함.
- TakeDamage로 맞은 대상의 체력을 감소.
<br>

***
## 🎱 [스킬]

### 1. 스킬 구조 (`Skill.cs`)
스킬에 내부 트리입니다.
- Skill(부여할 스킬)
- SkillType(증가시킬 능력치)
- WeaponController(무기에 대한 정보 중 참조형)
- Weapon(무기에 대한 정보 중 값형)
- Arrow(발사체)
<br>

### 2. 스킬 패턴 (`WeaponController.cs`)
스킬은 Arrow만 날라가는 것이 아닌 랜덤한 위치에 번개를 치게 하는 등에 수정 작업이 가능했으나, 
작업에 대한 시간관계상 WeaponController의 값을 다양하게 수정하는 것으로 스킬 패턴을 구현했습니다.
- ArrowSpeed, ArrowDamage 등 화살에 부여할 능력치 변경.
- 각도를 계산하여 화살을 여러개 쏠 때 단조로움 제거.
<br>

***
## 📺 [미니맵]

### 1. 미니맵 추적 기능 (`FollowMiniMap.cs`)
Mask와 SpriteRender로 미니맵을 구현하였고 맵과 비교하여 미니맵이 정상적으로 플레이어의 움직인만큼 움직이도록 만들었습니다. 
- 실제 맵의 움직임을 감지하여 미니맵의 움직임도 반영
- Math.Clamp를 활용하여 미니맵이 Mask 밖으로 나가는 거 방지

### 2. 미니맵 엔티티 감지 (`TargetSearch.cs`)
기존에 사용하던 맵의 사이즈를 줄이고 플레이어와 몬스터의 부분은 색칠된 점을 표시해주는 것으로 위치 파악을 쉽게 할 수 있도록 만들었습니다.
- 실제 맵에서의 몬스터와 캐릭터를 추적
- 추적한 위치를 기반으로 미니맵에 표시
- 몬스터의 존재 여부에 따라 List를 추가하거나 SetActive(false)로 관리
<br>  


 <!--이수명님-->
 ***
# 🏫 [스테이지 선택지]

### 1. 맵 선택지 생성 (`MapManager.cs`)
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

***
### 2. 씬 전환 (`SceneController.cs`)
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
***
### 3. 소스 초기화 (`NodeController.cs`)
 - 각 노드의 Image, Button 초기화
 - 클릭 시 MapManager.OnNodeClicked(r,c,type) 호출

***
### 4. 이벤트 맵 (`HealTrigger.cs/EventTrigger.cs`)
 - EventScene인 Heal 이벤트와 Levelup 이벤트를 작동하게 하는 클래스
 - OnTriggerEnter2D에서 Player 충돌 감지 → Player.HealTrigger(), Player.LevelUpTrigger() 호출


 <!--권우성님-->
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


 <!--박진우님-->
 ***

## 몬스터 생성

### 1. 몬스터 오브젝트 풀링 (`PoolManager.cs`)
![image](https://github.com/user-attachments/assets/ce3f5aab-f656-4cda-bed8-4c0c31ea269d)
- Enemies라는 오브젝트 배열 안에 Prefab으로 제작한 몬스터 오브젝트들을 담아놓습니다.
- 이후, 던전 씬에 입장할 시, 해당 Pool 안에 넣어둔 몬스터 오브젝트들을 스폰하도록 구현하였습니다.

---

### 2. 스폰 포인트 / 웨이브 시스템 (`Spawner.cs`)
![image](https://github.com/user-attachments/assets/4ee1f6ab-2134-4c8d-a3fb-a9017104f06a)

![bandicam 2025-05-14 14-24-16-357](https://github.com/user-attachments/assets/e43b89a3-d3da-41ee-a66b-da4b049d1329)
- 각 던전 씬에 지정한 SpawnPoint들을 [SpawnPoint] 배열에 넣은 후, 입장 시에 [SpawnPoint] 배열에 몬스터가 생성되도록 구현하였습니다.

![image](https://github.com/user-attachments/assets/6f8ad1db-f3ab-4764-a739-b31059bf3064)

- 또한, 해당 부분에서 각 던전마다 Wave 기능을 추가하였습니다.
    - 던전 레벨: 각 던전 씬을 클리어 시, 던전 레벨이 1 상승하며, 다음 던전 씬으로 이동할 시에 상승된 던전 레벨을 적용하여 몬스터의 스폰 수를 증가하도록 구현하였습니다.
    - 웨이브: 각 던전 씬 당 최대 3개의 웨이브로 구성되어 있으며, 3번째 웨이브 내 몬스터들이 다 죽을 경우, 해당 던전을 클리어 판정 처리하도록 구현하였습니다.
      - 던전 레벨 당, 웨이브에 나오는 몬스터의 수와 종류 등을 다르게 설정하였습니다.
    - Enemy Indices: 각 웨이브에 나올 몬스터 종류.
    - Base Enemy Count: 설정한 웨이브에 나올 몬스터 스폰 수. 던전 레벨이 오를 수록 Base Enemy Count + 던전 레벨 만큼 몬스터가 스폰되도록 구현하였습니다.

---
      
### 3. 웨이브 설정, 몬스터 풀 기능 구현 (`DungeonManager.cs`)
![image](https://github.com/user-attachments/assets/c207e74d-e21a-4112-b344-577be70e8359)

- 각 던전 씬에 설정한 몬스터 스폰 시작 웨이브 ~ 끝 웨이브를 설정하여, 해당 던전 씬에 입장할 때 DungeonManager → 설정한 웨이브 수 데이터 값을 읽게 하도록 구현하였습니다.
- Pools의 경우, 각 던전 씬 내 PoolManager 객체를 넣음으로써 던전마다 세팅한 몬스터 풀을 읽게 하도록 구현하였습니다.

---

## 작업한 스크립트 / 메서드 요약
|스크립트|메서드|설명|
|------|---|------|
|DungeonManager.cs |StartWave| 웨이브 시작 시 호출되는 메서드, 살아있는 적의 수를 설정하는 기능
|DungeonManager.cs|OnEnemyDeath| 몬스터 사망 시 호출되는 메서드, 웨이브 종료 조건을 확인하는 기능
|DungeonManager.cs|ClearDungeon| 던전 씬 클리어 메서드
|Spawner.cs|전체 메서드| 몬스터들을 웨이브 별로 스폰하는 기능 담당
|Dungeon.cs|전체 메서드| 몬스터 스폰 기능 담당
|Monster.cs|ResetEnemy| 몬스터 스폰 전, 모든 몬스터들의 변수값들을 초기화하는 기능 → 오브젝트 풀링 목적으로 추가한 기능
|GameManager.cs|SetStageInfo| 스테이지 정보 초기화용 메서드, 스테이지 선택지에서 던전 스테이지를 선택, 씬 입장과 동시에 해당 메서드의 값들로 스테이지 정보를 수정하는 기능
|GameManager.cs|IncreaseDungeonLevel| 던전 클리어 시, 던전 레벨을 증가시켜주는 메서드

---

## 📋 구현한 UI 기능

---

### 🎮 1. 플레이어 UI ('Player.cs, PlayerHP.cs, PlayerEXP.cs, PlayerLevel.cs')

- **체력 (HP)**  
  플레이어의 현재 체력을 실시간으로 표시합니다.

- **경험치 (EXP)**  
  적 처치 후 떨어지는 오브젝트를 획득 시 증가하는 경험치를 시각화합니다.

- **레벨**  
  현재 플레이어의 레벨 정보를 표시합니다.

- **HP Bar UI**  
  플레이어 머리 위에 항상 표시되는 체력 바를 구현하여 직관적인 상태 확인이 가능합니다.

![PlayerStatus](https://github.com/user-attachments/assets/ee3190a3-0d3f-4ff1-9956-f2c420ec13b7)
<img src="https://github.com/user-attachments/assets/7d4b0910-f794-41e3-ba92-4a91a11d7ed2" width="350">

---

### 👾 2. 몬스터 UI (`Monster.cs`)

- **Monster HP Bar UI**  
  모든 몬스터 개체 위에 실시간으로 체력을 보여주는 UI를 구현하여 전투 상황을 쉽게 파악할 수 있도록 하였습니다.

<img src="https://github.com/user-attachments/assets/9ece9f6d-8388-4285-99f7-93746e1065d8" width="350">

---

### ⚙️ 3. 일시정지 UI (`PauseUI.cs`)

게임 중 일시정지 시 활성화되며, 다음과 같은 기능을 제공합니다:

- **게임 계속하기**  
  일시정지를 해제하고 게임을 재개합니다.

- **메인화면으로 돌아가기**  
  현재 게임을 종료하고 메인 메뉴로 이동합니다.

- **보유 스킬 확인**  
  현재 플레이어가 보유 중인 스킬 목록을 확인할 수 있는 UI 창을 표시합니다.

![PauseUI](https://github.com/user-attachments/assets/3eb2d35a-b435-447d-aa58-077a78fbad94)

---

### 💀 4. 사망 UI (`DeathUI.cs`)

플레이어 사망 시 등장하는 UI로, 다음 기능을 제공합니다:

- **메인화면으로 돌아가기**  
  게임을 종료하고 메인 메뉴로 복귀합니다.

![DeathUI](https://github.com/user-attachments/assets/13f2bbc7-02d6-4841-8f88-681b8e8fc69c)

---

### 🌊 5. 웨이브 UI (`WaveMessageUI.cs`)

웨이브 시스템과 연동하여 현재 게임 진행 상태를 시각적으로 표현합니다:

- **WAVE 1, 2, 3 진행 번호 표기**  
  각 웨이브 시작 시 해당 번호를 화면에 표시합니다.

- **CLEAR**  
  해당 웨이브를 클리어했을 때 시각적으로 알림을 제공합니다.
  
![Wave1](https://github.com/user-attachments/assets/a2dc1758-7d35-461e-bc1b-623f5ad30a4c)
![Wave2](https://github.com/user-attachments/assets/38485bda-6c04-4172-b60c-764ac3eff384)
![Wave3](https://github.com/user-attachments/assets/48c43aaa-7d46-4dce-b3aa-7893e0ded143)
![CLEAR](https://github.com/user-attachments/assets/9cc94c8d-2469-4af1-ba44-429f7c997d7d)

---
