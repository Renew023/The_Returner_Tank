# Monster Spawn System

--- 

## Monster Spawn

### 1. 몬스터 스폰(1) - 오브젝트 풀링 시스템 사용 (DungeonScene1~4, BossBattleScene Hierarachy- PoolManager)
![image](https://github.com/user-attachments/assets/ce3f5aab-f656-4cda-bed8-4c0c31ea269d)
- Enemies라는 오브젝트 배열 안에 Prefab으로 제작한 몬스터 오브젝트들을 담아놓습니다.
- 이후, 던전 씬에 입장할 시, 해당 Pool 안에 넣어둔 몬스터 오브젝트들을 스폰하도록 구현하였습니다.

---

### 2. 몬스터 스폰(2) - 지정한 스폰 포인트에 몬스터 스폰 기능 / 웨이브 시스템 (DungeonScene1~4, BossBattleScene Hierarachy- Spawner)
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
      
### 3. 던전 매니저 중 웨이브 설정, 몬스터 풀 기능 구현 (DungeonScene1~4, BossBattleScene Hierarachy- DungeonManager)
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
