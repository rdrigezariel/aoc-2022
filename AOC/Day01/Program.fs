open System.IO

let processFile filePath =
    let mutable elvesCalorie = List.empty
    let mutable total = 0

    File.ReadLines filePath
    |> Seq.cache
    |> Seq.iter (fun cal ->
        match cal with
        | "" ->
            elvesCalorie <- total :: elvesCalorie
            total <- 0
        | _ -> total <- total + int (cal))

    elvesCalorie |> Seq.sortDescending


[<EntryPoint>]
let main argv =
    let inputFilePath = argv[0]

    if not (File.Exists inputFilePath) then
        printfn $"File not found: %s{inputFilePath}"
        1
    else
        let elvesCalories = processFile inputFilePath
        
        // Part one solution
        elvesCalories |> Seq.max |> printfn "Part one solution: %i"

        // Part two solution
        elvesCalories |> Seq.take 3 |> Seq.sum |> printfn "Part two solution: %i"

        0
