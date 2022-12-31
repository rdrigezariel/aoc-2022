open System.IO
open System

type RoundResult = 
    | Lost = 0
    | Draw = 3
    | Won  = 6

type Move = 
    | Rock     = 1
    | Paper    = 2
    | Scissors = 3

let parseMove (move : string) : Move =
    match move with
    | "A" | "X" -> Move.Rock
    | "B" | "Y" -> Move.Paper
    | "C" | "Z" -> Move.Scissors 
    | _         -> ArgumentException() |> raise

let scoreRound (playerMove : Move) (roundResult : RoundResult) : int = 
    int playerMove + int roundResult
    
// Part One

let mutable playerScorePartOne = 0

File.ReadLines "input.txt"
|> Seq.iter (fun round -> 
    let moves = round.Split(' ') |> Array.map parseMove
    let opponentMove = moves[0]
    let playerMove = moves[1]
    
    let roundResult =
        match playerMove, opponentMove with
        | Move.Rock, Move.Paper        -> RoundResult.Lost
        | Move.Paper, Move.Scissors    -> RoundResult.Lost
        | Move.Scissors, Move.Rock     -> RoundResult.Lost
        | Move.Rock, Move.Rock         -> RoundResult.Draw
        | Move.Paper, Move.Paper       -> RoundResult.Draw
        | Move.Scissors, Move.Scissors -> RoundResult.Draw
        | Move.Rock, Move.Scissors     -> RoundResult.Won
        | Move.Paper, Move.Rock        -> RoundResult.Won
        | Move.Scissors, Move.Paper    -> RoundResult.Won
        | _                            -> ArgumentException() |> raise
    
    playerScorePartOne <- playerScorePartOne + scoreRound playerMove roundResult)

printfn $"Part one: %i{playerScorePartOne}"

// Part Two

let mutable playerScorePartTwo = 0

File.ReadLines "input.txt"
|> Seq.iter (fun round ->
    let lineSplit = round.Split(' ')
    
    let opponentMove = lineSplit[0] |> parseMove
    let roundResult = 
        match lineSplit[1] with
        | "X" -> RoundResult.Lost
        | "Y" -> RoundResult.Draw
        | "Z" -> RoundResult.Won
        | _   -> ArgumentException() |> raise
    let playerM = 
        match opponentMove, roundResult with
        | Move.Rock, RoundResult.Lost     -> Move.Scissors
        | Move.Paper, RoundResult.Lost    -> Move.Rock
        | Move.Scissors, RoundResult.Lost -> Move.Paper
        | Move.Rock, RoundResult.Draw     -> Move.Rock
        | Move.Paper, RoundResult.Draw    -> Move.Paper
        | Move.Scissors, RoundResult.Draw -> Move.Scissors
        | Move.Rock, RoundResult.Won      -> Move.Paper
        | Move.Paper, RoundResult.Won     -> Move.Scissors
        | Move.Scissors, RoundResult.Won  -> Move.Rock
        | _ -> ArgumentException() |> raise

    playerScorePartTwo <- playerScorePartTwo + scoreRound playerM roundResult)

printfn $"Part two: %i{playerScorePartTwo}"
