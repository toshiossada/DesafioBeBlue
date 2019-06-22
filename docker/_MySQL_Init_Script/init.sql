CREATE DATABASE  IF NOT EXISTS BeBlue;

USE BeBlue

DROP TABLE IF EXISTS saleItems;
DROP TABLE IF EXISTS sales;
DROP TABLE IF EXISTS tracks;
DROP TABLE IF EXISTS cashback;
DROP TABLE IF EXISTS genres;


CREATE TABLE genres (
  id int NOT NULL AUTO_INCREMENT,
  name VARCHAR(255) NOT NULL,
  PRIMARY KEY (id)
);

INSERT INTO genres(name) VALUES('pop');
INSERT INTO genres(name) VALUES('mpb');
INSERT INTO genres(name) VALUES('classical');
INSERT INTO genres(name) VALUES('rock');

CREATE TABLE dayOfWeek(
  id int NOT NULL,
  name VARCHAR(50),
  PRIMARY KEY (id)
);

INSERT INTO dayOfWeek(id, name) VALUES(0, 'Domingo');
INSERT INTO dayOfWeek(id, name) VALUES(1, 'Segunda');
INSERT INTO dayOfWeek(id, name) VALUES(2, 'Terça');
INSERT INTO dayOfWeek(id, name) VALUES(3, 'Quarta');
INSERT INTO dayOfWeek(id, name) VALUES(4, 'Quinta');
INSERT INTO dayOfWeek(id, name) VALUES(5, 'Sábado');

CREATE TABLE cashback (
  id int NOT NULL AUTO_INCREMENT,
  idGenre INT NOT NULL,
  idDayOfWeek INT NOT NULL,
  percent decimal(19,2),
  PRIMARY KEY (id)
);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'pop' LIMIT 1), 0, 25);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'pop' LIMIT 1), 1, 7);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'pop' LIMIT 1), 2, 6);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'pop' LIMIT 1), 3, 2);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'pop' LIMIT 1), 4, 10);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'pop' LIMIT 1), 5, 15);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'pop' LIMIT 1), 6, 20);

INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'mpb' LIMIT 1), 0, 30);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'mpb' LIMIT 1), 1, 5);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'mpb' LIMIT 1), 2, 10);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'mpb' LIMIT 1), 3, 15);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'mpb' LIMIT 1), 4, 20);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'mpb' LIMIT 1), 5, 25);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'mpb' LIMIT 1), 6, 30);

INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'classical' LIMIT 1), 0, 35);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'classical' LIMIT 1), 1, 3);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'classical' LIMIT 1), 2, 5);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'classical' LIMIT 1), 3, 8);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'classical' LIMIT 1), 4, 13);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'classical' LIMIT 1), 5, 19);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'classical' LIMIT 1), 6, 25);

INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'rock' LIMIT 1), 0, 40);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'rock' LIMIT 1), 1, 10);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'rock' LIMIT 1), 2, 15);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'rock' LIMIT 1), 3, 15);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'rock' LIMIT 1), 4, 15);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'rock' LIMIT 1), 5, 20);
INSERT INTO cashback(idGenre, idDayOfWeek, percent) VALUES ((select id from genres where name = 'rock' LIMIT 1), 6, 40);


CREATE TABLE tracks (
  id int NOT NULL AUTO_INCREMENT,
  idGenre INT NOT NULL,
  name VARCHAR(255),
  nameOfArtist VARCHAR(255),
  price decimal(19,2),
  PRIMARY KEY (id)
);

CREATE TABLE sales(
	id INT NOT NULL AUTO_INCREMENT,
	dateSale DATETIME NOT NULL DEFAULT '1901-01-01 00:00:00',
	total decimal(19,2),
	totalCashback decimal(19,2),
	PRIMARY KEY (id)
);

CREATE TABLE saleItems(
	id INT NOT NULL AUTO_INCREMENT,
	idSale INT NOT NULL,
	idTrack INT NOT NULL,
  idDayOfWeek INT NOT NULL,
	cashback decimal(19,2), 
	PRIMARY KEY (id)
);