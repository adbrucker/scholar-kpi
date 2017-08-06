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

namespace LogicalHacking.ScholarKpi.Core
open FSharp.Configuration

module Configuration = 
    let [<Literal>] DefaultConfig = "../../archive/conf/ScholarKpi.yaml"
    type Config = YamlConfig<DefaultConfig>
    let cfg = Config()
 