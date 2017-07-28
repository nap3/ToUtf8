@echo ****************************************************************
@echo    リリース資源を集めて、リリース用のzipファイルを作成します。
@echo ****************************************************************
@echo.

@rem リリース資源を集めるフォルダを作成
@set stagingDir=Staging\
md %stagingDir%


@rem パッケージ化するモジュールを収集
copy "ToUtf8\bin\Release\ToUtf8.exe"         %stagingDir%  || call :func_show_error "ToUtf8.exe"
copy "README.md"        %stagingDir%  || call :func_show_error "README.md"
copy "LICENSE"          %stagingDir%  || call :func_show_error "LICENSE"




@rem パッケージ化処理
_GitTools\packageProduct.exe %stagingDir%ToUtf8.exe

rmdir /S /Q %stagingDir%

@rem 終了処理
@echo.
@echo ***************************************************************
@echo すべての処理が完了しました。
@echo ***************************************************************
Timeout /t 10
exit



@rem ---------------------------------------------
@rem  以降、サブルーチン
@rem ---------------------------------------------
:func_show_error
@rem %~1のようにチルダをつけるとダブルクオートが削られる。
@echo.
@echo ***************************************************************
@echo   %~1 のコピー処理に失敗しました。
@echo   リリース資源の作成処理を終了します。
@echo ***************************************************************
@pause
exit
@rem 元の処理に戻り続行する場合は/bオプションが必要
@rem exit /b
