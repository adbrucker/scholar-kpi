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

open System

/// Command line parsing.
module CommandLine = 
    
    type PredicateOperator =
    | EQ
    | GT
    | LT
    | Between

    type Predicate =
        {
            Operator : PredicateOperator
            StartDate : DateTime
            EndDate : DateTime option
        }
        with
            static member Create : operator : PredicateOperator -> startDate : DateTime -> endDate : DateTime option -> Predicate

    type Source =
        | File of string list
        | Console of string
        | NoSource

    type Target =
        | File of string
        | Console

    type ParsedCommand =
        {
        Usage : string
        Predicate : Predicate option
        Source : Source 
        Target : Target
        Error: Exception option
        }

    val parse : programName : string -> argv : string [] -> ParsedCommand
