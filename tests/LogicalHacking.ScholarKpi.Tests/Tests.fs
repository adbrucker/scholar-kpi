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

[<Test>]
let ``i10-index of empty list is 0`` () =
  let emptyPublicationList = PublicationList("Joe Doe", DateTime.Now, GoogleScholar, [], None, None, None)
  let result = i10Index emptyPublicationList
  printfn "%i" result
  Assert.AreEqual(0,result)

