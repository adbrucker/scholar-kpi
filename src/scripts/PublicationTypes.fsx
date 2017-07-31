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

namespace PublicationTypes

open System

type Publication = 
   struct
   val Title:string
   val Year: int option 
   val Id: string
   val CitationKey: string option
   val Citations:int   
   val CitationHistory: (int * int) list
   new(id,title,year,citationKey, citations,citationHistory) = {Id=id; Title=title; 
                                                                Year = year; 
                                                                CitationKey = citationKey; 
                                                                Citations=citations; 
                                                                CitationHistory=citationHistory}
end 

type BibMetrics = 
    struct
    val HIndex: int
    val I10Index: int 
    val Citations: int 
    new(hIndex, i10Index, citations) = {HIndex = hIndex; 
                                        I10Index = i10Index; 
                                        Citations=citations}
end

type PublicationList = 
    struct
    val AuthorId: string
    val DateTime: DateTime
    val Publications: (Publication list)
    val Metrics: BibMetrics option
    val RecentMetrics: BibMetrics option
    val RecentYear: int option
    new(authorId, dateTime, publications, metrics, recentMetrics, recentYear) = {AuthorId=authorId;
                                                                                 DateTime=dateTime;
                                                                                 Publications=publications;
                                                                                 Metrics=metrics;
                                                                                 RecentMetrics=recentMetrics;
                                                                                 RecentYear=recentYear}
end
