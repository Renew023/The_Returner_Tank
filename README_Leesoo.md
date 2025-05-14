## 개요

- 이 문서는 **MapManager** 브랜치에서 작업한 스크립트들의 기능, 주요 트러블슈팅 이력, 기술 하이라이트, 그리고 사용 기술 스택을 정리한 README

<br>
---
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
  }```

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
}```


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
}```

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
}```

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
}```

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
}```

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
}```

** 결과 
- ResetMap()을 거쳐 다시 맵씬에 들어올 때마다 initialized가 false로 바뀌어
GenerateMapData()가 반드시 실행됨 >> 항상 새로운 맵이 랜덤 생성되어 정상 작동
