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

let getPaperCitationPage citationId = HtmlDocument.Load("https://scholar.google.com/citations?"+
                                                       "view_op=view_citation&hl=en&"+
                                                       "citation_for_view="+citationId)

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
(* 
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
                                                        |> (fun s -> if isNull s || s = "" then None else Some s) 
                                                     with
                                                     | _ -> None
                  match tds:HtmlNode list with
                  | [a; citationNode; yearNode]     -> (a.InnerText(), parseCitationId a, parseCitation citationNode, parseCitationId citationNode, parseYear yearNode)
                  | _             -> ("", None, 0, None, None)


let getRowsOfTable (page:HtmlDocument) (rt:(string list)) id  = page.Descendants["table"]
                                                              |> Seq.filter(fun n -> n.TryGetAttribute("id") = Some(HtmlAttribute.New("id",id)))
                                                              |> Seq.map (fun n -> n.Descendants["tr"])                                      
                                                              |> Seq.fold Seq.append Seq.empty
                                                              |> Seq.map (fun n -> (n.Descendants rt) |> Seq.toList) 
                                                              |> Seq.filter(fun n -> ([] <> n))
                                                              |> Seq.toList
let getTdListOfTable (page:HtmlDocument) id = getRowsOfTable page ["td"] id
let getThListOfTable (page:HtmlDocument) id = List.concat (getRowsOfTable page ["th"] id) 

let getPublicationTableBody (page:HtmlDocument) =  List.map parsePublicationTd (getTdListOfTable page "gsc_a_t")

let getPublicationTable authorPages = List.map getPublicationTableBody authorPages
                                    |> List.fold List.append List.empty
 (*
<table id="gsc_rsb_st">
  <tbody>
    <tr>
      <th class="gsc_rsb_sc1">
        <a href="javascript">Citation indices</a>
      </th>
      <th class="gsc_rsb_sth">All</th>
      <th class="gsc_rsb_sth">Since 2012</th>
    </tr>
    <tr>
      <td class="gsc_rsb_sc1">
        <a href="javascript" class="gsc_rsb_f" title="...">Citations</a>
      </td>
      <td class="gsc_rsb_std">15725</td>
      <td class="gsc_rsb_std">3926</td>
    </tr>
    <tr>
      <td class="gsc_rsb_sc1">
        <a href="javascript" class="gsc_rsb_f" title="...">h-index</a>
      </td>
      <td class="gsc_rsb_std">31</td>
      <td class="gsc_rsb_std">21</td>
    </tr>
    <tr>
      <td class="gsc_rsb_sc1">
        <a href="javascript" class="gsc_rsb_f" title="...">i10-index</a>
      </td>
      <td class="gsc_rsb_std">46</td>
      <td class="gsc_rsb_std">30</td>
    </tr>
  </tbody>
</table>
 *)


let parseKpiTd tds = let parseInt (n:HtmlNode) = try
                                                   Some(int(n.InnerText()))
                                                 with
                                                  | _ -> None
                     match tds:HtmlNode list with
                            | [t; n1; n2]     -> (t.InnerText(), parseInt n1, parseInt n2)
                            | _               -> ("", None, None)

let getKpiTableBody (page:HtmlDocument) =  List.map parseKpiTd (getTdListOfTable page "gsc_rsb_st")


let getKpiTableBodyFromList = function 
    | (p::_) -> Some (getKpiTableBody p)
    |   []   -> None 


let citationYears (page:HtmlDocument) = page.Descendants["div"]
                                       |> Seq.filter(fun n -> n.TryGetAttribute("id") = Some(HtmlAttribute.New("id","gsc_g_x")))
                                       |> Seq.map (fun n -> n.Descendants(["span"]))
                                       |> Seq.fold Seq.append Seq.empty
                                       |> Seq.map (fun n -> int (n.InnerText()))
                                       |> Seq.toList

let rec tupleize = function 
                 | (x::xs), (y::ys) -> (x,y)::(tupleize (xs, ys)) 
                 | [], []           -> []
                 | (x::xs), []      -> []
                 | [], (y::ys)      -> []

let getCitationRow (page:HtmlDocument) id = page.Descendants["div"]
                                           |> Seq.filter(fun n -> n.TryGetAttribute("id") = Some(HtmlAttribute.New("id",id)))
                                           |> Seq.map (fun n -> n.Descendants(["span"]))
                                           |> Seq.fold Seq.append Seq.empty
                                           |> Seq.map (fun n -> int (n.InnerText()))
                                           |> Seq.toList
                                        

let getOverviewCitationTable (page:HtmlDocument) = let citationYears = getCitationRow page "gsc_g_x"
                                                   let citations     = getCitationRow page "gsc_g_bars"
                                                   tupleize (citationYears, citations)

let getPaperCitationTable (page:HtmlDocument) = let citationData  = getCitationRow page "gsc_graph_bars"
                                                let citationYears = citationData |> Seq.take ((Seq.length citationData) / 2) |> Seq.toList
                                                let citations     = citationData |> Seq.skip ((Seq.length citationData) / 2) |> Seq.toList
                                                tupleize (citationYears, citations)


let getOverviewCitationTableFromList = function 
    | (p::_) -> Some (getOverviewCitationTable p)
    |   []   -> None 

let getPaperCitationTables  publicationTable =  publicationTable |> List.filter (fun (_, id, citations, _, _) -> id <> None && citations <> 0)
                                                                 |> List.map (fun (_, id, _ , _, _) -> match id with 
                                                                                                        | Some citationId -> (citationId, getPaperCitationTable (getPaperCitationPage citationId))
                                                                                                        | None            -> ("", []))
                                                                 |> List.filter (fun (s, _) -> s <> "")

(* Some simple tests ... *)
let authorPages = getAuthorPages "ZWePF1QAAAAJ"
let publicationTable = getPublicationTable authorPages
let kpiTable = getKpiTableBodyFromList authorPages
let overviewCitationTable = getOverviewCitationTableFromList authorPages
let papercitationTables = getPaperCitationTables publicationTable