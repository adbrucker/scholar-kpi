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

namespace LogicalHacking.ScholarKpi.Scraper

open System
open LogicalHacking.ScholarKpi.Scraper.GoogleScholar
open LogicalHacking.ScholarKpi.Core.Types
open LogicalHacking.ScholarKpi.Core.Configuration


module Scraper = 
    exception NotYetImplemntedException
    let scrapDataSource = function 
                          | ({Service = GoogleScholar;   AuthorId=author; FullDownload=full}:DataSourceCfg) -> GoogleScholar.loadPublicationList full author
                          | ({Service = Orcid;           AuthorId=author; FullDownload=full}:DataSourceCfg) -> raise NotYetImplemntedException
                          | ({Service = Dblp;            AuthorId=author; FullDownload=full}:DataSourceCfg) -> raise NotYetImplemntedException
                          | ({Service = Pubzone;         AuthorId=author; FullDownload=full}:DataSourceCfg) -> raise NotYetImplemntedException
                          | ({Service = AcmDl;           AuthorId=author; FullDownload=full}:DataSourceCfg) -> raise NotYetImplemntedException
                          | ({Service = CsNet;           AuthorId=author; FullDownload=full}:DataSourceCfg) -> raise NotYetImplemntedException
                          | ({Service = SemanticScholar; AuthorId=author; FullDownload=full}:DataSourceCfg) -> raise NotYetImplemntedException
                           
