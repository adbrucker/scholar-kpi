(* 
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

namespace LogicalHacking.ScholarKpi.Core

open LogicalHacking.ScholarKpi.Core.Types

module Metrics = 
    let pubSince year (publications:PublicationList) = List.filter (fun (p:Publication) -> p.Year >= Some year) publications.Publications
                                                     
    let getPublicationsWihAtLeastNCitations n (publications:PublicationList) = List.filter (fun (p:Publication) -> p.Citations >= n) publications.Publications

    let iNIndex n (publications:PublicationList) = getPublicationsWihAtLeastNCitations n publications |> List.length
    let i10Index = iNIndex 10 

    let hIndex (publications:PublicationList)= 
           let rec hIndexRec i = function 
                               | (c::cs) -> if c  >= (i+1) then hIndexRec (i+1) cs else i 
                               | []      -> i
           hIndexRec 0 (List.rev (List.sort (List.map (fun (p:Publication) -> p.Citations) publications.Publications)))


    let totalCitations (publications:PublicationList) = List.sumBy (fun (p:Publication) -> p.Citations) publications.Publications

    let totalPublications  (publications:PublicationList) = List.length publications.Publications
