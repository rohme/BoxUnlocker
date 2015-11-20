![Icon](http://i.imgur.com/ScaNRKz.png)BoxUnlocker
---

BoxUnlockerはFF11の箱開けを支援するツールです。

![メイン](http://i.imgur.com/MQoDMFO.png)

## 主な機能
* GOVやFOVのフィールドで敵を倒した際、出現した宝箱を調べると自動的に開けます。
* MUMのキーナンバー９９を指定回数自動的に開けます。
* 監視対象を指定する事で、自動箱開けする項目を選択することができます。
* キーナンバー９９の種類及び連続実行する回数を指定できます。
* 監視状態にしておき、箱やNPCを調べると自動的に箱開けを行います。
* 運の要素があるので、必ず開けられるわけではありません。

## 使用方法
1. enternityアドオンを使用していることを前提に作成されています。enternityアドオンを起動してから実行してください。
2. 起動すると自動的に監視が開始されます。設定変更したい場合は一度停止させてください。
3. 監視されている状態で、フィールドの箱またはキーナンバー９９のNPCを選択し、メニューが表示されると箱開けが開始されます。

## 画面説明
### メイン画面
![メイン](http://i.imgur.com/MQoDMFO.png)
* 対象  
箱開け中の対象の名前が表示されます。
* 回数  
現在の箱開け回数と最大回数が表示されます。
* ![開始ボタン](http://i.imgur.com/LiFwhCb.png)開始ボタン  
監視を実行し、箱をタゲった時に自動的に箱開けを開始します。もう一度押すと監視を停止します。
* ![設定ボタン](http://i.imgur.com/GrwZHZD.png)設定ボタン  
設定画面を表示します。監視を実行している状態では押せません。

### 設定画面
![設定](http://i.imgur.com/RWte0Km.png)
* フィールド  
フィールド箱を対象対象にする場合Onにしてください。
* MUM  
MUM箱を監視対象にする場合Onにしてください。
* 種類  
MUMで箱開けをするゲームの種類を選択してください。
* 回数  
MUMの箱開けを連続して何回行うかを指定してください。
* BaseWait  
キー操作や数値の入力など、基本的なウェイトに使用しています。
* ChatWait  
数値を入力してから、チャットを読み取るまでのウェイトに使用しています。

デフォルトのウェイトは多めに設定しており、私の環境ではBaseWait=200,ChatWait=500でも動作しています。  
各人の環境に合わせて適時調整してください。

## 動かない場合
* 管理者権限で実行してください。
* セキュリティソフトを一旦切ってから実行してみる。
* [Windower4](http://windower.net/)をインストールする。
* 最新の[EliteAPI](http://www.elitemmonetwork.com/)をインストールする。  
http://ext.elitemmonetwork.com/downloads/eliteapi/
* [EliteAPI](http://www.elitemmonetwork.com/)に障害情報が挙がっていないか確認する。  
http://www.elitemmonetwork.com/forums/viewforum.php?f=28
* [VisualStudio2013のランタイム](http://www.microsoft.com/ja-JP/download/details.aspx?id=40784)をインストールする。**（必ずx86版を使用してください）**
* [.Net4.0](http://www.microsoft.com/ja-JP/download/details.aspx?id=17718)以上をインストールする。

## 開発環境
* Windows7 Ultimate 64bit
* Microsoft Visual Studio 2013 Ultimate
* [.NET Framework 4.0](http://www.microsoft.com/ja-jp/net/)
* [EliteAPI](http://www.elitemmonetwork.com/)

## ソース
BoxUnlockerは以下のサイトで、GPLv2ライセンスにて公開しています。  
[https://github.com/rohme/BoxUnlocker](https://github.com/rohme/BoxUnlocker)

## 免責事項
本ソフトはフリーソフトです。自由にご使用ください。  
このソフトウェアを使用したことによって生じたすべての障害・損害・不具合等に関しては、作者は一切の責任を負いません。各自の責任においてご使用ください。  

## 修正履歴
* 2015-11-20 Ver0.9.0 プレリリース
	- お勉強用にスクラップ＆ビルドしてみた
* 2015-01-05 Ver0.1.2
	- プラットフォームを.NET4.5から.NET4.0に変更(XP対応)
