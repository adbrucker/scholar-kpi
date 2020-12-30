(*
 * This file is part of the scholar-kpi project.
 * Copyright (c) 2018 Achim D. Brucker, https://www.brucker.ch
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

namespace LogicalHacking.ScholarKpi

open CommandLine
open System

module console1 =

    [<EntryPoint>]
    let main argv = 
        printfn "%A" argv

        let parsedCommand = parse (System.Reflection.Assembly.GetExecutingAssembly().GetName().Name) argv

        match parsedCommand.Error with
            | Some e -> 
                printfn "%s" parsedCommand.Usage
            | None -> 
                printfn "%A" parsedCommand

        printfn "Hit any key to exit."
        System.Console.ReadKey() |> ignore
        0
