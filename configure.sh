#!/bin/bash
if test "$OS" = "Windows_NT"
then
  MONO=""
else
  MONO="mono"
fi

$MONO .paket/paket.bootstrapper.exe
exit_code=$?
if [ $exit_code -ne 0 ]; then
  exit $exit_code
fi
if [ -e "paket.lock" ]
then
  $MONO .paket/paket.exe restore
else
  $MONO .paket/paket.exe install
fi
exit_code=$?
if [ $exit_code -ne 0 ]; then
        exit $exit_code
fi
if test "$OS" = "Windows_NT"
then
  echo "Please install System.Data.SQLite.dll into packages/SQLProvider/lib." 
else
  echo "Building libSQLite.Interop.so"
  BUILDIR=$(mktemp -d)
  CWD=$(pwd)
  cd $BUILDIR
  curl -o sqlite-netFx-source-1.0.108.0.zip https://system.data.sqlite.org/blobs/1.0.108.0/sqlite-netFx-source-1.0.108.0.zip
  unzip sqlite-netFx-source-1.0.108.0.zip
  bash Setup/compile-interop-assembly-release.sh
  mkdir -p $CWD/packages/System.Data.SQLite.Core/lib/net451/libSQLite.Interop.so
  cp bin/2013/Release/bin/libSQLite.Interop.so $CWD/packages/System.Data.SQLite.Core/lib/net451/libSQLite.Interop.so
  cd $CWD
  rm -rf $BUILDIR
fi

echo "Creating empty database for type provider."
rm -f src/resources/scholar-kpi-schema.sqlite
sqlite3 src/resources/scholar-kpi-schema.sqlite < src/resources/scholar-kpi-schema.sql

