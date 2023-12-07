namespace AoC 

open System
open System.IO

module General =
    let aocPath = "/Users/flavioc/Codes/AoC/2023/Fsharp/Runs/inputs"
    
    let readInput file =
        let lines = File.ReadLines(Path.Combine(aocPath,file))
                    |> Seq.toList  
        
        lines
        
    let stringToInt (inputString: string) =
        match Int32.TryParse(inputString) with
        | (true, result) -> Some result
        | _ -> None
        
    let stringToInt64 (inputString: string) =
        match Int64.TryParse(inputString) with
        | (true, result) -> Some result
        | _ -> None                        