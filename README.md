[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-c66648af7eb3fe8bc4f294546bfd86ef473780cde1dea487d3c4ff354943c9ae.svg)](https://classroom.github.com/online_ide?assignment_repo_id=10029209&assignment_repo_type=AssignmentRepo)
# Welcome to GitHub

캡스톤 팀 생성을 축하합니다.

## 팀소개 및 페이지를 꾸며주세요.

- 프로젝트 소개
  - 프로젝트 설치방법 및 데모, 사용방법, 프리뷰등을 readme.md에 작성.
  - Api나 사용방법등 내용이 많을경우 wiki에 꾸미고 링크 추가.

- 팀페이지 꾸미기
  - 프로젝트 소개 및 팀원 소개
  - index.md 예시보고 수정.

- GitHub Pages 리파지토리 Settings > Options > GitHub Pages 
  - Source를 marster branch
  - Theme Chooser에서 태마선택
  - 수정후 팀페이지 확인하여 점검.

**팀페이지 주소** -> https://kookmin-sw.github.io/ '{{자신의 리파지토리 아이디}}'

**예시)** 2023년 0조  https://kookmin-sw.github.io/capstone-2023-00/


## 내용에 아래와 같은 내용들을 추가하세요.

### 1. 프로잭트 소개

프로젝트

### 2. 소개 영상

프로젝트 소개하는 영상을 추가하세요

### 3. 팀 소개

팀을 소개하세요.

팀원정보 및 담당이나 사진 및 SNS를 이용하여 소개하세요.

### 4. 사용법

소스코드제출시 설치법이나 사용법을 작성하세요.

### 5. 기타

추가적인 내용은 자유롭게 작성하세요.


## Markdown을 사용하여 내용꾸미기

Markdown은 작문을 스타일링하기위한 가볍고 사용하기 쉬운 구문입니다. 여기에는 다음을위한 규칙이 포함됩니다.

```markdown
Syntax highlighted code block

# Header 1
## Header 2
### Header 3

- Bulleted
- List

1. Numbered
2. List

**Bold** and _Italic_ and `Code` text

[Link](url) and ![Image](src)
```

자세한 내용은 [GitHub Flavored Markdown](https://guides.github.com/features/mastering-markdown/).

### Support or Contact

readme 파일 생성에 추가적인 도움이 필요하면 [도움말](https://help.github.com/articles/about-readmes/) 이나 [contact support](https://github.com/contact) 을 이용하세요.

## Commit message Rules​

- 제목과 본문을 한 줄 띄워 분리하기
- 제목은 영문 기준 50자 이내로
  제목 첫 글자를 대문자로
- 제목 끝에 . 금지
- 제목은 명령조로
- Github - 제목(이나 본문)에 이슈 번호 붙이기
- 본문은 영문 기준 72자마다 줄 바꾸기
- 본문은 어떻게보다 무엇을, 왜에 맞춰 작성하기

------

[type] : [subject]

[body] //헤더로 표현이 가능하면 생략, 아닌 경우 자세한 내용 구성

[footer] // 어떠한 이슈에 대한 커밋인지 issue number 포함

------

+ feat : 새로운 기능 추가
+ fix : 버그 수정
+ docs : 문서 수정
+ style : 코드 formatting, 세미콜론(;) 누락, 코드 변경이 없는 경우
+ refactor : 코드 리팩터링
+ test : 테스트 코드, 리팩터링 테스트 코드 추가(프로덕션 코드 변경 X)
+ chore : 빌드 업무 수정, 패키지 매니저 수정(프로덕션 코드 변경 X)
+ design : CSS 등 사용자 UI 디자인 변경
+ comment : 필요한 주석 추가 및 변경
+ rename : 파일 혹은 폴더명을 수정하거나 옮기는 작업만인 경우
+ remove : 파일을 삭제하는 작업만 수행한 경우
