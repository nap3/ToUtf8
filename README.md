# ToUtf8 #########################
gitで差分を表示したときにShift-JISのファイルも文字化けしないようにする。

## Description ##########################################
Windowsの.batファイルなど、Shift-JISでないとダメなファイルがある。
それらのファイルに対してgitで差分比較する前処理としてUTF-8に変換する処理を行う。
差分比較自体はUTF-8で行われるため、文字化けは発生しない。

## Features ##########################################
* Shift-JISなどのファイルをUTF-8に変換して標準出力に出力する。

## Requirement ##########################################
### For use
* .Net Framework 4.0 or higher version

### For development
* Visual C# 2015


## Usage ##########################################
#### 1. git configに設定を加える。
.gitattributesで独自のテキスト変換処理として`sjis`が指定された場合に./_GitTools/に保存したToUtf8.exeで変換処理を行うには、次のようにgit configに設定を加える。
```
git config diff.sjis.textconv './_GitTools/ToUtf8.exe'
```
      
#### 2. .gitattributes にUTF-8に変換したいファイルを指定する。
拡張子が.batのファイルすべてをUTF-8に変換する場合は、以下のように記述する。
```
*.bat diff=sjis
```

## Licence ##########################################
This is licensed under the MIT Licence.     
<https://github.com/nap3/ToUtf8/blob/master/LICENSE>


### Open Source Components / Libraries
not use

## Author ##########################################
nap3, <https://github.com/nap3>

