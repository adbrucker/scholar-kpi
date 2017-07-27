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
(* )
<tr class="gsc_a_tr">
  <td class="gsc_a_t">
    <a href="/citations?view_op=view_citation&amp;hl=en&amp;user=ZWePF1QAAAAJ&amp;citation_for_view=ZWePF1QAAAAJ:WF5omc3nYNoC"
     class="gsc_a_at">Extending access control models with break-glass</a>
    <div class="gs_gray">AD Brucker, H Petritsch</div>
    <div class="gs_gray">Proceedings of the 14th ACM symposium on Access control models and ...<span class="gs_oph">, 2009</span></div>
  </td>
  <td class="gsc_a_c">
    <a href="https://scholar.google.com/scholar?oi=bibs&amp;hl=en&amp;cites=1468743576812343055" class="gsc_a_ac">116</a>
  </td>
  <td class="gsc_a_y"><span class="gsc_a_h">2009</span></td>
</tr>
*)
let parsePublicationTd tds =
                  let parseYear (n:HtmlNode) = try
                                                 Some(int(n.InnerText()))
                                               with
                                                 | _ -> None
                  let parseCitation (n:HtmlNode) = try
                                                     int(n.InnerText())
                                                   with
                                                    | _ -> 0
                  let parseCitationId (n:HtmlNode) = try
                                                       n.Descendants["a"]
                                                        |> Seq.choose (fun (x:HtmlNode) -> x.TryGetAttribute("href")
                                                                                         |> Option.map (fun a -> x.InnerText(), a.Value()))
                                                         |> Seq.head
                                                         |> snd
                                                         |> (fun s -> s.Split([| '='; '&' |]))
                                                         |> Seq.rev
                                                         |> Seq.head
                                                         |> Some
                                                     with
                                                     | _ -> None
                  match tds:HtmlNode list with
                  | [a; citationNode; yearNode]     -> (a.InnerText(), parseCitationId citationNode, parseCitation citationNode, parseYear yearNode)
                  | _             -> ("", None, 0, None)

let getPublicationTableBody (page:HtmlDocument) = page.Descendants["table"]
                                                  |> Seq.filter(fun n -> n.TryGetAttribute("id") = Some(HtmlAttribute.New("id","gsc_a_t")))
                                                  |> Seq.map (fun n -> n.Descendants["tbody"])                                   
                                                  |> Seq.fold Seq.append Seq.empty
                                                  |> Seq.map (fun n -> n.Descendants["tr"])                                      
                                                  |> Seq.fold Seq.append Seq.empty
                                                  |> Seq.map (fun n -> n.Descendants["td"] |> Seq.toList |> parsePublicationTd)
                                                  |> Seq.toList

(* Some simple tests ... *)
let authorPages = getAuthorPages "ZWePF1QAAAAJ"
let publicationTable = List.map getPublicationTableBody authorPages
                       |> List.fold List.append List.empty
                                               