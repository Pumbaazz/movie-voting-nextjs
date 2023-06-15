CREATE TABLE public."Movies" (
	"Id" uuid NOT NULL,
	"Title" text NOT NULL,
	"Path" text NOT NULL,
	"Likes" int4 NOT NULL,
	CONSTRAINT "PK_Movies" PRIMARY KEY ("Id")
);

CREATE TABLE public."Users" (
	"Id" uuid NOT NULL,
	"Name" text NOT NULL,
	"Email" text NOT NULL,
	"Password" text NOT NULL,
	CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);


create table public."Reactions"(
	"Id" uuid not null,
	"UserId" uuid,
	"MovieId" uuid,
	"ReactionType" int4,
	constraint "PK_Reactions" primary key ("Id"),
	foreign key("UserId") references public."Users"("Id"),
	foreign key("MovieId") references public."Movies"("Id")
);

INSERT INTO public."Users" ("Id", "Name" , "Email", "Password" ) VALUES 
	('de69fff8-bf0e-439e-8fc1-e7dc5f333bd0','John Smith', 'admin@gmail.com', 'password');

INSERT INTO public."Movies" ("Id", "Title","Path","Likes") VALUES
	('43d6b6ee-d54d-47b6-b0fe-d78493e063b3','The Shawshank Redemption','https://traditiononline.org/wp-content/uploads/2019/11/13-Best-Shawshank.jpg',0),
	('b6cf43f1-960c-4ddd-bc9f-a1c2f9be2b5c','The Godfather','https://www.lab111.nl/wp-content/uploads/2022/01/TGF50_INTL_DIGITAL_PAYOFF_1_SHEET__NED.jpg',0),
	('01681e07-c754-4c45-aa7b-8f65beb11620','The Dark Knight','https://m.media-amazon.com/images/I/91KkWf50SoL._AC_SL1500_.jpg', 0);

--INSERT INTO public."Reactions" ("Id", "UserId", "MovieId", "ReactionType") VALUES 
--	('619d5471-0b54-420b-801c-5ba932530491', 'de69fff8-bf0e-439e-8fc1-e7dc5f333bd0', '43d6b6ee-d54d-47b6-b0fe-d78493e063b3', 'Like');