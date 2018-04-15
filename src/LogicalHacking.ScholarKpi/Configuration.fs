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
    let [<Literal>] ConfigSchema      = __SOURCE_DIRECTORY__+ @"/../resources/scholar-kpi-schema.yaml"
    let [<Literal>] DefaultConfigFile = __SOURCE_DIRECTORY__+ @"/../resources/scholar-kpi.yaml"
    type ScholarKpiConfig = YamlConfig<ConfigSchema>

    let loadCfg (config:ScholarKpiConfig) = function
                                         | Some (cfg:string) -> config.Load(cfg); config
                                         | None              -> config.Load(DefaultConfigFile); config
                                         
    let mkCfg = loadCfg(ScholarKpiConfig())

    type DataSource = GoogleScholar | Orcid | Dblp | Pubzone | AcmDl | CsNet | SemanticScholar 

    let dataSourceToYamlString = function 
                                 | GoogleScholar   -> "GoogleScholar"
                                 | Orcid           -> "Orcid"
                                 | Dblp            -> "Dblp"
                                 | Pubzone         -> "Pubzone"
                                 | AcmDl           -> "AcmDl"
                                 | CsNet           -> "CsNet"
                                 | SemanticScholar -> "SemanticScholar" 

    type DataSourceCfg = struct
        val Service  : DataSource
        val AuthorId : string
        val FullDownload : bool
        new (service, authorId, fullDownload) = {Service = service; AuthorId = authorId; FullDownload=fullDownload}
    end

    let getDataSourceCfg (cfg:ScholarKpiConfig) (src:DataSource) author = 
         match (Seq.filter (fun (a:ScholarKpiConfig.Authors_Item_Type) -> a.Author.Equals(author)) cfg.Authors) |> Seq.toList with 
           | []      -> None
           | (x::xs) -> match (Seq.filter (fun (s:ScholarKpiConfig.Authors_Item_Type.Sources_Item_Type) -> s.Service = dataSourceToYamlString GoogleScholar)  x.Sources) |> Seq.toList with
                         | []      -> None
                         | (y::ys) -> Some (DataSourceCfg(src, y.AuthorId, y.FullDownload))
