# Himeno Flex

**Himeno Flex** is the calculate-based benchmark GUI application include the [Himeno Benchmark Test](https://i.riken.jp/supercom/documents/himenobmt/) (Published by the RIKEN[^riken]).

[^riken]: the Institute of Physical and Chemical Research (Japan)

## Technology

This application has been created using [.NET MAUI](https://dot.net/maui).

- UI
  - .NET MAUI
  - .NET 10 (C#)
- Processing
  - C-lang with C# P/Invoke

<!--
## 従来の姫野ベンチマークとの比較
| 項目 | 従来 | Himeno Flex |
| ---- | ---- | ----------- |
| UI   | CUI  | **GUI**     |
| ダウンロード | `.zip`展開 - `.lzh`展開 - `.c`のビルド - 起動 | **`.exe`起動** |
| OS   | **デスクトップ向け** | デスクトップ&モバイル(予定)(※) |

※当初の予定ではWindows/MacOS/Android/iOSに対応予定でしたが、Androidは技術不足で開発困難、MacOS/iOSは実機テストが不可能なために開発を断念しました。
<details>
  <summary>Android版撤退の経緯</summary>
  初めに、こちらのソフトウェアの仕組みですが、GUI部分(Himeno_Flex)から姫野ベンチマーク(HimenoBMTxps)を読み込んでいます。<br />
  読み込む方法として、Windows版では、.dllを利用しており、Android版でも同様に.dllでの読み込みを想定していましたが、<br />
  次のような問題が発生しました。<br />
  ・.dllはAndroidで読み込めない?<br />
  インターネットの情報では、読み込める説と読み込めない説が混在しており、真偽は不明です。<br />
  ・どのようにしてファイルに入れる?<br />
  .dllまたは、Android用の代替案の.soファイルをビルドした際にAndroidファイルの中に入れなければなりませんが、<br />
  どのファイルに入れればよいかの情報が全く見つかりませんでした。<br />
  Xamarin時代はAssetファイルに入れていたようですが、MAUIではそのようなファイルは存在しないので、どのファイルに入れればよいのかがわかりません。<br />
  Resoruce/Raw説がありますが…<br />
  .csprojでdll importするなども試していますが、それも特に意味はなさそうです。<br />
  <br />
  といったように、姫野ベンチマーク(HimenoBMTxps)部分の読み込みに苦労したうえ、進展がないと見込みましたので、開発停止とさせていただきます。<br />
  これについて詳しい方はぜひご意見いただけると幸いです。<br />
</details>
-->

## License

Licensed under the [LGPL ver2.0 or later](LICENSE.txt).
