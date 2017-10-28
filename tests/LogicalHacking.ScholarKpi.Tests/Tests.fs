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

module LogicalHacking.ScholarKpi.Tests
open System
open LogicalHacking.ScholarKpi.Core.Metrics
open NUnit.Framework
open LogicalHacking.ScholarKpi.Core.Configuration
open LogicalHacking.ScholarKpi.Core.Types

let emptyPublicationList = PublicationList("Joe Doe", DateTime.Now, GoogleScholar, [], None, None, None)
let smallPublicationList = PublicationList("Joe Doe", DateTime.Now, GoogleScholar, 
                           [ Publication("id-0", "A Title", Some 2015, None, 15, []);
                             Publication("id-1", "Another Title", Some 2016, None, 10, []);
                             Publication("id-2", "Yet Another Title", Some 2017, None,  1, []) 
                           ], None, None, None)

[<Test>]
let ``i10-index of empty list is 0`` () =
  let result = i10Index emptyPublicationList
  printfn "%i" result
  Assert.AreEqual(0,result)

[<Test>]
let ``i10-index of small example  list is 2`` () =
  let result = i10Index smallPublicationList
  printfn "%i" result
  Assert.AreEqual(2,result)

[<Test>]
let ``h-index of empty list is 0`` () =
  let result = hIndex emptyPublicationList
  printfn "%i" result
  Assert.AreEqual(0,result)

[<Test>]
let ``h-index of small example  list is 2`` () =
  let result = hIndex smallPublicationList
  printfn "%i" result
  Assert.AreEqual(2,result)

