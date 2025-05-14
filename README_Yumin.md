# The_Returner_Tank
[내배캠] 궁수의 전설 모작 팀프로젝트

***
**🎉 Table of Contents**
- 👨🏻 [플레이어](#-[플레이어])
    - [카메라 이동 및 제한](#1.-카메라-이동-및-제한-FollowCamera.cs)
    - [플레이어 이동](#2.-플레이어-이동)
    - [플레이어 공격](#3.-플레이어-공격)
    - [플레이어 피격](#4.-플레이어-피격)
- 🎱 [스킬](#-[스킬])
    - [스킬 구조](#1.-스킬-구조)
    - [스킬 패턴](#2.-스킬-패턴)
- 📺 [미니맵](#-미니맵)
    - [미니맵 구현](#1.-미니맵-구현)

***   
## 👨🏻 [플레이어]


### 1. 카메라 이동 및 제한 (FollowCamera.cs)
플레이어를 따라다니는 카메라 추적 기능입니다.


- 핵심기술
    - target을 지정하여 카메라가 추적
    - Math.Clamp로 카메라 영역 제한
![Movie_011](https://github.com/user-attachments/assets/45283051-18e3-4023-985c-c1a4aa06c7eb)

***
### 2. 플레이어 이동

Input.GetAxisRaw를 활용하여 쉽게 이동을 구현하였습니다. 
또한 ~~Mouse좌표~~ 쏠 공격 방향의 좌표와 플레이어의 좌표를 비교하여 플레이어의 방향이 바뀌도록 만들었습니다.


- 핵심기술
    - Input.GetAxisRaw로 좌표 이동
![Movie_005](https://github.com/user-attachments/assets/61e5e975-203e-413f-b014-088467dc1412)

***
### 3. 플레이어 공격
플레이어가 주변 적을 탐지하고 그 중 가장 가까운 상대방을 공격하게 만들었습니다.


- 핵심기술
    - Physics2D.OverlapCircle로 주변 Collider(trigger) 탐지
    - 탐지한 Collider까지의 거리를 비교 후 가장 짧은 거리 탐지
        - 예외처리로 Raycast로 그 좌표까지 이동하는 도중에 벽이 있을 경우 거리계산하지 않음.
    - WeaponController에서 일정 시간마다 공격 
![Movie_006](https://github.com/user-attachments/assets/7dcd25ca-bb57-4bb3-9f39-f0cb3df013dd)

***
### 4. 플레이어 피격
화살을 맞을 시 플레이어가 피가 닳는 판정을 만들었습니다.


- 핵심기술
    - WeaponController에서 발사할 화살에게 공격력과 좌표를 부여.
    - 피격받는 대상이 OnTriggerEnter2D로 화살에 맞았는지 판정.
    - ~~owner를 지정하여 본인이 피격받지 않게함~~
    - Tag를 활용하여 발사한 대상과 같은 Tag일 경우 피해를 입히지 않게함.
        - TakeDamage로 맞은 대상의 체력을 감소.
![Movie_013](https://github.com/user-attachments/assets/f0a81d29-8ca7-4154-a95b-10f086768aed)

***
## 🎱 [스킬]

### 1. 스킬 구조
스킬에 내부 트리입니다.


- 핵심기술
    - Skill(부여할 스킬)
        - SkillType(증가시킬 능력치)
        - WeaponController(무기에 대한 정보 중 참조형)
            - Weapon(무기에 대한 정보 중 값형)
            - Arrow(발사체)

![image](https://github.com/user-attachments/assets/039936c0-2e40-4d44-8393-2d95ee7986dd)

***
### 2. 스킬 패턴
스킬은 Arrow만 날라가는 것이 아닌 랜덤한 위치에 번개를 치게 하는 등에 수정 작업이 가능했으나, 
작업에 대한 시간관계상 WeaponController의 값을 다양하게 수정하는 것으로 스킬 패턴을 구현했습니다.


- 핵심기술
    - ArrowSpeed, ArrowDamage 등 화살에 부여할 능력치 변경.
    - 각도를 계산하여 화살을 여러개 쏠 때 단조로움 제거.

![Movie_020](https://github.com/user-attachments/assets/6f7f5c4d-4283-4480-a2c3-643bbece9d52)
![Movie_016](https://github.com/user-attachments/assets/3779a25b-a53a-4b8c-ae2a-fb1b1cf9b263)

***
## 📺 [미니맵]

### 1. 미니맵 구현
기존에 사용하던 맵의 사이즈를 줄이고 플레이어와 몬스터의 부분은 색칠된 점을 표시해주는 것으로 위치 파악을 쉽게 할 수 있도록 만들었습니다.


- 핵심기술
    - 실제 맵에서의 몬스터와 캐릭터를 추적
        - Math.Clamp를 활용하여 미니맵 밖으로 나가는 거 방지
        - 추적한 위치를 기반으로 미니맵에 표시
            - 몬스터의 존재 여부에 따라 List를 추가하거나 SetActive(false)로 관리
    - Mask 기능을 활용하여 기존 맵 구조 중 일부를 화면에 표기되지 않도록 설정
![Movie_021](https://github.com/user-attachments/assets/8865a3f1-d433-40cc-a333-70842492afd1)



