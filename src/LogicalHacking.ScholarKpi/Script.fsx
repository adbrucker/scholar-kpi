﻿(* 
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
 * SPDX-License-Identifie GPL-3.0-or-later
 *)

#load "ScholarKpi.fsx"

open LogicalHacking.ScholarKpi.Core.Types
open LogicalHacking.ScholarKpi.Core.Metrics
open LogicalHacking.ScholarKpi.Scraper.Scraper
open LogicalHacking.ScholarKpi.Core.Configuration
open LogicalHacking.ScholarKpi.Persistence.sqlite

open FSharp.Configuration

let config= mkCfg None

let ctx = getCtx Default

(* Example of scraping Google Scholar and basic KPI computations *)
let googleCfg = (getDataSourceCfg config GoogleScholar "Achim D. Brucker").Value
let publicationList = scrapDataSource googleCfg
let totalPubs = totalPublications publicationList
let citations = totalCitations publicationList
let hIndex    = hIndex publicationList
let i10Index = i10Index publicationList

 (* Using SQLite for persistence *)
(* For debugging:
FSharp.Data.Sql.Common.QueryEvents.SqlQueryEvent |> Event.add (printfn "Executing SQL: %O")
*)

let storePublicationList publicationList = 
    let con = ctx.CreateConnection()
    let row = ctx.Main.PublicationLists.``Create(AuthorID, DataSource, DateTime)`` ("author", "source", "date")
    row.PublicationListId
    ctx.SubmitUpdates()
    let publicationListId = row.PublicationListId
    let roww = ctx.Main.BibMetrics.``Create(PublicationListId, I10Index, Citations)`` (publicationListId.Value)
    roww.
