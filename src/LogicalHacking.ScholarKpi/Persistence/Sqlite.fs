(*
 * This file is part of the scholar-kpi project.
 * Copyright (c) 2017-2018 Achim D. Brucker, https://www.brucker.ch
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, version 3.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <https://www.gnu.org/licenses/>.
 *
 * SPDX-License-Identifier: GPL-3.0-or-later
 *)

namespace LogicalHacking.ScholarKpi.Persistence
    open FSharp.Data.Sql
    open System.IO

    module sqlite = 
        let [<Literal>] DbDir =  __SOURCE_DIRECTORY__
                                 + @"/../../resources"
        let [<Literal>] Db =  DbDir + @"/scholar-kpi.sqlite"
    // static (compile time)  ConnectionString
        let [<Literal>] ConnectionString = @"Data Source="
                                         + DbDir 
                                         + @"/scholar-kpi-schema.sqlite"
                                         + @";Version=3"
        let [<Literal>] ResolutionPath = __SOURCE_DIRECTORY__ + @"/../../../packages/System.Data.SQLite.Core/lib/net451/"
    // create a type alias with the connection string and database vendor settings
        type ExtensionDbProvider = SqlDataProvider< 
                                     SQLiteLibrary = Common.SQLiteLibrary.SystemDataSQLite,
                                     ConnectionString = ConnectionString,
                                     DatabaseVendor = Common.DatabaseProviderTypes.SQLITE,
                                     ResolutionPath = ResolutionPath,
                                     IndividualsAmount = 500,
                                     UseOptionTypes = true >
        type ExtensionDbType = ExtensionDbProvider.dataContext

        type Db = Default | Custom of string

        let getCtx database  = match  database with
                               | Default         -> ExtensionDbProvider.GetDataContext(
                                                      sprintf "Data Source=%s/%s;Version=3" DbDir Db)
                               | Custom customDb -> ExtensionDbProvider.GetDataContext(
                                                      sprintf "Data Source=%s;Version=3" customDb)
 
        let getPublications (ctx:ExtensionDbType)  _ = []
