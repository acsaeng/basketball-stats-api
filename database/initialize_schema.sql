CREATE DATABASE IF NOT EXISTS basketball_stats;

USE basketball_stats;

CREATE TABLE IF NOT EXISTS team (
  team_id         INT NOT NULL AUTO_INCREMENT,
  location_name   VARCHAR(255) NOT NULL,
  team_name       VARCHAR(255) NOT NULL,
  locale_full     VARCHAR(255) NOT NULL,
  locale_abbr     VARCHAR(3) NOT NULL,
  stadium_name    VARCHAR(255) NOT NULL,
  wins            INT NOT NULL,
  losses          INT NOT NULL,
  PRIMARY KEY (team_id)
  UNIQUE (team_name, locale_abbr)
);

CREATE TABLE IF NOT EXISTS player (
  player_id       INT NOT NULL AUTO_INCREMENT,
  team_id         INT NOT NULL,
  first_name      VARCHAR(255) NOT NULL,
  last_name       VARCHAR(255) NOT NULL,
  dob             DATE NOT NULL,
  player_height   INT NOT NULL, 
  player_weight   INT NOT NULL,
  position        VARCHAR(255) NOT NULL,
  jersey_number   INT NOT NULL,
  roster_status   VARCHAR(255) NOT NULL,
  PRIMARY KEY (player_id),
  FOREIGN KEY (team_id) REFERENCES team (team_id) 
);

CREATE TABLE IF NOT EXISTS game (
  game_id       INT NOT NULL AUTO_INCREMENT,
  date_time     DATETIME NOT NULL,
  game_status   VARCHAR(255) NOT NULL,
  PRIMARY KEY (game_id)
);

CREATE TABLE IF NOT EXISTS plays_in (
  player_id   INT NOT NULL,
  game_id     INT NOT NULL,
  points      INT,
  assists     INT,
  rebounds    INT,
  steals      INT,
  blocks      INT,
  turnovers   INT,
  FOREIGN KEY (player_id) REFERENCES player (player_id),
  FOREIGN KEY (game_id) REFERENCES game (game_id) 
);

CREATE TABLE IF NOT EXISTS competes_in (
  team_id   INT NOT NULL,
  game_id   INT NOT NULL,
  is_home   BOOLEAN,
  did_win   BOOLEAN,
  points    INT,
  PRIMARY KEY (team_id, game_id),
  FOREIGN KEY (team_id) REFERENCES team (team_id),
  FOREIGN KEY (game_id) REFERENCES game (game_id) 
);