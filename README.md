
## Design Detailed Document

### Group: [Group 18]

## Table of Contents

1. [Introduction](#introduction)
   1.1. [UFV airport](#UFV-airport)
   1.2. [Authors](#Authors)
   
2. [Project](#description)
   2.1. [How to run](#How-to-run)
   2.2. [Project structure](#Project-structure)
   2.3. [Demo](#Demo-(output-example))
   2.4. [UML](#UML)
3. [Conclusions](#conclusions)
   3.1 [Lessons Learned](#Lessons-Learned) 
   
   
# Introduction

## UFV airport

AirUFV is a C# program that simulates the operation of a simple airport. It manages runways and different types of aircraft (commercial, cargo and private), showing how they change status (in flight, waiting, landing and on the ground) as time progresses.
The system reads aircraft from a file, assigns runways automatically, and updates distances and fuel at 15-minute intervals (a “tick”).


## Authors

- [@nicoguzman12](https://github.com/nicoguzman12)
- [@JavierTome](https://github.com/JavierTome)
- [@JorgeAlmirall](https://github.com/JorgeAlmirall)

# Description
## How to run
- Open the solution in Visual Studio.

- Compile the project.

- Execute the program from the Visual Studio console. You can use "dotnet run".

## Project structure
- Airport: manages the runways and coordinates the progress of the airplanes.

- Runway: represents a runway, which can be free or occupied by an airplane.

- Aircraft: represents an aircraft, with attributes such as remaining distance, fuel and status.

    - Subtypes: Commercial, Cargo and Private.



## Demo (output example)

Airport Status:
Runway Runway-1: Free
Runway Runway-2: Free
Aircrafts at the airport:
[Commercial] ID: 1, The number of passengers is: 5, Distance: 300, Fuel: 20000/20000, Status: InFlight
[Cargo] ID: 2, MaximumLoad: 2, Distance: 500, Fuel: 25000/25000, Status: InFlight
[Private] ID: 3, Owner: 1, Distance: 200, Fuel: 10000/10000, Status: InFlight

--- Tick 1 ---
Airport Status:
Runway Runway-1: Free
Runway Runway-2: Free
Aircrafts at the airport:
[Commercial] ID: 1, The number of passengers is: 5, Distance: 75, Fuel: 19325/20000, Status: InFlight
[Cargo] ID: 2, MaximumLoad: 2, Distance: 300, Fuel: 24200/25000, Status: InFlight
[Private] ID: 3, Owner: 1, Distance: 25, Fuel: 9650/10000, Status: InFlight

--- Tick 2 ---
Airport Status:
Runway Runway-1: Free
Runway Runway-2: Free
Aircrafts at the airport:
[Commercial] ID: 1, The number of passengers is: 5, Distance: 0, Fuel: 18650/20000, Status: Waiting
[Cargo] ID: 2, MaximumLoad: 2, Distance: 100, Fuel: 23400/25000, Status: InFlight
[Private] ID: 3, Owner: 1, Distance: 0, Fuel: 9300/10000, Status: Waiting

--- Tick 3 ---
Airport Status:
Runway Runway-1: occupied
Aircraft on Runway: 1
Runway Runway-2: occupied
Aircraft on Runway: 3
Aircrafts at the airport:
[Commercial] ID: 1, The number of passengers is: 5, Distance: 0, Fuel: 18650/20000, Status: Landing
[Cargo] ID: 2, MaximumLoad: 2, Distance: 0, Fuel: 22600/25000, Status: Waiting
[Private] ID: 3, Owner: 1, Distance: 0, Fuel: 9300/10000, Status: Landing

Simulation complete. Press any key to exit.


## UML

![Image](https://github.com/user-attachments/assets/e28992d1-c8b7-4d2b-bd74-98a990918818)
# Lessons Learned

In developing AirUFV, we learned how to structure a large program by applying object-oriented programming principles in C#, creating and managing classes, enumerations and their relationships. To better organize ourselves, we sifted through the UML representation of the project before we started writing code.
One of the biggest challenges was managing multiple runways and ensuring that aircraft correctly changed their status (InFlight, Waiting, Landing, OnGround) according to their distance and fuel level.
We overcame these challenges by testing each part progressively and adding changes little by little. 



