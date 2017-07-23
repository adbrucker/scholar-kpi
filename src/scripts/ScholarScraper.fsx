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
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 *)

#r @"../../packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open FSharp.Data

let getAuthorPage author n = HtmlDocument.Load("https://scholar.google.com/citations?user="+
                                               author+"&cstart="+string(n*100)+"&pagesize=100")

let hasNextAuthorPage (ap:HtmlDocument) = ap.Descendants["button"]
                                        |> Seq.filter (fun n -> 
                                                        n.TryGetAttribute("aria-label") = 
                                                         Some(HtmlAttribute.New("aria-label","Next")))
                                        |> (Seq.exists (fun n -> n.TryGetAttribute("disabled") = None))

let getAuthorPages author = let rec getAuthorPagesRec author n = let page = getAuthorPage author n
                                                                 if hasNextAuthorPage page 
                                                                 then page::(getAuthorPagesRec author (n+1)) 
                                                                 else [page]
                            getAuthorPagesRec author 0
