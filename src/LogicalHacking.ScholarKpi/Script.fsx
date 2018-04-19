﻿(* 
 * This file is part of the scholar-kpi project. 
 * Copyright (c) 2017 Achim D. Brucker, https://www.brucker.ch
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
 *)

#I "../../packages/FSharp.Data/lib/net45"
#r "FSharp.Data.dll"
#I "../../packages/FSharp.Configuration/lib/net45"
#r "FSharp.Configuration.dll"

#I "../../packages/SQLProvider/lib/net451"
#r "FSharp.Data.SqlProvider.dll"

#I "../../bin/LogicalHacking.ScholarKpi/net47"
#r "LogicalHacking.ScholarKpi.dll"

open LogicalHacking.ScholarKpi.Core.Types
open LogicalHacking.ScholarKpi.Core.Metrics
open LogicalHacking.ScholarKpi.Scraper.Scraper
open LogicalHacking.ScholarKpi.Core.Configuration
open LogicalHacking.ScholarKpi.Persistence.sqlite

open FSharp.Configuration

let config= mkCfg None

let ctx = getCtx (Some "/tmp/") Default
let con = ctx.CreateConnection();



(* Workaround for SSL cert errof - just trust everything *)
open System.Net
open System.IO
ServicePointManager.ServerCertificateValidationCallback <-
  System.Net.Security.RemoteCertificateValidationCallback(fun _ _ _ _ -> true)
(* *)


let row = ctx.Main.Publications.Create()


let googleCfg = (getDataSourceCfg config GoogleScholar "Achim D. Brucker").Value

let publicationList = scrapDataSource googleCfg
let totalPubs = totalPublications publicationList
let citations = totalCitations publicationList
let hIndex    = hIndex publicationList
let i10Index = i10Index publicationList
