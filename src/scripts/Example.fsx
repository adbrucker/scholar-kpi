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

#I "../../bin/LogicalHacking.ScholarKpi"
#r "LogicalHacking.ScholarKpi.dll"

#load @"Metrics.fsx"

open LogicalHacking.ScholarKpi.Core.Types
open ScholarScraper
open Metrics
let authorId= "ZWePF1QAAAAJ" 
let publicationList = loadPublicationList false authorId
let citations = totalCitations publicationList.Publications
let hIndex    = hIndex publicationList.Publications
let i10Index = i10Index publicationList.Publications 
