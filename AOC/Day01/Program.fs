open System.IO

let processFile filePath =
    let mutable elvesCalorie = []
    let mutable total = 0

    File.ReadAllLines filePath
    |> Array.iter (fun cal ->
        match cal with
        | "" ->
            elvesCalorie <- total :: elvesCalorie
            total <- 0
        | _ -> total <- total + int (cal))

    elvesCalorie |> List.toArray |> Array.sortDescending


[<EntryPoint>]
let main argv =
    let inputFilePath = argv[0]

    if not (File.Exists inputFilePath) then
        printfn $"File not found: %s{inputFilePath}"
        1
    else
        let elvesCalories = processFile inputFilePath

        // Part one solution
        elvesCalories |> Array.max |> printfn "Part one solution: %i"

        // Part two solution
        elvesCalories |> Array.take 3 |> Array.sum |> printfn "Part two solution: %i"

        0
