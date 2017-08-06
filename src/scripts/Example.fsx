(* 
 * This file is part of the gscholar-kpi project. 
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

#I "../../packages/FSharp.Data/lib/net40"
#r "FSharp.Data.dll"
#I "../../bin/LogicalHacking.ScholarKpi"
#r "LogicalHacking.ScholarKpi.dll"

open LogicalHacking.ScholarKpi.Core.Types
open LogicalHacking.ScholarKpi.Core.Metrics
open LogicalHacking.ScholarKpi.Scraper.GoogleScholar

let authorId= "ZWePF1QAAAAJ" 
let publicationList = loadPublicationList false authorId
let citations = totalCitations publicationList.Publications
let hIndex    = hIndex publicationList.Publications
let i10Index = i10Index publicationList.Publications 
