#!/bin/bash
SYSTEM_SQLITE_VERSION="1.0.108.0"
SYSTEM_SQLITE_FRAMEWORK="net451"

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
  echo "Please install System.Data.SQLite.dll." 
else
  echo "Installing libSQLite.Interop.so"
  BUILDIR=$(mktemp -d)
  CWD=$(pwd)
  cd $BUILDIR
  ARCHIVE=sqlite-netFx-source-$SYSTEM_SQLITE_VERSION.zip
  echo "  Downloading $ARCHIVE"
  curl -s -o $ARCHIVE https://system.data.sqlite.org/blobs/$SYSTEM_SQLITE_VERSION/$ARCHIVE > /dev/null
  echo "  Extracting $ARCHIVE"
  unzip -q $ARCHIVE > /dev/null
  echo "  Compiling libSQLite.Interop.so"
  bash Setup/compile-interop-assembly-release.sh > /dev/null
  echo "  Installing libSQLite.Interop.so"
  mkdir -p $CWD/packages/System.Data.SQLite.Core/lib/$SYSTEM_SQLITE_FRAMEWORK/
  cp bin/2013/Release/bin/libSQLite.Interop.so $CWD/packages/System.Data.SQLite.Core/lib/$SYSTEM_SQLITE_FRAMEWORK/
  cd $CWD
  rm -rf $BUILDIR
fi

echo "Creating empty database for type provider."
rm -f src/resources/scholar-kpi-schema.sqlite
sqlite3 src/resources/scholar-kpi-schema.sqlite < src/resources/scholar-kpi-schema.sql

