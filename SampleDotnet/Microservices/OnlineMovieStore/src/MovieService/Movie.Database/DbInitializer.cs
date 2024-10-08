﻿using Microsoft.EntityFrameworkCore;
using Movie.Database.Entities;

namespace Movie.Database
{
    public static class DbInitializer
    {
        public static void Initialize(MovieDbContext context)
        {
            using (context)
            {
                context.Database.EnsureCreated();

                if (!context.Categories.Any())
                    context.AddRange(SeedCategories());

                if (!context.Directors.Any())
                    context.AddRange(SeedDirectors());

                if (!context.Writers.Any())
                    context.AddRange(SeedWriters());

                var seedRating = SeedRatings();
                if (!context.Ratings.Any())
                    context.AddRange(seedRating);

                if (!context.Movies.Any())
                {
                    var seedMovies = SeedMovies(seedRating);
                    context.AddRange(seedMovies);
                }

                if (!context.MovieWriters.Any())
                    context.AddRange(SeedMovieWriters());

                if (!context.MovieDirectors.Any())
                    context.AddRange(SeedmovieDirectors());

                if (!context.MovieCategories.Any())
                    context.AddRange(SeedMovieCategories());

                context.SaveChanges();
            }

        }

        public static CategoryEntity[] SeedCategories()
        {
            return new CategoryEntity[]
            {
                new CategoryEntity
                {
                    //Id = 1
                    Name = "Action"
                }, new CategoryEntity
                {
                    //Id = 2
                    Name = "Sci-Fi"
                }, new CategoryEntity
                {
                    //Id = 3
                    Name = "Adventure"
                }, new CategoryEntity
                {
                    //Id = 4
                    Name = "Fantasy"
                }, new CategoryEntity
                {
                    //Id = 5
                    Name = "Horror"
                }, new CategoryEntity
                {
                    //Id = 6
                    Name = "Drama"
                }, new CategoryEntity
                {
                    //Id = 7
                    Name = "Family"
                }, new CategoryEntity
                {
                    //Id = 8
                    Name = "Mystery"
                }, new CategoryEntity
                {
                    //Id = 9
                    Name = "Documentary"
                }, new CategoryEntity
                {
                    //Id = 10
                    Name = "Comedy"
                }, new CategoryEntity
                {
                    //Id = 11
                    Name = "Crime"
                }, new CategoryEntity
                {
                    //Id = 12
                    Name = "Thriller"
                }, new CategoryEntity
                {
                    //Id = 13
                    Name = "Romance"
                }, new CategoryEntity
                {
                    //Id = 14
                    Name = "History"
                }, new CategoryEntity
                {
                    //Id = 15
                    Name = "War"
                }
            };
        }

        public static DirectorEntity[] SeedDirectors()
        {
            return new DirectorEntity[]
            {
                new () { Id = "nm0901138", FullName = "Alfred Vohrer" },
                new () { Id = "nm0173775", FullName = "Lluís Josep Comerón" },
                new () { Id = "nm0905152", FullName = "Lilly Wachowski" },
                new () { Id = "nm0905154", FullName = "Lana Wachowski" },
                new () { Id = "nm0000116", FullName = "James Cameron" },
                new () { Id = "nm0807023", FullName = "Natalia Smirnoff" },
                new () { Id = "nm0006955", FullName = "Lewis Schoenbrun" },
                new () { Id = "nm0956913", FullName = "Andrei Zinca" },
                new () { Id = "nm0474289", FullName = "Heikki Kujanpää" },
                new () { Id = "nm10086396", FullName = "Bradley Alcime" },
                new () { Id = "nm10114969", FullName = "Ahmed Mansour" }
            };
        }

        public static WriterEntity[] SeedWriters()
        {
            return new WriterEntity[]
            {
                new () { Id = "nm0908624", FullName = "Edgar Wallace" },
                new () { Id = "nm0251912", FullName = "Egon Eis" },
                new () { Id = "nm0525742", FullName = "Wolfgang Lukschy" },
                new () { Id = "nm0173775", FullName = "Lluís Josep Comerón" },
                new () { Id = "nm0905152", FullName = "Lilly Wachowski" },
                new () { Id = "nm0905154", FullName = "Lana Wachowski" },
                new () { Id = "nm0000116", FullName = "James Cameron" },
                new () { Id = "nm0807023", FullName = "Natalia Smirnoff" },
                new () { Id = "nm0150043", FullName = "Ted Chalmers" },
                new () { Id = "nm0827500", FullName = "David S. Sterling" },
                new () { Id = "nm0956913", FullName = "Andrei Zinca" },
                new () { Id = "nm0610219", FullName = "Oren Moverman" },
                new () { Id = "nm0474289", FullName = "Heikki Kujanpää" },
                new () { Id = "nm0718558", FullName = "Mikko Reitala" },
                new () { Id = "nm10086396", FullName = "Bradley Alcime" }
            };
        }

        public static RatingEntity[] SeedRatings()
        {
            return new RatingEntity[]
            {
                new () { Id = Guid.Parse("6800797B-E32A-455A-2843-08DB06F18564"),  AverageRating = 87, NumVotes = 1493180, MovieId = "tt0133093" },
                new () { Id = Guid.Parse("B156C1E6-D1EB-4350-2844-08DB06F18564"),  AverageRating = 72, NumVotes = 475351, MovieId = "tt0234215" },
                new () { Id = Guid.Parse("CFA8083B-667A-4544-2845-08DB06F18564"),  AverageRating = 78, NumVotes = 1036901, MovieId = "tt0499549" },
                new () { Id = Guid.Parse("4CEBDAFF-DBBA-42A3-2846-08DB06F18564"),  AverageRating = 62, NumVotes = 41, MovieId = "tt1775309" },
                new () { Id = Guid.Parse("EEC8F583-D6B0-4AB6-2847-08DB06F18564"),  AverageRating = 16, NumVotes = 1538, MovieId = "tt1854506" },
                new () { Id = Guid.Parse("FAAA2A3E-2C58-4911-2848-08DB06F18564"),  AverageRating = 25, NumVotes = 17, MovieId = "tt8968844" },
                new () { Id = Guid.Parse("FE23E47A-F203-404E-2849-08DB06F18564"),  AverageRating = 67, NumVotes = 15, MovieId = "tt9024440" },
                new () { Id = Guid.Parse("25775F18-FAE1-40C3-284A-08DB06F18564"),  AverageRating = 64, NumVotes = 828, MovieId = "tt0054395" },
                new () { Id = Guid.Parse("4F4EF594-E312-4551-284B-08DB06F18564"),  AverageRating = 67, NumVotes = 23, MovieId = "tt7578416" },
                new () { Id = Guid.Parse("3016B702-01BB-4AA6-284C-08DB06F18564"),  AverageRating = 54, NumVotes = 29, MovieId = "tt7640234" },
                new () { Id = Guid.Parse("F7F4D4AA-CFCD-4ED1-284D-08DB06F18564"),  AverageRating = 61, NumVotes = 41, MovieId = "tt0091805" },
                new () { Id = Guid.Parse("95ABE909-2B8D-4F85-284E-08DB06F18564"),  AverageRating = 64, NumVotes = 260, MovieId = "tt0870915" },
                new () { Id = Guid.Parse("3A688F26-09C0-44FE-284F-08DB06F18564"),  AverageRating = 67, NumVotes = 517, MovieId = "tt1517238" },
                new () { Id = Guid.Parse("6CAD2F0E-E514-419D-2850-08DB06F18564"),  AverageRating = 68, NumVotes = 66, MovieId = "tt2076307" },
                new () { Id = Guid.Parse("E7D4CC23-BB28-4302-2851-08DB06F18564"),  AverageRating = 58, NumVotes = 218, MovieId = "tt3492330" },
                new () { Id = Guid.Parse("4987B4E1-B602-4BAD-2852-08DB06F18564"),  AverageRating = 67, NumVotes = 3404, MovieId = "tt6933454" },
                new () { Id = Guid.Parse("45A312C9-AC18-44B5-2853-08DB06F18564"),  AverageRating = 67, NumVotes = 391, MovieId = "tt7220696" }
            };
        }

        public static MovieEntity[] SeedMovies(RatingEntity[] ratings)
        {
            int ratingIndex = 0;
            return new MovieEntity[]
            {
                new () { UsdPrice = 14.99, Id = "tt0133093", Rating = ratings[ratingIndex++], RuntimeMinutes = 136, StartYear = 1999, Title = "The Matrix", Type = Enums.MovieType.movie, Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers." },
                new () { UsdPrice = 15.99, Id = "tt0234215", Rating = ratings[ratingIndex++], RuntimeMinutes = 138, StartYear = 2003, Title = "The Matrix Reloaded", Type = Enums.MovieType.movie, Description = "Following the events of Matrix (1999), Neo and the rebel leaders estimate they have 72 hours until 250,000 probes discover Zion and destroy it and its inhabitants. Neo must decide how he can save Trinity from a dark fate in his dreams." },
                new () { UsdPrice = 16.99, Id = "tt0499549", Rating = ratings[ratingIndex++], RuntimeMinutes = 162, StartYear = 2009, Title = "Avatar", Type = Enums.MovieType.movie, Description = "A paraplegic marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home." },
                new () { UsdPrice = 17.99, Id = "tt1775309", Rating = ratings[ratingIndex++], RuntimeMinutes = 93, StartYear = 2011, Title = "Avatar", Type = Enums.MovieType.movie, Description = "A student in high school obtains a rare avatar on a social networking site and the boundary between her real and online identities becomes blurred." },
                new () { UsdPrice = 13.99, Id = "tt1854506", Rating = ratings[ratingIndex++], RuntimeMinutes = 80, StartYear = 2011, Title = "Aliens vs. Avatars", Type = Enums.MovieType.movie, Description = "Six college friends find themselves caught up in a cat and mouse hunt with a race of creatures who possess the ability to transform into anything from which it has consumed DNA." },
                new () { UsdPrice = 12.99, Id = "tt8968844", Rating = ratings[ratingIndex++], RuntimeMinutes = 80, StartYear = 2018, Title = "Samhain: A Halloween Horror Movie", Type = Enums.MovieType.movie, Description = "Camille is heading to the west coast to start new life. Camille has one last night in town and she is house sitting for some extra cash. Question is will this be her last night in Telford, or her last night on this earth?" },
                new () { UsdPrice = 11.99, Id = "tt9024440", Rating = ratings[ratingIndex++], RuntimeMinutes = 50, StartYear = 2018, Title = "A Meowy Halloween", Type = Enums.MovieType.movie, Description = "When their home is haunted by a dream spirit, it's up to Whiskers to figure out what is haunting them and how to send the ghost to the other side. She'll use ghost hunting, the Nekkomeownicon, and Wally's credit card to get the job done." },
                new () { UsdPrice = 14.99, Id = "tt0054395", Rating = ratings[ratingIndex++], RuntimeMinutes = 104, StartYear = 1961, Title = "Dead Eyes of London", Type = Enums.MovieType.movie, Description = "Wealthy, heavily insured men are being murdered at an alarming rate. Scotland Yard investigates and finds clues that lead to a ring of blind men, led by a mysterious \"reverend.\"" },
                new () { UsdPrice = 15.99, Id = "tt7578416", Rating = ratings[ratingIndex++], RuntimeMinutes = 106, StartYear = 2017, Title = "We, the Dead", Type = Enums.MovieType.movie, Description = "Hui Ling's life will truly begin as soon as she makes it to Taiwan. Or so she thinks. Right now, she is at the Thai-Malaysian border, working odd jobs and saving money. Determined to make it even when she loses all her savings, she accepts her boss' offer for quick money as a human trafficker. As she descends into the darkness of her sordid trade, she witnesses the atrocities perpetrated against the Rohingya immigrants. Her beacon of light through this is Wei, a young hospital worker who believes that she is a woman from his past. AQÉRAT is a sweeping tale of displacement and morality in contemporary Malaysia." },
                new () { UsdPrice = 16.99, Id = "tt7640234", Rating = ratings[ratingIndex++], RuntimeMinutes = 65, StartYear = 2018, Title = "Drown Among the Dead", Type = Enums.MovieType.movie, Description = "An old comedian lies buried up to his neck in the middle of a desert. A strange woman bearing a nail-bat finds him and intends to kill him. To save himself, the prisoner lures her with mysterious words, distracting her with stories of a mythical threshold that holds the absolute truth. She is trapped by the words as she struggles with her own existential conflicts in a world where the boundaries between truth, lies, reality, and imagination are blurry. This bizarre situation detonates a monologue about solitude, emptiness, love, absurdity, fear of death, and the fragility of human passions." },
                new () { UsdPrice = 17.99, Id = "tt0091805", Rating = ratings[ratingIndex++], RuntimeMinutes = 93, StartYear = 1986, Title = "Puzzle", Type = Enums.MovieType.movie, Description = "A bank robbery goes wrong. The robbers, two of them unemployed family men, take two hostages, one of them needing heart medication, and leave them in an apartment, tied to a bomb. The police has to find the apartment before the bomb explodes. At the same time, in the same house, two old men are solving a jigsaw puzzle, and a female cellist practices the Bach cello suites." },
                new () { UsdPrice = 13.99, Id = "tt0870915", Rating = ratings[ratingIndex++], RuntimeMinutes = 91, StartYear = 2006, Title = "Puzzle", Type = Enums.MovieType.movie, Description = "Five strangers, each with his own special talents, are recruited by the mysterious 'X'. They do not know why they are brought together, but nonetheless agree to a series of potentially lucrative schemes. A planned bank robbery goes awry when a minor mistake leads to a hostage situation. The robbery is orchestrated by Hwan. When Hwan turns up dead at the designated meeting spot, the remaining men realize something is seriously wrong, and suspicion flares." },
                new () { UsdPrice = 12.99, Id = "tt1517238", Rating = ratings[ratingIndex++], RuntimeMinutes = 87, StartYear = 2009, Title = "Puzzle", Type = Enums.MovieType.movie, Description = "Maria's husband and children give her a puzzle for her 50th birthday. She's delighted, and finds it a great discovery. Not only does the patient housewife have fun doing the puzzles, she's also really good at them. Overflowing with enthusiasm for her new-found passion, she goes back to the shop where they bought the gift for another puzzle. There her eye is caught by a notice on the message board: \"Partner for puzzle tournament wanted\". Maria musters her courage and, despite her family's misgivings, answers the announcement." },
                new () { UsdPrice = 11.99, Id = "tt2076307", Rating = ratings[ratingIndex++], RuntimeMinutes = 90, StartYear = 2013, Title = "Puzzle", Type = Enums.MovieType.movie, Description = "A middle-aged man fed up with his marriage embarks on a torrid affair with a sultry young artist, while a blind, old writer defies the odds and reconnects with the enduring love of his life\n\nA repressed middle aged man realizes that his life had become one of misery and loneliness. After becoming friends with a blind writer he had to evict from his house, he finds the inspiration and courage he needs to break all the rules he had been forced to abide by and to reconnect with life's joy and excitement." },
                new () { UsdPrice = 18.99, Id = "tt3492330", Rating = ratings[ratingIndex++], RuntimeMinutes = 85, StartYear = 2014, Title = "Puzzle", Type = Enums.MovieType.movie, Description = "High school student Azusa jumps off from the rooftop of a school building, but she survives. One month later, the school is taken over by group of people wearing bizarre masks. A pregnant teacher is imprisoned, while the head director of the school and male students disappear. Azusa then finds pieces of a puzzle in an envelope given to her by classmate Shigeo. The puzzle pieces holds the key to solve the case. Azusa chases after Shigeo and she sees something which is unimaginable." },
                new () { UsdPrice = 19.99, Id = "tt6933454", Rating = ratings[ratingIndex++], RuntimeMinutes = 103, StartYear = 2018, Title = "Puzzle", Type = Enums.MovieType.movie, Description = "Agnes, taken for granted as a suburban mother, discovers a passion for solving jigsaw puzzles which unexpectedly draws her into a new world - where her life unfolds in ways she could never have imagined." },
                new () { UsdPrice = 20.99, Id = "tt7220696", Rating = ratings[ratingIndex++], RuntimeMinutes = 103, StartYear = 2018, Title = "Laugh or Die", Type = Enums.MovieType.movie, Description = "In a detention camp in 1918, a group of Finnish actors are sentenced to death. When an important German general arrives, the camp's vicious commandant forges out a cruel plan: the imprisoners have to perform a comedy - and if they can make the visiting general laugh, they will be spared. A story about zest for life and power of laughter. Based on true events." }
            };
        }

        public static MovieWriterEntity[] SeedMovieWriters()
        {
            return new MovieWriterEntity[]
            {
                new () { MovieId = "tt0054395", WriterId = "nm0908624" },
                new () { MovieId = "tt0054395", WriterId = "nm0251912" },
                new () { MovieId = "tt0054395", WriterId = "nm0525742" },
                new () { MovieId = "tt0091805", WriterId = "nm0173775" },
                new () { MovieId = "tt0133093", WriterId = "nm0905152" },
                new () { MovieId = "tt0133093", WriterId = "nm0905154" },
                new () { MovieId = "tt0234215", WriterId = "nm0905152" },
                new () { MovieId = "tt0234215", WriterId = "nm0905154" },
                new () { MovieId = "tt0499549", WriterId = "nm0000116" },
                new () { MovieId = "tt1517238", WriterId = "nm0807023" },
                new () { MovieId = "tt1854506", WriterId = "nm0150043" },
                new () { MovieId = "tt1854506", WriterId = "nm0827500" },
                new () { MovieId = "tt2076307", WriterId = "nm0956913" },
                new () { MovieId = "tt6933454", WriterId = "nm0610219" },
                new () { MovieId = "tt6933454", WriterId = "nm0807023" },
                new () { MovieId = "tt7220696", WriterId = "nm0474289" },
                new () { MovieId = "tt7220696", WriterId = "nm0718558" }
                // new () { MovieId = "tt8923874", WriterId = "nm10086396" }
            };
        }

        public static MovieDirectorEntity[] SeedmovieDirectors()
        {
            return new MovieDirectorEntity[]
            {
                new () { MovieId = "tt0054395", DirectorId = "nm0901138" },
                new () { MovieId = "tt0133093", DirectorId = "nm0905152" },
                new () { MovieId = "tt0133093", DirectorId = "nm0905154" },
                new () { MovieId = "tt0234215", DirectorId = "nm0905154" },
                new () { MovieId = "tt0234215", DirectorId = "nm0905152" },
                new () { MovieId = "tt0499549", DirectorId = "nm0000116" },
                new () { MovieId = "tt1517238", DirectorId = "nm0807023" },
                new () { MovieId = "tt1854506", DirectorId = "nm0006955" },
                new () { MovieId = "tt2076307", DirectorId = "nm0956913" },
                new () { MovieId = "tt7220696", DirectorId = "nm0474289" }
                // new () { MovieId = "tt8923874", DirectorId = "nm10086396" },
                // new () { MovieId = "tt8984748", DirectorId = "nm10114969" }
            };
        }

        public static MovieCategoryEntity[] SeedMovieCategories()
        {
            return new MovieCategoryEntity[]
            {
                new () { MovieId = "tt0133093", CategoryId = 1 },
                new () { MovieId = "tt0234215", CategoryId = 1 },
                new () { MovieId = "tt0499549", CategoryId = 1 },
                new () { MovieId = "tt0499549", CategoryId = 3 },
                new () { MovieId = "tt0499549", CategoryId = 4 },
                new () { MovieId = "tt1775309", CategoryId = 5 },
                new () { MovieId = "tt1854506", CategoryId = 5 },
                new () { MovieId = "tt8968844", CategoryId = 5 },
                new () { MovieId = "tt9024440", CategoryId = 10 },
                new () { MovieId = "tt9024440", CategoryId = 7 },
                new () { MovieId = "tt9024440", CategoryId = 5 },
                new () { MovieId = "tt0054395", CategoryId = 11 },
                new () { MovieId = "tt0054395", CategoryId = 5 },
                new () { MovieId = "tt0054395", CategoryId = 8 },
                new () { MovieId = "tt7578416", CategoryId = 11 },
                new () { MovieId = "tt7640234", CategoryId = 3 },
                new () { MovieId = "tt7640234", CategoryId = 6 },
                new () { MovieId = "tt7640234", CategoryId = 5 },
                new () { MovieId = "tt0091805", CategoryId = 1 },
                new () { MovieId = "tt0091805", CategoryId = 11 },
                new () { MovieId = "tt0091805", CategoryId = 12 },
                new () { MovieId = "tt0870915", CategoryId = 11 },
                new () { MovieId = "tt0870915", CategoryId = 12 },
                new () { MovieId = "tt1517238", CategoryId = 6 },
                new () { MovieId = "tt2076307", CategoryId = 6 },
                new () { MovieId = "tt2076307", CategoryId = 7 },
                new () { MovieId = "tt2076307", CategoryId = 13 },
                new () { MovieId = "tt3492330", CategoryId = 5 },
                new () { MovieId = "tt3492330", CategoryId = 12 },
                new () { MovieId = "tt6933454", CategoryId = 6 },
                new () { MovieId = "tt7220696", CategoryId = 6 },
                new () { MovieId = "tt7220696", CategoryId = 14 },
                new () { MovieId = "tt7220696", CategoryId = 15 }
            };
        }
    }
}