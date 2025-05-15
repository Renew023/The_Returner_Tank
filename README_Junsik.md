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
