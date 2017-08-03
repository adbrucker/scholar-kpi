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

#load @"ScholarScraper.fsx"
open PublicationTypes

let pubSince year publications = List.filter (fun (p:Publication) -> p.Year >= Some year) publications
                                                 

let i10Index publications= List.length (List.filter (fun (p:Publication) -> p.Citations > 9) publications)

let hIndex year publications= 
       let rec hIndexRec i = function 
                           | (c::cs) -> if c  >= (i+1) then hIndexRec (i+1) cs else i 
                           | []      -> i
       hIndexRec 0 (List.rev (List.sort (List.map (fun (p:Publication) -> p.Citations) publications)))


let totalCitations publications = List.sumBy (fun (p:Publication) -> p.Citations) publications

