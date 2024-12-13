@echo off

REM GitHub 리포지토리 정보
set REPO_OWNER=Natel210
set REPO_NAME=SimpleFileIO
set API_URL=https://api.github.com/repos/%REPO_OWNER%/%REPO_NAME%/releases/latest

REM 다운로드 폴더
set DOWNLOAD_DIR=.\BuildWorkspace\%REPO_OWNER%\%REPO_NAME%

REM 1. 다운로드 폴더 생성
if not exist "%DOWNLOAD_DIR%" (
    mkdir "%DOWNLOAD_DIR%"
)

REM 2. 최신 릴리스 정보 가져오기
curl -s %API_URL% > latest_release.json

REM 3. debug 및 release 관련 DLL 다운로드 링크 추출 및 다운로드
for /f "tokens=*" %%A in ('jq -r ".assets[] | select(.name == \"debug-related.dll\") | .browser_download_url" latest_release.json') do (
    echo Downloading debug-related.dll from %%A...
    curl -L -o "%DOWNLOAD_DIR%\debug-related.dll" %%A
)
for /f "tokens=*" %%A in ('jq -r ".assets[] | select(.name == \"release-related.dll\") | .browser_download_url" latest_release.json') do (
    echo Downloading release-related.dll from %%A...
    curl -L -o "%DOWNLOAD_DIR%\release-related.dll" %%A
)

REM 4. 다운로드 완료 메시지
echo All debug and release DLLs have been downloaded to %DOWNLOAD_DIR%.
pause