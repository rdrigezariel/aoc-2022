open System.IO

let processInputFile =
    File.ReadAllLines "input.txt"
let validateInputs (inputs : string[]) =
    Array.iter (fun line -> if (String.length line % 2) <> 0 then InvalidDataException() |> raise) inputs
    inputs
let distinguishCompartmentItems (inputs : string[]) =
   Array.map (fun r ->
       ("tesT", "Test")) inputs

processInputFile |> validateInputs |> distinguishCompartmentItems |> Array.iter (fun (x, y) -> printfn $"x: %s{x} y: %s{y}")