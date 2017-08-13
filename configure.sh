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
  if [ -f /usr/lib/mono/4.5/Mono.Data.Sqlite.dll ];
  then
    if [ ! -f packages/SQLProvider/lib/Mono.Data.Sqlite.dll ];
    then
      ln -s /usr/lib/mono/4.5/Mono.Data.Sqlite.dll  packages/SQLProvider/lib 
    fi
  else
    echo "Please install Mono.Data.Sqlite.dll into packages/SQLProvider/lib." 
  fi
fi

echo "Creating empty database for type provider."
rm -f src/resources/scholar-kpi-schema.sqlite
sqlite3 src/resources/scholar-kpi-schema.sqlite < src/resources/scholar-kpi-schema.sql

