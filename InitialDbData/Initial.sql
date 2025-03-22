/*
DROP TABLE IF EXISTS Tickets;
DROP TABLE IF EXISTS Screenings;
DROP TABLE IF EXISTS Purchases;
DROP TABLE IF EXISTS [Addresses];
DROP TABLE IF EXISTS [Users];
DROP TABLE IF EXISTS Rooms;
DROP TABLE IF EXISTS Films;
*/

DELETE FROM Tickets;
DELETE FROM Purchases;
DELETE FROM Addresses;
DELETE FROM Screenings;
DELETE FROM Users;
DELETE FROM Films;
DELETE FROM Rooms;

DBCC CHECKIDENT ('Tickets', RESEED, 0);
DBCC CHECKIDENT ('Purchases', RESEED, 0);
DBCC CHECKIDENT ('Addresses', RESEED, 0);
DBCC CHECKIDENT ('Screenings', RESEED, 0);
DBCC CHECKIDENT ('Users', RESEED, 0);
DBCC CHECKIDENT ('Films', RESEED, 0);
DBCC CHECKIDENT ('Rooms', RESEED, 0);

INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('Dragon Ball Super: Broly', 'Tatsuya Nagamine', 'Animation', 100, 'The Saiyans are faced with a new threat in the form of Broly, a powerful Saiyan warrior.', 12),
('Dragon Ball Super: Super Hero', 'Tetsuro Kodama', 'Animation', 101, 'The Red Ribbon Army returns with new androids to challenge Goku and his friends.', 12),
('Inception', 'Christopher Nolan', 'Sci-Fi', 148, 'A thief who steals corporate secrets through the use of dream-sharing technology.', 12),
('The Dark Knight', 'Christopher Nolan', 'Action', 152, 'Batman faces the Joker, a criminal mastermind who seeks to plunge Gotham City into anarchy.', 12),
('Interstellar', 'Christopher Nolan', 'Sci-Fi', 169, 'A team of explorers travel through a wormhole in space in an attempt to ensure humanity''s survival.', 12),
('The Matrix', 'Lana Wachowski', 'Sci-Fi', 136, 'A computer hacker learns about the true nature of his reality and his role in the war against its controllers.', 16),
('The Lord of the Rings: The Fellowship of the Ring', 'Peter Jackson', 'Fantasy', 178, 'A young hobbit, Frodo, embarks on a quest to destroy a powerful ring and save Middle-earth.', 12),
('The Lord of the Rings: The Two Towers', 'Peter Jackson', 'Fantasy', 179, 'Frodo and Sam continue their journey to Mordor, while the rest of the fellowship fights Saruman''s forces.', 12),
('The Lord of the Rings: The Return of the King', 'Peter Jackson', 'Fantasy', 201, 'Frodo and Sam reach Mordor, while the rest of the fellowship battles Sauron''s forces.', 12),
('The Hobbit: An Unexpected Journey', 'Peter Jackson', 'Fantasy', 169, 'Bilbo Baggins joins a group of dwarves on a quest to reclaim their mountain home from a dragon.', 12);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('The Hobbit: The Desolation of Smaug', 'Peter Jackson', 'Fantasy', 161, 'Bilbo and the dwarves continue their journey to the Lonely Mountain, facing new dangers.', 12),
('The Hobbit: The Battle of the Five Armies', 'Peter Jackson', 'Fantasy', 144, 'The dwarves, elves, and men must unite to face the growing threat of Sauron''s forces.', 12),
('Star Wars: Episode IV - A New Hope', 'George Lucas', 'Sci-Fi', 121, 'Luke Skywalker joins forces with a Jedi Knight, a pilot, and two droids to save the galaxy.', 12),
('Star Wars: Episode V - The Empire Strikes Back', 'Irvin Kershner', 'Sci-Fi', 124, 'Luke trains with Yoda while his friends are pursued by Darth Vader and the Empire.', 12),
('Star Wars: Episode VI - Return of the Jedi', 'Richard Marquand', 'Sci-Fi', 131, 'Luke Skywalker battles Darth Vader while the Rebel Alliance fights to destroy the Death Star.', 12),
('Star Wars: Episode I - The Phantom Menace', 'George Lucas', 'Sci-Fi', 136, 'Two Jedi Knights discover a young boy who may bring balance to the Force.', 12),
('Star Wars: Episode II - Attack of the Clones', 'George Lucas', 'Sci-Fi', 142, 'Anakin Skywalker begins his journey to the dark side while the galaxy edges closer to war.', 12),
('Star Wars: Episode III - Revenge of the Sith', 'George Lucas', 'Sci-Fi', 140, 'Anakin Skywalker succumbs to the dark side and becomes Darth Vader.', 12),
('Jurassic Park', 'Steven Spielberg', 'Adventure', 127, 'A billionaire creates a theme park with genetically engineered dinosaurs.', 12),
('The Lost World: Jurassic Park', 'Steven Spielberg', 'Adventure', 129, 'A research team is sent to investigate a second island filled with dinosaurs.', 12);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('Jurassic World', 'Colin Trevorrow', 'Adventure', 124, 'A new theme park with genetically modified dinosaurs faces disaster.', 12),
('Jurassic World: Fallen Kingdom', 'J.A. Bayona', 'Adventure', 128, 'The dinosaurs face extinction from a volcanic eruption, and a rescue mission is launched.', 12),
('Avatar', 'James Cameron', 'Sci-Fi', 162, 'A paraplegic Marine is sent to the alien world of Pandora on a unique mission.', 12),
('Titanic', 'James Cameron', 'Romance', 194, 'A young woman falls in love with a poor artist aboard the ill-fated RMS Titanic.', 12),
('The Terminator', 'James Cameron', 'Sci-Fi', 107, 'A cyborg assassin is sent back in time to kill a woman whose son will lead a rebellion.', 16),
('Terminator 2: Judgment Day', 'James Cameron', 'Sci-Fi', 137, 'A cyborg is sent back in time to protect a young boy from a more advanced cyborg.', 16),
('Aliens', 'James Cameron', 'Sci-Fi', 137, 'Ellen Ripley returns to face the aliens that killed her crew, this time with a team of Marines.', 18),
('The Avengers', 'Joss Whedon', 'Action', 143, 'Earth''s mightiest heroes must come together to stop Loki and his alien army.', 12),
('Avengers: Age of Ultron', 'Joss Whedon', 'Action', 141, 'The Avengers face a new threat in the form of Ultron, a sentient AI bent on human extinction.', 12),
('Avengers: Infinity War', 'Anthony Russo', 'Action', 149, 'The Avengers and their allies must stop Thanos from collecting all the Infinity Stones.', 12);

INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('Avengers: Endgame', 'Anthony Russo', 'Action', 181, 'The Avengers must undo Thanos'' actions and restore balance to the universe.', 12),
('Iron Man', 'Jon Favreau', 'Action', 126, 'Tony Stark builds a high-tech suit of armor to escape captivity and fight evil.', 12),
('Iron Man 2', 'Jon Favreau', 'Action', 124, 'Tony Stark faces new challenges as he deals with government pressure and a new enemy.', 12),
('Iron Man 3', 'Shane Black', 'Action', 130, 'Tony Stark faces a new enemy, the Mandarin, who threatens his world.', 12),
('Captain America: The First Avenger', 'Joe Johnston', 'Action', 124, 'Steve Rogers becomes Captain America and fights to stop the Red Skull.', 12),
('Captain America: The Winter Soldier', 'Anthony Russo', 'Action', 136, 'Captain America teams up with Black Widow and Falcon to uncover a conspiracy.', 12),
('Captain America: Civil War', 'Anthony Russo', 'Action', 147, 'The Avengers are divided over government oversight, leading to a conflict.', 12),
('Thor', 'Kenneth Branagh', 'Action', 115, 'Thor is cast out of Asgard and must learn humility to reclaim his powers.', 12),
('Thor: The Dark World', 'Alan Taylor', 'Action', 112, 'Thor must stop the Dark Elves from plunging the universe into darkness.', 12),
('Thor: Ragnarok', 'Taika Waititi', 'Action', 130, 'Thor must escape captivity and prevent the destruction of Asgard.', 12);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('Guardians of the Galaxy', 'James Gunn', 'Action', 121, 'A group of intergalactic misfits must stop a powerful artifact from falling into the wrong hands.', 12),
('Guardians of the Galaxy Vol. 2', 'James Gunn', 'Action', 136, 'The Guardians face new challenges as they uncover the mystery of Peter Quill''s father.', 12),
('Black Panther', 'Ryan Coogler', 'Action', 134, 'T''Challa returns to Wakanda to take his place as king and face a new enemy.', 12),
('Doctor Strange', 'Scott Derrickson', 'Action', 115, 'A former surgeon becomes a powerful sorcerer and must protect the world from mystical threats.', 12),
('Spider-Man: Homecoming', 'Jon Watts', 'Action', 133, 'Peter Parker balances his life as a high school student with his role as Spider-Man.', 12),
('Spider-Man: Far From Home', 'Jon Watts', 'Action', 129, 'Peter Parker faces new threats while on a school trip to Europe.', 12),
('Spider-Man: No Way Home', 'Jon Watts', 'Action', 148, 'Peter Parker seeks help from Doctor Strange to fix a spell gone wrong, leading to multiverse chaos.', 12),
('Ant-Man', 'Peyton Reed', 'Action', 117, 'A thief gains the ability to shrink in size and must use his new powers to stop a villain.', 12),
('Ant-Man and the Wasp', 'Peyton Reed', 'Action', 118, 'Scott Lang teams up with Hope van Dyne to rescue her mother from the quantum realm.', 12),
('Captain Marvel', 'Anna Boden', 'Action', 123, 'Carol Danvers becomes one of the universe''s most powerful heroes.', 12);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('Black Widow', 'Cate Shortland', 'Action', 134, 'Natasha Romanoff confronts her past as a spy and faces a dangerous conspiracy.', 12),
('Shang-Chi and the Legend of the Ten Rings', 'Destin Daniel Cretton', 'Action', 132, 'Shang-Chi must confront his past and face the Ten Rings organization.', 12),
('Eternals', 'Chloé Zhao', 'Action', 156, 'A group of immortal beings must reunite to protect Earth from their ancient enemies.', 12),
('The Batman', 'Matt Reeves', 'Action', 176, 'Batman uncovers corruption in Gotham City while pursuing the Riddler.', 15),
('Joker', 'Todd Phillips', 'Drama', 122, 'A failed comedian descends into madness and becomes the Joker.', 18),
('Wonder Woman', 'Patty Jenkins', 'Action', 141, 'Diana, an Amazonian warrior, leaves her home to fight in World War I.', 12),
('Wonder Woman 1984', 'Patty Jenkins', 'Action', 151, 'Diana Prince faces new challenges in the 1980s, including a mysterious new enemy.', 12),
('Aquaman', 'James Wan', 'Action', 143, 'Arthur Curry, the heir to the underwater kingdom of Atlantis, must step forward to lead his people.', 12),
('Shazam!', 'David F. Sandberg', 'Action', 132, 'A young boy gains the ability to transform into an adult superhero.', 12),
('The Suicide Squad', 'James Gunn', 'Action', 132, 'A group of supervillains is sent on a dangerous mission by the government.', 15);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('Birds of Prey', 'Cathy Yan', 'Action', 109, 'Harley Quinn teams up with other female superheroes to protect a young girl.', 15),
('Zack Snyder''s Justice League', 'Zack Snyder', 'Action', 242, 'The Justice League unites to stop a catastrophic threat to Earth.', 15),
('Man of Steel', 'Zack Snyder', 'Action', 143, 'Superman must face General Zod and his army to protect Earth.', 12),
('Batman v Superman: Dawn of Justice', 'Zack Snyder', 'Action', 151, 'Batman and Superman clash, while a new threat emerges.', 15),
('Justice League', 'Zack Snyder', 'Action', 120, 'The Justice League unites to stop a catastrophic threat to Earth.', 12),
('The Flash', 'Andy Muschietti', 'Action', 144, 'Barry Allen uses his super-speed to change the past, but his actions have unintended consequences.', 12),
('The Incredibles', 'Brad Bird', 'Animation', 115, 'A family of undercover superheroes must save the world from a new villain.', 12),
('The Incredibles 2', 'Brad Bird', 'Animation', 118, 'The Incredibles face a new villain while trying to balance their family life.', 12),
('Finding Nemo', 'Andrew Stanton', 'Animation', 100, 'A clownfish embarks on a journey to find his son, who has been captured by a diver.', null),
('Finding Dory', 'Andrew Stanton', 'Animation', 97, 'Dory embarks on a journey to find her long-lost parents.', null);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('Toy Story', 'John Lasseter', 'Animation', 81, 'A cowboy doll is threatened by a new spaceman action figure.', null),
('Toy Story 2', 'John Lasseter', 'Animation', 92, 'Woody is stolen by a toy collector, and Buzz and the gang must rescue him.', null),
('Toy Story 3', 'Lee Unkrich', 'Animation', 103, 'Woody and the gang face an uncertain future as Andy prepares to leave for college.', null),
('Toy Story 4', 'Josh Cooley', 'Animation', 100, 'Woody and the gang embark on a road trip with a new toy, Forky.', null),
('Coco', 'Lee Unkrich', 'Animation', 105, 'A young boy embarks on a journey to the Land of the Dead to uncover his family''s history.', null),
('Frozen', 'Chris Buck', 'Animation', 102, 'Anna teams up with Kristoff to find her sister Elsa, who has trapped their kingdom in eternal winter.', null),
('Frozen II', 'Chris Buck', 'Animation', 103, 'Elsa and Anna embark on a journey to discover the origin of Elsa''s powers.', null),
('Moana', 'Ron Clements', 'Animation', 107, 'A young girl sets sail on a daring mission to save her people.', null),
('Zootopia', 'Byron Howard', 'Animation', 108, 'A rabbit police officer teams up with a fox to solve a missing mammals case.', null),
('The Lion King', 'Jon Favreau', 'Animation', 118, 'Simba, a young lion prince, flees his kingdom after the murder of his father.', null);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('Aladdin', 'Guy Ritchie', 'Adventure', 128, 'A street urchin teams up with a genie to win the heart of a princess.', null),
('Beauty and the Beast', 'Bill Condon', 'Adventure', 129, 'A young woman falls in love with a cursed prince who lives in a magical castle.', null),
('Mulan', 'Niki Caro', 'Adventure', 115, 'A young woman disguises herself as a man to take her father''s place in the army.', null),
('Pirates of the Caribbean: The Curse of the Black Pearl', 'Gore Verbinski', 'Adventure', 143, 'A blacksmith teams up with a pirate to rescue a kidnapped woman.', 12),
('Pirates of the Caribbean: Dead Man''s Chest', 'Gore Verbinski', 'Adventure', 150, 'Jack Sparrow searches for the heart of Davy Jones to avoid eternal servitude.', 12),
('Pirates of the Caribbean: At World''s End', 'Gore Verbinski', 'Adventure', 169, 'Jack Sparrow and his crew must unite to defeat Davy Jones and the East India Trading Company.', 12),
('Pirates of the Caribbean: On Stranger Tides', 'Rob Marshall', 'Adventure', 136, 'Jack Sparrow searches for the Fountain of Youth.', 12),
('Pirates of the Caribbean: Dead Men Tell No Tales', 'Joachim Rønning', 'Adventure', 129, 'Jack Sparrow faces a new enemy, Captain Salazar, who seeks revenge.', 12),
('The Jungle Book', 'Jon Favreau', 'Adventure', 106, 'A young boy raised by wolves must leave the jungle and face the dangers of the human world.', 12),
('The Chronicles of Narnia: The Lion, the Witch and the Wardrobe', 'Andrew Adamson', 'Fantasy', 143, 'Four siblings enter a magical world and must fight to save it from an evil witch.', 12);

INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('The Chronicles of Narnia: Prince Caspian', 'Andrew Adamson', 'Fantasy', 150, 'The Pevensie siblings return to Narnia to help Prince Caspian reclaim his throne.', 12),
('The Chronicles of Narnia: The Voyage of the Dawn Treader', 'Michael Apted', 'Fantasy', 113, 'The Pevensie siblings embark on a voyage to the edge of the world.', 12),
('Harry Potter and the Sorcerer''s Stone', 'Chris Columbus', 'Fantasy', 152, 'A young boy discovers he is a wizard and begins his education at Hogwarts.', 12),
('Harry Potter and the Chamber of Secrets', 'Chris Columbus', 'Fantasy', 161, 'Harry returns to Hogwarts and uncovers a dark secret within the school.', 12),
('Harry Potter and the Prisoner of Azkaban', 'Alfonso Cuarón', 'Fantasy', 142, 'Harry learns about his parents'' past and faces a dangerous escaped prisoner.', 12),
('Harry Potter and the Goblet of Fire', 'Mike Newell', 'Fantasy', 157, 'Harry is chosen to compete in a dangerous tournament against other wizards.', 12),
('Harry Potter and the Order of the Phoenix', 'David Yates', 'Fantasy', 138, 'Harry forms a secret group to fight against the dark forces at Hogwarts.', 12),
('Harry Potter and the Half-Blood Prince', 'David Yates', 'Fantasy', 153, 'Harry learns more about Voldemort''s past and prepares for the final battle.', 12),
('Harry Potter and the Deathly Hallows: Part 1', 'David Yates', 'Fantasy', 146, 'Harry, Ron, and Hermione embark on a dangerous mission to destroy Voldemort''s Horcruxes.', 12),
('Harry Potter and the Deathly Hallows: Part 2', 'David Yates', 'Fantasy', 130, 'Harry and his friends face Voldemort in the final battle for the fate of the wizarding world.', 12);

INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('Fantastic Beasts and Where to Find Them', 'David Yates', 'Fantasy', 133, 'A magizoologist travels to New York City and encounters magical creatures.', 12),
('Fantastic Beasts: The Crimes of Grindelwald', 'David Yates', 'Fantasy', 134, 'Newt Scamander joins forces with a young Albus Dumbledore to stop Gellert Grindelwald.', 12),
('Fantastic Beasts: The Secrets of Dumbledore', 'David Yates', 'Fantasy', 142, 'Newt Scamander and Albus Dumbledore face Grindelwald''s growing power.', 12),
('The Hunger Games', 'Gary Ross', 'Action', 142, 'A young girl volunteers to take her sister''s place in a deadly competition.', 12),
('The Hunger Games: Catching Fire', 'Francis Lawrence', 'Action', 146, 'Katniss and Peeta are forced to compete in another deadly Hunger Games.', 12),
('The Hunger Games: Mockingjay - Part 1', 'Francis Lawrence', 'Action', 123, 'Katniss becomes the symbol of rebellion against the Capitol.', 12),
('The Hunger Games: Mockingjay - Part 2', 'Francis Lawrence', 'Action', 137, 'Katniss leads the rebellion against the Capitol in the final battle.', 12),
('Twilight', 'Catherine Hardwicke', 'Romance', 122, 'A teenage girl falls in love with a vampire, leading to a dangerous romance.', 12),
('The Twilight Saga: New Moon', 'Chris Weitz', 'Romance', 130, 'Bella is left heartbroken when Edward leaves, but finds comfort in her friendship with Jacob.', 12),
('The Twilight Saga: Eclipse', 'David Slade', 'Romance', 124, 'Bella must choose between Edward and Jacob as a new threat emerges.', 12);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('The Twilight Saga: Breaking Dawn - Part 1', 'Bill Condon', 'Romance', 117, 'Bella and Edward get married, but their happiness is threatened by a new danger.', 12),
('The Twilight Saga: Breaking Dawn - Part 2', 'Bill Condon', 'Romance', 115, 'Bella and Edward face the Volturi in a final battle to protect their family.', 12),
('Transformers', 'Michael Bay', 'Action', 144, 'A teenager discovers his car is actually a robot from another planet.', 12),
('Transformers: Revenge of the Fallen', 'Michael Bay', 'Action', 150, 'The Autobots must stop the Decepticons from reviving an ancient evil.', 12),
('Transformers: Dark of the Moon', 'Michael Bay', 'Action', 154, 'The Autobots discover a hidden Cybertronian spacecraft on the moon.', 12),
('Transformers: Age of Extinction', 'Michael Bay', 'Action', 165, 'The Autobots face a new threat from a human-made Transformer.', 12),
('Transformers: The Last Knight', 'Michael Bay', 'Action', 154, 'The Autobots must uncover the truth about their past to save the future.', 12),
('Bumblebee', 'Travis Knight', 'Action', 114, 'A young girl befriends a damaged Autobot and helps him find his place in the world.', 12),
('Mission: Impossible', 'Brian De Palma', 'Action', 110, 'Ethan Hunt is framed for the murder of his team and must clear his name.', 12),
('Mission: Impossible II', 'John Woo', 'Action', 123, 'Ethan Hunt must stop a rogue agent from releasing a deadly virus.', 12);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('Mission: Impossible III', 'J.J. Abrams', 'Action', 126, 'Ethan Hunt must rescue a captured agent and stop a dangerous arms dealer.', 12),
('Mission: Impossible - Ghost Protocol', 'Brad Bird', 'Action', 133, 'Ethan Hunt and his team must stop a nuclear terrorist.', 12),
('Mission: Impossible - Rogue Nation', 'Christopher McQuarrie', 'Action', 131, 'Ethan Hunt must prove the existence of a rogue organization.', 12),
('Mission: Impossible - Fallout', 'Christopher McQuarrie', 'Action', 147, 'Ethan Hunt and his team must stop a global catastrophe.', 12),
('Fast & Furious', 'Justin Lin', 'Action', 107, 'Dominic Toretto and Brian O''Conner team up to take down a drug lord.', 12),
('Fast Five', 'Justin Lin', 'Action', 130, 'Dominic Toretto and his crew plan a heist in Rio de Janeiro.', 12),
('Furious 7', 'James Wan', 'Action', 137, 'Dominic Toretto and his crew face a new enemy seeking revenge.', 12),
('The Fate of the Furious', 'F. Gary Gray', 'Action', 136, 'Dominic Toretto is forced to betray his family by a cyberterrorist.', 12),
('F9: The Fast Saga', 'Justin Lin', 'Action', 145, 'Dominic Toretto and his crew face a new threat from his past.', 12),
('Fast & Furious Presents: Hobbs & Shaw', 'David Leitch', 'Action', 137, 'Luke Hobbs and Deckard Shaw team up to stop a cyber-enhanced terrorist.', 12);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('The Conjuring', 'James Wan', 'Horror', 112, 'A paranormal investigator and his wife help a family terrorized by a dark presence.', 18),
('The Conjuring 2', 'James Wan', 'Horror', 134, 'The Warrens travel to England to help a single mother and her children.', 16),
('Annabelle', 'John R. Leonetti', 'Horror', 99, 'A couple is terrorized by a possessed doll.', 18),
('Annabelle: Creation', 'David F. Sandberg', 'Horror', 109, 'The origin story of the Annabelle doll.', 18),
('Annabelle Comes Home', 'Gary Dauberman', 'Horror', 106, 'The Annabelle doll is locked in a room, but it''s not enough to contain its evil.', 18),
('The Nun', 'Corin Hardy', 'Horror', 96, 'A priest and a nun investigate a mysterious death in a Romanian abbey.', 18),
('The Curse of La Llorona', 'Michael Chaves', 'Horror', 93, 'A social worker must protect her children from a malevolent spirit.', 18),
('It', 'Andy Muschietti', 'Horror', 135, 'A group of kids faces a shape-shifting monster that preys on their fears.', 18),
('It Chapter Two', 'Andy Muschietti', 'Horror', 169, 'The Losers Club returns to Derry to face Pennywise one last time.', 18),
('A Quiet Place', 'John Krasinski', 'Horror', 90, 'A family must live in silence to avoid being hunted by mysterious creatures.', 18);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('A Quiet Place Part II', 'John Krasinski', 'Horror', 97, 'The Abbott family continues to fight for survival in a world overrun by monsters.', 18),
('Get Out', 'Jordan Peele', 'Horror', 104, 'A young African-American man uncovers a disturbing secret when he visits his white girlfriend''s family.', 18),
('Us', 'Jordan Peele', 'Horror', 116, 'A family is terrorized by their doppelgängers.', 18),
('The Shining', 'Stanley Kubrick', 'Horror', 146, 'A family''s stay at a remote hotel turns into a nightmare as the father descends into madness.', 18),
('Doctor Sleep', 'Mike Flanagan', 'Horror', 152, 'An adult Danny Torrance must protect a young girl with psychic abilities from a cult.', 18),
('Hereditary', 'Ari Aster', 'Horror', 127, 'A family is haunted by a mysterious presence after the death of their grandmother.', 18),
('Midsommar', 'Ari Aster', 'Horror', 147, 'A couple travels to Sweden for a festival that turns into a nightmare.', 18),
('The Lighthouse', 'Robert Eggers', 'Horror', 109, 'Two lighthouse keepers descend into madness while stranded on a remote island.', 18),
('The Witch', 'Robert Eggers', 'Horror', 92, 'A Puritan family is terrorized by a witch in the woods.', 18),
('Parasite', 'Bong Joon-ho', 'Thriller', 132, 'A poor family infiltrates the lives of a wealthy family, leading to unexpected consequences.', 18);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('Oldboy', 'Park Chan-wook', 'Thriller', 120, 'A man is imprisoned for 15 years and seeks revenge upon his release.', 16),
('The Handmaiden', 'Park Chan-wook', 'Thriller', 145, 'A young woman is hired as a handmaiden to a Japanese heiress, but she has a secret agenda.', 16),
('Memories of Murder', 'Bong Joon-ho', 'Thriller', 131, 'Two detectives investigate a series of murders in a small town.', 18),
('The Wailing', 'Na Hong-jin', 'Thriller', 156, 'A policeman investigates a series of mysterious deaths in a remote village.', 18),
('Train to Busan', 'Yeon Sang-ho', 'Horror', 118, 'A group of passengers must survive a zombie outbreak on a train.', 18),
('The Host', 'Bong Joon-ho', 'Horror', 120, 'A monster emerges from the Han River and terrorizes Seoul.', 18),
('Snowpiercer', 'Bong Joon-ho', 'Sci-Fi', 126, 'The survivors of a global ice age live on a perpetually moving train.', 16),
('Okja', 'Bong Joon-ho', 'Adventure', 120, 'A young girl risks everything to save her genetically engineered super pig.', 12),
('The Grand Budapest Hotel', 'Wes Anderson', 'Comedy', 99, 'A concierge and his protégé are framed for murder and must clear their names.', 12),
('Moonrise Kingdom', 'Wes Anderson', 'Comedy', 94, 'Two young lovers run away together, sparking a search party in their small town.', 12);


INSERT INTO Films (Title, Director, Genre, Length, Description, AgeRating) VALUES
('The Royal Tenenbaums', 'Wes Anderson', 'Comedy', 110, 'A dysfunctional family reunites after their father fakes a terminal illness.', 12),
('Fantastic Mr. Fox', 'Wes Anderson', 'Animation', 87, 'A clever fox outwits three farmers who want to destroy his family.', 12),
('Isle of Dogs', 'Wes Anderson', 'Animation', 101, 'A boy searches for his lost dog on an island of exiled canines.', 12),
('The Life Aquatic with Steve Zissou', 'Wes Anderson', 'Comedy', 119, 'An oceanographer sets out to avenge the death of his partner.', 12),
('Rushmore', 'Wes Anderson', 'Comedy', 93, 'A precocious teenager befriends a wealthy industrialist and falls in love with a teacher.', 12),
('The Darjeeling Limited', 'Wes Anderson', 'Comedy', 91, 'Three brothers embark on a train journey across India to reconnect.', 12),
('Bottle Rocket', 'Wes Anderson', 'Comedy', 91, 'Three friends plan a series of heists, but things don''t go as planned.', 12),
('The French Dispatch', 'Wes Anderson', 'Comedy', 108, 'A collection of stories from the final issue of an American magazine in France.', 12);

INSERT INTO Rooms (Name, MaxSeatRow, MaxSeatColumn) VALUES
('Rooms 1', 10, 15),
('Rooms 2', 12, 18),
('Rooms 3', 8, 12),
('Rooms 4', 10, 20),
('Rooms 5', 15, 20),
('Rooms 6', 10, 15),
('Rooms 7', 12, 18),
('Rooms 8', 8, 12),
('Rooms 9', 10, 20),
('Rooms 10', 15, 20);

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
(13, 5, '2025-08-08 20:00:00'),

(14, 2, '2025-07-02 14:00:00'),
(14, 4, '2025-08-14 18:00:00'),
(14, 6, '2025-06-20 10:30:00'),
(14, 8, '2025-07-30 13:00:00'),

(15, 1, '2025-08-05 16:00:00'),
(15, 3, '2025-06-15 20:00:00'),
(15, 5, '2025-07-25 14:00:00'),

(16, 2, '2025-06-10 11:00:00'),
(16, 4, '2025-07-12 13:00:00'),
(16, 6, '2025-08-24 17:30:00'),
(16, 8, '2025-06-28 10:00:00'),

(17, 1, '2025-07-15 13:00:00'),
(17, 3, '2025-08-25 17:00:00'),
(17, 5, '2025-06-30 21:00:00'),

(18, 2, '2025-08-01 15:00:00'),
(18, 4, '2025-06-05 19:00:00'),
(18, 6, '2025-07-18 11:30:00'),
(18, 8, '2025-08-20 14:00:00'),

(19, 1, '2025-06-22 12:00:00'),
(19, 3, '2025-07-30 16:00:00'),
(19, 5, '2025-08-10 20:00:00'),

(20, 2, '2025-07-05 14:00:00'),
(20, 4, '2025-08-15 18:00:00'),
(20, 6, '2025-06-25 10:30:00'),
(20, 8, '2025-07-28 13:00:00'),

(21, 1, '2025-08-12 16:00:00'),
(21, 3, '2025-06-18 20:00:00'),
(21, 5, '2025-07-22 14:00:00'),

(22, 2, '2025-06-08 11:00:00'),
(22, 4, '2025-07-10 13:00:00'),
(22, 6, '2025-08-24 17:30:00'),
(22, 8, '2025-06-30 10:00:00'),

(23, 1, '2025-07-15 13:00:00'),
(23, 3, '2025-08-25 17:00:00'),
(23, 5, '2025-06-30 21:00:00'),

(24, 2, '2025-08-01 15:00:00'),
(24, 4, '2025-06-05 19:00:00'),
(24, 6, '2025-07-18 11:30:00'),
(24, 8, '2025-08-20 14:00:00'),

(25, 1, '2025-06-22 12:00:00'),
(25, 3, '2025-07-30 16:00:00'),
(25, 5, '2025-08-10 20:00:00'),

(26, 2, '2025-07-05 14:00:00'),
(26, 4, '2025-08-15 18:00:00'),
(26, 6, '2025-06-25 10:30:00'),
(26, 8, '2025-07-28 13:00:00'),

(27, 1, '2025-08-12 16:00:00'),
(27, 3, '2025-06-18 20:00:00'),
(27, 5, '2025-07-22 14:00:00'),

(28, 2, '2025-06-08 11:00:00'),
(28, 4, '2025-07-10 13:00:00'),
(28, 6, '2025-08-24 17:30:00'),
(28, 8, '2025-06-30 10:00:00'),

(29, 1, '2025-07-15 13:00:00'),
(29, 3, '2025-08-25 17:00:00'),
(29, 5, '2025-06-30 21:00:00'),

(30, 2, '2025-08-01 15:00:00'),
(30, 4, '2025-06-05 19:00:00'),
(30, 6, '2025-07-18 11:30:00'),
(30, 8, '2025-08-20 14:00:00'),

(31, 1, '2025-06-22 12:00:00'),
(31, 3, '2025-07-30 16:00:00'),
(31, 5, '2025-08-10 20:00:00'),

(32, 2, '2025-07-05 14:00:00'),
(32, 4, '2025-08-15 18:00:00'),
(32, 6, '2025-06-25 10:30:00'),
(32, 8, '2025-07-28 13:00:00'),

(33, 1, '2025-08-12 16:00:00'),
(33, 3, '2025-06-18 20:00:00'),
(33, 5, '2025-07-22 14:00:00'),

(34, 2, '2025-06-08 11:00:00'),
(34, 4, '2025-07-10 13:00:00'),
(34, 6, '2025-08-24 17:30:00'),
(34, 8, '2025-06-30 10:00:00'),

(35, 1, '2025-07-15 13:00:00'),
(35, 3, '2025-08-25 17:00:00'),
(35, 5, '2025-06-30 21:00:00'),

(36, 2, '2025-08-01 15:00:00'),
(36, 4, '2025-06-05 19:00:00'),
(36, 6, '2025-07-18 11:30:00'),
(36, 8, '2025-08-20 14:00:00'),

(37, 1, '2025-06-22 12:00:00'),
(37, 3, '2025-07-30 16:00:00'),
(37, 5, '2025-08-10 20:00:00'),

(38, 2, '2025-07-05 14:00:00'),
(38, 4, '2025-08-15 18:00:00'),
(38, 6, '2025-06-25 10:30:00'),
(38, 8, '2025-07-28 13:00:00'),

(39, 1, '2025-08-12 16:00:00'),
(39, 3, '2025-06-18 20:00:00'),
(39, 5, '2025-07-22 14:00:00'),

(40, 2, '2025-06-08 11:00:00'),
(40, 4, '2025-07-10 13:00:00'),
(40, 6, '2025-08-24 17:30:00'),
(40, 8, '2025-06-30 10:00:00'),

(41, 1, '2025-07-15 13:00:00'),
(41, 3, '2025-08-25 17:00:00'),
(41, 5, '2025-06-30 21:00:00'),

(42, 2, '2025-08-01 15:00:00'),
(42, 4, '2025-06-05 19:00:00'),
(42, 6, '2025-07-18 11:30:00'),
(42, 8, '2025-08-20 14:00:00'),

(43, 1, '2025-06-22 12:00:00'),
(43, 3, '2025-07-30 16:00:00'),
(43, 5, '2025-08-10 20:00:00'),

(44, 2, '2025-07-05 14:00:00'),
(44, 4, '2025-08-15 18:00:00'),
(44, 6, '2025-06-25 10:30:00'),
(44, 8, '2025-07-28 13:00:00'),

(45, 1, '2025-08-12 16:00:00'),
(45, 3, '2025-06-18 20:00:00'),
(45, 5, '2025-07-22 14:00:00'),

(46, 2, '2025-06-08 11:00:00'),
(46, 4, '2025-07-10 13:00:00'),
(46, 6, '2025-08-24 17:30:00'),
(46, 8, '2025-06-30 10:00:00'),

(47, 1, '2025-07-15 13:00:00'),
(47, 3, '2025-08-25 17:00:00'),
(47, 5, '2025-06-30 21:00:00'),

(48, 2, '2025-08-01 15:00:00'),
(48, 4, '2025-06-05 19:00:00'),
(48, 6, '2025-07-18 11:30:00'),
(48, 8, '2025-08-20 14:00:00'),

(49, 1, '2025-06-22 12:00:00'),
(49, 3, '2025-07-30 16:00:00'),
(49, 5, '2025-08-10 20:00:00'),

(50, 2, '2025-07-05 14:00:00'),
(50, 4, '2025-08-15 18:00:00'),
(50, 6, '2025-06-25 10:30:00'),
(50, 8, '2025-07-28 13:00:00'),

(51, 1, '2025-08-12 16:00:00'),
(51, 3, '2025-06-18 20:00:00'),
(51, 5, '2025-07-22 14:00:00'),

(52, 2, '2025-06-08 11:00:00'),
(52, 4, '2025-07-10 13:00:00'),
(52, 6, '2025-08-24 17:30:00'),
(52, 8, '2025-06-30 10:00:00'),

(53, 1, '2025-07-15 13:00:00'),
(53, 3, '2025-08-25 17:00:00'),
(53, 5, '2025-06-30 21:00:00'),

(54, 2, '2025-08-01 15:00:00'),
(54, 4, '2025-06-05 19:00:00'),
(54, 6, '2025-07-18 11:30:00'),
(54, 8, '2025-08-20 14:00:00'),

(55, 1, '2025-06-22 12:00:00'),
(55, 3, '2025-07-30 16:00:00'),
(55, 5, '2025-08-10 20:00:00'),

(56, 2, '2025-07-05 14:00:00'),
(56, 4, '2025-08-15 18:00:00'),
(56, 6, '2025-06-25 10:30:00'),
(56, 8, '2025-07-28 13:00:00'),

(57, 1, '2025-08-12 16:00:00'),
(57, 3, '2025-06-18 20:00:00'),
(57, 5, '2025-07-22 14:00:00'),

(58, 2, '2025-06-08 11:00:00'),
(58, 4, '2025-07-10 13:00:00'),
(58, 6, '2025-08-24 17:30:00'),
(58, 8, '2025-06-30 10:00:00'),

(59, 1, '2025-07-15 13:00:00'),
(59, 3, '2025-08-25 17:00:00'),
(59, 5, '2025-06-30 21:00:00'),

(60, 2, '2025-08-01 15:00:00'),
(60, 4, '2025-06-05 19:00:00'),
(60, 6, '2025-07-18 11:30:00'),
(60, 8, '2025-08-20 14:00:00'),

(61, 1, '2025-06-22 12:00:00'),
(61, 3, '2025-07-30 16:00:00'),
(61, 5, '2025-08-10 20:00:00'),

(62, 2, '2025-07-05 14:00:00'),
(62, 4, '2025-08-15 18:00:00'),
(62, 6, '2025-06-25 10:30:00'),
(62, 8, '2025-07-28 13:00:00'),

(63, 1, '2025-08-12 16:00:00'),
(63, 3, '2025-06-18 20:00:00'),
(63, 5, '2025-07-22 14:00:00'),

(64, 2, '2025-06-08 11:00:00'),
(64, 4, '2025-07-10 13:00:00'),
(64, 6, '2025-08-24 17:30:00'),
(64, 8, '2025-06-30 10:00:00'),

(65, 1, '2025-07-15 13:00:00'),
(65, 3, '2025-08-25 17:00:00'),
(65, 5, '2025-06-30 21:00:00'),

(66, 2, '2025-08-01 15:00:00'),
(66, 4, '2025-06-05 19:00:00'),
(66, 6, '2025-07-18 11:30:00'),
(66, 8, '2025-08-20 14:00:00'),

(67, 1, '2025-06-22 12:00:00'),
(67, 3, '2025-07-30 16:00:00'),
(67, 5, '2025-08-10 20:00:00'),

(68, 2, '2025-07-05 14:00:00'),
(68, 4, '2025-08-15 18:00:00'),
(68, 6, '2025-06-25 10:30:00'),
(68, 8, '2025-07-28 13:00:00'),

(69, 1, '2025-08-12 16:00:00'),
(69, 3, '2025-06-18 20:00:00'),
(69, 5, '2025-07-22 14:00:00'),

(70, 2, '2025-06-08 11:00:00'),
(70, 4, '2025-07-10 13:00:00'),
(70, 6, '2025-08-24 17:30:00'),
(70, 8, '2025-06-30 10:00:00');

INSERT INTO Users (Name, Email, Rank, BirthDate) VALUES
('Kovács Ádám', 'kovacs.adam@example.com', 1, '1990-01-01'),
('Nagy Eszter', 'nagy.eszter@example.com', 1, '1991-02-02'),
('Tóth Balázs', 'toth.balazs@example.com', 1, '1992-03-03'),
('Szabó Zsófia', 'szabo.zsofia@example.com', 1, '1993-04-04'),
('Horváth Dávid', 'horvath.david@example.com', 1, '1994-05-05'),
('Varga Réka', 'varga.reka@example.com', 1, '1995-06-06'),
('Molnár Gábor', 'molnar.gabor@example.com', 1, '1996-07-07'),
('Farkas Anna', 'farkas.anna@example.com', 1, '1997-08-08'),
('Takács Péter', 'takacs.peter@example.com', 1, '1998-09-09'),
('Kiss Judit', 'kiss.judit@example.com', 1, '1999-10-10'),
('Papp László', 'papp.laszlo@example.com', 1, '1985-11-11'),
('Balogh Éva', 'balogh.eva@example.com', 1, '1986-12-12'),
('Simon András', 'simon.andras@example.com', 1, '1987-01-13'),
('Rácz Katalin', 'racz.katalin@example.com', 1, '1988-02-14'),
('Fekete Tamás', 'fekete.tamas@example.com', 1, '1989-03-15'),
('Szűcs Márta', 'szucs.marta@example.com', 1, '1990-04-16'),
('Lukács István', 'lukacs.istvan@example.com', 1, '1991-05-17'),
('Mészáros Zoltán', 'meszaros.zoltan@example.com', 1, '1992-06-18'),
('Király Edit', 'kiraly.edit@example.com', 1, '1993-07-19'),
('Bakos Gergő', 'bakos.gergo@example.com', 1, '1994-08-20'),
('Gál Csaba', 'gal.csaba@example.com', 1, '1995-09-21'),
('Németh Krisztina', 'nemeth.krisztina@example.com', 1, '1985-11-11'),
('Vincze Attila', 'vincze.attila@example.com', 1, '1986-12-12'),
('Magyar Zsuzsanna', 'magyar.zsuzsanna@example.com', 1, '1987-01-13'),
('Fehér Gyula', 'feher.gyula@example.com', 1, '1988-02-14'),
('Szalai Ágnes', 'szalai.agnes@example.com', 1, '1989-03-15'),
('Bíró Szabolcs', 'biro.szabolcs@example.com', 1, '1990-04-16'),
('Katona Lilla', 'katona.lilla@example.com', 1, '1991-05-17'),
('Sándor Roland', 'sandor.roland@example.com', 1, '1992-06-18'),
('Fodor Bianka', 'fodor.bianka@example.com', 1, '1993-07-19'),
('Anna Müller', 'anna.muller@example.com', 1, '1990-01-01'),
('Ján Novák', 'jan.novak@example.com', 1, '1991-02-02'),
('Maria Popescu', 'maria.popescu@example.com', 1, '1992-03-03');

INSERT INTO Addresses (Country, County, ZipCode, City, Street, Floor, HouseNumber, UserId) VALUES
('Hungary', 'Budapest', 1011, 'Budapest', 'Kossuth Lajos utca', NULL, 10, 1),
('Hungary', 'Budapest', 1012, 'Budapest', 'Széchenyi utca', 2, 5, 2),
('Hungary', 'Budapest', 1013, 'Budapest', 'Andrássy út', NULL, 20, 3),
('Hungary', 'Budapest', 1014, 'Budapest', 'Bajcsy-Zsilinszky út', 3, 15, 4),
('Hungary', 'Budapest', 1015, 'Budapest', 'Váci utca', NULL, 8, 5),
('Hungary', 'Budapest', 1016, 'Budapest', 'Rákóczi út', 4, 12, 6),
('Hungary', 'Budapest', 1017, 'Budapest', 'Teréz körút', NULL, 7, 7),
('Hungary', 'Budapest', 1018, 'Budapest', 'Bartók Béla út', 5, 9, 8),
('Hungary', 'Budapest', 1019, 'Budapest', 'Dózsa György út', NULL, 11, 9),
('Hungary', 'Budapest', 1021, 'Budapest', 'Hősök tere', 6, 14, 10),
('Hungary', 'Budapest', 1022, 'Budapest', 'Vörösmarty tér', NULL, 13, 11),
('Hungary', 'Budapest', 1023, 'Budapest', 'Margit körút', 7, 16, 12),
('Hungary', 'Budapest', 1024, 'Budapest', 'Budaörsi út', NULL, 18, 13),
('Hungary', 'Budapest', 1025, 'Budapest', 'Nagymező utca', 8, 19, 14),
('Hungary', 'Budapest', 1026, 'Budapest', 'Hegyalja út', NULL, 21, 15),
('Hungary', 'Budapest', 1027, 'Budapest', 'Szent István körút', 9, 22, 16),
('Hungary', 'Budapest', 1028, 'Budapest', 'Árpád fejedelem útja', NULL, 23, 17),
('Hungary', 'Budapest', 1029, 'Budapest', 'Csörsz utca', 10, 24, 18),
('Hungary', 'Budapest', 1031, 'Budapest', 'Bécsi út', NULL, 25, 19),
('Hungary', 'Budapest', 1032, 'Budapest', 'Flórián tér', 11, 26, 20),
('Hungary', 'Budapest', 1033, 'Budapest', 'Szentendre út', NULL, 27, 21),
('Hungary', 'Pest', 2000, 'Szentendre', 'Fő tér', NULL, 3, 22),
('Hungary', 'Győr-Moson-Sopron', 9021, 'Győr', 'Baross Gábor út', 2, 7, 23),
('Hungary', 'Bács-Kiskun', 6000, 'Kecskemét', 'Kossuth tér', NULL, 10, 24),
('Hungary', 'Csongrád-Csanád', 6720, 'Szeged', 'Dugonics tér', 3, 5, 25),
('Hungary', 'Heves', 3300, 'Eger', 'Dobó tér', NULL, 8, 26),
('Hungary', 'Veszprém', 8200, 'Veszprém', 'Óvári Ferenc utca', 4, 12, 27),
('Hungary', 'Baranya', 7621, 'Pécs', 'Széchenyi tér', NULL, 6, 28),
('Hungary', 'Hajdú-Bihar', 4025, 'Debrecen', 'Piac utca', 5, 9, 29),
('Hungary', 'Jász-Nagykun-Szolnok', 5000, 'Szolnok', 'Kossuth Lajos utca', NULL, 11, 30),
('Austria', 'Vienna', 1010, 'Vienna', 'Stephansplatz', NULL, 5, 31),
('Slovakia', 'Bratislava', 81101, 'Bratislava', 'Hlavné námestie', 2, 7, 32),
('Romania', 'Bucharest', 010101, 'Bucharest', 'Calea Victoriei', NULL, 10, 33);

INSERT INTO Users (Name, Email, Rank, BirthDate) VALUES
('Kiss Áron', 'kiss.aron@example.com', 1, '1990-01-02'),
('Németh Barbara', 'nemeth.barbara@example.com', 1, '1991-02-03'),
('Szűcs Csaba', 'szucs.csaba@example.com', 1, '1992-03-04'),
('Fekete Dóra', 'fekete.dora@example.com', 1, '1993-04-05'),
('Balogh Erika', 'balogh.erika@example.com', 1, '1994-05-06'),
('Varga Ferenc', 'varga.ferenc@example.com', 1, '1995-06-07'),
('Molnár Gizella', 'molnar.gizella@example.com', 1, '1996-07-08'),
('Takács Henrik', 'takacs.henrik@example.com', 1, '1997-08-09'),
('Kovács Ilona', 'kovacs.ilona@example.com', 1, '1998-09-10'),
('Papp János', 'papp.janos@example.com', 1, '1999-10-11'),
('Tóth Katalin', 'toth.katalin@example.com', 1, '1985-11-12'),
('Horváth László', 'horvath.laszlo@example.com', 1, '1986-12-13'),
('Farkas Mária', 'farkas.maria@example.com', 1, '1987-01-14'),
('Rácz Norbert', 'racz.norbert@example.com', 1, '1988-02-15'),
('Simon Orsolya', 'simon.orsolya@example.com', 1, '1989-03-16'),
('Szabó Péter', 'szabo.peter@example.com', 1, '1990-04-17'),
('Lukács Renáta', 'lukacs.renata@example.com', 1, '1991-05-18'),
('Mészáros Sándor', 'meszaros.sandor@example.com', 1, '1992-06-19'),
('Király Tamás', 'kiraly.tamas@example.com', 1, '1993-07-20'),
('Bakos Zoltán', 'bakos.zoltan@example.com', 1, '1994-08-21'),
('Gál Zsuzsanna', 'gal.zsuzsanna@example.com', 1, '1995-09-22'),
('Németh Ágnes', 'nemeth.agnes@example.com', 1, '1985-11-13'),
('Vincze Béla', 'vincze.bela@example.com', 1, '1986-12-14'),
('Magyar Cecília', 'magyar.cecilia@example.com', 1, '1987-01-15'),
('Fehér Dénes', 'feher.denes@example.com', 1, '1988-02-16'),
('Szalai Elemér', 'szalai.elemer@example.com', 1, '1989-03-17'),
('Bíró Ferenc', 'biro.ferenc@example.com', 1, '1990-04-18'),
('Katona Gábor', 'katona.gabor@example.com', 1, '1991-05-19'),
('Sándor Henrietta', 'sandor.henrietta@example.com', 1, '1992-06-20'),
('Fodor István', 'fodor.istvan@example.com', 1, '1993-07-21'),
('Sophie Müller', 'sophie.muller@example.com', 1, '1990-01-03'),
('Peter Novák', 'peter.novak@example.com', 1, '1991-02-04'),
('Elena Popescu', 'elena.popescu@example.com', 1, '1992-03-05');

INSERT INTO Addresses (Country, County, ZipCode, City, Street, Floor, HouseNumber, UserId) VALUES
('Hungary', 'Budapest', 1034, 'Budapest', 'Árpád út', NULL, 10, 34),
('Hungary', 'Budapest', 1035, 'Budapest', 'Bécsi út', 2, 5, 35),
('Hungary', 'Budapest', 1036, 'Budapest', 'Csillaghegyi út', NULL, 20, 36),
('Hungary', 'Budapest', 1037, 'Budapest', 'Dunakeszi út', 3, 15, 37),
('Hungary', 'Budapest', 1038, 'Budapest', 'Fő tér', NULL, 8, 38),
('Hungary', 'Budapest', 1039, 'Budapest', 'Göncöl út', 4, 12, 39),
('Hungary', 'Budapest', 1041, 'Budapest', 'Hegyalja út', NULL, 7, 40),
('Hungary', 'Budapest', 1042, 'Budapest', 'Ipoly utca', 5, 9, 41),
('Hungary', 'Budapest', 1043, 'Budapest', 'Jókai utca', NULL, 11, 42),
('Hungary', 'Budapest', 1044, 'Budapest', 'Károly körút', 6, 14, 43),
('Hungary', 'Budapest', 1045, 'Budapest', 'Lajos utca', NULL, 13, 44),
('Hungary', 'Budapest', 1046, 'Budapest', 'Márton utca', 7, 16, 45),
('Hungary', 'Budapest', 1047, 'Budapest', 'Nádor utca', NULL, 18, 46),
('Hungary', 'Budapest', 1048, 'Budapest', 'Óbudai tér', 8, 19, 47),
('Hungary', 'Budapest', 1051, 'Budapest', 'Péterfy utca', NULL, 21, 48),
('Hungary', 'Budapest', 1052, 'Budapest', 'Rákóczi út', 9, 22, 49),
('Hungary', 'Budapest', 1053, 'Budapest', 'Szent István körút', NULL, 23, 50),
('Hungary', 'Budapest', 1054, 'Budapest', 'Teréz körút', 10, 24, 51),
('Hungary', 'Budapest', 1055, 'Budapest', 'Újpesti út', NULL, 25, 52),
('Hungary', 'Budapest', 1056, 'Budapest', 'Váci út', 11, 26, 53),
('Hungary', 'Budapest', 1057, 'Budapest', 'Wesselényi utca', NULL, 27, 54),
('Hungary', 'Pest', 2001, 'Szentendre', 'Fő utca', NULL, 3, 55),
('Hungary', 'Győr-Moson-Sopron', 9022, 'Győr', 'Baross út', 2, 7, 56),
('Hungary', 'Bács-Kiskun', 6001, 'Kecskemét', 'Kossuth utca', NULL, 10, 57),
('Hungary', 'Csongrád-Csanád', 6721, 'Szeged', 'Dugonics utca', 3, 5, 58),
('Hungary', 'Heves', 3301, 'Eger', 'Dobó utca', NULL, 8, 59),
('Hungary', 'Veszprém', 8201, 'Veszprém', 'Óvári út', 4, 12, 60),
('Hungary', 'Baranya', 7622, 'Pécs', 'Széchenyi utca', NULL, 6, 61),
('Hungary', 'Hajdú-Bihar', 4026, 'Debrecen', 'Piac utca', 5, 9, 62),
('Hungary', 'Jász-Nagykun-Szolnok', 5001, 'Szolnok', 'Kossuth út', NULL, 11, 63),
('Austria', 'Vienna', 1011, 'Vienna', 'Graben', NULL, 5, 64),
('Slovakia', 'Bratislava', 81102, 'Bratislava', 'Michalská ulica', 2, 7, 65),
('Romania', 'Bucharest', 010102, 'Bucharest', 'Lipscani', NULL, 10, 66);

INSERT INTO Users (Name, Email, Rank, BirthDate) VALUES
('Kiss Áron', 'kiss.aron@example.com', 1, '2005-03-12'),
('Németh Barbara', 'nemeth.barbara@example.com', 1, '2008-07-25'),
('Szűcs Csaba', 'szucs.csaba@example.com', 1, '2003-11-18'),
('Fekete Dóra', 'fekete.dora@example.com', 1, '2010-02-09'),
('Balogh Erika', 'balogh.erika@example.com', 1, '2006-09-30'),
('Varga Ferenc', 'varga.ferenc@example.com', 1, '2001-05-14'),
('Molnár Gizella', 'molnar.gizella@example.com', 1, '2012-12-03'),
('Takács Henrik', 'takacs.henrik@example.com', 1, '2004-08-22'),
('Kovács Ilona', 'kovacs.ilona@example.com', 1, '2009-04-17'),
('Papp János', 'papp.janos@example.com', 1, '2007-10-28'),
('Tóth Katalin', 'toth.katalin@example.com', 1, '2002-06-19'),
('Horváth László', 'horvath.laszlo@example.com', 1, '2011-01-07'),
('Farkas Mária', 'farkas.maria@example.com', 1, '2000-07-23'),
('Rácz Norbert', 'racz.norbert@example.com', 1, '2013-03-15'),
('Simon Orsolya', 'simon.orsolya@example.com', 1, '2005-09-08'),
('Szabó Péter', 'szabo.peter@example.com', 1, '2014-11-29'),
('Lukács Renáta', 'lukacs.renata@example.com', 1, '2008-02-14'),
('Mészáros Sándor', 'meszaros.sandor@example.com', 1, '2003-04-26'),
('Király Tamás', 'kiraly.tamas@example.com', 1, '2010-08-11'),
('Bakos Zoltán', 'bakos.zoltan@example.com', 1, '2006-12-05'),
('Gál Zsuzsanna', 'gal.zsuzsanna@example.com', 1, '2001-10-20'),
('Németh Ágnes', 'nemeth.agnes@example.com', 1, '2015-05-02'),
('Vincze Béla', 'vincze.bela@example.com', 1, '2004-01-31'),
('Magyar Cecília', 'magyar.cecilia@example.com', 1, '2009-07-16'),
('Fehér Dénes', 'feher.denes@example.com', 1, '2002-03-27'),
('Szalai Elemér', 'szalai.elemer@example.com', 1, '2012-09-10'),
('Bíró Ferenc', 'biro.ferenc@example.com', 1, '2007-06-21'),
('Katona Gábor', 'katona.gabor@example.com', 1, '2011-04-04'),
('Sándor Henrietta', 'sandor.henrietta@example.com', 1, '2000-12-18'),
('Fodor István', 'fodor.istvan@example.com', 1, '2013-08-07');

INSERT INTO Addresses (Country, County, ZipCode, City, Street, Floor, HouseNumber, UserId) VALUES
('Hungary', 'Budapest', 1034, 'Budapest', 'Árpád út', NULL, 10, 67),
('Hungary', 'Budapest', 1035, 'Budapest', 'Bécsi út', 2, 5, 68),
('Hungary', 'Budapest', 1036, 'Budapest', 'Csillaghegyi út', NULL, 20, 69),
('Hungary', 'Budapest', 1037, 'Budapest', 'Dunakeszi út', 3, 15, 70),
('Hungary', 'Budapest', 1038, 'Budapest', 'Fő tér', NULL, 8, 71),
('Hungary', 'Budapest', 1039, 'Budapest', 'Göncöl út', 4, 12, 72),
('Hungary', 'Budapest', 1041, 'Budapest', 'Hegyalja út', NULL, 7, 73),
('Hungary', 'Budapest', 1042, 'Budapest', 'Ipoly utca', 5, 9, 74),
('Hungary', 'Budapest', 1043, 'Budapest', 'Jókai utca', NULL, 11, 75),
('Hungary', 'Budapest', 1044, 'Budapest', 'Károly körút', 6, 14, 76),
('Hungary', 'Budapest', 1045, 'Budapest', 'Lajos utca', NULL, 13, 77),
('Hungary', 'Budapest', 1046, 'Budapest', 'Márton utca', 7, 16, 78),
('Hungary', 'Budapest', 1047, 'Budapest', 'Nádor utca', NULL, 18, 79),
('Hungary', 'Budapest', 1048, 'Budapest', 'Óbudai tér', 8, 19, 80),
('Hungary', 'Budapest', 1051, 'Budapest', 'Péterfy utca', NULL, 21, 81),
('Hungary', 'Budapest', 1052, 'Budapest', 'Rákóczi út', 9, 22, 82),
('Hungary', 'Budapest', 1053, 'Budapest', 'Szent István körút', NULL, 23, 83),
('Hungary', 'Budapest', 1054, 'Budapest', 'Teréz körút', 10, 24, 84),
('Hungary', 'Budapest', 1055, 'Budapest', 'Újpesti út', NULL, 25, 85),
('Hungary', 'Budapest', 1056, 'Budapest', 'Váci út', 11, 26, 86),
('Hungary', 'Budapest', 1057, 'Budapest', 'Wesselényi utca', NULL, 27, 87),
('Hungary', 'Pest', 2001, 'Szentendre', 'Fő utca', NULL, 3, 88),
('Hungary', 'Győr-Moson-Sopron', 9022, 'Győr', 'Baross út', 2, 7, 89),
('Hungary', 'Bács-Kiskun', 6001, 'Kecskemét', 'Kossuth utca', NULL, 10, 90),
('Hungary', 'Csongrád-Csanád', 6721, 'Szeged', 'Dugonics utca', 3, 5, 91),
('Hungary', 'Heves', 3301, 'Eger', 'Dobó utca', NULL, 8, 92),
('Hungary', 'Veszprém', 8201, 'Veszprém', 'Óvári út', 4, 12, 93),
('Hungary', 'Baranya', 7622, 'Pécs', 'Széchenyi utca', NULL, 6, 94),
('Hungary', 'Hajdú-Bihar', 4026, 'Debrecen', 'Piac utca', 5, 9, 95),
('Hungary', 'Jász-Nagykun-Szolnok', 5001, 'Szolnok', 'Kossuth út', NULL, 11, 96);


DECLARE @ScreeningId INT;
DECLARE @RoomId INT;
DECLARE @MaxSeatRow INT;
DECLARE @MaxSeatColumn INT;
DECLARE @SeatRow INT = 1;
DECLARE @SeatColumn INT = 1;
DECLARE @Price DECIMAL(18, 2);
DECLARE @AgeRating INT;

DECLARE @PriceGreen DECIMAL(18, 2) = 5.00;
DECLARE @PriceTwelve DECIMAL(18, 2) = 7.00;
DECLARE @PriceSixteen DECIMAL(18, 2) = 8.00;
DECLARE @PriceEighteen DECIMAL(18, 2) = 10.00;

DECLARE @Counter INT = 1;
DECLARE @TotalScreenings INT;

SET @TotalScreenings = (SELECT COUNT(*) FROM Screenings);

WHILE @Counter <= @TotalScreenings
BEGIN
    SELECT TOP 1 
        @ScreeningId = s.Id, 
        @RoomId = s.RoomId, 
        @MaxSeatRow = r.MaxSeatRow, 
        @MaxSeatColumn = r.MaxSeatColumn,
        @AgeRating = f.AgeRating
    FROM Screenings s
    JOIN Rooms r ON s.RoomId = r.RoomId
    JOIN Films f ON s.FilmId = f.Id
    WHERE s.Id NOT IN (SELECT TOP (@Counter - 1) Id FROM Screenings ORDER BY Id)
    ORDER BY s.Id;

    SET @Price = 
        CASE 
            WHEN @AgeRating IS NULL OR @AgeRating = 0 THEN @PriceGreen
            WHEN @AgeRating = 12 THEN @PriceTwelve
            WHEN @AgeRating = 16 THEN @PriceSixteen
            WHEN @AgeRating = 18 THEN @PriceEighteen
            ELSE @PriceGreen 
        END;

    SET @SeatRow = 1;
    SET @SeatColumn = 1;

    WHILE @SeatRow <= @MaxSeatRow
    BEGIN
        WHILE @SeatColumn <= @MaxSeatColumn
        BEGIN
            INSERT INTO Tickets (ScreeningId, SeatRow, SeatColumn, Price)
            VALUES (@ScreeningId, @SeatRow, @SeatColumn, @Price);

            SET @SeatColumn = @SeatColumn + 1;
        END;

        SET @SeatRow = @SeatRow + 1;
        SET @SeatColumn = 1; 
    END;


    SET @Counter = @Counter + 1;
END;


INSERT INTO Purchases (UserId, PurchaseDate, TotalPrice) VALUES (1, '2025-06-15 14:00:00', 14.00);
UPDATE Tickets SET PurchaseId = SCOPE_IDENTITY() WHERE ScreeningId = 1 AND SeatRow = 1 AND SeatColumn = 1;
UPDATE Tickets SET PurchaseId = SCOPE_IDENTITY() WHERE ScreeningId = 1 AND SeatRow = 1 AND SeatColumn = 2;


INSERT INTO Purchases (UserId, PurchaseDate, TotalPrice) VALUES (2, '2025-07-22 18:30:00', 14.00);
UPDATE Tickets SET PurchaseId = SCOPE_IDENTITY() WHERE ScreeningId = 2 AND SeatRow = 2 AND SeatColumn = 3;
UPDATE Tickets SET PurchaseId = SCOPE_IDENTITY() WHERE ScreeningId = 2 AND SeatRow = 2 AND SeatColumn = 4;


INSERT INTO Purchases (UserId, PurchaseDate, TotalPrice) VALUES (3, '2025-08-10 12:00:00', 14.00);
UPDATE Tickets SET PurchaseId = SCOPE_IDENTITY() WHERE ScreeningId = 3 AND SeatRow = 3 AND SeatColumn = 5;
UPDATE Tickets SET PurchaseId = SCOPE_IDENTITY() WHERE ScreeningId = 3 AND SeatRow = 3 AND SeatColumn = 6;


INSERT INTO Purchases (UserId, PurchaseDate, TotalPrice) VALUES (4, '2025-06-20 16:00:00', 14.00);
UPDATE Tickets SET PurchaseId = SCOPE_IDENTITY() WHERE ScreeningId = 4 AND SeatRow = 4 AND SeatColumn = 7;
UPDATE Tickets SET PurchaseId = SCOPE_IDENTITY() WHERE ScreeningId = 4 AND SeatRow = 4 AND SeatColumn = 8;


INSERT INTO Purchases (UserId, PurchaseDate, TotalPrice) VALUES (5, '2025-07-05 20:00:00', 14.00);
UPDATE Tickets SET PurchaseId = SCOPE_IDENTITY() WHERE ScreeningId = 5 AND SeatRow = 5 AND SeatColumn = 9;
UPDATE Tickets SET PurchaseId = SCOPE_IDENTITY() WHERE ScreeningId = 5 AND SeatRow = 5 AND SeatColumn = 10;

