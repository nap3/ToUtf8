# packageProduct #########################
githubにreleaseするためのzipファイルを作る。

## Description ##########################################
githubでのリリースのため、ファイル名にプロダクト名＋アセンブリバージョンを付けた圧縮ファイルを作成する。


## Features ##########################################
* リリースするメインのファイルのアセンブリバージョンからzipファイルのファイル名を生成する。
* メインのファイルがあるディレクトリをzipファイルのルートディレクトリとして関連資源を含むzipファイルを作成する。

## Requirement ##########################################
### For use
* .Net Framework 2.0 or higher version

### For development
* Visual C# 2010


## Usage ##########################################
#### 1. AssemblyInfo.csのアセンブリバージョンを以下のように`*`で指定する。
```csharp
[assembly: AssemblyVersion("2.0.*")]`
//アセンブリのバージョン情報は、以下の 4 つの値で構成されています       
//Major Version   【手動更新】機能追加       
//Minor Version   【手動更新】バグ修正レベルの修正       
//Build Number    【自動更新】2000/1/1からの経過日数      
//Revision        【自動更新】その日の00:00:00（JST基準）からの経過秒数を2で割った数（毎日0スタートになる。）      
```

#### 2. プロダクト名とするモジュールをzipのルートディレクトリとしてリリースする資源を集める。
プロダクト名とするモジュールをsample.exeとして以下のように資源を集めると、sample.exeがあるreleaseフォルダがzipのルートディレクトリとなる。     
     
X:\SampleProj     
|     
+---release     
|   |   LICENSE     
|   |   README.md     
|   |   **sample.exe**     
|   |     
|   \---README_subfiles     
|    　　　image1.png     
|     
|     
     
#### 3. コマンドラインで次のコマンドを実行して、リリースする資源を圧縮する。      
```cmd
packageProduct sample.exe
```

#### 4.  圧縮が完了すると次のような名前のzipファイルが作成される。    
圧縮が完了すると、sample.exeのAssemblyVersionのBuild Numberより、ファイル名にバージョンとビルドした日を含むzipファイルが生成される。         
**sample_2_0_170102.zip**    
      



## Licence ##########################################
This is licensed under the MIT Licence.     
<https://github.com/nap3/packageProduct/blob/master/LICENSE>


### Open Source Components / Libraries
* [DotNetZip v1.9.1.8](https://dotnetzip.codeplex.com/)  [Microsoft Public License (Ms-PL)](https://github.com/nap3/packageProduct/blob/master/packageProduct/Zip_Reduced/License.txt)

## Author ##########################################
nap3, <https://github.com/nap3>

