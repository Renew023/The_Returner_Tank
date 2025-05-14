# 🧠 Enemy AI & Pathfinding System

---

## 👾 EnemyAI

### 1. 상태 기반 FSM 구조 (`IEnemyState.cs` 및 하위 클래스)

- **`ChaseState.cs`**
  - 플레이어를 A* 경로로 추적하거나 랜덤 이동 수행
  - Raycast를 이용해 시야 확보 후 공격 범위 내 진입 시 `AttackState`로 전환

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
