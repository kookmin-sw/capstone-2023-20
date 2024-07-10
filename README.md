[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-2e0aaae1b6195c2367325f4f02e2d04e9abb55f0b24a779b69b11b9e10269abc.svg)](https://classroom.github.com/online_ide?assignment_repo_id=10050407&assignment_repo_type=AssignmentRepo)
# WIMP - 2인용 멀티플레이 방탈출 게임

[팀 홈페이지(2023-20)](https://kookmin-sw.github.io/capstone-2023-20/)


### 1. 프로젝트 소개

![WIMF_gitreadmepng](https://user-images.githubusercontent.com/67236054/229353376-b0528b79-79d8-4935-b61d-0d2fe58fcd4a.png)

 본 프로젝트는 기존의 1인형 생존형 공포 퍼즐 게임의 틀을 벗어나 포톤 서버를 활용한 2인 기반 협동 체제를 적용, 이를 인게임 시스템으로 확장함으로써 현재 게임시장에서 고착화된 1인형 공포게임 형태의 변화를 시도하려 한다. 현재까지 기획한 바는 2인형 협동 퍼즐게임인 ‘we were here’를 벤치마킹한 것으로, 서로 다른 플레이어의 소통 수단을 인게임 마이크로 제한하고 두 플레어는 정해진 스토리라인을 따라 플레이하며 갇힌 학교를 탈출하는 컨셉으로 설정하였다.

### 2. 소개 영상
[Gameplay Trailer](https://www.youtube.com/watch?v=A92wvyZtpd4)

### 3. 인게임 사진
<img src="https://github.com/kookmin-sw/capstone-2023-20/assets/31495131/81a27ba2-341f-4282-ad07-efef125b53e1"  width="300" height="150">
<img src="https://github.com/kookmin-sw/capstone-2023-20/assets/31495131/42ae5e10-6a52-40b1-94bc-fffe1f7d9f88"  width="300" height="150">
<br>
<img src="https://github.com/kookmin-sw/capstone-2023-20/assets/31495131/16fb7fdc-68f3-468c-b7fb-96b6ed3eddf60"  width="300" height="150">
<img src="https://github.com/kookmin-sw/capstone-2023-20/assets/31495131/452cb453-2982-4270-8510-bf24919bcd00"  width="300" height="150">




### 4. 팀 소개

|이름|학번|역할|개인 깃허브|
|-|-|-|-|
|김기범|****1583|멀티 플레이 환경구축, 채팅기능, UI/UX |[링크](https://github.com/jimi567)|
|곽혜정|****0655|SFX,몬스터, UI 디자인, 세부기능|[링크](https://github.com/kwawak)|
|김원진|****0538|퍼즐로직, 인벤토리, 미니맵 UI/UX|[링크](https://github.com/oen0thera)|
|유성현|****1656|퍼즐로직, 컷씬, 세부기능|[링크](https://github.com/SeongHyeon0409)|

### 5. 플레이 방법 및 조작법

 유니티 기반 2인용 멀티플레이 방탈출 게임으로 플레이어는 게임시작시 포톤서버를 이용한 로비로 입장, 서로의 캐릭터를 정해 시작하며, 둘 다 시작 준비 버튼을 눌렀을 경우에 시작되게 하여 본격적인 게임 시작 전 각 플레이어가 어떤 캐릭터를 선택할지를 정하고 각각 승연, 예담을 1인칭으로 플레이하게 된다. 방에 입장한 플레이어들은 서로의 위치나, 상태를 인지하지 못한채 오직 마이크를 통해서만 소통할 수 있으며 가이드를 따라 각각의 퍼즐요소들을 클리어하며 스토리라인을 따라 각 스테이지를 클리어하고, 마지막 스테이지까지 도달해서 학교를 탈출하여 최종 클리어한다.
 
 ![image](https://github.com/kookmin-sw/capstone-2023-20/assets/28584160/b9c797c6-1db4-46df-8b0b-b5f46b375f5e)


### 6. 프로젝트 구조

![image](https://user-images.githubusercontent.com/31495131/229294440-e6128fee-73ce-41b8-b303-66b767762299.png)
로컬플레이어는 유니티의 메인타이틀 씬을 통해 게임 온라인 환경에 접속하여, 로비에서 게임 플레이할 방을 입장하거나, 생성 할 수 있다. 한 방에 두 명의 플레이어가 모이고, 서로 합의하에 게임을 시작하면 포톤 서버에서 인게임씬을 빌드하여 제공한다.

### 7. 기타

+ 실행 방법 </br>
https://drive.google.com/file/d/1FkkTZ5PZSkZAf4noTX-ObEwL64bGWxHP/view?usp=sharing </br>
링크에서 zip파일을 다운받아 압축해제 한 후 WIMF.exe 파일을 실행한다.

+ <span style="color:red">현재 플레이가 불가능한 상태입니다.</span>
</br>(2023.05.28 오류) 현재 간혈적으로 플레이어 중 한명의 입력 스키마가 게임패드로 바뀌는 버그 발생으로 해결 중에 있습니다. 해결 후 파일 재업로드하겠습니다.

