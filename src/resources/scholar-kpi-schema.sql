--
-- This file is part of the scholar-kpi project.
-- Copyright (c) 2018 Achim D. Brucker, https://www.brucker.ch
--
-- This program is free software: you can redistribute it and/or modify
-- it under the terms of the GNU General Public License as published by
-- the Free Software Foundation, version 3.
--
-- This program is distributed in the hope that it will be useful, but
-- WITHOUT ANY WARRANTY; without even the implied warranty of
-- MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
-- General Public License for more details.
--
-- You should have received a copy of the GNU General Public License
-- along with this program. If not, see <https://www.gnu.org/licenses/>.
--
-- SPDX-License-Identifier: GPL-3.0-or-later
--

CREATE TABLE IF NOT EXISTS ScholarKpi (
      SchemaVersion INTEGER NOT NULL
);

INSERT INTO ScholarKpi VALUES (0);

CREATE TABLE IF NOT EXISTS PublicationLists (
      PublicationListId INTEGER PRIMARY KEY
    , AuthorID TEXT  NOT NULL
    , DateTime TEXT NOT NULL
    , DataSource TEXT NOT NULL 
);

CREATE TABLE IF NOT EXISTS Publications (
      PublicationId INTEGER PRIMARY KEY
    , PublicationListId INTEGER NOT NULL  
    , Title STRING NOT NULL
    , Year INTEGER
    , CitationKey TEXT NOT NULL
    , Citations INTEGER NOT NULL
    , FOREIGN KEY(PublicationListId) REFERENCES PublicationLists(PublicationListId)
);


CREATE TABLE IF NOT EXISTS CitationHistories (
      PublicationId INTEGER NOT NULL
    , Year INTEGER NOT NULL
    , Citations INTEGER NOT NULL
    , FOREIGN KEY(PublicationId) REFERENCES Publications(PublicationId)
);

CREATE TABLE IF NOT EXISTS BibMetrics (
      PublicationListId INTEGER NOT NULL  
    , I10Index INTEGER NOT NULL
    , Citations INTEGER NOT NULL
    , FOREIGN KEY(PublicationListId) REFERENCES PublicationLists(PublicationListId)
);

CREATE TABLE IF NOT EXISTS RecentBibMetrics (
      PublicationId INTEGER NOT NULL
    , I10Index INTEGER NOT NULL
    , Citations INTEGER NOT NULL
    , Year INTEGER NOT NULL
    , FOREIGN KEY(PublicationId) REFERENCES Publications(PublicationId)
);
