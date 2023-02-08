hookAmatsukazeCLI

■ 概要
Amatsukaze内join_logo_scpでtsファイルフルパス名を取得できるようにする
ちょっとしたプログラム

具体的には、AmatsukazeCLIをフックして環境変数設定後に本来のAmatsukazeCLIを起動
（このプログラム作成時点で使いやすいts入力パス取得方法が見つからないため作成）


■ 使い方
・exe_files内（AmatsukazeCLI.exeと同じ場所）に hookAmatsukazeCLI.exe を置く
・Amatsukaze基本設定でAmatsukazeCLIパスの名前部分をhookAmatsukazeCLI.exeに変更

以上で準備完了。以降は通常通りAmatsukazeを使用

設定される環境変数は以下の通り（AmatsukazeCLIの引数から設定される）
  CLI_IN_PATH  : -iオプションの文字列（入力ファイルパス）
  SERVICE_ID   : -sオプションの文字列（処理するサービスID）
  CLI_OUT_PATH : -oオプションの文字列（出力ファイルパス）

Amatsukaze使用時に上記環境変数が、join_logo_scpのJLスクリプトで使用可能になる


■ 注意点
設定される環境変数（CLI_*_PATH）はAmatsukazeCLI実行時のファイルパスのため
Unicode専用文字が含まれると一時フォルダにコピーされた場所になる
Unicode自体もjoin_logo_scpは想定なく未対応
