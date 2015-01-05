![Icon](http://i.imgur.com/ScaNRKz.png)BoxUnlocker
---

BoxUnlockerはFF11の箱空けを支援するツールです。

## 主な機能

* GOVやFOVのフィールドで敵を倒した際、出現した宝箱を調べると自動的に開けます。
* MUMのキーナンバー９９を指定回数自動的に開けます。
* 監視対象を指定する事で、自動実行する項目を選択することができます。
* キーナンバー９９の種類及び連続実行する回数を指定できます。
* 監視状態にしておき、箱やNPCを調べると自動的に箱開けを行います。
* 運の要素があるので、必ず開けられるわけではありません。

## 使用方法
1. 起動すると自動的に監視が開始されます。設定変更したい場合は一度停止させてください。
2. 監視されている状態で、フィールドの箱またはキーナンバー９９のNPCを選択し、メニューが表示されると箱開けが開始されます。
3. enternityアドオンを使用している場合、**必ず**画面の「enternityを使用している」をチェックしてから実行してください。

## 動かない場合
* [VisualStudio2013のランタイム](http://www.microsoft.com/ja-JP/download/details.aspx?id=40784)をインストールする。
* [.Net4.5](http://www.microsoft.com/ja-jp/download/details.aspx?id=30653)をインストールする。
* Windowerのenternityアドオンを使用している場合、「enternityを使用する」にチェックされているか？
* BoxUnlocker.iniのBaseWaitの値を増やす。（デフォルトは300ミリ秒）
* BoxUnlocker.iniのNumberInputWaitの値を増やす。（デフォルトは1000ミリ秒）


## iniファイルについて
BoxUnlocker.ini
```
[BoxUnlocker]
PosX=0
PosY=0
ExecField=True
ExecMum=True
ExecAbyssea=True
MumGameID=21
MumTryCount=10
BaseWait=300
NumberInputWait=1000
UseEnternity=False
```
* PosX,PosY
フォーム位置
* ExecField,ExecMum,ExecAbyssea
監視対象とするもの（自動的に箱空けするもの）
ExecField：FOV・GOVの茶箱  
ExecMum：キーナンバー９９  
ExecAbyssea：アビセアの箱（まだ未実装）  
* MumGameID
キーナンバー９９で実行するゲームの種類
* MumTryCount
キーナンバー９９を連続実行する回数
* BaseWait
基本となるウェイト秒数(ミリ秒で指定)  
正常に動作しない場合増やすことをオススメします  
* NumberInputWait
数字を入力したあと、受け取ったチャット内容を処理するまでのウェイト  
正常に動作しない場合増やすことをオススメします  
* UseEnternity
enternityを使用しているか

## 開発環境
* Windows7 Ultimate 64bit
* [Microsoft Visual Studio Express 2013 for Windows Desktop](http://www.visualstudio.com/ja-jp/products/visual-studio-express-vs.aspx)
* [.NET Framework 4.5](http://www.microsoft.com/ja-jp/net/)
* [FFACE](http://www.ffevo.net/)
* [FFACETools](https://github.com/h1pp0/FFACETools_ffevo.net/)の[日本語対応版](https://github.com/rohme/FFACEToolsJP)

## ソース
BoxUnlockerは以下のサイトで、GPLv2ライセンスにて公開しています。  
[https://github.com/rohme/BoxUnlocker](https://github.com/rohme/BoxUnlocker)

## 免責事項
本ソフトはフリーソフトです。自由にご使用ください。  
このソフトウェアを使用したことによって生じたすべての障害・損害・不具合等に関しては、作者は一切の責任を負いません。各自の責任においてご使用ください。  

## 修正履歴
* 2015-01-05 Ver0.1.2
	- プラットフォームを.NET4.5から.NET4.0に変更(XP対応)
