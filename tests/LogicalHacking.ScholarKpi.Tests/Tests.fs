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

namespace LogicalHacking.ScholarKpi.Tests

open System

open LogicalHacking.ScholarKpi.Core.Metrics
open LogicalHacking.ScholarKpi.Core.Configuration
open LogicalHacking.ScholarKpi.Core.Types

open Expecto
open FsCheck
(* open GeneratorsCode *)

module Tests = 
    let emptyPublicationList = PublicationList("Joe Doe", DateTime.Now, GoogleScholar, [], None, None, None)
    let smallPublicationList = PublicationList("Joe Doe", DateTime.Now, GoogleScholar, 
                               [ Publication("id-0", "A Title", Some 2015, None, 15, []);
                                 Publication("id-1", "Another Title", Some 2016, None, 10, []);
                                 Publication("id-2", "Yet Another Title", Some 2017, None,  1, []) 
                               ], None, None, None)

    let config10k = { FsCheckConfig.defaultConfig with maxTest = 10000}
    // bug somewhere:  registering arbitrary generators causes Expecto VS test adapter not to work
    //let config10k = { FsCheckConfig.defaultConfig with maxTest = 10000; arbitrary = [typeof<Generators>] }
    let configReplay = { FsCheckConfig.defaultConfig with maxTest = 10000 ; replay = Some <| (1940624926, 296296394) }

    [<Tests>]
    let testSimpleTests =

        testList "DomainTypes.Tag" [
            testCase "equality" <| fun () ->
                let result = 42
                Expect.isTrue (result = 42) "Expected True"

            testCase "Total number of publications of empty list is 0" <| fun () -> 
                let result = totalPublications emptyPublicationList
                Expect.isTrue (result = 0) "Expected True"

            testCase "Total number of publications of small example  list is 3`" <| fun () ->
                let result = totalPublications smallPublicationList
                Expect.isTrue (result = 3) "Expected True"

            testCase "i10-index of empty list is 0" <| fun () ->
                let result = i10Index emptyPublicationList
                Expect.isTrue (result = 0) "Expected True"

            testCase "i10-index of small example  list is 2" <| fun () ->
                let result = i10Index smallPublicationList
                Expect.isTrue (result = 2) "Expected True"

            testCase "h-index of empty list is 0" <| fun () ->
                let result = hIndex emptyPublicationList
                Expect.isTrue (result = 0) "Expected True"
            testCase "h-index of small example  list is 2" <| fun () ->
                let result = hIndex smallPublicationList
                Expect.isTrue (result = 2) "Expected True"
        ]
