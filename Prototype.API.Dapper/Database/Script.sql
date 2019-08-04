
/*******************************************************************************
   Chinook Database - Version 1.4
   Script: Chinook_SqlServer.sql
   Description: Creates and populates the Chinook database.
   DB Server: SqlServer
   Author: Luis Rocha
   License: http://www.codeplex.com/ChinookDatabase/license
********************************************************************************/
USE master
GO

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'ChinookReduced')
BEGIN
	DROP DATABASE ChinookReduced;
END

GO

/*******************************************************************************
   Create database
********************************************************************************/
CREATE DATABASE ChinookReduced;
GO

USE ChinookReduced;
GO

/*******************************************************************************
   Create Tables
********************************************************************************/
CREATE TABLE [dbo].[Albums]
(
    [AlbumId] INT NOT NULL IDENTITY (1,1),
    [Title] NVARCHAR(160) NOT NULL,
    [ArtistId] INT NOT NULL,
    CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED ([AlbumId])
);
GO
CREATE TABLE [dbo].[Artists]
(
    [ArtistId] INT NOT NULL IDENTITY (1,1),
    [Name] NVARCHAR(120),
    CONSTRAINT [PK_Artist] PRIMARY KEY CLUSTERED ([ArtistId])
);
GO



/*******************************************************************************
   Create Primary Key Unique Indexes
********************************************************************************/

/*******************************************************************************
   Create Foreign Keys
********************************************************************************/
ALTER TABLE [dbo].[Albums] ADD CONSTRAINT [FK_AlbumArtistId]
    FOREIGN KEY ([ArtistId]) REFERENCES [dbo].[Artists] ([ArtistId]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX [IFK_AlbumArtistId] ON [dbo].[Albums] ([ArtistId]);
GO

/*******************************************************************************
   Populate Tables
********************************************************************************/

INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'AC/DC');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Accept');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Aerosmith');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Alanis Morissette');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Alice In Chains');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Antônio Carlos Jobim');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Apocalyptica');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Audioslave');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'BackBeat');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Billy Cobham');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Black Label Society');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Black Sabbath');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Body Count');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Bruce Dickinson');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Buddy Guy');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Caetano Veloso');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Chico Buarque');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Chico Science & Nação Zumbi');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Cidade Negra');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Cláudio Zoli');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Various Artists');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Led Zeppelin');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Frank Zappa & Captain Beefheart');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Marcos Valle');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Milton Nascimento & Bebeto');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Azymuth');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Gilberto Gil');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'João Gilberto');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Bebel Gilberto');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Jorge Vercilo');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Baby Consuelo');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Ney Matogrosso');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Luiz Melodia');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Nando Reis');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Pedro Luís & A Parede');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'O Rappa');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Ed Motta');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Banda Black Rio');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Fernanda Porto');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Os Cariocas');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Elis Regina');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Milton Nascimento');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'A Cor Do Som');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Kid Abelha');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Sandra De Sá');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Jorge Ben');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Hermeto Pascoal');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Barão Vermelho');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Edson, DJ Marky & DJ Patife Featuring Fernanda Porto');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Metallica');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Queen');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Kiss');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Spyro Gyra');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Green Day');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'David Coverdale');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Gonzaguinha');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Os Mutantes');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Deep Purple');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Santana');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Santana Feat. Dave Matthews');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Santana Feat. Everlast');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Santana Feat. Rob Thomas');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Santana Feat. Lauryn Hill & Cee-Lo');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Santana Feat. The Project G&B');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Santana Feat. Maná');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Santana Feat. Eagle-Eye Cherry');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Santana Feat. Eric Clapton');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Miles Davis');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Gene Krupa');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Toquinho & Vinícius');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Vinícius De Moraes & Baden Powell');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Vinícius De Moraes');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Vinícius E Qurteto Em Cy');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Vinícius E Odette Lara');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Vinicius, Toquinho & Quarteto Em Cy');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Creedence Clearwater Revival');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Cássia Eller');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Def Leppard');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Dennis Chambers');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Djavan');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Eric Clapton');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Faith No More');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Falamansa');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Foo Fighters');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Frank Sinatra');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Funk Como Le Gusta');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Godsmack');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Guns N'' Roses');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Incognito');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Iron Maiden');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'James Brown');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Jamiroquai');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'JET');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Jimi Hendrix');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Joe Satriani');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Jota Quest');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'João Suplicy');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Judas Priest');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Legião Urbana');
INSERT INTO [dbo].[Artists] ([Name]) VALUES (N'Lenny Kravitz');


INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'For Those About To Rock We Salute You', 1);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Balls to the Wall', 2);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Restless and Wild', 2);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Let There Be Rock', 1);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Big Ones', 3);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Jagged Little Pill', 4);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Facelift', 5);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Warner 25 Anos', 6);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Plays Metallica By Four Cellos', 7);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Audioslave', 8);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Out Of Exile', 8);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'BackBeat Soundtrack', 9);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'The Best Of Billy Cobham', 10);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Alcohol Fueled Brewtality Live! [Disc 1]', 11);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Alcohol Fueled Brewtality Live! [Disc 2]', 11);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Black Sabbath', 12);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Black Sabbath Vol. 4 (Remaster)', 12);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Body Count', 13);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Chemical Wedding', 14);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'The Best Of Buddy Guy - The Millenium Collection', 15);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Prenda Minha', 16);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Sozinho Remix Ao Vivo', 16);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Minha Historia', 17);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Afrociberdelia', 18);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Da Lama Ao Caos', 18);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Acústico MTV [Live]', 19);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Cidade Negra - Hits', 19);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Na Pista', 20);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Axé Bahia 2001', 21);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'BBC Sessions [Disc 1] [Live]', 22);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Bongo Fury', 23);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Carnaval 2001', 21);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Chill: Brazil (Disc 1)', 24);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Chill: Brazil (Disc 2)', 6);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Garage Inc. (Disc 1)', 50);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Greatest Hits II', 51);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Greatest Kiss', 52);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Heart of the Night', 53);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'International Superhits', 54);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Into The Light', 55);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Meus Momentos', 56);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Minha História', 57);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'MK III The Final Concerts [Disc 1]', 58);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Physical Graffiti [Disc 1]', 22);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Sambas De Enredo 2001', 21);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Supernatural', 59);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'The Best of Ed Motta', 37);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'The Essential Miles Davis [Disc 1]', 68);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'The Essential Miles Davis [Disc 2]', 68);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'The Final Concerts (Disc 2)', 58);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Up An'' Atom', 69);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Vinícius De Moraes - Sem Limite', 70);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Vozes do MPB', 21);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Chronicle, Vol. 1', 76);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Chronicle, Vol. 2', 76);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Cássia Eller - Coleção Sem Limite [Disc 2]', 77);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Cássia Eller - Sem Limite [Disc 1]', 77);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Come Taste The Band', 58);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Deep Purple In Rock', 58);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Fireball', 58);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Knocking at Your Back Door: The Best Of Deep Purple in the 80''s', 58);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Machine Head', 58);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Purpendicular', 58);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Slaves And Masters', 58);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Stormbringer', 58);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'The Battle Rages On', 58);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Vault: Def Leppard''s Greatest Hits', 78);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Outbreak', 79);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Djavan Ao Vivo - Vol. 02', 80);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Djavan Ao Vivo - Vol. 1', 80);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Elis Regina-Minha História', 41);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'The Cream Of Clapton', 81);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Unplugged', 81);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Album Of The Year', 82);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Angel Dust', 82);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'King For A Day Fool For A Lifetime', 82);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'The Real Thing', 82);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Deixa Entrar', 83);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'In Your Honor [Disc 1]', 84);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'In Your Honor [Disc 2]', 84);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'One By One', 84);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'The Colour And The Shape', 84);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'My Way: The Best Of Frank Sinatra [Disc 1]', 85);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Roda De Funk', 86);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'As Canções de Eu Tu Eles', 27);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Quanta Gente Veio Ver (Live)', 27);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Quanta Gente Veio ver--Bônus De Carnaval', 27);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Faceless', 87);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'American Idiot', 54);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Appetite for Destruction', 88);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Use Your Illusion I', 88);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Use Your Illusion II', 88);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Blue Moods', 89);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'A Matter of Life and Death', 90);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'A Real Dead One', 90);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'A Real Live One', 90);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Brave New World', 90);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Dance Of Death', 90);
INSERT INTO [dbo].[Albums] ([Title], [ArtistId]) VALUES (N'Fear Of The Dark', 90);
