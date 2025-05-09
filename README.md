# The_Returner_Tank
[내배캠] 궁수의 전설 모작 팀프로젝트

**Table of Contents**
1. [구현1](#구현1)
    1. [카메라 이동 및 제한](#카메라-이동-및-제한)
    1. [플레이어 이동](#플레이어-이동)
    1. [플레이어 공격](#플레이어-공격)
    1. [플레이어 피격](#플레이어-피격)

## 구현1

### 카메라 이동 및 제한
카메라는 플레이어를 타겟팅하기 위해서 만들었습니다. Lerp를 활용하여 카메라는 플레이어를 바로 붙는 것이 아닌 부드럽게 따라오며, Camera.orthographic으로 카메라의 크기를 확인하고 Math.Clamp로 전체 맵과 카메라만큼의 너비를 제한함으로써 카메라가 범위 밖으로 벗어나지 않도록 조정했습니다.
- 플레이어가 현재 위, 아래로 빠져나가는 것은 벽이 따로 없이 카메라만 제한되어 있기 때문입니다.

![Movie_011](https://github.com/user-attachments/assets/45283051-18e3-4023-985c-c1a4aa06c7eb)


### 플레이어 이동
Input.GetAxisRaw를 활용하여 쉽게 이동을 구현하였습니다. 또한 Mouse의 X좌표와 플레이어의 X좌표를 비교하여 플레이어의 방향이 바뀌도록 만들었습니다.

![Movie_005](https://github.com/user-attachments/assets/61e5e975-203e-413f-b014-088467dc1412)


### 플레이어 공격
플레이어 이동에서 다루고 있는 마우스의 위치를 받아와서 그 방향으로 공격을 날립니다. normalized 를 활용하여 방향만을 감지하고 거리는 전부 일정한 거리만큼 이동합니다. 
또한 생성되는 발사체의 경우, OnEnable과 OnDisable을 통해 발사체의 활성화 시에는 일정 시간 이후 SetActive(false)를 해주도록 코루틴을 실행하고, 
비활성화 시에는 ObjectPool 기능을 활용하여 Destroy가 아닌 잠시 SetActive(false) 상태에서 다시 호출시 불러올 수 있는 로직을 만들었습니다.

![Movie_006](https://github.com/user-attachments/assets/7dcd25ca-bb57-4bb3-9f39-f0cb3df013dd)

### 플레이어 피격
몬스터 임시파일을 만들어 피격이 되는지 확인했습니다. 
피격에 대한 판정은 각 오브젝트에 있는 컴포넌트가 판정하며, 본인의 공격에는 본인이 당하지 않도록 만들었습니다. 
피격 시에는 공격한 상대방의 공격력만큼 체력이 감소하고 발사체는 비활성화됩니다. 

![Movie_013](https://github.com/user-attachments/assets/f0a81d29-8ca7-4154-a95b-10f086768aed)
