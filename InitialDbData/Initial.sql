/*
DROP TABLE IF EXISTS Tickets;
DROP TABLE IF EXISTS Screenings;
DROP TABLE IF EXISTS Purchases;
DROP TABLE IF EXISTS [Addresses];
DROP TABLE IF EXISTS [Users];
DROP TABLE IF EXISTS Rooms;
DROP TABLE IF EXISTS Films;
DROP TABLE IF EXISTS RoleUser;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Roles;
DROP TABLE IF EXISTS Roles;
*/

----!!!!Run twice
--Admin Email: admin@admin.com
--Admin password: admin123
--Cashier email: cash@ier.com
--Cashier password: cashier
--User password: password

DELETE FROM Tickets;
DELETE FROM Purchases;
DELETE FROM Addresses;
DELETE FROM Screenings;
DELETE FROM Users;
DELETE FROM Films;
DELETE FROM Rooms;
DELETE FROM Roles;
DELETE FROM RoleUser;
DELETE FROM RoomTypes;

DBCC CHECKIDENT ('Tickets', RESEED, 0);
DBCC CHECKIDENT ('Purchases', RESEED, 0);
DBCC CHECKIDENT ('Addresses', RESEED, 0);
DBCC CHECKIDENT ('Screenings', RESEED, 0);
DBCC CHECKIDENT ('Users', RESEED, 0);
DBCC CHECKIDENT ('Films', RESEED, 0);
DBCC CHECKIDENT ('Rooms', RESEED, 0);
DBCC CHECKIDENT ('RoomTypes', RESEED, 0);
DBCC CHECKIDENT ('Roles', RESEED, 0);

INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('Dragon Ball Super: Broly', 'Tatsuya Nagamine', 'Animation', 100, 'The Saiyans are faced with a new threat in the form of Broly, a powerful Saiyan warrior.', 12),
('Dragon Ball Super: Super Hero', 'Tetsuro Kodama', 'Animation', 101, 'The Red Ribbon Army returns with new androids to challenge Goku and his friends.', 12),
('Star Wars: Episode VI - Return of the Jedi', 'Richard Marquand', 'Sci-Fi', 131, 'Luke Skywalker battles Darth Vader while the Rebel Alliance fights to destroy the Death Star.',12),
('Star Wars: Episode III - Revenge of the Sith', 'George Lucas', 'Sci-Fi', 140, 'Anakin Skywalker succumbs to the dark side and becomes Darth Vader.', 12),
('The Terminator', 'James Cameron', 'Sci-Fi', 107, 'A cyborg assassin is sent back in time to kill a woman whose son will lead a rebellion.', 16),
('Terminator 2: Judgment Day', 'James Cameron', 'Sci-Fi', 137, 'A cyborg is sent back in time to protect a young boy from a more advanced cyborg.', 16),
('Aliens', 'James Cameron', 'Sci-Fi', 137, 'Ellen Ripley returns to face the aliens that killed her crew, this time with a team of Marines.', 18),
('Finding Nemo', 'Andrew Stanton', 'Animation', 100, 'A clownfish embarks on a journey to find his son, who has been captured by a diver.', null),
('Toy Story', 'John Lasseter', 'Animation', 81, 'A cowboy doll is threatened by a new spaceman action figure.', null),
('Zootopia', 'Byron Howard', 'Animation', 108, 'A rabbit police officer teams up with a fox to solve a missing mammals case.', null),
('The Conjuring', 'James Wan', 'Horror', 112, 'A paranormal investigator and his wife help a family terrorized by a dark presence.', 18),
('The Conjuring 2', 'James Wan', 'Horror', 134, 'The Warrens travel to England to help a single mother and her children.', 16),
('Annabelle', 'John R. Leonetti', 'Horror', 99, 'A couple is terrorized by a possessed doll.', 18);

INSERT INTO RoomTypes (Name) VALUES
('Cinema'),
('Concert'),
('VIP'),
('Drive-IN');

INSERT INTO Rooms (Name, Description, RoomTypeId, MaxSeatRow, MaxSeatColumn, Capacity, DisabilityFriendly, ComfortLevel, ConstructedAt) VALUES
('Screen 1', 'Main cinema hall with Dolby Atmos', 1, 15, 20, 300, 1, 4, '2015-06-10'),
('Screen 2', 'Standard digital projection', 1, 12, 18, 216, 1, 3, '2015-06-10'),
('Screen 3', '3D capable screen', 1, 10, 15, 150, 0, 3, '2017-03-15'),
('Screen 4', 'Small intimate screening room', 1, 8, 12, 96, 1, 2, '2018-11-20'),
('Screen 5', 'Premium large format screen', 1, 18, 25, 450, 1, 5, '2019-07-01'),
('Screen 6', 'Retro cinema with classic decor', 1, 10, 16, 160, 0, 3, '2020-05-12'),
('Main Hall', 'Large concert venue with excellent acoustics', 2, NULL, NULL, 600, 1, 4, '2016-02-28'),
('Chamber Room', 'Smaller space for intimate performances', 2, NULL, NULL, 120, 1, 3, '2018-09-05'),
('Diamond Lounge', 'Exclusive VIP experience with luxury seating', 3, 5, 8, 40, 1, 5, '2019-12-15'),
('Starlight Drive-In', 'Outdoor movie experience with FM sound', 4, NULL, NULL, 100, 1, 3, '2020-07-20');

INSERT INTO Screenings (FilmId, RoomId, Date) VALUES
(1, 1, '2025-06-15 14:00:00'),
(1, 3, '2025-07-22 18:30:00'),
(1, 5, '2025-08-10 12:00:00'),

(2, 2, '2025-06-20 16:00:00'),
(2, 4, '2025-07-05 20:00:00'),
(2, 6, '2025-08-18 14:30:00'),
(2, 8, '2025-06-25 11:00:00'),

(3, 1, '2025-07-01 19:00:00'),
(3, 3, '2025-08-05 13:00:00'),
(3, 5, '2025-06-30 17:00:00'),

(4, 2, '2025-07-12 15:00:00'),
(4, 4, '2025-08-20 19:00:00'),
(4, 6, '2025-06-28 10:00:00'),
(4, 8, '2025-07-18 16:00:00'),

(5, 1, '2025-08-01 18:00:00'),
(5, 3, '2025-06-10 14:00:00'),
(5, 5, '2025-07-25 20:00:00'),

(6, 2, '2025-08-15 12:00:00'),
(6, 4, '2025-06-22 16:00:00'),
(6, 6, '2025-07-30 18:30:00'),
(6, 8, '2025-08-05 13:00:00'),

(7, 1, '2025-06-05 10:00:00'),
(7, 3, '2025-07-15 15:00:00'),
(7, 5, '2025-08-25 19:00:00'),

(8, 2, '2025-07-08 14:00:00'),
(8, 4, '2025-08-12 18:00:00'),
(8, 6, '2025-06-18 12:30:00'),
(8, 8, '2025-07-28 17:00:00'),

(9, 1, '2025-08-03 16:00:00'),
(9, 3, '2025-06-14 20:00:00'),
(9, 5, '2025-07-20 14:00:00'),

(10, 2, '2025-06-08 11:00:00'),
(10, 4, '2025-07-10 13:00:00'),
(10, 6, '2025-08-22 17:30:00'),
(10, 8, '2025-06-30 10:00:00'),

(11, 1, '2025-07-05 13:00:00'),
(11, 3, '2025-08-15 17:00:00'),
(11, 5, '2025-06-25 21:00:00'),

(12, 2, '2025-08-10 15:00:00'),
(12, 4, '2025-06-12 19:00:00'),
(12, 6, '2025-07-22 11:30:00'),
(12, 8, '2025-08-28 14:00:00'),

(13, 1, '2025-06-18 12:00:00'),
(13, 3, '2025-07-28 16:00:00'),
(13, 5, '2025-08-08 20:00:00');

INSERT INTO Users (Name, Email, PasswordHash, AddressId, BirthDate) VALUES
('Admin admin', 'admin@admin.com', '$2a$11$EeroP1p1rS.dvBoXZ7WcsOiCf9lZMOa05YCj6F8MSFmH0TPSze626', 1, '2003-04-08'),
('TicketMaster Guardian', 'cash@ier.com', '$2a$11$JZ3Fy4kRWxOSkN1UEqRcx.yqWO3BEGkTvaUG1xct.Ay3fF5l2.FGy', 2, '2000-02-02'),
('Tóth Balázs', 'toth.balazs@example.com', '$2a$11$osj1JYqLF9Z5T/hAx1zZ1O8AHPJdeBMTSRKnpkGXzYvYsCxqcRWTa', 3, '1992-03-03'),
('Szabó Zsófia', 'szabo.zsofia@example.com', '$2a$11$osj1JYqLF9Z5T/hAx1zZ1O8AHPJdeBMTSRKnpkGXzYvYsCxqcRWTa', 4, '1993-04-04'),
('Horváth Dávid', 'horvath.david@example.com', '$2a$11$osj1JYqLF9Z5T/hAx1zZ1O8AHPJdeBMTSRKnpkGXzYvYsCxqcRWTa', 5, '1994-05-05'),
('Varga Réka', 'varga.reka@example.com', '$2a$11$osj1JYqLF9Z5T/hAx1zZ1O8AHPJdeBMTSRKnpkGXzYvYsCxqcRWTa', 6, '1995-06-06'),
('Molnár Gábor', 'molnar.gabor@example.com', '$2a$11$osj1JYqLF9Z5T/hAx1zZ1O8AHPJdeBMTSRKnpkGXzYvYsCxqcRWTa', 7, '1996-07-07'),
('Farkas Anna', 'farkas.anna@example.com', '$2a$11$osj1JYqLF9Z5T/hAx1zZ1O8AHPJdeBMTSRKnpkGXzYvYsCxqcRWTa', 8, '1997-08-08'),
('Takács Péter', 'takacs.peter@example.com', '$2a$11$osj1JYqLF9Z5T/hAx1zZ1O8AHPJdeBMTSRKnpkGXzYvYsCxqcRWTa', 9, '1998-09-09'),
('Kiss Judit', 'kiss.judit@example.com', '$2a$11$osj1JYqLF9Z5T/hAx1zZ1O8AHPJdeBMTSRKnpkGXzYvYsCxqcRWTa', 10, '1999-10-10');

INSERT INTO Addresses (Country, County, ZipCode, City, Street, Floor, HouseNumber, UserId) VALUES
('Hungary', 'Budapest', 1011, 'Budapest', 'Kossuth Lajos utca', NULL, 10, 1),
('Hungary', 'Budapest', 1012, 'Budapest', 'Széchenyi utca', 2, 5, 2),
('Hungary', 'Budapest', 1013, 'Budapest', 'Andrássy út', NULL, 20, 3),
('Hungary', 'Budapest', 1014, 'Budapest', 'Bajcsy-Zsilinszky út', 3, 15, 4),
('Hungary', 'Pest', 2000, 'Szentendre', 'Fő tér', NULL, 3, 5),
('Hungary', 'Győr-Moson-Sopron', 9021, 'Győr', 'Baross Gábor út', 2, 7, 6),
('Hungary', 'Bács-Kiskun', 6000, 'Kecskemét', 'Kossuth tér', NULL, 10, 7),
('Hungary', 'Veszprém', 8200, 'Veszprém', 'Óvári Ferenc utca', 4, 12, 8),
('Hungary', 'Hajdú-Bihar', 4025, 'Debrecen', 'Piac utca', 5, 9, 9),
('Austria', 'Vienna', 1010, 'Vienna', 'Stephansplatz', NULL, 5, 10);

INSERT INTO Roles (Name) VALUES
('Admin'),
('Cashier'),
('Customer');

INSERT INTO RoleUser (UsersId, RolesId) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 3),
(5, 3),
(6, 3),
(7, 3),
(8, 3),
(9, 3),
(10, 3);

INSERT INTO Purchases (UserId, PurchaseDate) VALUES (3, '2025-06-15 14:00:00');
INSERT INTO Tickets (ScreeningId, PurchaseId, SeatRow, SeatColumn, Price) VALUES (1, 1, 1, 1, 7.00);
INSERT INTO Tickets (ScreeningId, PurchaseId, SeatRow, SeatColumn, Price) VALUES (1, 1, 1, 2, 7.00);

INSERT INTO Purchases (UserId, PurchaseDate) VALUES (4, '2025-07-22 18:30:00');
INSERT INTO Tickets (ScreeningId, PurchaseId, SeatRow, SeatColumn, Price) VALUES (2, 2, 2, 3, 7.00);
INSERT INTO Tickets (ScreeningId, PurchaseId, SeatRow, SeatColumn, Price) VALUES (2, 2, 2, 4, 7.00);

INSERT INTO Purchases (UserId, PurchaseDate) VALUES (5, '2025-08-10 12:00:00');
INSERT INTO Tickets (ScreeningId, PurchaseId, SeatRow, SeatColumn, Price) VALUES (3, 3, 3, 5, 7.00);
INSERT INTO Tickets (ScreeningId, PurchaseId, SeatRow, SeatColumn, Price) VALUES (3, 3, 3, 6, 7.00);

INSERT INTO Purchases (UserId, PurchaseDate) VALUES (6, '2025-06-20 16:00:00');
INSERT INTO Tickets (ScreeningId, PurchaseId, SeatRow, SeatColumn, Price) VALUES (4, 4, 4, 7, 7.00);
INSERT INTO Tickets (ScreeningId, PurchaseId, SeatRow, SeatColumn, Price) VALUES (4, 4, 4, 8, 7.00);

INSERT INTO Purchases (UserId, PurchaseDate) VALUES (7, '2025-07-05 20:00:00');
INSERT INTO Tickets (ScreeningId, PurchaseId, SeatRow, SeatColumn, Price) VALUES (5, 5, 5, 9, 7.00);
INSERT INTO Tickets (ScreeningId, PurchaseId, SeatRow, SeatColumn, Price) VALUES (5, 5, 5, 10, 7.00);