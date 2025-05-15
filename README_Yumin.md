# The_Returner_Tank
[내배캠] 궁수의 전설 모작 팀프로젝트

**Table of Contents**
- [플레이어](#[플레이어])
    - [카메라 이동 및 제한](#카메라-이동-및-제한)
    - [플레이어 이동](#플레이어-이동)
    - [플레이어 공격](#플레이어-공격)
    - [플레이어 피격](#플레이어-피격)
- [스킬](#[스킬])
    - [스킬 구조](#스킬-구조)
    - [스킬 패턴](#스킬-패턴)
    - [스킬 선택창](#스킬-선택창)
- [미니맵](#[미니맵])

## [플레이어]

### 카메라 이동 및 제한
카메라는 플레이어를 타겟팅하기 위해서 만들었습니다. Lerp를 활용하여 카메라는 플레이어를 바로 붙는 것이 아닌 부드럽게 따라오며, Camera.orthographic으로 카메라의 크기를 확인하고 Math.Clamp로 전체 맵과 카메라만큼의 너비를 제한함으로써 카메라가 범위 밖으로 벗어나지 않도록 조정했습니다.
- 플레이어가 현재 위, 아래로 빠져나가는 것은 벽이 따로 없이 카메라만 제한되어 있기 때문입니다.

<img src="https://github.com/user-attachments/assets/45283051-18e3-4023-985c-c1a4aa06c7eb" width="500">

<br>

### 플레이어 이동
Input.GetAxisRaw를 활용하여 쉽게 이동을 구현하였습니다. 또한 Mouse의 X좌표와 플레이어의 X좌표를 비교하여 플레이어의 방향이 바뀌도록 만들었습니다.

<img src="https://github.com/user-attachments/assets/61e5e975-203e-413f-b014-088467dc1412" width="500">

<br>

### 플레이어 공격
플레이어 이동에서 다루고 있는 마우스의 위치를 받아와서 그 방향으로 공격을 날립니다. normalized 를 활용하여 방향만을 감지하고 거리는 전부 일정한 거리만큼 이동합니다. 
또한 생성되는 발사체의 경우, OnEnable과 OnDisable을 통해 발사체의 활성화 시에는 일정 시간 이후 SetActive(false)를 해주도록 코루틴을 실행하고, 
비활성화 시에는 ObjectPool 기능을 활용하여 Destroy가 아닌 잠시 SetActive(false) 상태에서 다시 호출시 불러올 수 있는 로직을 만들었습니다.

<img src="https://github.com/user-attachments/assets/7dcd25ca-bb57-4bb3-9f39-f0cb3df013dd" width="500">

<br>

### 플레이어 피격
몬스터 임시파일을 만들어 피격이 되는지 확인했습니다. 
피격에 대한 판정은 각 오브젝트에 있는 컴포넌트가 판정하며, 본인의 공격에는 본인이 당하지 않도록 만들었습니다. 
피격 시에는 공격한 상대방의 공격력만큼 체력이 감소하고 발사체는 비활성화됩니다. 

<img src="https://github.com/user-attachments/assets/f0a81d29-8ca7-4154-a95b-10f086768aed" width="500">

<br>

## [스킬]

### 스킬 구조
각 발사체를 날리는 무기에 대한 정보를 담을 수 있는 Arrow클래스와 그 무기에 값을 전달하고 무기를 다룰 수 있는 Weapon, WeaponController와 그 무기에 대한 정보 및 플레이어의 능력치를 바꿔주는 Skill클래스로 나누어 스킬을 제작했습니다. WeaponController를 수정하여 Arrow에 줄 수 있는 값을 수정할 수 있고 이를 이용하여 다양한 능력치를 가진 무기를 만들 수 있었습니다.

![image](https://github.com/user-attachments/assets/039936c0-2e40-4d44-8393-2d95ee7986dd)

<br>

### 스킬 패턴
스킬은 Arrow에서 날라가는 것이 아닌 그 위치에 번개를 친다던지 수정을 할 수 있었으나, 작업에 대한 시간관계상 WeaponController의 값을 다양하게 수정하는 것으로 스킬 패턴을 구현했습니다.

<img src="https://github.com/user-attachments/assets/6f7f5c4d-4283-4480-a2c3-643bbece9d52" width="500">
<img src="https://github.com/user-attachments/assets/3779a25b-a53a-4b8c-ae2a-fb1b1cf9b263" width="500">

<br>

### 스킬 선택창
스킬을 순차적으로 얻는 것이 아닌 3개의 선택지 중 골라서 스킬을 고를 수 있도록 만들었습니다. 
- 레벨업 시 스킬 선택창 표시 및 R키를 누를 시 테스트 가능하도록 설정.
- Random.Range를 통해 랜덤한 선택지를 각 버튼에 할당.
- 선택지마다 이해를 돕기 위한 텍스트와 스프라이트 표시
<br>

## [미니맵]

### 미니맵 구조
기존에 사용하던 맵의 사이즈를 줄이고 플레이어와 몬스터의 부분은 색칠된 점을 표시해주는 것으로 위치 파악을 쉽게 할 수 있도록 만들었습니다.

<img src="https://github.com/user-attachments/assets/8865a3f1-d433-40cc-a333-70842492afd1" width="500">



